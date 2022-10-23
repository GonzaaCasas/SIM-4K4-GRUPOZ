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

	public partial class PuntoB : Page {

		private List<double> tPromedio = new List<double>();

		private static List<decimal> listaOcupacionServidor = new List<decimal>();
		private static List<decimal> listaMaxProductosEnEspera = new List<decimal>();
		private static List<decimal> listaTPromedioPermanencia = new List<decimal>();

		private static int cantEnsambles;
		private static decimal prob;

		private bool flagCola = false;
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

				DataTable tablaOcupacionServidor = construirTabla(listaOcupacionServidor, "Cola");
				DataTable tablaMaxProductosEnEspera = construirTabla(listaMaxProductosEnEspera, "Cola");
				DataTable tablaTPromedioPermanencia = construirTabla(listaTPromedioPermanencia, "Cola");

			}
		}

		private void HabilitarBotones(bool flag) {
			BtnCola.IsEnabled = flag;
			BtnEnsamble.IsEnabled = flag;
		}

		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		#region botones para mostrar/ocultar graficos

		private void BtnCola_Click(object sender, RoutedEventArgs e) {
			DockEnsambles.Visibility = Visibility.Hidden;
			DockColas.Visibility = Visibility.Visible;


		}

		private void BtnEnsamble_Click(object sender, RoutedEventArgs e) {
			DockColas.Visibility = Visibility.Hidden;
			DockEnsambles.Visibility = Visibility.Visible;
		}

		#endregion




		private DataTable construirTabla(List<decimal> lista, string nombreFila ) {
			DataTable tabla = new DataTable();
			tabla.Columns.Add("col1");
			tabla.Columns.Add("col2");

			for (int i = 0; i < lista.Count; i++) {
				DataRow _row = tabla.NewRow();
				_row[0] = nombreFila + " " + i.ToString();
				_row[1] = Math.Round(lista[i], 4, MidpointRounding.AwayFromZero).ToString();
				tabla.Rows.Add(_row);
			}

			return tabla;
		}

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
			cantEnsambles = int.Parse(TxtEnsambles.Text);
			//prob = MathNet.Numerics.Distributions.Normal.CDF(Gestor.promEnsamblesHora, (double)DE, cantEnsambles);
		}
    }


}
