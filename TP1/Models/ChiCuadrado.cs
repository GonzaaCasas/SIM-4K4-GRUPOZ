using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Models
{
    internal class ChiCuadrado
    {
        static decimal esperado;
        static decimal lim_inferior;
        static decimal lim_superior;
        static decimal paso;

        private static List<decimal> acumuladoChi = new List<decimal>(); 
        public static void testChiCuadrado(List<decimal> numeros_aleatorios, int muestra, int subintervalos)
        {
            acumuladoChi.Clear();

            decimal min = numeros_aleatorios.Min();
            decimal max = numeros_aleatorios.Max();

            esperado = muestra / subintervalos;
            paso = (max - min) / subintervalos;

            lim_inferior = min;
            lim_superior = lim_inferior + paso;

            int[] arr = new int[subintervalos];
            List<int> observados = new List<int>(arr);   // para tener un vector de frecuencias observadas de largo n intervalos


            for (int i = 0; i < subintervalos - 1; i++)
            {
                foreach (var random in numeros_aleatorios)
                {
                    if (random >= lim_inferior && random <= lim_superior)
                    {
                        observados[i]++;
                    }
                }

                lim_inferior = lim_superior;
                lim_superior = lim_superior + paso;

            }

            for (int i = 0; i < observados.Count(); i++)
            {
                double e_o = (double)(esperado - observados[i]);
                decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);  // lo puse en  varios paso para q se entienda al leerlo 
                acumuladoChi.Add(e_oExpDos / esperado);
            }

            decimal chiCuadrado = acumuladoChi.Sum();

            double gradosLibertad = subintervalos - 1;
            double alfa = 0.05;

            decimal chiSquareTeorico = (decimal)MathNet.Numerics.Distributions.ChiSquared.InvCDF(gradosLibertad, 1 - alfa);

            if (chiCuadrado < chiSquareTeorico)
            {
                // tirar que esta todo ok
            }

        }

    }
}
