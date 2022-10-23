using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP5.Models {
	internal class DistribucionExponencial : IDistribucion {

		private double lambda;
		static Random random = new Random();

		public DistribucionExponencial(double _media) {
			lambda = 1 / _media;
		}


		public double Generar_x() {
			return (-1 / lambda) * Math.Log(1 - random.NextDouble());
		}
	}
}
