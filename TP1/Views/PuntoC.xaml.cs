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
        private List<decimal> observados = new List<decimal>();
        private List<decimal> limites = new List<decimal>();

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
        private void BtnGenerarC_Click(object sender, RoutedEventArgs e)
        {

            muestra = Int32.Parse(TxtMuestra.Text); 
            subintervalos = Int32.Parse(TxtSubintervalos.Text); 

          
            xi_1 = Int32.Parse(TxtSemilla.Text);
            cIndependiente = Int32.Parse(TxtConstanteIndependiente.Text);
            cMultiplicadora = Int32.Parse(TxtConstanteMultiplicadora.Text);
            modulo = Int32.Parse(TxtModulo.Text);


            numeros_aleatorios.Clear(); // deja el vector estado vacio


            numeros_aleatorios = Gestor.generar("Mixto", xi_1, cIndependiente, cMultiplicadora, modulo, muestra ); 

            mostrarVectorEstado(numeros_aleatorios);
           
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            observados = Gestor.test(numeros_aleatorios, this.muestra, this.subintervalos);
            decimal esperado = muestra / subintervalos;
            limites = Gestor.obtenerLimites(numeros_aleatorios, subintervalos);
           


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
