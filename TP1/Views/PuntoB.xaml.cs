using System;
using System.Collections;
using System.Collections.Generic;
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

namespace TP1.Views
{
    /// <summary>
    /// Interaction logic for PuntoB.xaml
    /// </summary>
    public partial class PuntoB : Page
    {
       // decimal rand_num;
        Random rd = new Random();
        int muestra;
        int subintervalos;
        private decimal xi_1;
        private decimal cIndependiente;
        private decimal cMultiplicadora;
        private decimal modulo;
        private ArrayList numeros_aleatorios = new ArrayList(); // desp lo cambio por list

        decimal esperado;
        decimal lim_inferior;
        decimal lim_superior;

        public PuntoB()
        {
            InitializeComponent();
            
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void BtnGenerar_B_Click(object sender, RoutedEventArgs e)
        {
            //muestra = Int32.Parse(TxtMuestra.Text);
            //subintervalos = Int32.Parse(TxtSubintervalos.Text);
            subintervalos = 10;
            muestra = 20;

            xi_1 = 37; // semilla
            cIndependiente = 7;
            cMultiplicadora = 19;
            modulo = 53;


            numeros_aleatorios.Clear(); // deja el vector estado vacio
           

            numeros_aleatorios = Gestor.generar(xi_1, cIndependiente, cMultiplicadora, modulo, muestra - 1); // le pongo -1 pq ya agregue al array el random de la semilla

            mostrarVectorEstado(numeros_aleatorios);

            

            // generarSerie();
        }

        public void testChiCuadrado()
        {

            this.esperado = this.muestra / this.subintervalos;

            this.lim_inferior = numeros_aleatorios.Min();

            for (int i = 0; i < subintervalos - 1; i++)
            {

            }
        }

        //public void generarSerie()
        //{
        //    rand_num = rd.Next(0, 9);
        //    Console.WriteLine(rand_num);
        //}
        private void mostrarVectorEstado(ArrayList vectorEstado)
        {
            

            foreach (var item in vectorEstado)
            {
                Console.WriteLine(item);
               
            }

            Console.WriteLine(vectorEstado.ToArray().Count());
            

            //t.vectorEstado = strEstado;


        }



    }
}
