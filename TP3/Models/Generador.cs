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

        public static (List<decimal> , List<decimal> , List<decimal> ) generarVariables(decimal media, decimal desviacion, decimal lambda, decimal lambdaExp)

        {
           
            valores_variableAleatoriaExpNeg.Clear();
            valores_variableAleatoriaPoisson.Clear();
            valores_variableAleatoriaNormal.Clear();

            // a la muestra le hago + 1 pq la distribucion normal hace uso de un random mas que el resto, pero el resto de las distribuciones no usan ese random extra

             Distribucion.GenerarVariableExpNegativa(lambdaExp, numeros_aleatorios);
             Distribucion.GenerarVariablePoisson(lambda, numeros_aleatorios);
            Distribucion.GenerarVariableNormal(media, desviacion, numeros_aleatorios);

            return (valores_variableAleatoriaExpNeg, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal);

        }

        public static void generarRandomcSharp(int muestra)
        {
            double[] samples = SystemRandomSource.Doubles(muestra, new Random().Next(1, 100));  // genera numeros random [0; 1), primer argumento es la cantidad a generar, el segundo arg es una semilla aleatoria
            numeros_aleatorios = samples.Select(a => (decimal)a).ToList();
        }
    }
}
