using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5.Models
{
    internal class Cliente
    {
        public string estado { get; set; }

        public double horaLlegada { get; set; }
        public double tiempoEspera { get; set; }
        public double horaFinAtencion { get; set; }
        public double tiempoSistema { get; set; }
        public double tiempoEsperaAcumulado { get; set; } = 0;


        public Cliente(double reloj)
        { 
            horaLlegada = reloj;
            tiempoEspera = 0;
            horaFinAtencion = 0;
            tiempoSistema = 0;

        }

    }
}
