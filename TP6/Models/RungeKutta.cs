using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP6.Models {
	internal class RungeKutta {
		public Tuple<double, double> calcular(double x0, double y0, RandomAux rnd) {

			double a = (double)new DistribucionUniforme(0.5, 2, rnd).Generar_x();
			double b = 10;
			double c = 5;
			double t = 0;
			double h = 0.05;

			double x = x0;
			double y = y0;
			double yd = derivada(a, b, c, y, x, t);
			double l1 = h * yd;
			double k1 = h * y;
			double l2 = h * derivada(a, b, c, (x + 0.5 * k1), (y + 0.5 * l1), (t + 0.5 * h));
			double k2 = h * (y + 0.5 * l1);
			double l3 = h * derivada(a, b, c, (x + 0.5 * k2), (y + 0.5 * l2), (t + 0.5 * h));
			double k3 = h * (y + 0.5 * l2);
			double l4 = h * derivada(a, b, c, (x + k3), (y + l3), (t + h));
			double k4 = h * (y + l3);

			int picos = 0;
			double anterior = 0;
			double pico = 0;

			while (picos < 2) {
				t = t + h;
				x = x + (k1 + 2 * k2 + 2 * k3 + k4) / 6;
				y = y + (l1 + 2 * l2 + 2 * l3 + l4) / 6;
				yd = derivada(a, b, c, y, x, t);
				l1 = h * yd;
				k1 = h * y;
				l2 = h * derivada(a, b, c, (y + 0.5 * l1), (x + 0.5 * k1), (t + 0.5 * h));
				k2 = h * (y + 0.5 * l1);
				l3 = h * derivada(a, b, c, (y + 0.5 * l2), (x + 0.5 * k2), (t + 0.5 * h));
				k3 = h * (y + 0.5 * l2);
				l4 = h * derivada(a, b, c, (y + l3), (x + k3), (t + h));
				k4 = h * (y + l3);

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
