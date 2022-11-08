﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP6.Models {
	internal class Euler {
		public Tuple<double, double> calcular(double x0, double y0) {

		  double a = (double)new DistribucionUniforme(0.5, 2, new RandomAux()).Generar_x();
			double b = 10;
			double c = 5;
			double t = 0;
			double h = 0.05;

			double x = x0;
			double y = y0;
			double yd = this.derivada(a, b, c, y, x, t);

			int picos = 0;
			double anterior = 0;
			double pico = 0;

			while (picos < 2) {
				t = t + h;
				x = x + y * h;
				y = y + yd * h;
				yd = this.derivada(a, b, c, y, x, t);

				if (pico > x && pico > anterior) {
					picos++;
				}
				anterior = pico;
				pico = x;
			}

			return new Tuple<double, double>(t, pico);
		}

		private double derivada(double a, double b, double c, double y, double x, double t) {
			return Math.Exp(-c * t) - (a * y) - b * x;
		}
	}
}
