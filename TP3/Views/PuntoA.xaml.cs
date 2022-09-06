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
        //private decimal xi_1;
        //private decimal xi_2; //segund semilla
        //private decimal cIndependiente;
        //private decimal cMultiplicadora;
        //private decimal modulo;
        private decimal media;
        private decimal DE;
        private decimal lambda;
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

#pragma region validaciones
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
                    //        decimal semilla = decimal.Parse(TxtSemilla.Text);
                    //        decimal modulo = decimal.Parse(TxtModulo.Text);
                    //        decimal multiplicador = decimal.Parse(TxtConstanteMultiplicadora.Text);
                    //        decimal indepediente = decimal.Parse(TxtConstanteIndependiente.Text);
                    //        return (ValidarSemilla(semilla, modulo) && ValidarCMultiplicadora(multiplicador, modulo) && ValidarModulo(modulo) && ValidarCIncremento(indepediente, modulo));
                    return true;
                }
                else
                {
                    return false;
                }
            
            
          
        }

#pragma endregion validaciones



        private void BtnGenerar_Click(object sender, RoutedEventArgs e)  // para generar 20 numeros aleatorios
        {
            

            if (ValidarCamposForm())
            {
             
                cantidad = int.Parse(TxtCantidad.Text);
                lambda = decimal.Parse(TxtLambda.Text);
                media = decimal.Parse(TxtMedia.Text);
                DE = decimal.Parse(TxtDE.Text);


                Gestor.generarVariablesAleatorias( media, DE, lambda, cantidad);

                (valores_variableAleatoriaExp, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal) = Gestor.obtenerVariablesAleatorias();

          

                mostrarVectorEstado(valores_variableAleatoriaExp, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal); //Falta agregar para que tmb muestre valores_variableAleatoriaPoisson y valores_variableAleatoriaNormal 

                estadoBotones(true);

                //porcentajes = porcentajeIntervalos();

                //mostrarIntervalos(porcentajes);
            }
            else
            {
                MessageBox.Show("Intente escribir valores positivos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }




        private void estadoBotones(bool estado)
        {
            //BtnGenerarProximo.IsEnabled = estado;
            //BtnGenerarVeinte.IsEnabled = estado;
            //BtnGenerarDiezMil.IsEnabled = estado;
        }

        //private void BtnGenerarProximo_Click(object sender, RoutedEventArgs e)  // para seguir la serie de a un valor por vez 
        //{
        //    if (ValidarCamposForm())
        //    {
        //        muestra = 1;

        //        numeros_aleatorios = Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra);

        //        mostrarVectorEstado(numeros_aleatorios.Skip(Math.Max(0, numeros_aleatorios.Count() - 20)).ToList());
        //        porcentajes = porcentajeIntervalos();
        //        mostrarIntervalos(porcentajes);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Intente escribir valores positivos y menores al modulo ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void BtnGenerarVeinte_Click(object sender, RoutedEventArgs e)  // para generar nuevamente 20 randoms mas 
        //{
        //    if (ValidarCamposForm())
        //    {
        //        muestra = 20;

        //        numeros_aleatorios = Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra);

        //        mostrarVectorEstado(numeros_aleatorios.Skip(Math.Max(0, numeros_aleatorios.Count() - 20)).ToList());

        //        porcentajes = porcentajeIntervalos();
        //        mostrarIntervalos(porcentajes);
        //    }
        //    else
        //    {
        //        MessageBox.Show("Intente escribir valores positivos y menores al modulo ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private void BtnGenerarDiezMil_Click(object sender, RoutedEventArgs e)  // para simular hasta 10000 numeros aleatorios 
        //{
        //    if (ValidarCamposForm())
        //    {
        //        muestra = 10001 - numeros_aleatorios.Count(); // para simular hasta llegar 10000

        //        numeros_aleatorios = Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra - 1);

        //        mostrarVectorEstado(numeros_aleatorios.Skip(Math.Max(0, numeros_aleatorios.Count() - 20)).ToList());
        //        porcentajes = porcentajeIntervalos();
        //        mostrarIntervalos(porcentajes);

        //    }
        //    else
        //    {
        //        MessageBox.Show("Intente escribir valores positivos y menores al modulo ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        //    }
        //}

        //private IEnumerable<decimal> porcentajeIntervalos()
        //{

        //    return porcentajes = Gestor.probabilidad(numeros_aleatorios).Select(x => x * 100).ToList();

        //}

        private void mostrarVectorEstado(List<decimal> vectorEstado, List<decimal> vectorEstado2, List<decimal> vectorEstado3)
        {

            dgvVectorEstado.DataContext = generarTabla(vectorEstado, "num", "valor");
            dgvVectorEstado2.DataContext = generarTabla(vectorEstado2, "num", "valor");
            dgvVectorEstado3.DataContext = generarTabla(vectorEstado3, "num", "valor");

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

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cargado)
            {
                estadoBotones(false);

            }

        }

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
