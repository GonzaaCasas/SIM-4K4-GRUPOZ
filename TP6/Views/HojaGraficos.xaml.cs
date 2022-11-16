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
using TP6.Mvvm;

namespace TP6.Views
{
    /// <summary>
    /// Interaction logic for HojaGraficos.xaml
    /// </summary>
    public partial class HojaGraficos : Page
    {

        Grafico2 grafico2 = new Grafico2();

        List<double> valorest;
        List<double> valoresx;
        List<double> valoresy;
        List<double> valoresyd;

        public HojaGraficos()
        {
            InitializeComponent();
            HabilitarBotones(Gestor.puntoA);
            CargarTodo(Gestor.puntoA);
        }

        private void CargarTodo(bool flag)
        {
            if (flag)
            {
                //valorest =  Gestor.ObtenerValoresT();
                //valoresx =  Gestor.ObtenerValoresX();
                //valoresy =  Gestor.ObtenerValoresY();
                //valoresyd = Gestor.ObtenerValoresYD();
                BtnFuncT_Click(new Object(), new RoutedEventArgs());
            }
        }

        private void BtnFuncT_Click(object sender, RoutedEventArgs e)
        {
            grafico2 = new Grafico2();
            dockPlot.Children.Clear();
            dockPlot.Children.Add(grafico2);
            grafico2.AgregarSerieLinea(valoresx, "Valores X");
            grafico2.AgregarSerieLinea(valoresy, "Valores X'");
            grafico2.AgregarSerieLinea(valoresyd, "Valores X''");
            string[] intervalos = ConstruirLabels(valorest);
            grafico2.AgregarIntervalos(intervalos, false);
            grafico2.Visibility = Visibility.Visible;
        }

        private string[] ConstruirLabels(List<double> valores)
        {
            string[] labels = new string[valores.Count];
            for (int i = 1; i <= valores.Count(); i++)
            {
                labels[i - 1] = i.ToString();
            }
            return labels;
        }

        private void BtnYDFuncX_Click(object sender, RoutedEventArgs e)
        {
            grafico2 = new Grafico2();
            dockPlot.Children.Clear();
            dockPlot.Children.Add(grafico2);
            grafico2.AgregarSerieLinea(valoresyd, "Valores X''");
            string[] intervalos = ConstruirLabels(valoresx);
            grafico2.AgregarIntervalos(intervalos, false);
            grafico2.Visibility = Visibility.Visible;
        }

        private void BtnYFuncX_Click(object sender, RoutedEventArgs e)
        {
            grafico2 = new Grafico2();
            dockPlot.Children.Clear();
            dockPlot.Children.Add(grafico2);
            grafico2.AgregarSerieLinea(valoresy, "Valores X'");
            string[] intervalos = ConstruirLabels(valoresx);
            grafico2.AgregarIntervalos(intervalos, false);
            grafico2.Visibility = Visibility.Visible;
        }

        private void BtnYDFuncY_Click(object sender, RoutedEventArgs e)
        {
            grafico2 = new Grafico2();
            dockPlot.Children.Clear();
            dockPlot.Children.Add(grafico2);
            grafico2.AgregarSerieLinea(valoresyd, "Valores X''");
            string[] intervalos = ConstruirLabels(valoresy);
            grafico2.AgregarIntervalos(intervalos, false);
            grafico2.Visibility = Visibility.Visible;
        }

        private void HabilitarBotones(bool flag)
        {
            BtnFuncT.IsEnabled = flag;
            BtnYDFuncX.IsEnabled = flag;
            BtnYFuncX.IsEnabled = flag;
            BtnYDFuncY.IsEnabled = flag;
        }
    }
}
