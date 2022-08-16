using MathNet.Numerics.Random;
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

        public static List<decimal> generar(string metodo, decimal xi_1, decimal c, decimal a, decimal modulo, int muestra)
        {
            numeros_aleatorios.Add(Math.Round((xi_1 / modulo), 4, MidpointRounding.AwayFromZero));

            for (int i = 0; i < muestra; i++)
            {
                switch (metodo)
                {
                    case "Multiplicativo":
                        xi = generadorCongruenteMultiplicativo(xi_1, a, modulo);
                        break;
                    case "Mixto":
                        xi = generadorCongruenteMixto(xi_1, a, c, modulo);
                        break;
                }

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

//        private static decimal generadorCongruenteAditivo(decimal xi_1, decimal xi_0, decimal m)
//        {
//            // FIX THIS NIGGER
//            return (xi_1 + xi_0) % m;
//        }

        public static List<decimal> generarRandomcSharp(int muestra)
        {
            //new Random().Next(1, 100)
            double[] samples = SystemRandomSource.Doubles(muestra, 37);  // genera numeros random [0; 1), primer argumento es la cantidad a generar, el segundo arg es una semilla aleatoria
            return samples.Select(a => (decimal)a).ToList();
        }

    }
}
