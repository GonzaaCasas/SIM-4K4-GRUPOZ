using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using TP5.Models;
using TP5.ViewModels;


namespace TP5.Mvvm {
	internal class Gestor {

        public static decimal reloj;
		public static decimal proxLlegada;
		public static decimal _eventos;

        public static Servidor servidorFin;

        public static Queue<Cliente> clientesSeccion2 = new Queue<Cliente>();
        public static Queue<Cliente> clientesSeccion3 = new Queue<Cliente>();
		public static Queue<Cliente> clientesSeccion4 = new Queue<Cliente>();
		public static Queue<Cliente> clientesSeccion5 = new Queue<Cliente>();

		public static decimal acumTiempoSistema= 0;
		public static int acumEnsamblados = 0 ;
		public static int acumSolicitadas = 0;
		public static int acumProductosEnSistema = 0;
		public static int cantProductosEnSistema = 0;

		public static decimal propRealizadosSolicitados;
		public static decimal promedioDuracionEnsamble;
		public static decimal promedioProductosEnSistema;

        public static List<decimal> ensamblesPorHora = new List<decimal>(new decimal[24]); // cada indice corresponde a una hora de las 24

        public static decimal diaCalculado;
        public static decimal horaCalculada;
        public static decimal promedioEnsamblesPorHora;
        public static decimal acumstdEnsamblesPorHora;
        public static decimal stdEnsamblesPorHora;




        public static decimal promedioProductosEnCola;
        public static decimal acumProductosEnCola;




        public static decimal tiempoAcumuladoOcupadoSeccion1;
        public static decimal tiempoAcumuladoOcupadoSeccion2;
        public static decimal tiempoAcumuladoOcupadoSeccion3;
        public static decimal tiempoAcumuladoOcupadoSeccion4;
        public static decimal tiempoAcumuladoOcupadoSeccion5;

        public static decimal tiempoAcumuladoEnEsperaSeccion1;
        public static decimal tiempoAcumuladoEnEsperaSeccion2;
        public static decimal tiempoAcumuladoEnEsperaSeccion3;
        public static decimal tiempoAcumuladoEnEsperaSeccion4;
        public static decimal tiempoAcumuladoEnEsperaSeccion5;


        public static decimal tiempoAcumuladoSimulacionSeccion1;
        public static decimal tiempoAcumuladoSimulacionSeccion2;
        public static decimal tiempoAcumuladoSimulacionSeccion3;
        public static decimal tiempoAcumuladoSimulacionSeccion4;
        public static decimal tiempoAcumuladoSimulacionSeccion5;




        public static decimal porcentajeOcupacioSeccion1;
        public static decimal porcentajeOcupacioSeccion2;
        public static decimal porcentajeOcupacioSeccion3;
        public static decimal porcentajeOcupacioSeccion4;
        public static decimal porcentajeOcupacioSeccion5;



        public static decimal acumuladorTiempoBloqueoSeccion5;
        public static decimal acumuladorTiempoBloqueoSeccion3;





        public static decimal cantMaxCola1;
        public static decimal cantMaxCola2;
        public static decimal cantMaxCola3;
        public static decimal cantMaxCola4;
        public static decimal cantMaxCola5;


        public static decimal promedioPermanenciaColaSeccion1;
        public static decimal promedioPermanenciaColaSeccion2;
        public static decimal promedioPermanenciaColaSeccion3;
        public static decimal promedioPermanenciaColaSeccion4;
        public static decimal promedioPermanenciaColaSeccion5;

		public static decimal proporcionTiempoBloqueo;


        public static decimal probabilidadDeCompletarxEnsambles;


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
			_ExponencialPedidos = new DistribucionExponencial(20, rnd); // 3 pedidos por hora = 3 pedidos en 60 minutos = 1 pedido en 20 minutos

		}




