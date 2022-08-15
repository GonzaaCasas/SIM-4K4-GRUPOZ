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
using TP1.Mvvm;
using TP1.ViewModels;

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


        PuntoAVM t = new PuntoAVM();



        public PuntoA()
        {
            InitializeComponent();

            lblVectorEstado.DataContext = t;

        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9,]+");
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
            numeros_aleatorios.Add(xi_1 / modulo); // agrega el primer random que corresponde a la semilla

            numeros_aleatorios = Gestor.generar(xi_1, cIndependiente, cMultiplicadora, modulo, muestra - 1); // le pongo -1 pq ya agregue al array el random de la semilla

            mostrarVectorEstado(numeros_aleatorios);
        }

        private void BtnGenerarProximo_Click(object sender, RoutedEventArgs e)  // para seguir la serie de a un valor por vez ---  falta para que se habilite solo despues de apretar el boton generar
        {
            muestra = 1;

            numeros_aleatorios = Gestor.generar(this.xi_1, cIndependiente, cMultiplicadora, modulo, muestra);
        }

        private void BtnGenerarVeinte_Click(object sender, RoutedEventArgs e)  // para generar nuevamente 20 randoms mas ---  falta para que se habilite solo despues de apretar el boton generar
        {
            muestra = 20;

            numeros_aleatorios = Gestor.generar(this.xi_1, cIndependiente, cMultiplicadora, modulo, muestra);
        }

        private void BtnGenerarDiezMil_Click(object sender, RoutedEventArgs e)  // para simular hasta 10000 numeros aleatorios --- falta para que se habilite solo despues de apretar el boton generar
        {

            muestra = 10000 - numeros_aleatorios.ToArray().Count(); // para simular hasta llegar 10000

            numeros_aleatorios = Gestor.generar(this.xi_1, cIndependiente, cMultiplicadora, modulo, muestra);

        }

        private void CreateLabelDynamically(string numero)
        {
            Label dynamicLabel = new Label();
            dynamicLabel.Width = 240;

            dynamicLabel.Height = 30;

            dynamicLabel.Content = numero;

            stckEstado.Children.Add(dynamicLabel);

        }

        private void mostrarVectorEstado(ArrayList vectorEstado)
        {
            string strEstado;
            strEstado = "Numeros aleatorios:";

            foreach (var item in vectorEstado)
            {
                CreateLabelDynamically(item.ToString());
            }
            
            //t.vectorEstado = strEstado;


        }

       

    }
}
