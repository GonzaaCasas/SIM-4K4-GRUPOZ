using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Ookii.Dialogs.Wpf;
using TP1.Mvvm;

namespace TP1.Views
{
    /// <summary>
    /// Interaction logic for PuntoC.xaml
    /// </summary>
    public partial class PuntoC : Page
    {
        Random rd = new Random();
        int muestra;
        int subintervalos;
        private decimal xi_1;
        private decimal cIndependiente;
        private decimal cMultiplicadora;
        private decimal modulo;
        private List<decimal> numeros_aleatorios = new List<decimal>();
        private Gestor gestor;
        private List<decimal> observados = new List<decimal>();
        private List<decimal> limites = new List<decimal>();
        private List<decimal> resultadosTest = new List<decimal>();
        bool cargado = false;
        DataTable tablaExcel;
        private Grafico grafico = null;


        public PuntoC()
        {
            InitializeComponent();
            gestor = new Gestor();
            cargado = true;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private bool ValidarModulo(decimal modulo)
        {
            return (modulo > 0);
        }

        private bool ValidarCMultiplicadora(decimal multiplicador, decimal modulo)
        {
            return (multiplicador > 0 && multiplicador < modulo);
        }

        private bool ValidarCIncremento(decimal incremento, decimal modulo)
        {
            return (incremento >= 0 && incremento < modulo);
        }

        private bool ValidarSemilla(decimal semilla, decimal modulo)
        {
            return (semilla >= 0 && semilla < modulo);
        }
        private bool ValidarCamposForm()
        {
           if( !String.IsNullOrEmpty(TxtConstanteIndependiente.Text) && !String.IsNullOrEmpty(TxtSemilla.Text) && !String.IsNullOrEmpty(TxtModulo.Text) && !String.IsNullOrEmpty(TxtConstanteMultiplicadora.Text) && !String.IsNullOrEmpty(TxtMuestra.Text) && !String.IsNullOrEmpty(TxtSubintervalos.Text) && Int32.Parse(TxtSubintervalos.Text) > 1 && Int32.Parse(TxtMuestra.Text)>0)
            {
                muestra = Int32.Parse(TxtMuestra.Text);
                subintervalos = Int32.Parse(TxtSubintervalos.Text);
                decimal semilla = decimal.Parse(TxtSemilla.Text);
                decimal cIndependiente = decimal.Parse(TxtConstanteIndependiente.Text);
                decimal cMultiplicadora = decimal.Parse(TxtConstanteMultiplicadora.Text);
                decimal modulo = decimal.Parse(TxtModulo.Text);
                return (ValidarSemilla(semilla, modulo) && ValidarCMultiplicadora(cMultiplicadora, modulo) && ValidarModulo(modulo) && ValidarCIncremento(cIndependiente, modulo));
            }
            else
            {
                return false;
            }
        }
        private void BtnGenerarC_Click(object sender, RoutedEventArgs e)
        {
            if (ValidarCamposForm())
            {
                dockPlot.Children.Remove(grafico);
                muestra = Int32.Parse(TxtMuestra.Text);
                subintervalos = Int32.Parse(TxtSubintervalos.Text);
                xi_1 = Int32.Parse(TxtSemilla.Text);
                cIndependiente = Int32.Parse(TxtConstanteIndependiente.Text);
                cMultiplicadora = Int32.Parse(TxtConstanteMultiplicadora.Text);
                modulo = Int32.Parse(TxtModulo.Text);
                numeros_aleatorios.Clear(); // deja el vector estado vacio
                numeros_aleatorios = Gestor.generar("Mixto", xi_1, cIndependiente, cMultiplicadora, modulo, muestra, 0);
                tablaExcel = mostrarVectorEstado(numeros_aleatorios);
                estadoBotones(true);
            }
            else
            {
                MessageBox.Show("Intente escribir valores positivos y menores al modulo ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            dgvVectorEstado.Visibility = Visibility.Hidden;
            resultadosTest.Clear();
            resultadosTest = Gestor.test(numeros_aleatorios, this.muestra, this.subintervalos); // devuelve el chi cuadrado obtenido y el tabulado
            observados = Gestor.obtenerObservaciones(numeros_aleatorios, this.muestra, this.subintervalos);


            decimal esperado = muestra / subintervalos;
            limites = Gestor.obtenerLimites(numeros_aleatorios, subintervalos);

            string[] labels = new string[subintervalos];
            decimal anterior = limites[0];
            for (int i = 1; i < subintervalos + 1; i++)
            {
                labels[i - 1] = i.ToString() + "\n" + Math.Round(anterior, 4, MidpointRounding.AwayFromZero).ToString()
                    + " - " + Math.Round(limites[i], 4, MidpointRounding.AwayFromZero);
                anterior = limites[i];
            }

            decimal[] vectorEsperado = new decimal[subintervalos];

            for (int i = 0; i < subintervalos; i++)
            {
                vectorEsperado[i] = esperado;
            }

            decimal[] vectorObservados = observados.ToArray();

            grafico = new Grafico();
            dockPlot.Children.Add(grafico);
            grafico.AgregarColeccion(vectorEsperado, "Esperado");
            grafico.AgregarColeccion(vectorObservados, "Observados");
            grafico.AgregarIntervalos(labels);
            grafico.Visible(true);


            lblJiObtenida.Content = "Ji Obtenido \n" + Math.Round(resultadosTest[0], 4, MidpointRounding.AwayFromZero).ToString();
            lblJiTabulada.Content = "Ji Tabulado \n" + Math.Round(resultadosTest[1], 4, MidpointRounding.AwayFromZero).ToString();

            if (resultadosTest[0] < resultadosTest[1])
            {
                lblAprobacion.Content = "H0 Aceptada";
                lblAprobacion.Foreground = Brushes.DarkGreen;
            }
            else
            {
                lblAprobacion.Content = "H0 Rechazada";
                lblAprobacion.Foreground = Brushes.Red;
            }

        }
        private DataTable mostrarVectorEstado(List<decimal> vectorEstado)
        {
            DataTable tablaNumero = new DataTable();
            tablaNumero.Columns.Add("num");
            tablaNumero.Columns.Add("valor");

            foreach (var item in vectorEstado)
            {
                // Math.Round(item, 4, MidpointRounding.AwayFromZero).ToString();
                DataRow _row = tablaNumero.NewRow();
                _row[0] = tablaNumero.Rows.Count + 1;
                //  _row[1] = item.ToString();
                _row[1] = Math.Round(item, 4, MidpointRounding.AwayFromZero).ToString();
                tablaNumero.Rows.Add(_row);
            }
            dgvVectorEstado.DataContext = tablaNumero;
            dgvVectorEstado.Visibility = Visibility.Visible;
            return tablaNumero;

        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cargado)
            {
                estadoBotones(false);

            }
        }

        private void estadoBotones(bool estado)
        {
            BtnTest.IsEnabled = estado;
            BtnExportar.IsEnabled = estado;
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
                //MessageBox.Show($"The selected folder was:{Environment.NewLine}{dialog.SelectedPath}", "Sample folder browser dialog");
                Gestor.ExportarExcel(dialog.SelectedPath, tablaExcel, "Serie generada punto C");
                MessageBox.Show("La serie de numeros generada se ha exportado en " + dialog.SelectedPath,
                    "Exportacion exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
