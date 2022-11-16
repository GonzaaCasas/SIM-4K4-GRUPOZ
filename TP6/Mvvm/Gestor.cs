using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using TP6.Models;
using TP6.ViewModels;
using TP6.Views;

namespace TP6.Mvvm {
	internal class Gestor {

		public static decimal reloj = 0;
		public static decimal proxLlegada;
		public static decimal _eventos;

		public static List<object> filaCompleta;
		private static List<List<object>> filasparaGrilla = new List<List<object>>();

        static  RandomAux rnd = new RandomAux();

        public static List<double> valorest = new List<double>();
        public static List<double> valoresx = new List<double>();
        public static List<double> valoresy = new List<double>();
        public static List<double> valoresyd = new List<double>();

		public static double valorT;
        public static double pico;
        public static double y;
        public static double yd;

        public static Servidor servidorFin;

		public static Queue<Cliente> clientesSeccion2 = new Queue<Cliente>();
		public static Queue<Cliente> clientesSeccion3 = new Queue<Cliente>();
		public static Queue<Cliente> clientesSeccion4 = new Queue<Cliente>();
		public static Queue<Cliente> clientesSeccion5 = new Queue<Cliente>();

		public static decimal acumTiempoSistema = 0;
		public static int acumEnsamblados = 0;
		public static int acumSolicitadas = 0;
		public static int acumProductosEnSistema = 0;
		public static int cantProductosEnSistema = 0;

		public static decimal propRealizadosSolicitados;
		public static decimal promedioDuracionEnsamble;
		public static decimal promedioProductosEnSistema;

		public static List<decimal> ensamblesPorHora = new List<decimal>(); // cada indice corresponde a una hora de las 24

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
		public static decimal proporcionTiempoBloqueoA5;


		public static decimal probabilidadDeCompletarxEnsambles;

		public static Cliente cliente2 { get; set; }
		public static Cliente cliente4 { get; set; }


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
		public static DistribucionExponencial _ExponencialPedidos;


		private static FilaVectorEstado filaActual;
		private static FilaVectorEstado filaAnterior;

		private static Calculo calculo;
		private static Calculo calculoAnterior;


		private static bool esInicio = true;
		private static bool noHayEncastreEnProceso = true;

        private static double min = 0;
		private static double max = 0;


		public static bool puntoA { get; set; }


