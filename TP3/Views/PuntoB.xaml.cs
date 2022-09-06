using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TP3.Mvvm;
using System.Threading;
using System.ComponentModel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using Ookii.Dialogs.Wpf;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for PuntoB.xaml
    /// </summary>
    /// 


    public partial class PuntoB : Page
    {
        int muestra;
        int subintervalos;
        private List<decimal> numeros_aleatorios = new List<decimal>();
        private Gestor gestor;

        private static List<decimal> valores_variableAleatoriaExpNeg = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaPoisson = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaNormal = new List<decimal>();

        private static List<decimal> observadosUniforme = new List<decimal>();
        private static List<decimal> observadosExponencial = new List<decimal>();
        private static List<decimal> observadosPoisson = new List<decimal>();
        private static List<decimal> observadosNormal = new List<decimal>();
        private static List<decimal> esperadosUniforme = new List<decimal>();
        private static List<decimal> esperadosExponencial = new List<decimal>();
        private static List<decimal> esperadosPoisson = new List<decimal>();
        private static List<decimal> esperadosNormal = new List<decimal>();
        private static List<decimal> mediosExponencial = new List<decimal>();
        private static List<decimal> mediosPoisson = new List<decimal>();
        private static List<decimal> mediosNormal = new List<decimal>();

        private List<decimal> limites = new List<decimal>();
        private List<decimal> resultadosTest = new List<decimal>();
        bool cargado = false;
        DataTable tablaExcel;
        private Grafico grafico = null;

        public PuntoB()
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


        private void BtnGenerar_B_Click(object sender, RoutedEventArgs e) // EL BOTON GENERAR DEL PUNTO B DEBERIA HABILITARSE SOLO CUANDO YA SE USO EL PUNTO A
        {
            if (validarCampos())
            {
                if (Int32.Parse(TxtSubintervalos.Text) > 1)
                {
                    dockPlot.Children.Remove(grafico);
                    muestra = Int32.Parse(TxtMuestra.Text);
                    subintervalos = Int32.Parse(TxtSubintervalos.Text);
                    numeros_aleatorios.Clear(); // deja el vector estado vacio
                    (valores_variableAleatoriaExpNeg, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal) = Gestor.obtenerVariablesAleatorias(); 
                    tablaExcel = mostrarVectorEstado(valores_variableAleatoriaExpNeg);   //Falta agregar para que tmb muestre valores_variableAleatoriaPoisson y valores_variableAleatoriaNormal 

                    // Gestor.probabilidad(numeros_aleatorios);
                    estadoBotones(true);

                }
                else
                {
                    MessageBox.Show("El Sub Intervalo debe ser mayor a 1", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Falta Ingresar datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)  // EL TEST DEBERIA HABILITARSE SOLO CUANDO YA SE USO EL PUNTO A 
        {
            if (validarCampos())
            {
                if (Int32.Parse(TxtSubintervalos.Text) > 0)
                {
                    

                    dgvVectorEstado.Visibility = Visibility.Hidden;
                    resultadosTest.Clear();
                    resultadosTest = Gestor.test(this.subintervalos); // devuelve los chi cuadrado obtenidos, el primer indice es de la distribucion uniforme, despues es exponencial, poisson, normal y el ultmo es el tabulado teorico
                    (observadosExponencial, observadosPoisson, observadosNormal ) = Gestor.obtenerObservaciones();
                    ( esperadosExponencial, esperadosPoisson, esperadosNormal) = Gestor.obtenerEsperados();

                    //decimal esperado = muestra / subintervalos; // no hace falta mas esto, los esperados ahora estan guardados arriba, te devuelve tmb el uniforme aunque no hace falta

                    (mediosExponencial, mediosPoisson, mediosNormal) = Gestor.obtenerMedioIntervalos(subintervalos); // para tener el valor medio de cada intervalo q va en el grafico, aca habria  que cambiar de nombre la variable limites por medio (?

                    //string[] labels = new string[subintervalos];
                    //decimal anterior = limites[0];
                    //for (int i = 1; i < subintervalos + 1; i++)
                    //torE{
                    //    labels[i - 1] = i.ToString() + "\n" + Math.Round(anterior, 4, MidpointRounding.AwayFromZero).ToString()
                    //        + " - " + Math.Round(limites[i], 4, MidpointRounding.AwayFromZero);
                    //    anterior = limites[i];
                    //}

                    //decimal[] vectorEsperado = new decimal[subintervalos];

                    //for (int i = 0; i < subintervalos; i++)
                    //{
                    //    vecsperado[i] = esperado;
                    //}

                    //decimal[] vectorObservados = observados.ToArray();


                    //grafico = new Grafico();
                    //dockPlot.Children.Add(grafico);
                    //grafico.AgregarColeccion(vectorEsperado, "Esperado");
                    //grafico.AgregarColeccion(vectorObservados, "Observados");
                    //grafico.AgregarIntervalos(labels);
                    //grafico.Visible(true);
                    


                    lblJiObtenida.Content = "Chi Obtenido \n" + Math.Round(resultadosTest[0], 4, MidpointRounding.AwayFromZero).ToString();
                    lblJiTabulada.Content = "Chi Tabulado \n" + Math.Round(resultadosTest[1], 4, MidpointRounding.AwayFromZero).ToString();

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
                }// ver si sacar esta parte dependiendo como se habilita los botones
                else
                {
                    MessageBox.Show("El Sub Intervalo debe ser mayor a 0", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Falta Ingresar datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private bool validarCampos()
        {
            if (!String.IsNullOrEmpty(TxtMuestra.Text) && !String.IsNullOrEmpty(TxtSubintervalos.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
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
                Gestor.ExportarExcel(dialog.SelectedPath, tablaExcel, "Serie generada punto B");
                MessageBox.Show("La serie de numeros generada se ha exportado en " + dialog.SelectedPath,
                    "Exportacion exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
    }


}
