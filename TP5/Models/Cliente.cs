using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP5.Mvvm;
namespace TP5.Models
{
    internal class Cliente
    {
        public string estado { get; set; }

        public decimal horaLlegada { get; set; }
        public decimal tiempoEspera { get; set; }
        public decimal horaFinAtencion { get; set; }

        public decimal horaEmpiezoAtencion { get; set; }

        public decimal tiempoSistema { get; set; }
        public decimal tiempoEsperaAcumulado { get; set; } = 0;

       


        public Cliente(decimal reloj)
        {
            Gestor.cantProductosEnSistema++;
            horaLlegada = reloj;
            tiempoEspera = 0;
            horaFinAtencion = 0;
            tiempoSistema = 0;
        }

    }
}
