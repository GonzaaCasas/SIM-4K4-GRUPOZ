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


		private static double min = 0;
		private static double max = 0;
	
		public static bool puntoA { get; set; }


		public static void simular(int eventos, double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5) {
			puntoA = true;
			_eventos = eventos;
            limpiarVariables();
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

						reloj = proxLlegada;
						proxLlegada = GenerarLlegadaCliente();
						InicializarPedido();
						acumSolicitadas++; //para obtener proporcion ensambladas / solicitadas 
						break;

					case "finAtencion":

						reloj = servidorFin.finAtencion ?? 0;
						clienteFin = servidorFin.TerminarAtencion();


						if (servidorFin.Equals(Seccion1))
						{
							servidorFin.GenerarLlegadaCliente(Seccion4);

                            tiempoAcumuladoOcupadoSeccion1 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion; // del cliente que termino su atencion, calcula cuanto tiempo estuvo ocupado en el servidor y acumula
							tiempoAcumuladoEnEsperaSeccion1 += clienteFin.tiempoEspera;

                        }
						if (servidorFin.Equals(Seccion2))
						{
							clientesSeccion2.Enqueue(clienteFin);

                            tiempoAcumuladoOcupadoSeccion2 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion;
							tiempoAcumuladoEnEsperaSeccion2 += clienteFin.tiempoEspera;

                        }
                        if (servidorFin.Equals(Seccion3))
						{
							clientesSeccion3.Enqueue(clienteFin);

                            tiempoAcumuladoOcupadoSeccion3 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion;
							tiempoAcumuladoEnEsperaSeccion3 += clienteFin.tiempoEspera;

                        }
                        if (servidorFin.Equals(Seccion4))
						{
							clientesSeccion4.Enqueue(clienteFin);

                            tiempoAcumuladoOcupadoSeccion4 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion;
							tiempoAcumuladoEnEsperaSeccion4 += clienteFin.tiempoEspera;

                        }
                        if (servidorFin.Equals(Seccion5))
						{
							clientesSeccion5.Enqueue(clienteFin);

                            tiempoAcumuladoOcupadoSeccion5 += clienteFin.horaFinAtencion - clienteFin.horaEmpiezoAtencion;
							tiempoAcumuladoEnEsperaSeccion5 += clienteFin.tiempoEspera;

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

                                acumTiempoSistema += cliente3.tiempoSistema;
                            }
							else
							{
                                acumTiempoSistema += cliente5.tiempoSistema;
                            }

							diaCalculado = Math.Floor(reloj / (60 * 24)) ; // si paso mas de un dia calcula cuantos paso
                            horaCalculada = Math.Floor( ( (reloj - (60 * 24 * diaCalculado) ) / 60  )); // si la hora cae en 23.5 entonces es la hora 23 ; la lista va de 0 a 23 horas (24 horas total)
							ensamblesPorHora[(int)horaCalculada]++;

						}
						break;

					default:
						break;
				}

				acumProductosEnSistema += cantProductosEnSistema;

				acumProductosEnCola += servidores.Sum(servidor => servidor.cola.Count());

            }

            calcularPorcentajeOcupacionSecciones();

            calcularPermanenciaColaSecciones();


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
            proporcionTiempoBloqueo = acumuladorTiempoBloqueoSeccion3 / (acumuladorTiempoBloqueoSeccion5 + acumuladorTiempoBloqueoSeccion3); // del tiempo total de bloqueo para encastrar entre los productos de las dos secciones, indica la proporcion que estuvo bloqueado los productos provenientes de la seccion 3
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
                acumstdEnsamblesPorHora += (decimal)Math.Pow((double)(ensamblesPorHora[i-1] - promedioEnsamblesPorHora), 2);

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

			porcentajeOcupacioSeccion1 = (tiempoAcumuladoOcupadoSeccion1 / reloj) * 100;
			porcentajeOcupacioSeccion2 = (tiempoAcumuladoOcupadoSeccion2 / reloj) * 100;
			porcentajeOcupacioSeccion3 = (tiempoAcumuladoOcupadoSeccion3 / reloj) * 100;
			porcentajeOcupacioSeccion4 = (tiempoAcumuladoOcupadoSeccion4 / reloj) * 100;
			porcentajeOcupacioSeccion5 = (tiempoAcumuladoOcupadoSeccion5 / reloj) * 100;

		}
        private static void calcularPermanenciaColaSecciones()
		{

                promedioPermanenciaColaSeccion1 = tiempoAcumuladoEnEsperaSeccion1 / Seccion1.cantClientesPasaronPorCola;
                promedioPermanenciaColaSeccion2 = tiempoAcumuladoEnEsperaSeccion2 / Seccion2.cantClientesPasaronPorCola;
                promedioPermanenciaColaSeccion3 = tiempoAcumuladoEnEsperaSeccion3 / Seccion3.cantClientesPasaronPorCola;
                promedioPermanenciaColaSeccion4 = tiempoAcumuladoEnEsperaSeccion4 / Seccion4.cantClientesPasaronPorCola;
                promedioPermanenciaColaSeccion5 = tiempoAcumuladoEnEsperaSeccion5 / Seccion5.cantClientesPasaronPorCola;
            
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
            

			if ( servidorFin == null || proxLlegada <= servidorFin.finAtencion )
			{

				return "llegada";
			}

			return "finAtencion";


        }

        private static void limpiarVariables()
        {
            reloj = 0;
            acumTiempoSistema = 0;
            acumEnsamblados = 0;
            servidores.Clear();
            acumSolicitadas = 0;
            tiempoAcumuladoOcupadoSeccion1 = 0;
            tiempoAcumuladoOcupadoSeccion2 = 0;
            tiempoAcumuladoOcupadoSeccion3 = 0;
            tiempoAcumuladoOcupadoSeccion4 = 0;
            tiempoAcumuladoOcupadoSeccion5 = 0;
            tiempoAcumuladoEnEsperaSeccion1 = 0;
            tiempoAcumuladoEnEsperaSeccion2 = 0;
            tiempoAcumuladoEnEsperaSeccion3 = 0;
            tiempoAcumuladoEnEsperaSeccion4 = 0;
            tiempoAcumuladoEnEsperaSeccion5 = 0;
            clientesSeccion2.Clear();
            clientesSeccion3.Clear();
            clientesSeccion4.Clear();
            clientesSeccion5.Clear();
            cantProductosEnSistema = 0;
            acumuladorTiempoBloqueoSeccion5 = 0;
            acumuladorTiempoBloqueoSeccion3 = 0;
            ensamblesPorHora = new List<decimal>(new decimal[24]);
            acumProductosEnSistema = 0;
            acumProductosEnCola = 0;
            acumstdEnsamblesPorHora = 0;
        }
        public static void inicializarDistribuciones(double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5)
        {

            RandomAux rnd = new RandomAux();

            _uniformeActividad1 = new DistribucionUniforme(a1, b1, rnd);
            _uniformeActividad2 = new DistribucionUniforme(a2, b2, rnd);
            _ExponencialActividad3 = new DistribucionExponencial(media3, rnd);
            _uniformeActividad4 = new DistribucionUniforme(a4, b4, rnd);
            _ExponencialActividad5 = new DistribucionExponencial(media5, rnd);
            _ExponencialPedidos = new DistribucionExponencial(20, rnd); // 3 pedidos por hora = 3 pedidos en 60 minutos = 1 pedido en 20 minutos

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

    




	}

}
