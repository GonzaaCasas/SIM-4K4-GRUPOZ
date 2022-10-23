using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP5.Mvvm;

namespace TP5.Models
{
    internal class Servidor
    {
        public string nombre { get; set; }
        public string estado { get; set; }

        public Nullable<double> finAtencion { get; set; }

        public Queue<Cliente> cola { get; set; }

        private IDistribucion distribucion { get; set; }

        private Cliente clienteActual { get; set; }
        private Cliente clienteAnterior { get; set; }

        public Servidor(IDistribucion dist, string _nombre)
        {
            estado = "libre";
            cola = new Queue<Cliente>();
            distribucion = dist;
            finAtencion = null;
            nombre = _nombre;

        }


        public void NuevoCliente(Cliente material)
        {

            if (estado == "libre")
            {
                estado = "ocupado";

                AtenderCliente(material);
            }
            else
            {
                cola.Enqueue(material);
            }
        }

        public Cliente TerminarAtencion()
        {
            clienteAnterior = clienteActual;
            //finAtencion = null;
            //Console.WriteLine(cola.Count);
            if (cola.Count >= 1)
            {
                AtenderCliente(cola.Dequeue());
            }
            else
            {
                estado = "libre";
                finAtencion = null;
            }
            return clienteAnterior;
        }

        private void AtenderCliente(Cliente material)
        {
            clienteActual = material;
            this.finAtencion = GenerarFinAtencion() + Gestor.reloj;

            material.tiempoEspera = Gestor.reloj - material.horaLlegada;
            material.horaFinAtencion = this.finAtencion ?? 0;
            material.tiempoSistema = material.horaFinAtencion - material.horaLlegada;
        }

        private double GenerarFinAtencion()
        {
            return distribucion.Generar_x();
        }

        public void GenerarLlegadaCliente(Servidor servidor)
        {
            servidor.NuevoCliente(clienteAnterior);
        }

    }
}
