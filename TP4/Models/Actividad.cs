using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Models
{
    internal class Actividad
    {
  
        public decimal d { get; set; } // duracion de la actividad 
        public decimal mi { get; set; }// momento mas temprano de inicio de la actividad 
        public decimal mf { get; set; } // momento mas temprano de finalizaciön de la actividad
        public decimal mi_tarde { get; set; } // momento mås tarde de inicio de la actividad 
        public decimal mf_tarde { get; set; }   // momento mas tarde de finalizacion de la actividad

        public Actividad(decimal _d)
        {
           this.d = _d;
        }


    }
}
