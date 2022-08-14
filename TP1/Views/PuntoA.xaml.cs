using System;
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
        private ArrayList numeros_aleatorios = new ArrayList();
        private decimal xi;
        

        public PuntoA()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnGenerar_Click(object sender, RoutedEventArgs e)  // para generar 20 numeros aleatorios
        {
            xi_1 = Int32.Parse(TxtSemilla.Text); // semilla
            cIndependiente = Int32.Parse(TxtConstanteIndependiente.Text);
            cMultiplicadora = Int32.Parse(TxtConstanteMultiplicadora.Text);
            modulo = Int32.Parse(TxtModulo.Text);

            muestra = 20;

            numeros_aleatorios.Clear(); // deja el vector estado vacio
            numeros_aleatorios.Add(xi_1 / modulo); // agregar el primer random que corresponde a la semilla

            generar(xi_1, cIndependiente, cMultiplicadora, modulo, muestra - 1); // le pongo -1 pq ya agregue al array el random de la semilla

        }

        private void BtnGenerarProximo_Click(object sender, RoutedEventArgs e)  // para seguir la serie de a un valor por vez ---  falta para que se habilite solo despues de apretar el boton generar
        {
            muestra = 1;

            generar(xi_1, cIndependiente, cMultiplicadora, modulo, muestra);
        }

        private void BtnGenerarVeinte_Click(object sender, RoutedEventArgs e)  // para generar nuevamente 20 randoms mas ---  falta para que se habilite solo despues de apretar el boton generar
        {
            muestra = 20;

            generar(xi_1, cIndependiente, cMultiplicadora, modulo, muestra);
        }

        private void BtnGenerarDiezMil_Click(object sender, RoutedEventArgs e)  // para simular 10000 numeros aleatorios --- falta para que se habilite solo despues de apretar el boton generar
        {

            muestra = 10000 - numeros_aleatorios.ToArray().Count(); // para simular hasta llegar 10000

            generar(xi_1, cIndependiente, cMultiplicadora, modulo, muestra);

        }


        private void generar(decimal xi_1,decimal cIndependiente, decimal cMultiplicadora,decimal modulo, int muestra)
        {
         

            for (int i = 0; i < muestra ; i++)
            {

                xi = generadorCongruenteMultiplicativo(xi_1, cMultiplicadora, modulo);
                numeros_aleatorios.Add(xi / modulo); // agrega los random
                xi_1 = xi;


            }

            mostrarVectorEstado(numeros_aleatorios);
        }

        private decimal generadorCongruenteMultiplicativo(decimal xi_1, decimal a, decimal m)
        {
            return (a * xi_1 ) % m;
        }


        private void mostrarVectorEstado(ArrayList vectorEstado)
        {
            foreach (var item in vectorEstado)
            {
                Console.WriteLine(item); // esto es para chequear nomas en la consola 
            }
            Console.WriteLine(vectorEstado.Count); // es solo para chequear 
        }

        //private int generadorCongruenteMixto(int xi_1, int a, int c, int m)
        //{

        //    return (a * xi_1 + c) % m;
        //}

    }
}
