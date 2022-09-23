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


namespace TP4.Views
{
    /// <summary>
    /// Interaction logic for PuntoB.xaml
    /// </summary>
    /// 


    public partial class PuntoB : Page
    {

        int subintervalos;



        private static List<decimal> observadosExponencial = new List<decimal>();
        private static List<decimal> observadosPoisson = new List<decimal>();
        private static List<decimal> observadosNormal = new List<decimal>();

        private static List<decimal> esperadosExponencial = new List<decimal>();
        private static List<decimal> esperadosPoisson = new List<decimal>();
        private static List<decimal> esperadosNormal = new List<decimal>();
        private static List<string> mediosExponencial = new List<string>();
        private static List<string> mediosPoisson = new List<string>();
        private static List<string> mediosNormal = new List<string>();


        // ------------------------

        private static List<decimal> frequencias = new List<decimal>();
        private static List<string> medios = new List<string>();


        private List<decimal> resultadosTest = new List<decimal>();

        private Grafico2 grafico2 = new Grafico2();


        DataTable tablaExcel;


        //private Grafico grafico = null;
        //private Grafico2VM grafico2 = null;

        public PuntoB()
        {

            InitializeComponent();
            HabilitarBotones(Gestor.puntoA);
            
        }

        private void HabilitarBotones(bool flag)
        {
            //BtnNormal.IsEnabled = flag;
            //BtnPoisson.IsEnabled = flag;
            //BtnExp.IsEnabled = flag;
            //BtnExportar.IsEnabled = flag;
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void BtnExportar_OnClick(object sender, RoutedEventArgs e)
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Elija una carpeta para exportar la serie generada";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.

            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
            {
                MessageBox.Show("Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            }

            if ((bool)dialog.ShowDialog())
            {

                Gestor.ExportarExcel(dialog.SelectedPath, tablaExcel, "Distribucion");
                MessageBox.Show("La distribución generada se ha exportado en " + dialog.SelectedPath,
                    "Exportacion exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        #region botones para mostrar distribuciones

        private void BtnExp_OnClick(object sender, RoutedEventArgs e)
        {
            //subintervalos = int.Parse(TxtSubintervalos.Text);
            if (subintervalos > 1)
            {
                ejecutarTestChi(2);
                (observadosExponencial, esperadosExponencial, mediosExponencial) = Gestor.obtenerTodoExp(subintervalos);
                construirGrafico(observadosExponencial, esperadosExponencial, mediosExponencial.ToArray());
                tablaExcel = construirTabla(observadosExponencial, esperadosExponencial);
                //BtnExportar.IsEnabled = true;
            }
        }

        private void BtnPoisson_OnClick(object sender, RoutedEventArgs e)
        {
            //subintervalos = int.Parse(TxtSubintervalos.Text);
            if (subintervalos > 1)
            {
                ejecutarTestChi(1);
                (observadosPoisson, esperadosPoisson, mediosPoisson) = Gestor.obtenerTodoPoisson(subintervalos);
                construirGrafico(observadosPoisson, esperadosPoisson, mediosPoisson.ToArray());
                tablaExcel = construirTabla(observadosPoisson, esperadosPoisson);
                //BtnExportar.IsEnabled = true;
            }
        }

        private void BtnNormal_OnClick(object sender, RoutedEventArgs e)
        {
            //subintervalos = int.Parse(TxtSubintervalos.Text);
            if (subintervalos > 1)
            {
                ejecutarTestChi(0);
                (observadosNormal, esperadosNormal, mediosNormal) = Gestor.obtenerTodoNormal(subintervalos);
                construirGrafico(observadosNormal, esperadosNormal, mediosNormal.ToArray());
                tablaExcel = construirTabla(observadosNormal, esperadosNormal);
                //BtnExportar.IsEnabled = true;
            }
        }

        #endregion

        private void ejecutarTestChi(int indice)
        {
            resultadosTest.Clear();
            resultadosTest = Gestor.test(this.subintervalos); // 0: normal | 1: poisson | 2: exp | 3: teorico


            //lblJiObtenida.Content = "Chi Obtenido \n" + Math.Round(resultadosTest[indice], 4, MidpointRounding.AwayFromZero).ToString();
            //lblJiTabulada.Content = "Chi Tabulado \n" + Math.Round(resultadosTest[3], 4, MidpointRounding.AwayFromZero).ToString();

            //lblJiObtenida.Visibility = Visibility.Visible;
            //lblJiTabulada.Visibility = Visibility.Visible;
            //if (resultadosTest[indice] < resultadosTest[3])
            //{
            //    lblAprobacion.Content = "H0 Aceptada";
            //    lblAprobacion.Foreground = Brushes.DarkGreen;
            //}
            //else
            //{
            //    lblAprobacion.Content = "H0 Rechazada";
            //    lblAprobacion.Foreground = Brushes.Red;
            //}
            //lblAprobacion.Visibility = Visibility.Visible;
        }

        private void construirGrafico(List<decimal> observadoListaDecimals, List<decimal> esperadoListaDecimals, string[] intervalos)
        {
            grafico2 = new Grafico2();
            dockPlot.Children.Clear();
            dockPlot.Children.Add(grafico2);

            List<double> observadoListaDoubles = observadoListaDecimals.ConvertAll(x => (double)x);              //De lista de decimal a lista de doubles
            List<double> esperadoListaDoubles = esperadoListaDecimals.ConvertAll(x => (double)x);                //De lista de decimal a lista de doubles

            grafico2.AgregarSerie(esperadoListaDoubles, "Esperado");
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
