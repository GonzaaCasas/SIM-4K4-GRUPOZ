using System;
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
        public PuntoC()
        {
            InitializeComponent();
            gestor = new Gestor();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void BtnGenerar_C_Click(object sender, RoutedEventArgs e)
        {

            muestra = Int32.Parse(TxtMuestra.Text); //asociar a la interfaz
            subintervalos = Int32.Parse(TxtSubintervalos.Text); //asociar a la interfaz

            // desp ver para que lo pida por pantalla a estos valores
            xi_1 = 37; // semilla  
            cIndependiente = 7;
            cMultiplicadora = 19;
            modulo = 53;


            numeros_aleatorios.Clear(); // deja el vector estado vacio


            numeros_aleatorios = Gestor.generar("Mixto", xi_1, cIndependiente, cMultiplicadora, modulo, muestra - 1); // le pongo -1 pq ya agregue al array el random de la semilla

            mostrarVectorEstado(numeros_aleatorios);

        }
          private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            gestor.testChiCuadrado(numeros_aleatorios, this.muestra, this.subintervalos);
        }
        private void mostrarVectorEstado(List<decimal> vectorEstado)
        {


            foreach (var item in vectorEstado)
            {
                Console.WriteLine(item);

            }

            Console.WriteLine(vectorEstado.ToArray().Count());


        }
    }
}
