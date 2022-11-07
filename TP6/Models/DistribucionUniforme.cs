using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP6.Models {
	internal class DistribucionUniforme : IDistribucion {
		private double a;
		private double b;


		private RandomAux random;

		public DistribucionUniforme(double _a, double _b, RandomAux rnd) {
			this.a = _a;
			this.b = _b;
			this.random = rnd;
		}

		public decimal Generar_x() {
			return (decimal)Math.Round((this.a + random.generarRandom() * (this.b - this.a)), 2, MidpointRounding.AwayFromZero);
		}

	}
}
