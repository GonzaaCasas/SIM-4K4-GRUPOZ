using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP6.Models {
	internal class Euler {
		public static (double, double,double, double) calcular(double x0, double y0, RandomAux rnd) {

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

			return (t, pico, y , yd) ;
		}


		public static (List<double>, List<double>, List<double>, List<double>) ObtenerVector(double x0, double y0, RandomAux rnd)
		{

			List<double> valorest = new List<double>();
			List<double> valoresx = new List<double>();
			List<double> valoresy = new List<double>();
			List<double> valoresyd = new List<double>();

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

			while (picos < 2)
			{
				t = t + h;
				x = x + y * h;
				y = y + yd * h;
				yd = derivada(a, b, c, y, x, t);

				if (pico > x && pico > anterior)
				{
					picos++;
				}
				anterior = pico;
				pico = x;

				valorest.Add(t);
				valoresx.Add(x);
				valoresy.Add(y);
				valoresyd.Add(yd);

			}

			return (valorest, valoresx, valoresy, valoresyd);
		}


		private static double derivada(double a, double b, double c, double y, double x, double t) {
			return Math.Exp(-c * t) - (a * y) - b * x;
		}
	}
}