		public static void simular(int eventos, double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5, double rangomin, double rangomax, string estrategia) {

			puntoA = true;
            noHayEncastreEnProceso = true;
            _eventos = eventos;
			filaAnterior = new FilaVectorEstado();
			calculoAnterior = new Calculo();
			limpiarVariables();
			inicializarDistribuciones(a1, b1, a2, b2, a4, b4, media3, media5);
			InicializarServidores();

			HojaGrilla.grilla = new DataTable();
			HojaGrilla.tablaCreada = false;

			Cliente clienteFin;
			for (int i = 0; i < eventos; i++) {

				filaActual = new FilaVectorEstado(filaAnterior);
				calculo = new Calculo(calculoAnterior);



				determinarRelojActual();
				filaActual.reloj = reloj;
				calculo.reloj = reloj;

				switch (filaActual.DeterminarEvento(filaAnterior.proximaLlegada, filaAnterior.proximoFinEncastre)) {
					case "Inicio":
						//proxLlegada = GenerarLlegadaCliente();
						//filaActual.evento = "Inicio";
						filaActual.GenerarLlegadaCliente();
						// reloj = filaActual.proximaLlegada;
						break;

					case "Llegada Materiales":

						//reloj = proxLlegada;
						//filaActual.reloj = reloj;
						//proxLlegada = GenerarLlegadaCliente();
						filaActual.GenerarLlegadaCliente();
						InicializarPedido();

						filaActual.material = $"{Seccion1.materialEnLlegada}, {Seccion2.materialEnLlegada}, {Seccion3.materialEnLlegada}";

						// para obtener proporcion ensambladas / solicitadas 

						calculo.acumSolicitadas++;

						// reloj = filaActual.proximaLlegada;                                                 

						break;

					case "Fin Atencion Servidor 1":

						//	reloj = servidorFin.finAtencion ?? 0;
						clienteFin = servidorFin.TerminarAtencion();


						calculo.tiempoAcumuladoOcupadoSeccion1 += clienteFin.horaFinAtencion - clienteFin.horaInicioAtencion; // del cliente que termino su atencion, calcula cuanto tiempo estuvo ocupado en el servidor y acumula


						calculo.tiempoAcumuladoEnEsperaSeccion1 += clienteFin.horaInicioAtencion - clienteFin.horaLlegada; // del cliente que termino su atencion en s1, calcula cuanto tiempo estuvo esperando en cola

						servidorFin.GenerarLlegadaCliente(Seccion4);


						filaActual.material = clienteFin.ClienteId.ToString(); // como no hay 

						break;

					case "Fin Atencion Servidor 2":

						clienteFin = servidorFin.TerminarAtencion();

						calculo.tiempoAcumuladoOcupadoSeccion2 += clienteFin.horaFinAtencion - clienteFin.horaInicioAtencion;

						calculo.tiempoAcumuladoEnEsperaSeccion2 += clienteFin.horaInicioAtencion - clienteFin.horaLlegada;



						clienteFin.horaLlegadaDesdeS2 = reloj;
						clientesSeccion2.Enqueue(clienteFin);

						if (clientesSeccion4.Count > 0) // si ya hay cola en producto proveniente de s4 que se genere una llegada de cliente a s5
						{
							Seccion2.GenerarLlegadaCliente(Seccion5);
							cliente4 = clientesSeccion4.Dequeue();
							cliente2 = clientesSeccion2.Dequeue();
							cantProductosEnSistema--;

						} else // si todavia no hay cola de producto  proveniente de s4 que permanezca en cola de s2
							{
							calculo.cantClientesPasaronporColaDesdeS2++;

						}


						// del cliente que termino su atencion en s2, calcula cuanto tiempo estuvo esperando en cola

						filaActual.material = clienteFin.ClienteId.ToString();

						break;

					case "Fin Atencion Servidor 3":

						clienteFin = servidorFin.TerminarAtencion();

						clientesSeccion3.Enqueue(clienteFin);

						calculo.tiempoAcumuladoOcupadoSeccion3 += clienteFin.horaFinAtencion - clienteFin.horaInicioAtencion;

						calculo.tiempoAcumuladoEnEsperaSeccion3 += clienteFin.horaInicioAtencion - clienteFin.horaLlegada; // del cliente que termino su atencion en s3, calcula cuanto tiempo estuvo esperando en cola

						filaActual.material = clienteFin.ClienteId.ToString();

						break;

					case "Fin Atencion Servidor 4":

						clienteFin = servidorFin.TerminarAtencion(); // cliente4 es cliente fin


						calculo.tiempoAcumuladoOcupadoSeccion4 += clienteFin.horaFinAtencion - clienteFin.horaInicioAtencion;

						calculo.tiempoAcumuladoEnEsperaSeccion4 += clienteFin.horaInicioAtencion - clienteFin.horaLlegada; // del cliente que termino su atencion en s4, calcula cuanto tiempo estuvo esperando en cola

						clienteFin.horaLlegadaDesdeS4 = reloj;
						clientesSeccion4.Enqueue(clienteFin);


						if (clientesSeccion2.Count > 0) // si ya hay cola en producto proveniente de s2 que se genere una llegada de cliente a s5
						{
							Seccion4.GenerarLlegadaCliente(Seccion5);
							cliente2 = clientesSeccion2.Dequeue();
							cliente4 = clientesSeccion4.Dequeue();
							cantProductosEnSistema--;

						} else // sino permanece en cola
							{
							calculo.cantClientesPasaronporColaDesdeS4++;
						}


						filaActual.material = clienteFin.ClienteId.ToString();

						break;

					case "Fin Atencion Servidor 5":

						clienteFin = servidorFin.TerminarAtencion();


						clientesSeccion5.Enqueue(clienteFin);

						calculo.tiempoAcumuladoOcupadoSeccion5 += clienteFin.horaFinAtencion - clienteFin.horaInicioAtencion;

						if (clienteFin.horaInicioAtencion > cliente4.horaLlegadaDesdeS4) {
							calculo.tiempoAcumuladoEnEsperaSeccion5DesdeS4 += clienteFin.horaInicioAtencion - cliente4.horaLlegadaDesdeS4;

						}
						if (clienteFin.horaInicioAtencion > cliente2.horaLlegadaDesdeS2) {
							calculo.tiempoAcumuladoEnEsperaSeccion5DesdeS2 += clienteFin.horaInicioAtencion - cliente2.horaLlegadaDesdeS2;

						}



						//  calculo.tiempoAcumuladoEnEsperaSeccion5 += clienteFin.horaInicioAtencion - clienteFin.horaLlegada; // del cliente que termino su atencion en s2 o s4, calcula cuanto tiempo estuvo esperando en cola

						filaActual.material = clienteFin.ClienteId.ToString();
										
						break;

                    case "Fin Encastre":

                        calculo.acumEnsamblados++;



                        Cliente cliente3 = clientesSeccion3.Dequeue();
                        Cliente cliente5 = clientesSeccion5.Dequeue();

						filaActual.material = cliente5.ClienteId % 2 == 0 ? $"{cliente5.ClienteId - 1}, {cliente5.ClienteId}, {cliente3.ClienteId}" :
                        cliente5.ClienteId == 1 ?  $"{cliente5.ClienteId}, {cliente5.ClienteId + 1}, {cliente3.ClienteId}" :
                          $"{cliente5.ClienteId - 1}, {cliente5.ClienteId},  {cliente3.ClienteId}" ;


                        calculo.acumuladorTiempoBloqueoSeccion5 += reloj - cliente5.horaFinAtencion; // acumula tiempo bloqueo de los productos provenientes de seccion5
                        calculo.acumuladorTiempoBloqueoSeccion3 += reloj - cliente3.horaFinAtencion; // acumula tiempo bloqueo de los productos provenientes de seccion3



                        if (cliente3.tiempoSistema > cliente5.tiempoSistema)
                        {

                            acumTiempoSistema += cliente3.tiempoSistema;
                        }
                        else
                        {
                            acumTiempoSistema += cliente5.tiempoSistema;
                        }

                        //diaCalculado = Math.Floor(reloj / (60 * 24)); // si paso mas de un dia calcula cuantos paso
                        horaCalculada = Math.Floor(reloj / 60); // si la hora cae en 23.5 entonces es la hora 23 ; la lista va de 0 a 23 horas (24 horas total)

                        if (horaCalculada >= 0 && horaCalculada < calculo.ensamblesPorHora.Count)
                        {
                            calculo.ensamblesPorHora[(int)horaCalculada]++;
                        }
                        else
                        {
                            calculo.ensamblesPorHora.Add(1);
                        }

                        noHayEncastreEnProceso = true;
						filaActual.proximoFinEncastre = null; // no hay prox fin encastre

                        break;

                    default:
						break;
				}


				// para determinar el proximo fin Encastre

				if (clientesSeccion5.Count >= 1 && clientesSeccion3.Count >= 1 && noHayEncastreEnProceso) {


                    if (estrategia == "RK")
					{
                        //  (valorest, valoresx, valoresy, valoresyd) = RungeKutta.calcular(0, 0, rnd);

                        (valorT, pico, y, yd) = RungeKutta.calcular(0, 0, rnd);
                    }
                    else
					{
                        (valorT, pico, y, yd) = Euler.calcular(0, 0, rnd);

                    }

                    filaActual.valort = (decimal)valorT;
                    filaActual.picox = (decimal)pico;
                    filaActual.y = (decimal)y;
                    filaActual.yd = (decimal)yd;

                    filaActual.proximoFinEncastre =  reloj + filaActual.valort;


                    noHayEncastreEnProceso = false;
				}

				// colas de productos provenientes de 4 y de 2 al servidor 5

				filaActual.colaS5_ProductoDesdeS4 = clientesSeccion4.Count;
				filaActual.colaS5_ProductoDesdeS2 = clientesSeccion2.Count;

				calculo.acumProductosEnSistema += cantProductosEnSistema;

				calculo.acumProductosEnCola += servidores.Sum(servidor => servidor.cola.Count());

				guardarDatosServidoresEnVectorEstado();

				if (filaActual.evento != "Inicio") {
					calculo.calcularPromedioDuracionEnsamble();

					calculo.calcularProporcionRealizadosSolicitados();

					calculo.calcularPromedioEnsamblesPorHora();

					calculo.calcularPermanenciaColaSecciones(servidores);

					calculo.calcularPorcentajeOcupacionSecciones();

					calculo.calcularProductosEnCola(eventos);

					calculo.calcularProductosEnSistemas(eventos);

					calculo.determinarCantMaxColas(servidores, clientesSeccion3, clientesSeccion5, clientesSeccion2, clientesSeccion4);

					calculo.calcularStdEnsamblesPorHora();

					calculo.calcularProporcionTiempoBloqueo();
				}


				filaAnterior = filaActual;

				calculoAnterior = calculo;

				//aca guarddar filaActual y calculo en grilla

				if (i >= rangomin - 1 && i <= rangomax) {
					filaCompleta = new List<object> { filaActual, calculo };

					HojaGrilla.CargarFila(filaCompleta, i);
					valorest.Add((double)filaActual.valort);
                    valoresx.Add((double)filaActual.picox);
                    valoresy.Add((double)filaActual.y);
                    valoresyd.Add((double)filaActual.yd);

                }

                //filasparaGrilla.Add(filaCompleta);
            }


		}

