using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP4.Mvvm;

namespace TP4.Views
{
    /// <summary>
    /// Interaction logic for PuntoA.xaml
    /// </summary>
    /// 

    public partial class PuntoA : Page
    {
        private decimal minimoA1;
        private decimal maximoA1;

        private decimal minimoA2;
        private decimal maximoA2;

        private decimal mediaA3;

        private decimal minimoA4;
        private decimal maximoA4;

        private decimal mediaA5;

        private decimal cantidadSimular;


        //private static List<decimal> valores_variableAleatoriaExp = new List<decimal>();
        //private static List<decimal> valores_variableAleatoriaPoisson = new List<decimal>();
        //private static List<decimal> valores_variableAleatoriaNormal = new List<decimal>();


        public PuntoA()
        {
            InitializeComponent();
        }


        #region validaciones

        private void NumeroEnteroTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumeroDecimalTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void NumeroRealTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9.-]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool EsNumeroDecimal(string txt)
        {
            decimal resultado;
            return decimal.TryParse(txt, out resultado);
        }

        private bool EsNumeroReal(string txt)
        {
            decimal resultado;
            return decimal.TryParse(txt, out resultado);
        }

        private bool ValidarCamposForm()
        {
            return true;
        }

        #endregion



        private void BtnGenerar_Click(object sender, RoutedEventArgs e)
        {

            if (ValidarCamposForm())
            {


                if (true) //validaciones n>algo
                {
                    minimoA1 = decimal.Parse(TxtMinimoA1.Text);
                    maximoA1 = decimal.Parse(TxtMaximoA1.Text);

                    minimoA2 = decimal.Parse(TxtMinimoA2.Text);
                    maximoA2 = decimal.Parse(TxtMaximoA2.Text);

                    mediaA3 = decimal.Parse(TxtMediaA3.Text);

                    minimoA4 = decimal.Parse(TxtMinimoA4.Text);
                    maximoA4 = decimal.Parse(TxtMaximoA4.Text);

                    mediaA5 = decimal.Parse(TxtMediaA5.Text);

                    cantidadSimular = decimal.Parse(TxtCantidad.Text);



                    stackCarga.Visibility = Visibility.Visible;

                    //funciones o hacer algo no se

                    
                    Gestor.simular(cantidadSimular, minimoA1, maximoA1, minimoA2, maximoA2, minimoA4, maximoA4, mediaA3, mediaA5);


                    lblCarga.Content = "Listo";
                    animacionCarga.Visibility = Visibility.Hidden;

                }
                else
                {
                    MessageBox.Show("Intente escribir valores positivos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Intente escribir valores correctos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        //private DataTable generarTabla(List<decimal> lista, string strCol1, string strCol2)
        //{
        //    DataTable tabla = new DataTable();
        //    tabla.Columns.Add(strCol1);
        //    tabla.Columns.Add(strCol2);

        //    foreach (var item in lista)
        //    {

        //        DataRow _row = tabla.NewRow();
        //        _row[0] = (tabla.Rows.Count + 1).ToString();

        //        _row[1] = Math.Round(item, 4, MidpointRounding.AwayFromZero).ToString();
        //        tabla.Rows.Add(_row);
        //    }

        //    return tabla;
        //}


    }
}
