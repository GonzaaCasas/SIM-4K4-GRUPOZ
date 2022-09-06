using System;
using System.Collections.Generic;
using System.Linq;

namespace TP3.Models
{
    internal class ChiCuadrado
    {


        private static List<decimal> acumuladoChi = new List<decimal>();
        private static List<decimal> chis = new List<decimal>();
        private static List<decimal> observados = new List<decimal>();
        private static List<decimal> esperadosUniforme = new List<decimal>();
        private static List<decimal> esperadosExponencial = new List<decimal>();
        private static List<decimal> esperadosPoisson = new List<decimal>();
        private static List<decimal> esperadosNormal = new List<decimal>();
        private static decimal chiCuadradoObtenido;
        private static List<decimal> valoresMediosIntervalos = new List<decimal>();





        public static List<decimal> testChiCuadrado(List<decimal> numeros_aleatorios, int muestra, int subintervalos)
        {
            
            chis.Clear();
            observados.Clear();
            esperadosUniforme.Clear();
            esperadosExponencial.Clear();
            esperadosPoisson.Clear();
            esperadosNormal.Clear();

            CalcularfreqAbsolutas(numeros_aleatorios, subintervalos, muestra);
            calcularEsperados(subintervalos, muestra);


            // calculo de chi para distribucion Uniforme
            acumuladoChi.Clear();
            for (int i = 0; i < observados.Count(); i++)
            {
                double e_o = (double)(esperadosUniforme[i] - observados[i]);
                decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);  
                acumuladoChi.Add(e_oExpDos / esperadosUniforme[i]);
            }
            chiCuadradoObtenido = acumuladoChi.Sum();
            chis.Add(chiCuadradoObtenido);

            // calculo de chi para distribucion Exponencial
            acumuladoChi.Clear();
            for (int i = 0; i < observados.Count(); i++)
            {
                double e_o = (double)(esperadosUniforme[i] - observados[i]);
                decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);
                acumuladoChi.Add(e_oExpDos / esperadosUniforme[i]);
            }
            chiCuadradoObtenido = acumuladoChi.Sum();
            chis.Add(chiCuadradoObtenido);

            // calculo de chi para distribucion Poisson
            acumuladoChi.Clear();
            for (int i = 0; i < observados.Count(); i++)
            {
                double e_o = (double)(esperadosUniforme[i] - observados[i]);
                decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);
                acumuladoChi.Add(e_oExpDos / esperadosUniforme[i]);
            }
            chiCuadradoObtenido = acumuladoChi.Sum();
            chis.Add(chiCuadradoObtenido);

            // calculo de chi para distribucion Normal
            acumuladoChi.Clear();
            for (int i = 0; i < observados.Count(); i++)
            {
                double e_o = (double)(esperadosUniforme[i] - observados[i]);
                decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);
                acumuladoChi.Add(e_oExpDos / esperadosUniforme[i]);
            }

            chiCuadradoObtenido = acumuladoChi.Sum();
            chis.Add(chiCuadradoObtenido);

            // calculo de chi Tabulado

            double gradosLibertad = subintervalos - 1;
            double alfa = 0.05; // nivel de significación 95 %

            decimal chiSquareTeorico = (decimal)MathNet.Numerics.Distributions.ChiSquared.InvCDF(gradosLibertad, 1 - alfa);
            chis.Add(chiSquareTeorico); 


            return chis; // indice cero corresponde al chi uniforme y el ultimo indice al chi tabulado

        }

        public static void calcularEsperados(int subintervalos, int muestra)
        {
            esperadosUniforme = Distribucion.obtenerEsperadosUniforme(muestra, subintervalos);
            esperadosExponencial = Distribucion.obtenerEsperadosPoisson(muestra, subintervalos);
            esperadosPoisson = Distribucion.obtenerEsperadosPoisson(muestra, subintervalos);
            esperadosNormal = Distribucion.obtenerEsperadosNormal(muestra, subintervalos);

        }




        public static void CalcularfreqAbsolutas(List<decimal> numeros_aleatorios, int subintervalos, int muestra)
        {
            decimal[] arr = new decimal[subintervalos];
            List<decimal> frequencias = new List<decimal>(arr); 


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
                        frequencias[i] = (frequencias[i] * simAnterior + 1) / simActual; // observados
                    }
                    else
                    {
                        frequencias[i] = (frequencias[i] * simAnterior + 0) / simActual;
                    }

                    lim_inferior = lim_superior;
                    lim_superior = lim_inferior + paso;


                }

                simAnterior = simActual;
                simActual++;
                lim_inferior = min;
                lim_superior = lim_inferior + paso;

            }


            observados = frequencias.ConvertAll(obs => (Math.Round(obs * muestra, 4, MidpointRounding.AwayFromZero))); ; //cada indice de la lista corresponde a la freq relativa de un intervalo, al multiplicarla por la muestra tenemos la absoluta

        }

        public static List<decimal> obtenerFreqAbsolutas()
        {
            return observados;
        }
        public static List<decimal> obtenerEsperadoUniforme()
        {
            return esperadosUniforme;
        }
        public static List<decimal> obtenerEsperadoExponencial()
        {
            return esperadosExponencial;
        }
        public static List<decimal> obtenerEsperadoPoisson()
        {
            return esperadosPoisson;
        }
        public static List<decimal> obtenerEsperadoNormal()
        {
            return esperadosNormal;
        }

        public static List<decimal> obtenerMedios(List<decimal> conjuntoNumeros, int intervalos)
        {
            // 0 - 1 , 1 - 5, 5, 9
            valoresMediosIntervalos.Clear();
            List<decimal> limites = calcularLimites(conjuntoNumeros, intervalos);

            for (int i = 0; i < limites.Count() - 1; i++)
            {
                valoresMediosIntervalos.Add((limites[i] + limites[i + 1]) / 2); // (limite inferior + limite superior) / 2
            }

            return valoresMediosIntervalos;
        }


        public static List<decimal> calcularLimites(List<decimal> conjuntoNumeros, int subintervalos)
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

                // List<decimal> limites = new List<decimal>();


                array.Add(lim_superior);


                lim_superior = lim_superior + paso;

            }

            array.Add(lim_superior);


            return array;

        }

    }

}