		public static void simular(int eventos, double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5) {
			puntoA = true;
			_eventos = eventos;
			inicializarDistribuciones(a1, b1, a2, b2, a4, b4, media3, media5);
			InicializarServidores();
			acumTiempoSistema = 0;
			acumEnsamblados =0;

			
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
                        //Console.WriteLine("llegada");
						reloj = proxLlegada;
						proxLlegada = GenerarLlegadaCliente();
						InicializarPedido();
						acumSolicitadas++; //para obtener proporcion ensambladas / solicitadas 
						break;

					case "finAtencion":
                        //Console.WriteLine("finatencion");
						reloj = servidorFin.finAtencion ?? 0;
						clienteFin = servidorFin.TerminarAtencion();
						//clienteFin.tiempoEsperaAcumulado += clienteFin.tiempoEspera;

						if (servidorFin.Equals(Seccion1))
						{
							servidorFin.GenerarLlegadaCliente(Seccion4);


                            tiempoAcumuladoSimulacionSeccion1 += clienteFin.tiempoSistema;
                            tiempoAcumuladoOcupadoSeccion1 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion; // del cliente que termino su atencion, calcula cuanto tiempo estuvo ocupado en el servidor y acumula
							tiempoAcumuladoEnEsperaSeccion1 += clienteFin.tiempoEspera;


                        }
						if (servidorFin.Equals(Seccion2))
						{
							clientesSeccion2.Enqueue(clienteFin);

                            tiempoAcumuladoSimulacionSeccion2 += clienteFin.tiempoSistema;
                            tiempoAcumuladoOcupadoSeccion2 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion;
							tiempoAcumuladoEnEsperaSeccion2 += clienteFin.tiempoEspera;


                        }
                        if (servidorFin.Equals(Seccion3))
						{
							clientesSeccion3.Enqueue(clienteFin);

                            tiempoAcumuladoSimulacionSeccion3 += clienteFin.tiempoSistema;
                            tiempoAcumuladoOcupadoSeccion3 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion;
                            tiempoAcumuladoEnEsperaSeccion3 += clienteFin.tiempoEspera / clienteFin.tiempoSistema;


                        }
                        if (servidorFin.Equals(Seccion4))
						{
							clientesSeccion4.Enqueue(clienteFin);

                            tiempoAcumuladoSimulacionSeccion4 += clienteFin.tiempoSistema;
                            tiempoAcumuladoOcupadoSeccion4 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion;
                            tiempoAcumuladoEnEsperaSeccion4 += clienteFin.tiempoEspera / clienteFin.tiempoSistema;


                        }
                        if (servidorFin.Equals(Seccion5))
						{
							clientesSeccion5.Enqueue(clienteFin);

                            tiempoAcumuladoSimulacionSeccion5 += clienteFin.tiempoSistema;
                            tiempoAcumuladoOcupadoSeccion5 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion;
                            tiempoAcumuladoEnEsperaSeccion5 += clienteFin.tiempoEspera / clienteFin.tiempoSistema;

                        }

                      

                        if (clientesSeccion2.Count >= 1 && clientesSeccion4.Count >= 1)
						{
							Cliente cliente2 = clientesSeccion2.Dequeue();
							Cliente cliente4 = clientesSeccion4.Dequeue();
							cantProductosEnSistema--;

							if (cliente4.tiempoSistema > cliente2.tiempoSistema)
							{
								Seccion4.GenerarLlegadaCliente(Seccion5);
							}
							else
							{
								Seccion2.GenerarLlegadaCliente(Seccion5);
							}

						}

          


                        if (clientesSeccion5.Count >= 1 && clientesSeccion3.Count >= 1)
						{
                            

                            Cliente cliente3 = clientesSeccion3.Dequeue();
							Cliente cliente5 = clientesSeccion5.Dequeue();

							acumuladorTiempoBloqueoSeccion5 += reloj - cliente5.horaFinAtencion; // acumula tiempo bloqueo de los productos provenientes de seccion5
                            acumuladorTiempoBloqueoSeccion3 += reloj - cliente3.horaFinAtencion; // acumula tiempo bloqueo de los productos provenientes de seccion3

                            acumEnsamblados++;

							if (cliente3.tiempoSistema > cliente5.tiempoSistema)
							{
                                //promedioDuracionEnsamble = (promedioDuracionEnsamble * (acumEnsamblados - 1) + cliente3.tiempoSistema)/acumEnsamblados;
                                acumTiempoSistema += cliente3.tiempoSistema;
                            }
							else
							{
                                //promedioDuracionEnsamble = (promedioDuracionEnsamble * (acumEnsamblados - 1) + cliente5.tiempoSistema) / acumEnsamblados;
                                acumTiempoSistema += cliente5.tiempoSistema;
                            }

							Console.WriteLine($"Prod ensamblados: {acumEnsamblados}. \n minutos : {reloj}. \n Tiempo de espera acumulado: {acumTiempoSistema}\n\n");

							diaCalculado = Math.Floor(reloj / (60 * 24)) ; // si paso mas de un dia calcula cuantos paso
                            horaCalculada = Math.Floor( ( (reloj - (60 * 24 * diaCalculado) ) / 60  )); // si la hora cae en 23.5 entonces es la hora 23 ; la lista va de 0 a 23 horas (24 horas total)
							ensamblesPorHora[(int)horaCalculada]++;

						}
						break;

					default:
						break;
				}



				//Console.WriteLine($"reloj : {reloj}");
				acumProductosEnSistema += cantProductosEnSistema;

