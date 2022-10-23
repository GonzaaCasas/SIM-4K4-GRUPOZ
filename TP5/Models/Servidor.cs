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
        public string estado { get; set; }

        public Nullable<double> finAtencion { get; set; }

        public Queue<Cliente> cola { get; set; }

        private IDistribucion distribucion { get; set; }

        public Servidor(IDistribucion dist)
        {
            estado = "libre";
            cola = new Queue<Cliente>();
            distribucion = dist;
            finAtencion = null;

        }


        public void NuevoCliente(Cliente material)
        {
            if (estado == "libre")
            {
                estado = "ocupado";
                this.finAtencion =  GenerarFinAtencion() + Gestor.reloj;
            }
            else
            {
                cola.Enqueue(material);
            }
        }

        public void TerminarAtencion()
        {
            //finAtencion = null;
            this.finAtencion = GenerarFinAtencion() + Gestor.reloj;

            if (cola.Count >= 1)
            {
                cola.Dequeue();
 
            }
            else
            {
                estado = "libre";

            }

        }

        private double GenerarFinAtencion()
        {
            return distribucion.Generar_x();
        }

        public void GenerarLlegadaCliente(Servidor servidor)
        {
            servidor.NuevoCliente(new Cliente());
        }

    }
}
