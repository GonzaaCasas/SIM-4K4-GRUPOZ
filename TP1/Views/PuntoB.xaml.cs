using MathNet.Numerics.Random;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TP1.Mvvm;
using ScottPlot;
using MathNet.Numerics;
using LiveCharts.Wpf;
using LiveCharts;

namespace TP1.Views
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
        private List<decimal> observados = new List<decimal>();
        private List<decimal> limites = new List<decimal>();
        private List<decimal> resultadosTest = new List<decimal>();



        public PuntoB()
        {
            InitializeComponent();
            gestor = new Gestor();

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void BtnGenerar_B_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {
                
                GraficoB.Reset();
                muestra = Int32.Parse(TxtMuestra.Text);
                subintervalos = Int32.Parse(TxtSubintervalos.Text);
                numeros_aleatorios.Clear(); // deja el vector estado vacio
                numeros_aleatorios = Gestor.generadorRandomPuntoB(muestra);
                mostrarVectorEstado(numeros_aleatorios);
                // Gestor.probabilidad(numeros_aleatorios);
                BtnTest.IsEnabled = true;
                dgvVectorEstado.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("Falta Ingresar datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            if (validarCampos())
            {
                dgvVectorEstado.Visibility = Visibility.Hidden;

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
                GraficoB.Reset(); //funciona dudoso
                GraficoB.AgregarColeccion(vectorEsperado, "Esperado");
                GraficoB.AgregarColeccion(vectorObservados, "Observados");
                GraficoB.AgregarIntervalos(labels);
                GraficoB.Visible();
            }
            else
            {
                MessageBox.Show("Falta Ingresar datos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

       
        private void mostrarVectorEstado(List<decimal> vectorEstado)
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
        }
        private bool validarCampos()
        {
            if(!String.IsNullOrEmpty(TxtMuestra.Text) && !String.IsNullOrEmpty(TxtSubintervalos.Text))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }


}