				acumProductosEnCola += servidores.Sum(servidor => servidor.cola.Count());

            }


			


            calcularPorcentajeOcupacionSecciones();
			
			calcularPromedioEnsamblesPorHora();
           
            calcularPromedioDuracionEnsamble();
       
			calcularProporcionRealizadosSolicitados();
         
			calcularProductosEnSistemas();

			calcularProductosEnCola();

			calcularStdEnsamblesPorHora();
      
			determinarCantMaxColas();
            
			calcularProporcionTiempoBloqueo();
            



            Console.WriteLine($"Promedio duracion ensamble: {promedioDuracionEnsamble}");


        }

		private static void calcularProporcionTiempoBloqueo()
		{
            proporcionTiempoBloqueo = acumuladorTiempoBloqueoSeccion3 / (acumuladorTiempoBloqueoSeccion5 + acumuladorTiempoBloqueoSeccion3); // del tiempo total de bloqueo para encastrar, indica la proporcion que estuvo bloqueado los productos provenientes de la seccion 3
        }

		private static void determinarCantMaxColas()
		{
            cantMaxCola1 = servidores[0].maximoCola;
            cantMaxCola2 = servidores[1].maximoCola;
            cantMaxCola3 = servidores[2].maximoCola;
            cantMaxCola4 = servidores[3].maximoCola;
            cantMaxCola5 = servidores[4].maximoCola;
        }

		private static void calcularStdEnsamblesPorHora()
		{
            for (int i = 1; i < ensamblesPorHora.Count; i++)
            {
                acumstdEnsamblesPorHora += (decimal)Math.Pow((double)(ensamblesPorHora[i] - promedioEnsamblesPorHora), 2);

                if (i == 1)
                {
                    stdEnsamblesPorHora = (decimal)Math.Sqrt((double)(acumstdEnsamblesPorHora / i));
                }
                else
                {
                    stdEnsamblesPorHora = (decimal)Math.Sqrt((double)(acumstdEnsamblesPorHora / (i - 1)));
                }
            }
        }

		private static void calcularProductosEnCola()
		{
            promedioProductosEnCola = acumProductosEnCola / _eventos;
        }

		private static void calcularProductosEnSistemas()
		{
            promedioProductosEnSistema = acumProductosEnSistema / _eventos;
        }

		private static void calcularProporcionRealizadosSolicitados()
		{
            propRealizadosSolicitados = acumEnsamblados / acumSolicitadas;

        }

        private static void calcularPromedioDuracionEnsamble()
		{
            promedioDuracionEnsamble = reloj / acumEnsamblados;
        }

		private static void calcularPromedioEnsamblesPorHora()
		{
            promedioEnsamblesPorHora = ensamblesPorHora.Sum() / 24;
        }

		private static void calcularPorcentajeOcupacionSecciones()
		{
            porcentajeOcupacioSeccion1 = (tiempoAcumuladoOcupadoSeccion1 / tiempoAcumuladoSimulacionSeccion1) * 100;
            porcentajeOcupacioSeccion2 = (tiempoAcumuladoOcupadoSeccion2 / tiempoAcumuladoSimulacionSeccion2) * 100;
            porcentajeOcupacioSeccion3 = (tiempoAcumuladoOcupadoSeccion3 / tiempoAcumuladoSimulacionSeccion3) * 100;
            porcentajeOcupacioSeccion4 = (tiempoAcumuladoOcupadoSeccion4 / tiempoAcumuladoSimulacionSeccion4) * 100;
            porcentajeOcupacioSeccion5 = (tiempoAcumuladoOcupadoSeccion5 / tiempoAcumuladoSimulacionSeccion5) * 100;


            promedioPermanenciaColaSeccion1 = tiempoAcumuladoEnEsperaSeccion1 / Seccion1.cantClientesPasaronACola;
            promedioPermanenciaColaSeccion2 = tiempoAcumuladoEnEsperaSeccion2 / Seccion2.cantClientesPasaronACola;
            promedioPermanenciaColaSeccion3 = tiempoAcumuladoEnEsperaSeccion3 / Seccion3.cantClientesPasaronACola;
            promedioPermanenciaColaSeccion4 = tiempoAcumuladoEnEsperaSeccion4 / Seccion4.cantClientesPasaronACola;
            promedioPermanenciaColaSeccion5 = tiempoAcumuladoEnEsperaSeccion5 / Seccion5.cantClientesPasaronACola;
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

        public static decimal GenerarLlegadaCliente()
        {
			decimal tiempo = _ExponencialPedidos.Generar_x();

            if (tiempo > 600) // 0.1 pedidos por hora = 0.1 pedidos en 60 minutos = 1 pedido en 600 minutos.
            {
				tiempo = 600;
            }
            else
            {
				if (tiempo < 2) //30 pedidos por hora = 30 pedidos en 60 minutos = 1 pedido en 2 minutos
				{
					tiempo = 2;
				}
			} 
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
