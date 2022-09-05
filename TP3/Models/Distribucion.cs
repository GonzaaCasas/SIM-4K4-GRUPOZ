﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3.Models
{
    internal class Distribucion
    {
        //private static decimal lambda;
        //private static decimal media;
        //private static decimal desviacion;
        private static double z;
        private static List<double> p_acumulada = new List<double>();
        private static double fx;
        private static double Fx;
        private static List<decimal> VariableExp = new List<decimal>();
        private static List<decimal> VariablePoisson = new List<decimal>();
        private static List<decimal> VariableNormal = new List<decimal>();





        public static List<decimal> DistExpNegativa(decimal lambda, List<decimal> numeros_aleatorios)
        {
            VariableExp.Clear();

            // Metodo de la transformada Inversa
            for (int i = 0; i < numeros_aleatorios.Count() - 1; i++)
            {
                VariableExp.Add((-1 / lambda) * (decimal)Math.Log(1 - (double)numeros_aleatorios[i])); // (-1/lambda) * ln(1-random)
            }

            return VariableExp;
        }

        public static List<decimal> DistPoisson(decimal lambda, List<decimal> numeros_aleatorios)
        {
            VariablePoisson.Clear();
            p_acumulada.Clear();
            p_acumulada.Add(0); // aca va guardado las probabilidades acumuladas  

            // Método de la transformación Inversa
            for (int xi = 0; p_acumulada[xi] < 0.9999; xi++) // puse 0.9999 pq en un excel usaban hasta 4 decimales
            {
                fx = (double)(Math.Pow((double)lambda, xi) * Math.Pow(Math.E, -(double)lambda) / MathNet.Numerics.SpecialFunctions.Factorial(xi)); // [ lamba^x * e^(-lambda) ] / x!   funcion probabilidad de xi 
                Fx = fx + (double)p_acumulada[xi]; // funcion acumulada de Xi
                p_acumulada.Add(Math.Round((double)Fx, 4, MidpointRounding.AwayFromZero));  // truncar a 4 decimales asi la probabilidad acumulada ultima llega a  1
               ;
            }

            // aca va preguntando si el random se encuentra en algun intervalo, si se encuentra toma un valor discreto segun el intervalo que se encuentra empezando desde cero, uno ...

        

            for (int xi = 0; xi < numeros_aleatorios.Count() - 1; xi++)
            {
                for (int s = 1; s < p_acumulada.Count(); s++)
                {
                    if (numeros_aleatorios[xi] <= (decimal)p_acumulada[s])
                    {
                        VariablePoisson.Add(s-1);
                        break;
                    }
                }

               

            }

            return VariablePoisson;
        }

        public static List<decimal> DistNormal(decimal media, decimal desviacion, List<decimal> numeros_aleatorios)
        {
            VariableNormal.Clear();

            // Metodo Directo

            for (int i = 0; i < numeros_aleatorios.Count() - 1; i++)
            {
                z = Math.Sqrt(-2 * Math.Log(1 - (double)numeros_aleatorios[i])) * Math.Cos(2 * Math.PI * (double)numeros_aleatorios[i+1]);
                VariableNormal.Add(media + (decimal)z * desviacion);

            }

            return VariableNormal;
        }

    }
}
