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
        private decimal xi_1;
        private decimal xi_2; //segund semilla
        private decimal cIndependiente;
        private decimal cMultiplicadora;
        private decimal modulo;
        private int muestra;
        private string metodo = "";
        private List<decimal> numeros_aleatorios = new List<decimal>();
        private List<decimal> frecuencia_nroaleatorios = new List<decimal>();
        private IEnumerable<decimal> porcentajes;
        bool cargado = false;


        //PuntoAVM numeros_aleatorios = new PuntoAVM();



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
            if ((bool)rbMixto.IsChecked)
            {
                if (!String.IsNullOrEmpty(TxtConstanteIndependiente.Text) && !String.IsNullOrEmpty(TxtSemilla.Text) && !String.IsNullOrEmpty(TxtModulo.Text) && !String.IsNullOrEmpty(TxtConstanteMultiplicadora.Text))
                {
                    decimal semilla = decimal.Parse(TxtSemilla.Text);
                    decimal modulo = decimal.Parse(TxtModulo.Text);
                    decimal multiplicador = decimal.Parse(TxtConstanteMultiplicadora.Text);
                    decimal indepediente = decimal.Parse(TxtConstanteIndependiente.Text);
                    return (ValidarSemilla(semilla, modulo) && ValidarCMultiplicadora(multiplicador, modulo) && ValidarModulo(modulo) && ValidarCIncremento(indepediente, modulo));
                }
                else
                {
                    return false;
                }
            }
            else if ((bool)rbMultiplicativo.IsChecked)
            {
                if (!String.IsNullOrEmpty(TxtSemilla.Text) && !String.IsNullOrEmpty(TxtModulo.Text) && !String.IsNullOrEmpty(TxtConstanteMultiplicadora.Text))
                {
                    decimal semilla = decimal.Parse(TxtSemilla.Text);
                    decimal modulo = decimal.Parse(TxtModulo.Text);
                    decimal multiplicador = decimal.Parse(TxtConstanteMultiplicadora.Text);

                    return (ValidarSemilla(semilla, modulo) && ValidarCMultiplicadora(multiplicador, modulo) && ValidarModulo(modulo));
                }
                else
                {
                    return false;
                }
            }
            else if ((bool)rbAditivo.IsChecked)
            {
                if (!String.IsNullOrEmpty(TxtSemilla.Text) && !String.IsNullOrEmpty(TxtModulo.Text) && !String.IsNullOrEmpty(TxtConstanteMultiplicadora.Text))
                {
                    decimal semilla = decimal.Parse(TxtSemilla.Text);
                    decimal modulo = decimal.Parse(TxtModulo.Text);
                    decimal multiplicador = decimal.Parse(TxtConstanteMultiplicadora.Text);

                    return (ValidarSemilla(semilla, modulo) && ValidarCMultiplicadora(multiplicador, modulo) && ValidarModulo(modulo));
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        private void BtnGenerar_Click(object sender, RoutedEventArgs e)  // para generar 20 numeros aleatorios
        {

            if (ValidarCamposForm())
            {
                xi_1 = decimal.Parse(TxtSemilla.Text); // semilla
                xi_2 = decimal.Parse(TxtSemilla2.Text);
                if ((bool)rbMixto.IsChecked) { cIndependiente = decimal.Parse(TxtConstanteIndependiente.Text); }
                cMultiplicadora = decimal.Parse(TxtConstanteMultiplicadora.Text);
                modulo = decimal.Parse(TxtModulo.Text);

                metodo = (bool)rbMultiplicativo.IsChecked ? "Multiplicativo" : (bool)rbMixto.IsChecked ?  "Mixto" : "Aditivo";
                muestra = 20;



                numeros_aleatorios.Clear(); // deja el vector estado vacio

                numeros_aleatorios = Gestor.generar(metodo, xi_1, cIndependiente, cMultiplicadora, modulo, muestra, xi_2); // le pongo -1 pq ya agregue al array el random de la semilla

                mostrarVectorEstado(numeros_aleatorios);
                // frecuencia_nroaleatorios= Gestor.probabilidad(numeros_aleatorios); lo comento para despues chequear cuando esté la grafica
                estadoBotones(true);

                porcentajes = porcentajeIntervalos();

                mostrarIntervalos(porcentajes);
            }
            else
            {
                MessageBox.Show("Intente escribir valores positivos y menores al modulo ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void estadoBotones(bool estado)
        {
            BtnGenerarProximo.IsEnabled = estado;
            BtnGenerarVeinte.IsEnabled = estado;
            BtnGenerarDiezMil.IsEnabled = estado;
        }

        private void BtnGenerarProximo_Click(object sender, RoutedEventArgs e)  // para seguir la serie de a un valor por vez 
        {
            if (ValidarCamposForm())
            {
                muestra = 1;

                numeros_aleatorios = Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra);

                mostrarVectorEstado(numeros_aleatorios.Skip(Math.Max(0, numeros_aleatorios.Count() - 20)).ToList());
                porcentajes = porcentajeIntervalos();
                mostrarIntervalos(porcentajes);
            }
            else
            {
                MessageBox.Show("Intente escribir valores positivos y menores al modulo ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnGenerarVeinte_Click(object sender, RoutedEventArgs e)  // para generar nuevamente 20 randoms mas 
        {
            if (ValidarCamposForm())
            {
                muestra = 20;

                numeros_aleatorios = Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra);

                mostrarVectorEstado(numeros_aleatorios.Skip(Math.Max(0, numeros_aleatorios.Count() - 20)).ToList());

                porcentajes = porcentajeIntervalos();
                mostrarIntervalos(porcentajes);
            }
            else
            {
                MessageBox.Show("Intente escribir valores positivos y menores al modulo ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BtnGenerarDiezMil_Click(object sender, RoutedEventArgs e)  // para simular hasta 10000 numeros aleatorios 
        {
            if (ValidarCamposForm())
            {
                muestra = 10001 - numeros_aleatorios.Count(); // para simular hasta llegar 10000

                numeros_aleatorios = Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra - 1);

                mostrarVectorEstado(numeros_aleatorios.Skip(Math.Max(0, numeros_aleatorios.Count() - 20)).ToList());
                porcentajes = porcentajeIntervalos();
                mostrarIntervalos(porcentajes);

            }
            else
            {
                MessageBox.Show("Intente escribir valores positivos y menores al modulo ingresado", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private IEnumerable<decimal> porcentajeIntervalos()
        {

            return porcentajes = Gestor.probabilidad(numeros_aleatorios).Select(x => x * 100).ToList();

        }


        //private void CreateLabelDynamically(string numero)
        //{
        //    Label dynamicLabel = new Label();
        //    dynamicLabel.Width = 240;

        //    dynamicLabel.Height = 30;

        //    dynamicLabel.Content = numero;

        //    stckEstado.Children.Add(dynamicLabel);

        //}


        private void mostrarVectorEstado(List<decimal> vectorEstado)
        {

            dgvVectorEstado.DataContext = generarTabla(vectorEstado, "num", "valor");

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

        private void mostrarIntervalos(IEnumerable<decimal> vectorIntervalos)
        {
            dgvIntervalos.DataContext = generarTabla(vectorIntervalos.ToList(), "nIntervalo", "porcentaje");

        }

        private void rbMixto_Checked(object sender, RoutedEventArgs e)
        {
            var boton = sender as RadioButton;

            LblSemilla2.Visibility = Visibility.Hidden;
            TxtSemilla2.Visibility = Visibility.Hidden;
            LblCInd.Visibility = Visibility.Visible;
            TxtConstanteIndependiente.Visibility = Visibility.Visible;
            LblCMult.Visibility = Visibility.Visible;
            TxtConstanteMultiplicadora.Visibility = Visibility.Visible;
        }

        private void rbMultiplicativo_Checked(object sender, RoutedEventArgs e)
        {

            var boton = sender as RadioButton;

            LblCInd.Visibility = Visibility.Hidden;
            TxtConstanteIndependiente.Visibility = Visibility.Hidden;
            LblSemilla2.Visibility = Visibility.Hidden;
            TxtSemilla2.Visibility = Visibility.Hidden;
            LblCMult.Visibility = Visibility.Visible;
            TxtConstanteMultiplicadora.Visibility = Visibility.Visible;
        }

        private void Txt_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (cargado)
            {
                estadoBotones(false);

            }

        }

        private void RbAditivo_OnChecked(object sender, RoutedEventArgs e)
        {
            var boton = sender as RadioButton;

            LblCInd.Visibility = Visibility.Hidden;
            TxtConstanteIndependiente.Visibility = Visibility.Hidden;
            LblCMult.Visibility = Visibility.Hidden;
            TxtConstanteMultiplicadora.Visibility = Visibility.Hidden;
            LblSemilla2.Visibility = Visibility.Visible;
            TxtSemilla2.Visibility = Visibility.Visible;

        }
    }
}
