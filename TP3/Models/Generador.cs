using MathNet.Numerics.Random;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TP3.Models
{
    internal class Generador
    {
        // variables para generar randoms
        private static List<decimal> numeros_aleatorios = new List<decimal>();  

        private static decimal xn = 0;
        private static decimal xn_1;  // este es solo para el metodo aditivo

        // variables para generar valores de variable aleatorias
        private static List<decimal> valores_variableAleatoriaExpNeg = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaPoisson = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaNormal = new List<decimal>();

        // -------------------------------------

        public static (List<decimal> , List<decimal> , List<decimal> ) generarVariables(decimal media, decimal desviacion, decimal lambda, int muestra)

        {
           
            valores_variableAleatoriaExpNeg.Clear();
            valores_variableAleatoriaPoisson.Clear();
            valores_variableAleatoriaNormal.Clear();

           /* numeros_aleatorios = generarRandoms("Mixto", 37, 7, 19, 53, muestra + 1, 38)*/;  // por defecto al conjunto de randoms lo generamos con el metodo Mixto  pq no aclara el enunciado con cual

            // En vez de hacerlo con mixto lo podemos cambiar por randoms generados por el lenguaje si pinta 

            // a la muestra le hago + 1 pq la distribucion normal hace uso de un random mas que el resto, pero el resto de las distribuciones no usan ese random extra

            // validar antes que lambda sea positva
             Distribucion.GenerarVariableExpNegativa(lambda, numeros_aleatorios);
             Distribucion.GenerarVariablePoisson(lambda, numeros_aleatorios);
            Distribucion.GenerarVariableNormal(media, desviacion, numeros_aleatorios);

            return (valores_variableAleatoriaExpNeg, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal);

        }



        public static void generarRandoms(string metodo, decimal xi,decimal c, decimal a, decimal modulo, int muestra, decimal semilla2)
          
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
