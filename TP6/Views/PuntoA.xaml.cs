using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP6.Mvvm;

namespace TP6.Views {
	/// <summary>
	/// Interaction logic for PuntoA.xaml
	/// </summary>
	/// 

	public partial class PuntoA : Page {
		private double minimoA1;
		private double maximoA1;

		private double minimoA2;
		private double maximoA2;

		private double mediaA3;

		private double minimoA4;
		private double maximoA4;

		private string estrategia;

		private double mediaA5;

		private double rangomin;
		private double rangomax;


		private int cantidadSimular;

		public string strCarga { get; set; } = "";
		public bool cargaActiva { get; set; } = false;
		public Visibility visibilidadCarga { get; set; } = Visibility.Collapsed;


		//private static List<decimal> valores_variableAleatoriaExp = new List<decimal>();
		//private static List<decimal> valores_variableAleatoriaPoisson = new List<decimal>();
		//private static List<decimal> valores_variableAleatoriaNormal = new List<decimal>();


		public PuntoA() {
			InitializeComponent();
			DataContext = this;
		}


		#region validaciones

		private void NumeroEnteroTextBox(object sender, TextCompositionEventArgs e) {
			Regex regex = new Regex("[^0-9]");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void NumeroDecimalTextBox(object sender, TextCompositionEventArgs e) {
			Regex regex = new Regex("[^0-9.]");
			e.Handled = regex.IsMatch(e.Text);
		}

		private void NumeroRealTextBox(object sender, TextCompositionEventArgs e) {
			Regex regex = new Regex("[^0-9.-]");
			e.Handled = regex.IsMatch(e.Text);
		}

		private bool EsNumeroDecimal(string txt) {
			decimal resultado;
			return decimal.TryParse(txt, out resultado);
		}

		private bool EsNumeroReal(string txt) {
			decimal resultado;
			return decimal.TryParse(txt, out resultado);
		}

		private bool ValidarCamposForm() {
			return true;
		}

		#endregion



		private void BtnGenerar_Click(object sender, RoutedEventArgs e) {

			if (ValidarCamposForm()) {


				if (true) //validaciones n>algo
				{
					minimoA1 = double.Parse(TxtMinimoA1.Text);
					maximoA1 = double.Parse(TxtMaximoA1.Text);

					minimoA2 = double.Parse(TxtMinimoA2.Text);
					maximoA2 = double.Parse(TxtMaximoA2.Text);

					mediaA3 = double.Parse(TxtMediaA3.Text);

					minimoA4 = double.Parse(TxtMinimoA4.Text);
					maximoA4 = double.Parse(TxtMaximoA4.Text);

					mediaA5 = double.Parse(TxtMediaA5.Text);

					cantidadSimular = int.Parse(TxtCantidad.Text);

                    if ((bool)rbRK.IsChecked)
                    {
						estrategia = "RK";
                    }
                    else
                    {
						estrategia = "Euler";
                    }


					rangomin = Slider.LowerValue;
					rangomax = Slider.UpperValue;

					animacionCarga.IsActive = true;
					animacionCarga.Visibility = Visibility.Visible;
					lblCarga.Content = "Calculando...";
					Task.Factory.StartNew(() => {
						Gestor.simular(cantidadSimular, minimoA1, maximoA1, minimoA2, maximoA2, minimoA4, maximoA4, mediaA3, mediaA5, rangomin, rangomax);
					}).ContinueWith(task => {
						animacionCarga.IsActive = false;
						animacionCarga.Visibility = Visibility.Hidden;
						lblCarga.Content = "Listo";
					}, System.Threading.CancellationToken.None, TaskContinuationOptions.None, TaskScheduler.FromCurrentSynchronizationContext());

				} else {
					MessageBox.Show("Intente escribir valores positivos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
				}

			} else {
				MessageBox.Show("Intente escribir valores correctos", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
			}

		}

        private void rbEuler_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void rbRK_Checked(object sender, RoutedEventArgs e)
        {

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