		private static void InicializarPedido() {
			Seccion1.NuevoCliente(new Cliente(reloj));
			Seccion2.NuevoCliente(new Cliente(reloj));
			Seccion3.NuevoCliente(new Cliente(reloj));

		}

		public static void determinarRelojActual() {

			if (esInicio) {
				filaActual.reloj = 0;
				esInicio = false;
			} else {

				determinarServidorFin();
				if (servidorFin == null) {
					reloj = filaAnterior.proximaLlegada;
				}
				else {

					if (filaAnterior.proximoFinEncastre != null)
					{

                        reloj = (decimal)(filaAnterior.proximaLlegada <= servidorFin.finAtencion && filaAnterior.proximaLlegada <= filaAnterior.proximoFinEncastre ? filaAnterior.proximaLlegada
                         : servidorFin.finAtencion <= filaAnterior.proximoFinEncastre ? servidorFin.finAtencion
                         : filaAnterior.proximoFinEncastre);
					}
					else
					{
						reloj = (decimal)(filaAnterior.proximaLlegada <= servidorFin.finAtencion ? filaAnterior.proximaLlegada : servidorFin.finAtencion);

                    }

                }
			}


		}

		public static void determinarServidorFin() {
			servidorFin = servidores.Where(x => x.finAtencion != null).OrderBy(servidor => servidor.finAtencion).FirstOrDefault();

		}

