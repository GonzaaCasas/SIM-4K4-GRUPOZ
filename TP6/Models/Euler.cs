using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP6.Models {
	internal class Euler {
		public Tuple<double, double> calcular(double x0, double y0, RandomAux rnd) {

			double a = (double)new DistribucionUniforme(0.5, 2, rnd).Generar_x();
			double b = 10;
			double c = 5;
			double t = 0;
			double h = 0.05;

			double x = x0;
			double y = y0;
			double yd = derivada(a, b, c, y, x, t);

			int picos = 0;
			double anterior = 0;
			double pico = 0;

			while (picos < 2) {
				t = t + h;
				x = x + y * h;
				y = y + yd * h;
				yd = derivada(a, b, c, y, x, t);

				if (pico > x && pico > anterior) {
					picos++;
				}
				anterior = pico;
				pico = x;

			}

			return new Tuple<double, double>(t, pico);
		}

		private static double derivada(double a, double b, double c, double y, double x, double t) {
			return Math.Exp(-c * t) - (a * y) - b * x;
		}
	}
}
