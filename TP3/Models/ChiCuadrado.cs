using DocumentFormat.OpenXml.Wordprocessing;
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
        private static List<decimal> esperados = new List<decimal>();


        private static decimal chiCuadradoObtenido;
        private static List<string> valoresMediosIntervalos = new List<string>();

        private static List<decimal> valores_variableAleatoriaExpNeg = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaPoisson = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaNormal = new List<decimal>();



        // Para obtener chiChuadrado de cualquier distribucion

        public static decimal testChiCuadrado(List<decimal> conjuntoAleatorio, List<decimal> observados, List<decimal> esperados ,int muestra, int subintervalos)
        {

            // calculo de chi 
            acumuladoChi.Clear();

            for (int i = 0; i < observados.Count(); i++)
            {
                double e_o = (double)(esperados[i] - observados[i]);
                decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);
                acumuladoChi.Add(e_oExpDos / esperados[i]);
            }
            chiCuadradoObtenido = acumuladoChi.Sum();



            return chiCuadradoObtenido;

        }

        // Para obtener el chi cuadrado tabulado

        public static decimal obtenerChitabulado(int subintervalos)
        {

            // calculo de chi Tabulado

            double gradosLibertad = subintervalos - 1;
            double alfa = 0.05; // nivel de significación 95 %

            decimal chiSquareTeorico = (decimal)MathNet.Numerics.Distributions.ChiSquared.InvCDF(gradosLibertad, 1 - alfa);

            return chiSquareTeorico;

        }
 

        public  List<decimal> obtenerFreqAbsolutas()
        {
            return observados;
        }

        public static List<string> obtenerMedios(List<decimal> conjuntoNumeros, int intervalos)
        {
            // 0 - 1 , 1 - 5, 5, 9
            List<decimal> valoresMediosIntervalos = new List<decimal>();

             List<decimal> limites = calcularLimites(conjuntoNumeros, intervalos);

            for (int i = 0; i < limites.Count() - 1; i++)
            {
                valoresMediosIntervalos.Add(((limites[i] + limites[i + 1]) / 2).ToString()); // (limite inferior + limite superior) / 2
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