		public static void guardarDatosServidoresEnVectorEstado() {

			// cuando devuelve null, habria que devoler un string vacio en el front end

			filaActual.estadoS1 = Seccion1.estado;
			filaActual.materialS1 = Seccion1.clienteActual == null || Seccion1.estado == "libre" ? null : Seccion1.clienteActual.ClienteId;
			filaActual.tiempoAtencionS1 = Seccion1.estado == "libre" ? null : Seccion1.tiempoAtencion;
			filaActual.proximoFinAtencionS1 = Seccion1.finAtencion;
			filaActual.colaS1 = Seccion1.cola.Count();

			filaActual.estadoS2 = Seccion2.estado;
			filaActual.materialS2 = Seccion2.clienteActual == null || Seccion2.estado == "libre" ? null : Seccion2.clienteActual.ClienteId;
			filaActual.tiempoAtencionS2 = Seccion2.estado == "libre" ? null : Seccion2.tiempoAtencion;
			filaActual.proximoFinAtencionS2 = Seccion2.finAtencion;
			filaActual.colaS2 = Seccion2.cola.Count();

			filaActual.estadoS3 = Seccion3.estado;
			filaActual.materialS3 = Seccion3.clienteActual == null || Seccion3.estado == "libre" ? null : Seccion3.clienteActual.ClienteId;
			filaActual.tiempoAtencionS3 = Seccion3.estado == "libre" ? null : Seccion3.tiempoAtencion;
			filaActual.proximoFinAtencionS3 = Seccion3.finAtencion;
			filaActual.colaS3 = Seccion3.cola.Count();

			filaActual.estadoS4 = Seccion4.estado;
			filaActual.materialS4 = Seccion4.clienteActual == null || Seccion4.estado == "libre" ? null : Seccion4.clienteActual.ClienteId;
			filaActual.tiempoAtencionS4 = Seccion4.estado == "libre" ? null : Seccion4.tiempoAtencion;
			filaActual.proximoFinAtencionS4 = Seccion4.finAtencion;
			filaActual.colaS4 = Seccion4.cola.Count();

			filaActual.estadoS5 = Seccion5.estado;
			filaActual.materialS5 = Seccion5.clienteActual == null || Seccion5.estado == "libre" ? null : Seccion5.clienteActual.ClienteId;
			filaActual.tiempoAtencionS5 = Seccion5.estado == "libre" ? null : Seccion5.tiempoAtencion;
			filaActual.proximoFinAtencionS5 = Seccion5.finAtencion;
			filaActual.colaS5 = Seccion5.cola.Count();
		}


