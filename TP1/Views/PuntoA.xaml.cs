﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using System.Text.RegularExpressions;
using System.Collections;
using TP1.Mvvm;
using TP1.ViewModels;
using System.Data;

namespace TP1.Views
{
    /// <summary>
    /// Interaction logic for PuntoA.xaml
    /// </summary>
    /// 

    public partial class PuntoA : Page
    {
        private decimal xi_1;
        private decimal cIndependiente;
        private decimal cMultiplicadora;
        private decimal modulo;
        private int muestra;
        private string metodo = "";
        private List<decimal> numeros_aleatorios = new List<decimal>();
        private List<decimal> frecuencia_nroaleatorios = new List<decimal>();
        private IEnumerable<decimal> porcentajes;


        //PuntoAVM numeros_aleatorios = new PuntoAVM();



        public PuntoA()
        {
            InitializeComponent();

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

        private bool ValidarCIncremento(decimal multiplicador, decimal modulo)
        {
            return (multiplicador > 0 && multiplicador < modulo);
        }

        private bool ValidarSemilla(decimal semilla, decimal modulo)
        {
            return (semilla >= 0 && semilla < modulo);
        }


        private bool ValidarCamposForm()
        {
            decimal semilla = decimal.Parse(TxtSemilla.Text);
            decimal modulo = decimal.Parse(TxtModulo.Text);
            decimal multiplicador = decimal.Parse(TxtConstanteMultiplicadora.Text);
            decimal indepediente = decimal.Parse(TxtConstanteIndependiente.Text);

            //return (ValidarSemilla(semilla, modulo) && ValidarCMult(multiplicador, modulo) && ValidarCampo(TxtConstanteIndependiente.Text) && ValidarCampo(TxtModulo.Text));
            return true;
        }

        private void BtnGenerar_Click(object sender, RoutedEventArgs e)  // para generar 20 numeros aleatorios
        {

            if (ValidarCamposForm())
            {
                xi_1 = decimal.Parse(TxtSemilla.Text); // semilla
                cIndependiente = decimal.Parse(TxtConstanteIndependiente.Text);
                cMultiplicadora = decimal.Parse(TxtConstanteMultiplicadora.Text);
                modulo = decimal.Parse(TxtModulo.Text);

                metodo = (bool)rbMultiplicativo.IsChecked ? "Multiplicativo" : "Mixto";
                muestra = 20;

                

                numeros_aleatorios.Clear(); // deja el vector estado vacio

                numeros_aleatorios = Gestor.generar(metodo, xi_1, cIndependiente, cMultiplicadora, modulo, muestra); // le pongo -1 pq ya agregue al array el random de la semilla

                mostrarVectorEstado(numeros_aleatorios);
                // frecuencia_nroaleatorios= Gestor.probabilidad(numeros_aleatorios); lo comento para despues chequear cuando esté la grafica
                activarBotones();

                porcentajes = porcentajeIntervalos();
            }
            else
            {
                MessageBox.Show("Intente escribir numeros enteros o decimales", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void activarBotones()
        {
            BtnGenerarProximo.IsEnabled = true;
            BtnGenerarVeinte.IsEnabled = true;
            BtnGenerarDiezMil.IsEnabled = true;
        }

        private void BtnGenerarProximo_Click(object sender, RoutedEventArgs e)  // para seguir la serie de a un valor por vez 
        {
            muestra = 1;

            Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra);

            mostrarVectorEstado(numeros_aleatorios);
        }

        private void BtnGenerarVeinte_Click(object sender, RoutedEventArgs e)  // para generar nuevamente 20 randoms mas 
        {
            muestra = 20;

            Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra);

            mostrarVectorEstado(numeros_aleatorios);
        }

        private void BtnGenerarDiezMil_Click(object sender, RoutedEventArgs e)  // para simular hasta 10000 numeros aleatorios 
        {
            muestra = 10000 - numeros_aleatorios.Count(); // para simular hasta llegar 10000

            Gestor.generarSiguientes(metodo, cIndependiente, cMultiplicadora, modulo, muestra - 1);

            mostrarVectorEstado(numeros_aleatorios);
        }

        private IEnumerable<decimal> porcentajeIntervalos()
        {

            return porcentajes =  Gestor.probabilidad(numeros_aleatorios).Select(x => x* 100).ToList();
         
        }


        private void CreateLabelDynamically(string numero)
        {
            Label dynamicLabel = new Label();
            dynamicLabel.Width = 240;

            dynamicLabel.Height = 30;

            dynamicLabel.Content = numero;

            stckEstado.Children.Add(dynamicLabel);

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
                _row[0] = tablaNumero.Rows.Count +1;
                //  _row[1] = item.ToString();
                _row[1] = Math.Round(item, 4, MidpointRounding.AwayFromZero).ToString();
                tablaNumero.Rows.Add(_row);
            }
            dgvVectorEstado.DataContext = tablaNumero;

        }

        private void rbMixto_Checked(object sender, RoutedEventArgs e)
        {
            var boton = sender as RadioButton;

            TxtConstanteIndependiente.IsEnabled = true;
        }

        private void rbMultiplicativo_Checked(object sender, RoutedEventArgs e)
        {

            var boton = sender as RadioButton;

            TxtConstanteIndependiente.IsEnabled = false;
        }
    }
}
