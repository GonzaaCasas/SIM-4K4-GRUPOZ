using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Models
{
    internal class ChiCuadrado
    {
       

        private static List<decimal> acumuladoChi = new List<decimal>();
        public static List<decimal> testChiCuadrado(List<decimal> numeros_aleatorios, int muestra, int subintervalos)
        {
            acumuladoChi.Clear();


            List<decimal> observados = new List<decimal>();

            decimal esperado = muestra / subintervalos;



            observados = freqRelativas(numeros_aleatorios, subintervalos, muestra);
            observados = observados.ConvertAll(obs => (obs * muestra));  /// para tener las freq absolutas
            observados = observados.ConvertAll(obs => (Math.Round(obs, 4, MidpointRounding.AwayFromZero)));




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

            return observados;

        }




        public  static List<decimal> freqRelativas(List<decimal> numeros_aleatorios, int subintervalos, int muestra)
        {
            decimal[] arr = new decimal[subintervalos];
            List<decimal> array = new List<decimal>(arr); //cada indice de la lista corresponde a la freq relativa de un intervalo


            decimal min = numeros_aleatorios.Min();
            decimal max = numeros_aleatorios.Max();

          
            decimal paso = (max - min) / subintervalos;

            decimal lim_inferior = min;
            decimal lim_superior = lim_inferior + paso;

            int simActual = 1;
            int simAnterior = 0;

           

            foreach (var random in numeros_aleatorios)
            {
                for (int i = 0; i < subintervalos; i++)
                {
                    if (random >= lim_inferior && random <= lim_superior)
                    {
                        array[i] = (array[i] * simAnterior + 1) / simActual; // observados
                    }
                    else
                    {
                        array[i] = (array[i] * simAnterior + 0) / simActual;
                    }

                    lim_inferior = lim_superior;
                    lim_superior = lim_inferior + paso;

                    
                }

                 simAnterior = simActual;
                 simActual++;
                 lim_inferior = min;
                 lim_superior = lim_inferior + paso;
                
            }

            return array;

        }

        public static List<decimal> limites(List<decimal> conjuntoNumeros, int subintervalos)
        {
            decimal min = conjuntoNumeros.Min();
            decimal max = conjuntoNumeros.Max();
            decimal paso = (max - min) / subintervalos;

            decimal lim_inferior = min;
            decimal lim_superior = lim_inferior + paso;


            List<decimal> array = new List<decimal>();

            array.Add(lim_inferior);

            for (int i = 0; i < subintervalos - 1; i++)
            {

                List<decimal> limites = new List<decimal>();


                array.Add(lim_superior);


                lim_superior = lim_superior + paso;

            }

            array.Add(lim_superior);


            return array;

        }

    }

}
