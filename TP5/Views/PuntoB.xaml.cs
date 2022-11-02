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

		//private List<double> tPromedio = new List<double>();

		private static int cantEnsambles;

		private static decimal DE;

		//colas
		private static List<decimal> listaOcupacionServidor = new List<decimal>();
		private static List<decimal> listaMaxProductosEnEspera = new List<decimal>();
		private static List<decimal> listaTPromedioPermanencia = new List<decimal>();
		private static decimal cantPromProductosEnSistema;
		private static decimal cantPromProductosEnCola;

		//Ensamble

		private static decimal propRealizadosSolicitados;
		private static decimal promDuracionEnsamble;
		private static decimal A3EsperaA5;
		private static decimal A5EsperaA3;
		private static List<decimal> ensamblesHora = new List<decimal>();
		private static decimal promEnsamblesHora;
		private static double prob;

		public PuntoB() {

			InitializeComponent();
			HabilitarBotones(Gestor.puntoA);
			cargarTodo(Gestor.puntoA);
		}

		private void cargarTodo(bool flag) {
			if (flag) {

				DE = Gestor.stdEnsamblesPorHora;

				//recuperarColas
				//listaOcupacionServidor = new List<decimal> { Gestor.porcentajeOcupacioSeccion1,
				//	Gestor.porcentajeOcupacioSeccion2, Gestor.porcentajeOcupacioSeccion3,
				//	Gestor.porcentajeOcupacioSeccion4, Gestor.porcentajeOcupacioSeccion5 };
				//listaMaxProductosEnEspera = new List<decimal> { Gestor.cantMaxCola1, Gestor.cantMaxCola2,
				//	Gestor.cantMaxCola3, Gestor.cantMaxCola4, Gestor.cantMaxCola5 };
				//listaTPromedioPermanencia = new List<decimal> { Gestor.promedioPermanenciaColaSeccion1,
				//	Gestor.promedioPermanenciaColaSeccion2, Gestor.promedioPermanenciaColaSeccion3,
				//	Gestor.promedioPermanenciaColaSeccion4, Gestor.promedioPermanenciaColaSeccion5};
				//cantPromProductosEnSistema = Gestor.promedioProductosEnSistema;
				//cantPromProductosEnCola = Gestor.promedioProductosEnCola;

				//recuperarEnsamble
				propRealizadosSolicitados = Gestor.propRealizadosSolicitados;
				promDuracionEnsamble = Gestor.promedioDuracionEnsamble;
				A3EsperaA5 = Gestor.proporcionTiempoBloqueoA5;
				A5EsperaA3 = Gestor.proporcionTiempoBloqueo;
                ensamblesHora = Gestor.ensamblesPorHora;
				//promEnsamblesHora = Gestor.promedioEnsamblesPorHora;
				//prob se hace en PuntoB



				//cargar Colas
				//DataTable tablaOcupacionServidor = construirTabla(listaOcupacionServidor, "Cola");
				//DgvOcupacionServidor.DataContext = tablaOcupacionServidor;
				//DataTable tablaMaxProductosEnEspera = construirTabla(listaMaxProductosEnEspera, "Cola");
				//DgvMaxProductosEspera.DataContext = tablaMaxProductosEnEspera;
				//DataTable tablaTPromedioPermanencia = construirTabla(listaTPromedioPermanencia, "Cola");
				//DgvTPromedioPermanencia.DataContext = tablaTPromedioPermanencia;

				//LblCantPromProductosEnSistema.Content = Math.Round(cantPromProductosEnSistema, 2, MidpointRounding.AwayFromZero).ToString();
				//LblCantPromProductosEnCola.Content = Math.Round(cantPromProductosEnCola, 2, MidpointRounding.AwayFromZero).ToString();

				//cargar Ensamble
				//LblPropRealizadoSolicitado.Content = Math.Round(propRealizadosSolicitados, 4, MidpointRounding.AwayFromZero).ToString(); 
				//LblPromDuracionEnsamble.Content = Math.Round(promDuracionEnsamble, 2, MidpointRounding.AwayFromZero).ToString();
				//LblA3EsperaA5.Content = Math.Round(A3EsperaA5, 4, MidpointRounding.AwayFromZero).ToString();
				//LblA5EsperaA3.Content = Math.Round(A5EsperaA3, 4, MidpointRounding.AwayFromZero).ToString();
				//DataTable tablaEnsamblesHora = construirTabla(ensamblesHora, "Hora");
				//DgvEnsamblesHora.DataContext = tablaEnsamblesHora;
				//LblPromEnsambleHoras.Content = promEnsamblesHora.ToString();
				//prob se hace en funcion OnClick
			}
		}

		private void HabilitarBotones(bool flag) {
			//BtnCola.IsEnabled = flag;
			//BtnEnsamble.IsEnabled = flag;
			BtnCalcular.IsEnabled = flag;
		}

		private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) {
			Regex regex = new Regex("[^0-9]+");
			e.Handled = regex.IsMatch(e.Text);
		}

		#region botones para mostrar/ocultar graficos

		//private void BtnCola_Click(object sender, RoutedEventArgs e) {
		//	DockEnsambles.Visibility = Visibility.Hidden;
		//	DockColas.Visibility = Visibility.Visible;


		//}

		//private void BtnEnsamble_Click(object sender, RoutedEventArgs e) {
		//	DockColas.Visibility = Visibility.Hidden;
		//	DockEnsambles.Visibility = Visibility.Visible;
		//}

		#endregion




		private DataTable construirTabla(List<decimal> lista, string nombreFila ) {
			DataTable tabla = new DataTable();
			tabla.Columns.Add("col1");
			tabla.Columns.Add("col2");

			for (int i = 0; i < lista.Count; i++) {
				DataRow _row = tabla.NewRow();
				_row[0] = nombreFila + " " + (i+1).ToString();
				_row[1] = Math.Round(lista[i], 2, MidpointRounding.AwayFromZero).ToString();
				tabla.Rows.Add(_row);
			}

			return tabla;
		}

        private void BtnCalcular_Click(object sender, RoutedEventArgs e)
        {
			cantEnsambles = int.Parse(TxtEnsambles.Text);
            prob = 1 - MathNet.Numerics.Distributions.Normal.CDF((double)promEnsamblesHora, (double)DE, cantEnsambles);
			LblProb.Content = Math.Round(prob, 4, MidpointRounding.AwayFromZero).ToString();
			LblTxtProb.Content = $"de completar \n {cantEnsambles} ensambles en \n 1 hora";
        }
    }


}
