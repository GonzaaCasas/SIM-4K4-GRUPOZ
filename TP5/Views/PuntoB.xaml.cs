using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TP5.Mvvm;


namespace TP5.Views {
	/// <summary>
	/// Interaction logic for PuntoB.xaml
	/// </summary>
	/// 


	public partial class PuntoB : Page {

		private List<double> tPromedio = new List<double>();
		private static double min;
		private static double max;
		private static double prob45d;
		private static double fecha90;

		private static List<double> MITardio = new List<double>();
		private static List<double> actCriticas = new List<double>();

		private Grafico2 grafico2 = new Grafico2();
		private bool flagGrafico = false;
		private bool flagIntervalos = false;

		//private Grafico grafico = null;
		//private Grafico2VM grafico2 = null;

		public PuntoB() {

			InitializeComponent();
			HabilitarBotones(Gestor.puntoA);
			cargarTodo(Gestor.puntoA);
		}

		private void cargarTodo(bool flag) {
			if (flag) {
				tPromedio = Gestor.ObtenerTiempoPromedio();
				LblTiempoPromedio.Content = Math.Round(tPromedio.Last(), 4, MidpointRounding.AwayFromZero).ToString();

				min = Gestor.ObtenerMinimo();
				max = Gestor.ObtenerMaximo();
				LblMinimo.Content = Math.Round(min, 4, MidpointRounding.AwayFromZero).ToString();
				LblMaximo.Content = Math.Round(max, 4, MidpointRounding.AwayFromZero).ToString();

				prob45d = Gestor.ObtenerProb45d();
				fecha90 = Gestor.Obtenerfecha90();
				LblProb45d.Content = Math.Round(prob45d, 4, MidpointRounding.AwayFromZero).ToString();
				LblFecha90.Content = Math.Round(fecha90, 2, MidpointRounding.AwayFromZero).ToString() + " Días";


				MITardio = Gestor.ObtenerMITardio();
				actCriticas = Gestor.ObtenerActCriticas();

				DataTable tablaMITardio = construirTabla(MITardio);
				DataTable tablaActCriticas = construirTabla(actCriticas);
				DgvMITarido.DataContext = tablaMITardio;
				DgvTCriticas.DataContext = tablaActCriticas;


			}
		}

		private void HabilitarBotones(bool flag) {
			BtnGrafico.IsEnabled = flag;
			BtnIntervalos.IsEnabled = flag;
		}

		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		#region botones para mostrar/ocultar graficos

		private void BtnGrafico_Click(object sender, RoutedEventArgs e) {
			ManejarGrafico(flagGrafico);
			flagGrafico = !flagGrafico;

		}


		private void BtnIntervalos_Click(object sender, RoutedEventArgs e) {
			ManejarIntervalos(flagIntervalos);
			flagIntervalos = !flagIntervalos;
		}

		private void ManejarGrafico(bool flag) {
			if (!flag) {
				ManejarIntervalos(true);
				construirGrafico(tPromedio);
				IconGrafico.Kind = MahApps.Metro.IconPacks.PackIconModernKind.EyeHide;
				StrGrafico.Text = "  Ocultar grafico";
				flagIntervalos = false;

			} else {
				grafico2.Visibility = Visibility.Hidden;
				IconGrafico.Kind = MahApps.Metro.IconPacks.PackIconModernKind.Eye;
				StrGrafico.Text = "  Mostrar grafico";
			}
		}

		private void ManejarIntervalos(bool flag) {
			if (!flag) {
				ManejarGrafico(true);
				List<double> lista;
				List<string> intervalos;
				(lista, intervalos) = Gestor.obtenerDatosIntervalos();
				construirIntervalos(lista, intervalos.ToArray());
				IconIntervalo.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ChartBox;
				StrIntervalos.Text = "  Ocultar intervalos";
				flagGrafico = false;

			} else {
				grafico2.Visibility = Visibility.Hidden;
				IconIntervalo.Kind = MahApps.Metro.IconPacks.PackIconMaterialKind.ChartBoxOutline;
				StrIntervalos.Text = "  Mostrar intervalos";
			}
		}


		#endregion


		private void construirGrafico(List<double> promedios) {
			grafico2 = new Grafico2();
			dockPlot.Children.Clear();
			dockPlot.Children.Add(grafico2);
			grafico2.AgregarSerieLinea(promedios, "Promedio");
			string[] intervalos = ConstruirLabels(promedios);
			grafico2.AgregarIntervalos(intervalos, false);
			grafico2.Visibility = Visibility.Visible;
		}

		private string[] ConstruirLabels(List<double> promedios) {
			string[] labels = new string[promedios.Count];
			for (int i = 1; i <= promedios.Count(); i++) {
				labels[i - 1] = i.ToString();
			}
			return labels;
		}

		private void construirIntervalos(List<double> observadoLista, string[] intervalos) {
			grafico2 = new Grafico2();
			dockPlot.Children.Clear();
			dockPlot.Children.Add(grafico2);
			grafico2.AgregarSerieBarras(observadoLista, "Observados");
			grafico2.AgregarIntervalos(intervalos, true);
			grafico2.Visibility = Visibility.Visible;
		}

		private DataTable construirTabla(List<double> lista) {
			DataTable tabla = new DataTable();
			tabla.Columns.Add("col1");
			tabla.Columns.Add("col2");

			for (int i = 0; i < lista.Count; i++) {
				DataRow _row = tabla.NewRow();
				_row[0] = $"A{i + 1}";
				_row[1] = Math.Round(lista[i], 4, MidpointRounding.AwayFromZero).ToString();
				tabla.Rows.Add(_row);
			}

			return tabla;
		}

	}


}
