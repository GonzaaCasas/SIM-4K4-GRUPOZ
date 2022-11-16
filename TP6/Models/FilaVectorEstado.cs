using System;
using System.Collections.Generic;
using System.Linq;
using TP6.Mvvm;

namespace TP6.Models {
	internal class FilaVectorEstado {
		public decimal reloj { get; set; }
		public string evento { get; set; }
		public string material { get; set; } // cliente , por cada prox llegada generamos 3 materiales uno para cada server en paralelo
																				 // public string proximoMaterial { get; set; }  // proximo cliente
		public decimal tiempoEntreLlegadas { get; set; }
		public decimal proximaLlegada { get; set; }
        public Nullable<decimal> proximoFinEncastre { get; set; }

        public List<double> valorest = new List<double>();
        public List<double> valoresx = new List<double>();
        public List<double> valoresy = new List<double>();
        public List<double> valoresyd = new List<double>();

		public decimal valort;
        public decimal picox;

        // server 1
        public string estadoS1 { get; set; }
		public Nullable<int> materialS1 { get; set; }
		public Nullable<decimal> tiempoAtencionS1 { get; set; }
		public Nullable<decimal> proximoFinAtencionS1 { get; set; }
		public int colaS1 { get; set; }


		// server 2
		public string estadoS2 { get; set; }
		public Nullable<int> materialS2 { get; set; }
		public Nullable<decimal> tiempoAtencionS2 { get; set; }
		public Nullable<decimal> proximoFinAtencionS2 { get; set; }
		public int colaS2 { get; set; }


		// server 3
		public string estadoS3 { get; set; }
		public Nullable<int> materialS3 { get; set; }
		public Nullable<decimal> tiempoAtencionS3 { get; set; }
		public Nullable<decimal> proximoFinAtencionS3 { get; set; }
		public int colaS3 { get; set; }


		// server 4
		public string estadoS4;
		public Nullable<int> materialS4;
		public Nullable<decimal> tiempoAtencionS4;
		public Nullable<decimal> proximoFinAtencionS4 { get; set; }
		public int colaS4 { get; set; }


		// server 5
		public string estadoS5 { get; set; }
		public Nullable<int> materialS5 { get; set; }
		public Nullable<decimal> tiempoAtencionS5 { get; set; }
		public Nullable<decimal> proximoFinAtencionS5 { get; set; }
		public int colaS5 { get; set; }

		public int colaS5_ProductoDesdeS4 { get; set; }
		public int colaS5_ProductoDesdeS2 { get; set; }




		public FilaVectorEstado() {

		}

		public FilaVectorEstado(FilaVectorEstado filaAnterior) {
			this.material = filaAnterior.material;
			//proximoMaterial = filaAnterior.proximoMaterial;
			this.tiempoEntreLlegadas = filaAnterior.tiempoEntreLlegadas;
			this.proximaLlegada = filaAnterior.proximaLlegada;
			this.proximoFinEncastre = filaAnterior.proximoFinEncastre;

			// server 1
			this.estadoS1 = filaAnterior.estadoS1;
			this.materialS1 = filaAnterior.materialS1;
			this.tiempoAtencionS1 = filaAnterior.tiempoAtencionS1;
			this.proximoFinAtencionS1 = filaAnterior.proximoFinAtencionS1;
			this.colaS1 = filaAnterior.colaS1;


			// server 2
			this.estadoS2 = filaAnterior.estadoS2;
			this.materialS2 = filaAnterior.materialS2;
			this.tiempoAtencionS2 = filaAnterior.tiempoAtencionS2;
			this.proximoFinAtencionS2 = filaAnterior.proximoFinAtencionS2;
			this.colaS2 = filaAnterior.colaS2;


			// server 3
			this.estadoS3 = filaAnterior.estadoS3;
			this.materialS3 = filaAnterior.materialS3;
			this.tiempoAtencionS3 = filaAnterior.tiempoAtencionS3;
			this.proximoFinAtencionS3 = filaAnterior.proximoFinAtencionS3;
			this.colaS3 = filaAnterior.colaS3;


			// server 4
			this.estadoS4 = filaAnterior.estadoS4;
			this.materialS4 = filaAnterior.materialS4;
			this.tiempoAtencionS4 = filaAnterior.tiempoAtencionS4;
			this.proximoFinAtencionS4 = filaAnterior.proximoFinAtencionS4;
			this.colaS4 = filaAnterior.colaS4;


			// server 5
			this.estadoS5 = filaAnterior.estadoS5;
			this.materialS5 = filaAnterior.materialS5;
			this.tiempoAtencionS5 = filaAnterior.tiempoAtencionS5;
			this.proximoFinAtencionS5 = filaAnterior.proximoFinAtencionS5;
			this.colaS5 = filaAnterior.colaS5;

			// valores de Ecuacion Diferencial

			 this.valorest = filaAnterior.valorest;
			 this.valoresx = filaAnterior.valoresx;
			 this.valoresy = filaAnterior.valoresy;
			 this.valoresyd = filaAnterior.valoresyd;

			 this.valort = filaAnterior.valort;
			 this.picox = filaAnterior.picox;
    }

		public string DeterminarEvento(decimal proximaLllegadaFilaAnterior, Nullable<decimal> proximoFinEncastreFilaAnteriror) {


			if (this.reloj == 0) {
				return this.evento = "Inicio";

			}

			//   determinar Servidor Fin

			Gestor.servidorFin = Gestor.servidores.Where(x => x.finAtencion != null).OrderBy(servidor => servidor.finAtencion).FirstOrDefault();


			if (Gestor.servidorFin == null || proximaLllegadaFilaAnterior <= Gestor.servidorFin.finAtencion) {
				return this.evento = "Llegada Materiales";

			}

			if (proximoFinEncastreFilaAnteriror != null && proximoFinEncastreFilaAnteriror == Gestor.reloj)
			{
				return this.evento = "Fin Encastre";

            }

			this.evento = Gestor.servidorFin.nombre.Equals("Seccion1") ? "Fin Atencion Servidor 1" : Gestor.servidorFin.nombre.Equals("Seccion2") ? "Fin Atencion Servidor 2" :
									 Gestor.servidorFin.nombre.Equals("Seccion3") ? "Fin Atencion Servidor 3" : Gestor.servidorFin.nombre.Equals("Seccion4") ? "Fin Atencion Servidor 4" :
									 "Fin Atencion Servidor 5";

			return this.evento;

		}

		public decimal GenerarLlegadaCliente()                    // pedido entre 0.1 y 30 por hora
		{
			this.tiempoEntreLlegadas = Gestor._ExponencialPedidos.Generar_x();

			if (this.tiempoEntreLlegadas > 600) // 0.1 pedidos por hora = 0.1 pedidos en 60 minutos = 1 pedido en 600 minutos.
			{
				this.tiempoEntreLlegadas = 600;
			} else {
				if (this.tiempoEntreLlegadas < 2) //30 pedidos por hora = 30 pedidos en 60 minutos = 1 pedido en 2 minutos
				{
					this.tiempoEntreLlegadas = 2;
				}
			}
			return this.proximaLlegada = this.tiempoEntreLlegadas + this.reloj;

		}





	}
}
