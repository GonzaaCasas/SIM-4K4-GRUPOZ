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
        private List<decimal> numeros_aleatorios = new List<decimal>();
        private List<decimal> calculo = new List<decimal>(); // desp le ppongo otro nombre a la lista no sabia q poner 



        decimal esperado;
        decimal lim_inferior;
        decimal lim_superior;
        decimal paso;

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
           

            numeros_aleatorios = Gestor.generar("metodo",xi_1, cIndependiente, cMultiplicadora, modulo, muestra - 1); // le pongo -1 pq ya agregue al array el random de la semilla

            mostrarVectorEstado(numeros_aleatorios);

            

            // generarSerie();
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            testChiCuadrado();
        }



        public void testChiCuadrado()
        {

            decimal min = numeros_aleatorios.Min();
            decimal max = numeros_aleatorios.Max();

            this.esperado = this.muestra / this.subintervalos;
            this.paso = (max - min) / this.subintervalos;

            this.lim_inferior = min;
            this.lim_superior = lim_inferior + paso;

            int[] count = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

            for (int i = 0; i < subintervalos - 1; i++)
            {
                foreach (var random in numeros_aleatorios)
                {
                    if (random >= lim_inferior && random < lim_superior)
                    {
                        count[i]++;
                    }
                }

                this.lim_inferior = this.lim_superior;
                this.lim_superior = this.lim_superior + paso;

            }

            //  e_o = this.esperado
            for (int i = 0; i < count.Count() ; i++)
            {
               double e_o = (double)(this.esperado - count[i]);
               decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);  // lo puse en  varios paso para q se entienda al leerlo 
               decimal res = e_oExpDos / this.esperado;
               this.calculo.Add(res);
            }

            decimal chiCuadrado = calculo.Sum();

            double gradosLibertad = subintervalos - 1;
            double alfa = 0.05;

            decimal chiSquareTeorico = (decimal)MathNet.Numerics.Distributions.ChiSquared.InvCDF(gradosLibertad, 1- alfa);

            if( chiCuadrado < chiSquareTeorico )
            {
                // tirar que esta todo ok
            }

            Console.WriteLine(chiCuadrado.ToString());
        }

        //public void generarSerie()
        //{
        //    rand_num = rd.Next(0, 9);
        //    Console.WriteLine(rand_num);
        //}
        private void mostrarVectorEstado(List<decimal> vectorEstado)
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
