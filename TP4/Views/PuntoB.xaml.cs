using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TP4.Mvvm;
using System.Linq;


namespace TP4.Views
{
    /// <summary>
    /// Interaction logic for PuntoB.xaml
    /// </summary>
    /// 


    public partial class PuntoB : Page
    {

        private List<double> tPromedio = new List<double>();
        private static double min;
        private static double max;
        private static double prob45d;
        private static double fecha90;



     


        // ------------------------

        private static List<double> frequencias = new List<double>();
        private static List<string> medios = new List<string>();


        private List<decimal> resultadosTest = new List<decimal>();

        private Grafico2 grafico2 = new Grafico2();
        private bool flagGrafico = false;
        private bool flagIntervalos = false;

        DataTable tablaExcel;


        //private Grafico grafico = null;
        //private Grafico2VM grafico2 = null;

        public PuntoB()
        {

            InitializeComponent();
            HabilitarBotones(Gestor.puntoA);
            cargarTodo();
        }

        private void cargarTodo()
        {
            tPromedio = Gestor.ObtenerTiempoPromedio();
            LblTiempoPromedio.Content = Math.Round(tPromedio.Last(), 4, MidpointRounding.AwayFromZero).ToString();

            min = Gestor.ObtenerMinimo();
            max = Gestor.ObtenerMaximo();
            LblMinimo.Content = Math.Round(min, 4, MidpointRounding.AwayFromZero).ToString();
            LblMaximo.Content = Math.Round(max, 4, MidpointRounding.AwayFromZero).ToString();

            prob45d = Gestor.ObtenerProb45d();
            fecha90 = Gestor.Obtenerfecha90();
            LblProb45d.Content = Math.Round(prob45d, 4, MidpointRounding.AwayFromZero).ToString() + " Días";
            LblFecha90.Content = Math.Round(fecha90, 2, MidpointRounding.AwayFromZero).ToString();

        }

        private void HabilitarBotones(bool flag)
        {
            BtnGrafico.IsEnabled = flag;
            BtnIntervalos.IsEnabled = flag;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        #region botones para mostrar/ocultar graficos

        private void BtnGrafico_Click(object sender, RoutedEventArgs e)
        {
            ManejarGrafico(flagGrafico);
            flagGrafico = !flagGrafico;

        }


        private void BtnIntervalos_Click(object sender, RoutedEventArgs e)
        {
            ManejarIntervalos(flagIntervalos);
            flagIntervalos = !flagIntervalos;
        }

        private void ManejarGrafico(bool flag)
        {
            if (!flag)
            {
                //construirGrafico()
                IconGrafico.Kind = MahApps.Metro.IconPacks.PackIconModernKind.EyeHide;
                StrGrafico.Text = "  Ocultar grafico";
                flagIntervalos = false;
                ManejarIntervalos(true);
            }
            else
            {
                grafico2.Visibility = Visibility.Hidden;
                IconGrafico.Kind = MahApps.Metro.IconPacks.PackIconModernKind.Eye;
                StrGrafico.Text = "  Mostrar grafico";
            }
        }

        private void ManejarIntervalos(bool flag)
        {
            if (!flag)
            {
                //construirgrafico()
                IconIntervalo.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ChartBox;
                StrIntervalos.Text = "  Ocultar intervalos";
                flagGrafico = false;
                ManejarGrafico(true);
            }
            else
            {
                grafico2.Visibility = Visibility.Hidden;
                IconIntervalo.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ChartBoxOutline;
                StrIntervalos.Text = "  Mostrar intervalos";
            }
        }


        #endregion


        private void construirGrafico(List<decimal> observadoListaDecimals, string[] intervalos)
        {
            grafico2 = new Grafico2();
            dockPlot.Children.Clear();
            dockPlot.Children.Add(grafico2);

            List<double> observadoListaDoubles = observadoListaDecimals.ConvertAll(x => (double)x);              //De lista de decimal a lista de doubles
            grafico2.AgregarSerie(observadoListaDoubles, "Observados");
            grafico2.AgregarIntervalos(intervalos);
            grafico2.Visibility = Visibility.Visible;
        }

        private DataTable construirTabla(List<decimal> observada, List<decimal> esperada)
        {
            DataTable tablaNumero = new DataTable();
            tablaNumero.Columns.Add("Observada");
            tablaNumero.Columns.Add("Esperada");

            for (int i = 0; i < observada.Count; i++)
            {
                DataRow _row = tablaNumero.NewRow();
                _row[0] = Math.Round(observada[i], 4, MidpointRounding.AwayFromZero).ToString();
                _row[1] = Math.Round(esperada[i], 4, MidpointRounding.AwayFromZero).ToString();
                tablaNumero.Rows.Add(_row);
            }

            return tablaNumero;
        }

        private void obtenerDatosIntervalos()
        {
            (frequencias, medios) = Gestor.obtenerDatosIntervalos();

        }


    }


}
