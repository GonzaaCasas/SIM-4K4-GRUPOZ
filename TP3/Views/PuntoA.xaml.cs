using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP3.Mvvm;

namespace TP3.Views
{
    /// <summary>
    /// Interaction logic for PuntoA.xaml
    /// </summary>
    /// 

    public partial class PuntoA : Page
    {
        private decimal media;
        private decimal DE;
        private decimal lambda;
        private decimal lambdaExp;
        private int cantidad;
        private int muestra;

        private string metodo = "";
        private List<decimal> numeros_aleatorios = new List<decimal>();
        private List<decimal> frecuencia_nroaleatorios = new List<decimal>();
        private IEnumerable<decimal> porcentajes;
        bool cargado = false;

        private List<decimal> numeros_variableAleatoria = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaExp = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaPoisson = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaNormal = new List<decimal>();


        public PuntoA()
        {
            InitializeComponent();
            cargado = true;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        #region validaciones
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
            
                if (!String.IsNullOrEmpty(TxtLambda.Text) && decimal.Parse(TxtLambda.Text) > 0  && !String.IsNullOrEmpty(TxtCantidad.Text) && int.Parse(TxtCantidad.Text) > 1 && !String.IsNullOrEmpty(TxtMedia.Text) && !String.IsNullOrEmpty(TxtDE.Text))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            
            
          
        }

        #endregion



        private void BtnGenerar_Click(object sender, RoutedEventArgs e) 
        {
            

            if (ValidarCamposForm())
            {
             
                cantidad = int.Parse(TxtCantidad.Text);
                lambda = decimal.Parse(TxtLambda.Text);
                lambdaExp = decimal.Parse(TxtLambdaExp.Text);
                media = decimal.Parse(TxtMedia.Text);
                DE = decimal.Parse(TxtDE.Text);


                Gestor.generarVariablesAleatorias( media, DE, lambda, cantidad);

                (valores_variableAleatoriaExp, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal) = Gestor.obtenerVariablesAleatorias();

          

                mostrarVectorEstado(valores_variableAleatoriaExp, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal); //Falta agregar para que tmb muestre valores_variableAleatoriaPoisson y valores_variableAleatoriaNormal 


                //porcentajes = porcentajeIntervalos();

                //mostrarIntervalos(porcentajes);
            }
            else
            {
                MessageBox.Show("Intente escribir valores positivos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }


        //private IEnumerable<decimal> porcentajeIntervalos()
        //{

        //    return porcentajes = Gestor.probabilidad(numeros_aleatorios).Select(x => x * 100).ToList();

        //}

        private void mostrarVectorEstado(List<decimal> vectorEstado, List<decimal> vectorEstado2, List<decimal> vectorEstado3)
        {

            dgvVectorEstado3.DataContext = generarTabla(vectorEstado, "num", "valor");
            dgvVectorEstado2.DataContext = generarTabla(vectorEstado2, "num", "valor");
            dgvVectorEstado.DataContext = generarTabla(vectorEstado3, "num", "valor");

        }

        private DataTable generarTabla(List<decimal> lista, string strCol1, string strCol2)
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add(strCol1);
            tabla.Columns.Add(strCol2);

            foreach (var item in lista)
            {
                // Math.Round(item, 4, MidpointRounding.AwayFromZero).ToString();
                DataRow _row = tabla.NewRow();
                _row[0] = tabla.Rows.Count + 1;
                //  _row[1] = item.ToString();
                _row[1] = Math.Round(item, 4, MidpointRounding.AwayFromZero).ToString();
                tabla.Rows.Add(_row);
            }

            return tabla;
        }

        //private void mostrarIntervalos(IEnumerable<decimal> vectorIntervalos)
        //{
        //    dgvIntervalos.DataContext = generarTabla(vectorIntervalos.ToList(), "nIntervalo", "porcentaje");

        //}


      /*  private void rbExponencial_Checked(object sender, RoutedEventArgs e) //para exponencial y para poisson
        {
            TxtLambda.IsEnabled = true;
            TxtDE.IsEnabled = false;
            TxtMedia.IsEnabled = false;
        }

        private void rbNormal_Checked(object sender, RoutedEventArgs e)
        {
            TxtLambda.IsEnabled = false;
            TxtDE.IsEnabled = true;
            TxtMedia.IsEnabled = true;
        }*/
    }
}
