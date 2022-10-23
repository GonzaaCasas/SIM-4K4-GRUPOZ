using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5.Models {
	internal class DistribucionExponencial : IDistribucion {

		private double lambda;
		private RandomAux random = new RandomAux();

		public DistribucionExponencial(double _media, RandomAux rnd) {
			lambda = 1 / _media;
            this.random = rnd;
        }


		public double Generar_x() {
			return (-1 / lambda) * Math.Log(1 - random.generarRandom());
		}
	}
}
