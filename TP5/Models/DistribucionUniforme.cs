using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5.Models {
	internal class DistribucionUniforme : IDistribucion {
		private double a;
		private double b;


		private RandomAux random;

		public DistribucionUniforme(double _a, double _b, RandomAux rnd) {
			this.a = _a;
			this.b = _b;
			this.random = rnd;
		}

		public double Generar_x() {
			return this.a + random.generarRandom() * (this.b - this.a);
		}

	}
}
