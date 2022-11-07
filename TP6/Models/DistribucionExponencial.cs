using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP6.Models {
	internal class DistribucionExponencial : IDistribucion {

		private double lambda;
		private RandomAux random;

		public DistribucionExponencial(double _media, RandomAux rnd) {
			lambda = 1 / _media;
			this.random = rnd;
		}


		public decimal Generar_x() {
			return (decimal)Math.Round((-1 / lambda) * Math.Log(1 - random.generarRandom()), 2, MidpointRounding.AwayFromZero);
		}
	}
}
