using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP6.Mvvm;

namespace TP6.Models {
	internal class Servidor {
		public string nombre { get; set; }
		public string estado { get; set; }

		public Nullable<decimal> finAtencion { get; set; }
		public Nullable<decimal> tiempoAtencion { get; set; }


		public Queue<Cliente> cola { get; set; }

		public int maximoCola { get; set; } = 0;

		private IDistribucion distribucion { get; set; }

		public Cliente clienteActual { get; set; }
		private Cliente clienteAnterior { get; set; }

		public int cantClientesPasaronPorCola { get; set; }

		public Nullable<int> materialEnLlegada { get; set; }


		public Servidor(IDistribucion dist, string _nombre) {
			estado = "libre";
			cola = new Queue<Cliente>();
			distribucion = dist;
			finAtencion = null;
			nombre = _nombre;
			cantClientesPasaronPorCola = 0;

		}


		public void NuevoCliente(Cliente material) {


			if (estado == "libre") {
				estado = "ocupado";

				AtenderCliente(material);
			} else {
				cola.Enqueue(material);
				cantClientesPasaronPorCola++;

			}

			if (cola.Count > maximoCola) //obtiene el maximo de una cola
			{
				maximoCola = cola.Count;
			}

			this.materialEnLlegada = material.ClienteId; // Para saber que materiales llegaron en el evento Llegada de Materiales
		}

		public Cliente TerminarAtencion() {
			clienteAnterior = clienteActual;
			//finAtencion = null;
			//Console.WriteLine(cola.Count);
			if (cola.Count >= 1) {
				AtenderCliente(cola.Dequeue());

			} else {
				estado = "libre";
				this.finAtencion = null;
				this.tiempoAtencion = null;
			}
			return clienteAnterior;
		}

		private void AtenderCliente(Cliente material) {
			clienteActual = material;
			this.tiempoAtencion = GenerarFinAtencion();
			this.finAtencion = this.tiempoAtencion + Gestor.reloj;

			//finAtencion = null;

			//this.horaInicioAtencion = Gestor.reloj;
			//this.tiempoEspera = (Gestor.reloj - material.horaLlegada);

			//this.tiempoSistema = (material.horaFinAtencion - material.horaLlegada);
			//this.tiempoEsperaAcumulado += material.tiempoEspera;

			material.horaFinAtencion = (decimal)this.tiempoAtencion + Gestor.reloj;
			material.horaInicioAtencion = Gestor.reloj;
			// material.tiempoEspera = (Gestor.reloj - material.horaLlegada);
			material.horaFinAtencion = this.finAtencion ?? 0;
			material.tiempoSistema = (material.horaFinAtencion - material.horaLlegada);
			material.tiempoEsperaAcumulado += (decimal)material.tiempoEspera;
		}

		private decimal GenerarFinAtencion() {
			return distribucion.Generar_x();
		}

		public void GenerarLlegadaCliente(Servidor servidor) {
			// Si pasa de s1 a s4 por ejemplo, la "hora de llegada" cambia, esto es para despues calcular bien los tiempos de esperars entre los distintos servidores
			clienteAnterior.horaLlegada = clienteAnterior.horaFinAtencion; // hora fin atencion de s1 o s2 es la hora de llegada si pasa a s4 o s5 por ejemplo
			servidor.NuevoCliente(clienteAnterior);
		}

	}
}
