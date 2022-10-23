using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TP5.Models;
using TP5.ViewModels;


namespace TP5.Mvvm {
	internal class Gestor {

        public static double reloj;
		public static double proxLlegada;
		public static Servidor servidorFin;

		static Servidor Seccion1;
		static Servidor Seccion2;
		static Servidor Seccion3;
		static Servidor Seccion4;
		static Servidor Seccion5;

		static Actividad actividadI;
		static Actividad actividad1;
		static Actividad actividad2;
		static Actividad actividad3;
		static Actividad actividad4;
		static Actividad actividad5;
		static Actividad actividadF;

		private static DistribucionUniforme _uniformeActividad1;
		private static DistribucionUniforme _uniformeActividad2;
		private static DistribucionExponencial _ExponencialActividad3;
		private static DistribucionUniforme _uniformeActividad4;
		private static DistribucionExponencial _ExponencialActividad5;
		private static DistribucionExponencial _ExponencialPedidos;



		private static List<object> vectorEstado = new List<object>();
		private static List<object> fila = new List<object>();
		private static List<List<Actividad>> listaDatos = new List<List<Actividad>>();
		private static List<object> listaResultados = new List<object>();
		private static List<double> duracionesFinalizacionTarea = new List<double>();
		private static List<double> freqDuraciones = new List<double>();
		private static List<string> medios = new List<string>();
		private static List<double> tPromedio = new List<double>();

		private static List<double> actividadesMI_tardePromedios = new List<double>();
		private static List<double> actividadesCriticasProbalidades = new List<double>();


		private static double min = 0;
		private static double max = 0;
		private static double prob45d;
		private static double fecha90;

		public static bool puntoA { get; set; }
		static Random random = new Random();
     

        public static void inicializarDistribuciones(double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5) {
			_uniformeActividad1 = new DistribucionUniforme(a1, b1);
			_uniformeActividad2 = new DistribucionUniforme(a2, b2);
			_ExponencialActividad3 = new DistribucionExponencial(media3);
			_uniformeActividad4 = new DistribucionUniforme(a4, b4);
			_ExponencialActividad5 = new DistribucionExponencial(media5);
			_ExponencialPedidos = new DistribucionExponencial(0.05); // 3 pedidos por hora = 0.05 pedidos/minuto

		}




		public static void simular(double eventos, double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5) {
			inicializarDistribuciones(a1, b1, a2, b2, a4, b4, media3, media5);
			InicializarServidores();
			bool inicio = true;
			for (int i = 0; i < eventos; i++) {

                switch (DeterminarEvento())
                {
					case "inicio":
						proxLlegada = GenerarLlegadaCliente();
						reloj = proxLlegada;
						break;

					case "llegada":
						reloj = proxLlegada;
						proxLlegada = GenerarLlegadaCliente();
						InicializarPedido();
						break;

					case "finAtencion":
						reloj = servidorFin.finAtencion;
						servidorFin.TerminarAtencion();
						break;

                    default:
                        break;
                }

			}

		}


        private static void InicializarPedido()
        {
			Seccion1.NuevoCliente(new Cliente());
			Seccion2.NuevoCliente(new Cliente());
			Seccion3.NuevoCliente(new Cliente());
		}


        private static string DeterminarEvento()
        {
            throw new NotImplementedException();
        }

        private static void InicializarServidores()
        {
			Seccion1 = new Servidor(_uniformeActividad1);
			Seccion2 = new Servidor(_uniformeActividad2);
			Seccion3 = new Servidor(_ExponencialActividad3);
			Seccion4 = new Servidor(_uniformeActividad4);
			Seccion5 = new Servidor(_ExponencialActividad5);
		}

        public static double GenerarLlegadaCliente()
        {
			double tiempo = _ExponencialPedidos.Generar_x();
			return (tiempo + reloj);

        }

        #region obtener variables
        public static List<double> ObtenerTiempoPromedio() {
			return tPromedio;
		}

		public static List<double> ObtenerMITardio() {
			return actividadesMI_tardePromedios;
		}

		public static List<double> ObtenerActCriticas() {
			return actividadesCriticasProbalidades;
		}

		public static double ObtenerMinimo() {
			return min;
		}
		public static double ObtenerMaximo() {
			return max;
		}

		public static double ObtenerProb45d() {
			return prob45d;
		}
		public static double Obtenerfecha90() {
			return fecha90;
		}

		public static (List<double>, List<string>) obtenerDatosIntervalos() {
			return (freqDuraciones, medios);
		}
		#endregion


		public static List<string> obtenerMedios(List<double> conjuntoNumeros, int intervalos) {
			// 0 - 1 , 1 - 5, 5, 9
			List<string> valoresMediosIntervalos = new List<string>();

			List<double> limites = calcularLimites(conjuntoNumeros, intervalos);

			for (int i = 0; i < limites.Count() - 1; i++) {
				valoresMediosIntervalos.Add(Math.Round(((limites[i] + limites[i + 1]) / 2), 4, MidpointRounding.AwayFromZero).ToString()); // (limite inferior + limite superior) / 2
			}

			return valoresMediosIntervalos;
		}