		private static void limpiarVariables() {
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
		public static void inicializarDistribuciones(double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5) {


			_uniformeActividad1 = new DistribucionUniforme(a1, b1, rnd);
			_uniformeActividad2 = new DistribucionUniforme(a2, b2, rnd);
			_ExponencialActividad3 = new DistribucionExponencial(media3, rnd);
			_uniformeActividad4 = new DistribucionUniforme(a4, b4, rnd);
			_ExponencialActividad5 = new DistribucionExponencial(media5, rnd);
			_ExponencialPedidos = new DistribucionExponencial(20, rnd); // 3 pedidos por hora = 3 pedidos en 60 minutos = 1 pedido en 20 minutos

		}

		private static void InicializarServidores() {
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

		public static decimal GenerarLlegadaCliente()                    // pedido entre 0.1 y 30 por hora
		{
			decimal tiempo = _ExponencialPedidos.Generar_x();

			if (tiempo > 600) // 0.1 pedidos por hora = 0.1 pedidos en 60 minutos = 1 pedido en 600 minutos.
			{
				tiempo = 600;
			} else {
				if (tiempo < 2) //30 pedidos por hora = 30 pedidos en 60 minutos = 1 pedido en 2 minutos
				{
					tiempo = 2;
				}
			}
			return (tiempo + reloj);

		}


		public static decimal ObtenerPromEnsamblesHora() {
			return calculo.promedioEnsamblesPorHora;
		}


		public static decimal ObtenerDEEnsamblesHora() {
			return calculo.stdEnsamblesPorHora;
		}

		public static List<decimal> ObtenerEnsamblesHora() {
			return calculo.ensamblesPorHora;
		}

		public static List<double> ObtenerValoresT()
		{
			return valorest;
		}
        public static List<double> ObtenerValoresY()
        {
            return valoresy;
        }
        public static List<double> ObtenerValoresX()
        {
            return valoresx;
        }
        public static List<double> ObtenerValoresYD()
        {
            return valoresyd;
        }


    }

}
