using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5.Models {
	internal class DistribucionUniforme : IDistribucion {
		private double a;
		private double b;

		static Random random = new Random();

		public DistribucionUniforme(double _a, double _b) {
			this.a = _a;
			this.b = _b;
		}

		public double Generar_x() {
			return this.a + random.NextDouble() * (this.b - this.a);
		}

	}
}