		public static List<double> calcularLimites(List<double> conjuntoNumeros, int subintervalos) {
			double min = conjuntoNumeros.Min();
			double max = conjuntoNumeros.Max();
			double paso = (max - min) / subintervalos;

			double lim_inferior = min;
			double lim_superior = lim_inferior + paso;


			List<double> array = new List<double>();

			array.Add(lim_inferior);

			for (int i = 0; i < subintervalos - 1; i++) {

				// List<decimal> limites = new List<decimal>();


				array.Add(lim_superior);


				lim_superior = lim_superior + paso;

			}

			array.Add(lim_superior);


			return array;

		}

		

		public static void determinarMomentosTempranosTardes() {
			//  momentos mas temprano de la actividadI 

			actividadI.mi = 0;
			actividadI.mf = actividadI.d + actividadI.mi;

			//  momentos mas temprano de la actividad1 

			actividad1.mi = actividadI.mf;
			actividad1.mf = actividad1.d + actividad1.mi;


			//  momentos mas temprano de la actividad2

			actividad2.mi = actividadI.mf;
			actividad2.mf = actividad2.d + actividad2.mi;


			//  momentos mas temprano de la actividad3

			actividad3.mi = actividadI.mf;
			actividad3.mf = actividad3.d + actividad3.mi;


			//  momentos mas temprano de la actividad4

			actividad4.mi = actividad1.mf;
			actividad4.mf = actividad4.d + actividad4.mi;


			//  momentos mas tempranos de la actividad5

			actividad5.mi = actividad4.mf > actividad2.mf ? actividad4.mf : actividad2.mf;
			actividad5.mf = actividad5.d + actividad5.mi;


			//  momentos mas tempranos de la actividadF

			actividadF.mi = actividad5.mf > actividad2.mf ? actividad5.mf : actividad2.mf;
			actividadF.mf = actividadF.d + actividadF.mi;

			// momentos mas tarde de la actividadF

			actividadF.mf_tarde = actividadF.mf;
			actividadF.mi_tarde = actividadF.mf_tarde - actividadF.d;

			// momentos mas tarde la actividad5

			actividad5.mf_tarde = actividadF.mi_tarde;
			actividad5.mi_tarde = actividad5.mf_tarde - actividad5.d;

			// momentos mas tarde la actividad4


			actividad4.mf_tarde = actividad5.mi_tarde;
			actividad4.mi_tarde = actividad4.mf_tarde - actividad4.d;

			// momentos mas tarde la actividad3

			actividad3.mf_tarde = 0; /// (?)
			actividad3.mi_tarde = 0; // (?)

			// momentos mas tarde la actividad2

			actividad2.mf_tarde = actividad5.mi_tarde < actividadF.mi_tarde ? actividad5.mi_tarde : actividadF.mi_tarde;
			actividad2.mi_tarde = actividad2.mf_tarde - actividad2.d;

			// momentos mas tarde la actividad1


			actividad1.mf_tarde = actividad4.mi_tarde;
			actividad1.mi_tarde = actividad1.mf_tarde - actividad1.d;

			// momentos mas tarde la actividadI

			actividadI.mf_tarde = actividad1.mi_tarde < actividad2.mi_tarde ? actividad1.mi_tarde : actividad2.mi_tarde;
			actividadI.mi_tarde = actividadI.mf_tarde - actividadI.d;


		}

		public static List<double> CalcularfreqAbsolutas(List<double> serie, int subintervalos, int muestra) {


			double[] arr = new double[subintervalos];
			List<double> frequencias = new List<double>(arr);


			double min = serie.Min();
			double max = serie.Max();


			double paso = (max - min) / subintervalos;

			double lim_inferior = min;
			double lim_superior = lim_inferior + paso;

			int simActual = 1;
			int simAnterior = 0;



			foreach (var random in serie) {
				for (int i = 0; i < subintervalos; i++) {
					if (random >= lim_inferior && random <= lim_superior) {
						frequencias[i] = (frequencias[i] * simAnterior + 1) / simActual; // observados
					} else {
						frequencias[i] = (frequencias[i] * simAnterior + 0) / simActual;
					}

					lim_inferior = lim_superior;
					lim_superior = lim_inferior + paso;


				}

				simAnterior = simActual;
				simActual++;
				lim_inferior = min;
				lim_superior = lim_inferior + paso;

			}


			return frequencias.ConvertAll(obs => (Math.Round(obs * muestra, 4, MidpointRounding.AwayFromZero))); ; //cada indice de la lista corresponde a la freq relativa de un intervalo, al multiplicarla por la muestra tenemos la absoluta

		}

	}

}
