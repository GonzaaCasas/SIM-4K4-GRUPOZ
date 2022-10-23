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
		public static Queue<Cliente> clientesSeccion2 = new Queue<Cliente>();
        public static Queue<Cliente> clientesSeccion3 = new Queue<Cliente>();
		public static Queue<Cliente> clientesSeccion4 = new Queue<Cliente>();
		public static Queue<Cliente> clientesSeccion5 = new Queue<Cliente>();

		public static double acumTiempoSistema= 0;
		public static int acumEnsamblados = 0 ;
		public static double promedioDuracionEnsamble;

		public static int acumFin = 0;
		public static int acumLlegadas = 0;



		public static List<Servidor> servidores = new List<Servidor>();


        static Servidor Seccion1;
		static Servidor Seccion2;
		static Servidor Seccion3;
		static Servidor Seccion4;
		static Servidor Seccion5;


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


     

        public static void inicializarDistribuciones(double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5) {

			RandomAux rnd = new RandomAux();

			_uniformeActividad1 = new DistribucionUniforme(a1, b1, rnd);
			_uniformeActividad2 = new DistribucionUniforme(a2, b2, rnd);
			_ExponencialActividad3 = new DistribucionExponencial(media3, rnd);
			_uniformeActividad4 = new DistribucionUniforme(a4, b4, rnd);
			_ExponencialActividad5 = new DistribucionExponencial(media5, rnd);
			_ExponencialPedidos = new DistribucionExponencial(20, rnd); // cada 20 minutos se tiene un pedido

		}




		public static void simular(double eventos, double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5) {
			inicializarDistribuciones(a1, b1, a2, b2, a4, b4, media3, media5);
			InicializarServidores();

			Cliente clienteFin;
			for (int i = 0; i < eventos; i++)
			{

				switch (DeterminarEvento())
				{
					case "inicio":
						proxLlegada = GenerarLlegadaCliente();
						reloj = proxLlegada;
						break;

					case "llegada":
						acumLlegadas++;
                        //Console.WriteLine("llegada");
						reloj = proxLlegada;
						proxLlegada = GenerarLlegadaCliente();
						InicializarPedido();
						break;

					case "finAtencion":
						acumFin++;
                        //Console.WriteLine("finatencion");
						reloj = servidorFin.finAtencion ?? 0;
						clienteFin = servidorFin.TerminarAtencion();
						clienteFin.tiempoEsperaAcumulado += clienteFin.tiempoEspera;

						if (servidorFin.Equals(Seccion1))
						{
							servidorFin.GenerarLlegadaCliente(Seccion4);
						}
						if (servidorFin.Equals(Seccion2))
						{
							clientesSeccion2.Enqueue(clienteFin);
						}
						if (servidorFin.Equals(Seccion3))
						{
							clientesSeccion3.Enqueue(clienteFin);
						}
						if (servidorFin.Equals(Seccion4))
						{
							clientesSeccion4.Enqueue(clienteFin);
						}
						if (servidorFin.Equals(Seccion5))
						{
							clientesSeccion5.Enqueue(clienteFin);
						}

						if (clientesSeccion2.Count() >= 1 && clientesSeccion4.Count() >= 1)
						{
							Cliente cliente2 = clientesSeccion2.Dequeue();
							Cliente cliente4 = clientesSeccion4.Dequeue();

							if (cliente4.tiempoEsperaAcumulado > cliente2.tiempoEsperaAcumulado)
							{
								Seccion4.GenerarLlegadaCliente(Seccion5);
							}
							else
							{
								Seccion2.GenerarLlegadaCliente(Seccion5);
							}

						}

						if (clientesSeccion5.Count() >= 1 && clientesSeccion3.Count() >= 1)
						{
							Cliente cliente3 = clientesSeccion3.Dequeue();
							Cliente cliente5 = clientesSeccion5.Dequeue();

							acumEnsamblados++;

							if (cliente3.tiempoEsperaAcumulado > cliente5.tiempoEsperaAcumulado)
							{
								acumTiempoSistema += cliente3.tiempoSistema;
							}
							else
							{
								acumTiempoSistema += cliente5.tiempoSistema;
							}

                            if (acumEnsamblados >= 1000)
                            {
                                Console.WriteLine("");
                            }

							Console.WriteLine($"Prod ensamblados: {acumEnsamblados}");
						}
						break;

					default:
						break;
				}

                //Console.WriteLine($"reloj : {reloj}");
			}

			promedioDuracionEnsamble = acumTiempoSistema / acumEnsamblados;

            Console.WriteLine($"Promedio duracion ensamble: {promedioDuracionEnsamble}");
			Console.WriteLine($"Acum llegadas: {acumLlegadas}");
			Console.WriteLine($"Acum fin: {acumFin}");
		}


        private static void InicializarPedido()
        {
			Seccion1.NuevoCliente(new Cliente(reloj));
			Seccion2.NuevoCliente(new Cliente(reloj));
			Seccion3.NuevoCliente(new Cliente(reloj));
		}


        private static string DeterminarEvento()
        {
			if (reloj == 0)
			{
				return "inicio";
            }
			
			servidorFin = servidores.Where( x => x.finAtencion != null).OrderBy(servidor => servidor.finAtencion).FirstOrDefault();
            
			//if (servidorFin == null)
   //         {
   //             Console.WriteLine("NULL");
   //         }
   //         else
   //         {
			//	Console.WriteLine($"servidor fin: {servidorFin.nombre}");
			//}

			if ( servidorFin == null || proxLlegada <= servidorFin.finAtencion )
			{
				//Console.WriteLine($"llegada: {proxLlegada}");
				return "llegada";
			}
            //Console.WriteLine($"finAtencion: {servidorFin.finAtencion}");
			return "finAtencion";


        }

        private static void InicializarServidores()
        {
			Seccion1 = new Servidor(_uniformeActividad1, "Seccion1");
			Seccion2 = new Servidor(_uniformeActividad2, "Seccion2");
			Seccion3 = new Servidor(_ExponencialActividad3, "Seccion3");
			Seccion4 = new Servidor(_uniformeActividad4, "Seccion4");
			Seccion5 = new Servidor(_ExponencialActividad5, "Seccion5");

            servidores.Add(Seccion1);
            servidores.Add(Seccion2);
            servidores.Add(Seccion3);
            servidores.Add(Seccion4);
            servidores.Add(Seccion5);



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

	}

}
