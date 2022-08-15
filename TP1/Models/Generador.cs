using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Models
{
    internal class Generador
    {
        private static List<decimal> numeros_aleatorios = new List<decimal>();
        private static decimal xi;

        public static List<decimal> generar(string metodo, decimal semilla, decimal c, decimal a, decimal modulo, int muestra)
        {
            decimal xi_1 = semilla;

            for (int i = 0; i < muestra; i++)
            {
                xi = generadorCongruenteMultiplicativo(xi_1, a, modulo);

                numeros_aleatorios.Add(Math.Round((xi / modulo), 4, MidpointRounding.AwayFromZero)); // agrega los random

                xi_1 = xi;
            }

            return numeros_aleatorios;
        }

        private static decimal generadorCongruenteMixto(decimal xi_1, decimal a, decimal c, decimal m)
        {
            return (a * xi_1 + c) % m;
		    }

        private static decimal generadorCongruenteMultiplicativo(decimal xi_1, decimal a, decimal m)
        {
            return (a * xi_1) % m;
        }

        private static decimal generadorCongruenteAditivo(decimal xi_1, decimal xi_0, decimal m)
        {
            return (xi_1 + xi_0) % m;
        }
    }
}
