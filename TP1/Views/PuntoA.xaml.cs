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

namespace TP1.Views
{
    /// <summary>
    /// Interaction logic for PuntoA.xaml
    /// </summary>
    /// 

    public partial class PuntoA : Page
    {
        private int semilla;
        private int cIndependiente;
        private int cMultiplicadora;
        private int modulo;

        public PuntoA()
        {
            InitializeComponent();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BtnGenerar_Click(object sender, RoutedEventArgs e)
        {
            semilla = Int32.Parse(TxtSemilla.Text);
            cIndependiente = Int32.Parse(TxtConstanteIndependiente.Text);
            cMultiplicadora = Int32.Parse(TxtConstanteMultiplicadora.Text);
            modulo = Int32.Parse(TxtModulo.Text);

            //generar(parametros)
        }
    }
}
