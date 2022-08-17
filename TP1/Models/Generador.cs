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
        private static decimal xi_1 = 0;

        // public static List<decimal> generar(string metodo, decimal xi, decimal c, decimal a, decimal modulo, int muestra)
        public static List<decimal> generar(string metodo, decimal xi,decimal c, decimal a, decimal modulo, int muestra)
          
        {
            numeros_aleatorios.Clear();
          
            for (int i = 0; i < muestra; i++)
            {
                switch (metodo)
                {
                    case "Multiplicativo":
                        xi = generadorCongruenteMultiplicativo(xi, a, modulo);
                        break;
                    case "Mixto":
                        xi = generadorCongruenteMixto(xi, a, c, modulo);
                        break;
                }

                // decimal rnd = Math.Round((xi / modulo), 4, MidpointRounding.AwayFromZero);
                decimal rnd = (xi / modulo);

                numeros_aleatorios.Add(rnd); // agrega los random
            }
            xi_1 = xi;

            return numeros_aleatorios;
        }

        public static List<decimal> generarSiguientes(string metodo, decimal c, decimal a, decimal modulo, int muestra)

        {
            for (int i = 0; i < muestra; i++)
            {
                switch (metodo)
                {
                    case "Multiplicativo":
                        xi_1 = generadorCongruenteMultiplicativo(xi_1, a, modulo);
                        break;
                    case "Mixto":
                        xi_1 = generadorCongruenteMixto(xi_1, a, c, modulo);
                        break;
                }

                // decimal rnd = Math.Round((xi / modulo), 4, MidpointRounding.AwayFromZero);
                decimal rnd = (xi_1 / modulo);

                numeros_aleatorios.Add(rnd); // agrega los random
            }

            return numeros_aleatorios;
        }


        private static decimal generadorCongruenteMixto(decimal xi, decimal a, decimal c, decimal m)
        {
            return (a * xi + c) % m;
		    }

        private static decimal generadorCongruenteMultiplicativo(decimal xi, decimal a, decimal m)
        {
            return (a * xi) % m;
        }

//        private static decimal generadorCongruenteAditivo(decimal xi_1, decimal xi_0, decimal m)
//        {
//            return (xi_1 + xi_0) % m;
//        }

        public static List<decimal> generarRandomcSharp(int muestra)
        {
            double[] samples = SystemRandomSource.Doubles(muestra, new Random().Next(1, 100));  // genera numeros random [0; 1), primer argumento es la cantidad a generar, el segundo arg es una semilla aleatoria
            return samples.Select(a => (decimal)a).ToList();
        }
    }
}
