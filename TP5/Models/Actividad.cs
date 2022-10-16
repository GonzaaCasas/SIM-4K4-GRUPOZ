using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5.Models {
	internal class Actividad {

		public double d { get; set; } // duracion de la actividad 
		public double mi { get; set; }// momento mas temprano de inicio de la actividad 
		public double mf { get; set; } // momento mas temprano de finalizaciön de la actividad
		public double mi_tarde { get; set; } // momento mås tarde de inicio de la actividad 
		public double mf_tarde { get; set; }   // momento mas tarde de finalizacion de la actividad

		public Actividad(double _d) {
			this.d = _d;
		}


	}
}
