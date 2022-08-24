using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TP1.Models
{
    internal class Generador
    {
        private static List<decimal> numeros_aleatorios = new List<decimal>();
        private static decimal xn = 0;
        private static decimal xn_1;  // este es solo para el metodo aditivo

        public static List<decimal> generar(string metodo, decimal xi,decimal c, decimal a, decimal modulo, int muestra, decimal semilla2)
          
        {
            xn_1 = semilla2; //  segunda semilla para el metodo aditivo
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
                    case "Aditivo":
                        (xi, xn_1) = (generadorCongruenteAditivo(xn_1, xi, modulo), xi); // para que quede guardado el xni anterior para seguir usando solo en el metodo aditivo 
                        break;
                }

                decimal rnd = (xi / modulo);

                numeros_aleatorios.Add(rnd); // agrega los random
            }
            xn = xi; // para que quede guardado el xi  para seguir usando en la funcion generarSiguientes

            return numeros_aleatorios;
        }

        public static List<decimal> generarSiguientes(string metodo, decimal c, decimal a, decimal modulo, int muestra)

        {
            for (int i = 0; i < muestra; i++)
            {
                switch (metodo)
                {
                    case "Multiplicativo":
                        xn = generadorCongruenteMultiplicativo(xn, a, modulo);
                        break;
                    case "Mixto":
                        xn = generadorCongruenteMixto(xn, a, c, modulo);
                        break;
                    case "Aditivo":
                        (xn, xn_1) = (generadorCongruenteAditivo(xn_1, xn, modulo), xn); // lo segundo que se devuelve es el xn para que se guarde como xn_1 para seguir usando solo en el metodo aditivo 
                        break;
                }

                decimal rnd = (xn / modulo);

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

        private static decimal generadorCongruenteAditivo(decimal xi_1, decimal xi_0, decimal m)
        {
            return (xi_1 + xi_0) % m;
        }

        public static List<decimal> generarRandomcSharp(int muestra)
        {
            double[] samples = SystemRandomSource.Doubles(muestra, new Random().Next(1, 100));  // genera numeros random [0; 1), primer argumento es la cantidad a generar, el segundo arg es una semilla aleatoria
            return samples.Select(a => (decimal)a).ToList();
        }
    }
}
