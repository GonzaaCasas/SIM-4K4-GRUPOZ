using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP6.Models {
	internal class Calculo {

		public int acumSolicitadas { get; set; }

		public int acumEnsamblados { get; set; }

		public decimal propRealizadosSolicitados { get; set; }

		public decimal promedioDuracionEnsamble { get; set; }

		public decimal reloj { get; set; }

		public List<decimal> ensamblesPorHora { get; set; }

		public decimal promedioEnsamblesPorHora { get; set; }

		public decimal cantMaxCola1 { get; set; }
		public decimal cantMaxCola2 { get; set; }
		public decimal cantMaxCola3 { get; set; }
		public decimal cantMaxCola4 { get; set; }
		public decimal cantMaxCola5 { get; set; }
		public decimal cantMaxColaS5_ProductoDesdeS2 { get; set; }
		public decimal cantMaxColaS5_ProductoDesdeS4 { get; set; }

		public decimal cantMaxColaEncastre { get; set; }

		public decimal tiempoAcumuladoEnEsperaSeccion1 { get; set; }
		public decimal tiempoAcumuladoEnEsperaSeccion2 { get; set; }
		public decimal tiempoAcumuladoEnEsperaSeccion3 { get; set; }
		public decimal tiempoAcumuladoEnEsperaSeccion4 { get; set; }
		public decimal tiempoAcumuladoEnEsperaSeccion5 { get; set; }

		public decimal tiempoAcumuladoOcupadoSeccion1 { get; set; }
		public decimal tiempoAcumuladoOcupadoSeccion2 { get; set; }
		public decimal tiempoAcumuladoOcupadoSeccion3 { get; set; }
		public decimal tiempoAcumuladoOcupadoSeccion4 { get; set; }
		public decimal tiempoAcumuladoOcupadoSeccion5 { get; set; }

		public decimal tiempoAcumuladoEnEsperaSeccion5DesdeS4 { get; set; }
		public decimal tiempoAcumuladoEnEsperaSeccion5DesdeS2 { get; set; }


		public decimal promedioPermanenciaColaSeccion1 { get; set; }
		public decimal promedioPermanenciaColaSeccion2 { get; set; }
		public decimal promedioPermanenciaColaSeccion3 { get; set; }
		public decimal promedioPermanenciaColaSeccion4 { get; set; }
		public decimal promedioPermanenciaColaSeccion5DesdeS2 { get; set; }
		public decimal promedioPermanenciaColaSeccion5DesdeS4 { get; set; }

		public int cantClientesPasaronporColaDesdeS2 { get; set; }
		public int cantClientesPasaronporColaDesdeS4 { get; set; }

		// public decimal promedioPermanenciaColaSeccion5 { get; set; }

		public decimal porcentajeOcupacioSeccion1 { get; set; }
		public decimal porcentajeOcupacioSeccion2 { get; set; }
		public decimal porcentajeOcupacioSeccion3 { get; set; }
		public decimal porcentajeOcupacioSeccion4 { get; set; }
		public decimal porcentajeOcupacioSeccion5 { get; set; }

		public decimal acumProductosEnCola { get; set; }
		public decimal promedioProductosEnCola { get; set; }

		public decimal acumProductosEnSistema { get; set; }
		public decimal promedioProductosEnSistema { get; set; }

		public decimal acumstdEnsamblesPorHora { get; set; }
		public decimal stdEnsamblesPorHora { get; set; }

		public decimal proporcionTiempoBloqueoA3 { get; set; }
		public decimal proporcionTiempoBloqueoA5 { get; set; }

		public decimal acumuladorTiempoBloqueoSeccion3 { get; set; }
		public decimal acumuladorTiempoBloqueoSeccion5 { get; set; }


		public Calculo() {
			this.ensamblesPorHora = new List<decimal>();
		}

		public Calculo(Calculo calculoAnterior) {
			this.acumSolicitadas = calculoAnterior.acumSolicitadas;

			this.acumEnsamblados = calculoAnterior.acumEnsamblados;

			this.tiempoAcumuladoEnEsperaSeccion1 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion1;
			this.tiempoAcumuladoEnEsperaSeccion2 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion2;
			this.tiempoAcumuladoEnEsperaSeccion3 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion3;
			this.tiempoAcumuladoEnEsperaSeccion4 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion4;
			this.tiempoAcumuladoEnEsperaSeccion5DesdeS2 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion5DesdeS2;
			this.tiempoAcumuladoEnEsperaSeccion5DesdeS4 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion5DesdeS4;
			//this.tiempoAcumuladoEnEsperaSeccion5 = calculoAnterior.tiempoAcumuladoEnEsperaSeccion5;

			this.cantClientesPasaronporColaDesdeS2 = calculoAnterior.cantClientesPasaronporColaDesdeS2;
			this.cantClientesPasaronporColaDesdeS4 = calculoAnterior.cantClientesPasaronporColaDesdeS4;

			this.tiempoAcumuladoOcupadoSeccion1 = calculoAnterior.tiempoAcumuladoOcupadoSeccion1;
			this.tiempoAcumuladoOcupadoSeccion2 = calculoAnterior.tiempoAcumuladoOcupadoSeccion2;
			this.tiempoAcumuladoOcupadoSeccion3 = calculoAnterior.tiempoAcumuladoOcupadoSeccion3;
			this.tiempoAcumuladoOcupadoSeccion4 = calculoAnterior.tiempoAcumuladoOcupadoSeccion4;
			this.tiempoAcumuladoOcupadoSeccion5 = calculoAnterior.tiempoAcumuladoOcupadoSeccion5;

			this.acumuladorTiempoBloqueoSeccion3 = calculoAnterior.acumuladorTiempoBloqueoSeccion3;
			this.acumuladorTiempoBloqueoSeccion5 = calculoAnterior.acumuladorTiempoBloqueoSeccion5;


			this.acumProductosEnCola = calculoAnterior.acumProductosEnCola;
			this.acumProductosEnSistema = calculoAnterior.acumProductosEnSistema;

			this.reloj = calculoAnterior.reloj;

			this.ensamblesPorHora = calculoAnterior.ensamblesPorHora;

		}


		public void calcularProporcionRealizadosSolicitados() {
			if (this.acumSolicitadas != 0) {
				this.propRealizadosSolicitados = (decimal)this.acumEnsamblados / (decimal)this.acumSolicitadas;

			}


		}

		public void calcularPromedioDuracionEnsamble() {
			if (this.acumEnsamblados != 0) {
				this.promedioDuracionEnsamble = this.reloj / this.acumEnsamblados;

			}

		}

		public void calcularPromedioEnsamblesPorHora() {
			if (this.ensamblesPorHora.Count > 0) {
				this.promedioEnsamblesPorHora = this.ensamblesPorHora.Average();

			}

			//this.promedioEnsamblesPorHora = this.acumEnsamblados / (this.reloj / 60);
		}

		public void calcularPermanenciaColaSecciones(List<Servidor> servidores) {
			if (servidores[0].cantClientesPasaronPorCola != 0) {
				promedioPermanenciaColaSeccion1 = this.tiempoAcumuladoEnEsperaSeccion1 / servidores[0].cantClientesPasaronPorCola;

			}

			if (servidores[1].cantClientesPasaronPorCola != 0) {
				promedioPermanenciaColaSeccion2 = this.tiempoAcumuladoEnEsperaSeccion2 / servidores[1].cantClientesPasaronPorCola;


			}
			if (servidores[2].cantClientesPasaronPorCola != 0) {
				promedioPermanenciaColaSeccion3 = this.tiempoAcumuladoEnEsperaSeccion1 / servidores[2].cantClientesPasaronPorCola;

			}
			if (servidores[3].cantClientesPasaronPorCola != 0) {
				promedioPermanenciaColaSeccion4 = this.tiempoAcumuladoEnEsperaSeccion1 / servidores[3].cantClientesPasaronPorCola;

			}

			if (this.cantClientesPasaronporColaDesdeS2 != 0) {
				this.promedioPermanenciaColaSeccion5DesdeS2 = this.tiempoAcumuladoEnEsperaSeccion5DesdeS2 / this.cantClientesPasaronporColaDesdeS2;

			}

			if (this.cantClientesPasaronporColaDesdeS4 != 0) {

				this.promedioPermanenciaColaSeccion5DesdeS4 = this.tiempoAcumuladoEnEsperaSeccion5DesdeS4 / this.cantClientesPasaronporColaDesdeS4;

			}


		}

		public void calcularPorcentajeOcupacionSecciones() {

			this.porcentajeOcupacioSeccion1 = (this.tiempoAcumuladoOcupadoSeccion1 / this.reloj) * 100;
			this.porcentajeOcupacioSeccion2 = (this.tiempoAcumuladoOcupadoSeccion2 / this.reloj) * 100;
			this.porcentajeOcupacioSeccion3 = (this.tiempoAcumuladoOcupadoSeccion3 / this.reloj) * 100;
			this.porcentajeOcupacioSeccion4 = (this.tiempoAcumuladoOcupadoSeccion4 / this.reloj) * 100;
			this.porcentajeOcupacioSeccion5 = (this.tiempoAcumuladoOcupadoSeccion5 / this.reloj) * 100;

		}

		public void determinarCantMaxColas(List<Servidor> servidores, Queue<Cliente> productosProvenientesSeccion3, Queue<Cliente> productosProvenientesSeccion5,
																				Queue<Cliente> productosProvenientesSeccion2, Queue<Cliente> productosProvenientesSeccion4) {
			this.cantMaxCola1 = servidores[0].maximoCola;
			this.cantMaxCola2 = servidores[1].maximoCola;
			this.cantMaxCola3 = servidores[2].maximoCola;
			this.cantMaxCola4 = servidores[3].maximoCola;
			//  this.cantMaxCola5 = servidores[4].maximoCola;

			this.cantMaxColaS5_ProductoDesdeS2 = productosProvenientesSeccion2.Count > this.cantMaxColaS5_ProductoDesdeS2 ? productosProvenientesSeccion2.Count : this.cantMaxColaS5_ProductoDesdeS2;

			this.cantMaxColaS5_ProductoDesdeS4 = productosProvenientesSeccion4.Count > this.cantMaxColaS5_ProductoDesdeS4 ? productosProvenientesSeccion4.Count : this.cantMaxColaS5_ProductoDesdeS4;

			this.cantMaxColaEncastre = productosProvenientesSeccion3.Count + productosProvenientesSeccion5.Count > this.cantMaxColaEncastre ? productosProvenientesSeccion3.Count + productosProvenientesSeccion5.Count : this.cantMaxColaEncastre;
		}

		public void calcularProductosEnCola(decimal cantEventos) {
			this.promedioProductosEnCola = this.acumProductosEnCola / cantEventos;
		}

		public void calcularProductosEnSistemas(decimal cantEventos) {
			this.promedioProductosEnSistema = this.acumProductosEnSistema / cantEventos;
		}

		public void calcularStdEnsamblesPorHora() {
			for (int i = 1; i < ensamblesPorHora.Count; i++) {
				this.acumstdEnsamblesPorHora += (decimal)Math.Pow((double)(ensamblesPorHora[i - 1] - promedioEnsamblesPorHora), 2);

				if (i == 1) {
					this.stdEnsamblesPorHora = (decimal)Math.Sqrt((double)(this.acumstdEnsamblesPorHora / i));
				} else {
					this.stdEnsamblesPorHora = (decimal)Math.Sqrt((double)(this.acumstdEnsamblesPorHora / (i - 1)));
				}
			}
		}

		public void calcularProporcionTiempoBloqueo() {
			if ((this.acumuladorTiempoBloqueoSeccion5 + this.acumuladorTiempoBloqueoSeccion3) != 0) {
				this.proporcionTiempoBloqueoA3 = this.acumuladorTiempoBloqueoSeccion3 / (this.acumuladorTiempoBloqueoSeccion5 + this.acumuladorTiempoBloqueoSeccion3); // del tiempo total de bloqueo para encastrar entre los productos de las dos secciones, indica la proporcion que estuvo bloqueado los productos provenientes de la seccion 3
				this.proporcionTiempoBloqueoA5 = 1 - this.proporcionTiempoBloqueoA3;
			}


		}

	}
}
