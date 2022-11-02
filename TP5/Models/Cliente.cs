using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TP5.Mvvm;
namespace TP5.Models
{
    internal class Cliente
    {

        static int nextId;
        public Nullable<int> ClienteId { get; private set; }
      

        public string estado { get; set; }

        public decimal horaLlegada { get; set; }
        public Nullable<decimal> tiempoEspera { get; set; }
        public decimal horaFinAtencion { get; set; }

        public decimal horaInicioAtencion { get; set; }

        public decimal tiempoSistema { get; set; }
        public decimal tiempoEsperaAcumulado { get; set; } = 0;

        public decimal horaLlegadaDesdeS4 { get; set; }
        public decimal horaLlegadaDesdeS2 { get; set; }



        public Cliente(decimal reloj)
        {
            Gestor.cantProductosEnSistema++;
            horaLlegada = reloj;
            tiempoEspera = 0;
            horaFinAtencion = 0;
            tiempoSistema = 0;
            ClienteId = Interlocked.Increment(ref nextId);

        }

    }
}
