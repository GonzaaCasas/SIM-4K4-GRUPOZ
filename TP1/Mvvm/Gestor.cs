using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1.Models;

namespace TP1.Mvvm
{
    internal class Gestor
    {
        private ArrayList numeros_aleatorios = new ArrayList();
        private decimal xi_1;

        decimal esperado;
        decimal lim_inferior;
        decimal lim_superior;
        decimal paso;
        private List<decimal> e_oExpDosDividoEsperado = new List<decimal>(); // desp le ppongo otro nombre a la lista no sabia q poner 

        public static List<decimal> generar(string metodo, decimal xi_1, decimal c, decimal a, decimal modulo, int muestra)
        {
            return Generador.generar(metodo, xi_1, c, a, modulo, muestra);
        }

        public void testChiCuadrado(List<decimal>  numeros_aleatorios,int muestra, int subintervalos)
        {

            decimal min = numeros_aleatorios.Min();
            decimal max = numeros_aleatorios.Max();

            esperado = muestra / subintervalos;
            paso = (max - min) / subintervalos;

            this.lim_inferior = min;
            this.lim_superior = lim_inferior + paso;

            int[] observados = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // cada indice corresponde a un intervalo de los 10

            for (int i = 0; i < subintervalos - 1; i++)
            {
                foreach (var random in numeros_aleatorios)
                {
                    if (random >= lim_inferior && random < lim_superior)
                    {
                        observados[i]++;
                    }
                }

                this.lim_inferior = this.lim_superior;
                this.lim_superior = this.lim_superior + paso;

            }

            for (int i = 0; i < observados.Count(); i++)
            {
                double e_o = (double)(this.esperado - observados[i]);
                decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);  // lo puse en  varios paso para q se entienda al leerlo 
                this.e_oExpDosDividoEsperado.Add(e_oExpDos / this.esperado);
            }

            decimal chiCuadrado = e_oExpDosDividoEsperado.Sum();

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
