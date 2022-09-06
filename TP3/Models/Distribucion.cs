using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3.Models
{
    internal class Distribucion
    {
        // variables para Uniforme
        private static List<decimal> esperadosUniforme = new List<decimal>();
        private static List<decimal> observadosUniforme = new List<decimal>();


        // Variables para Exponencial
        private static List<decimal> esperadosExponencial = new List<decimal>();
        private static List<decimal> observadosExponencial = new List<decimal>();


        private static List<decimal> observados = new List<decimal>();



        // variables para Poisson
        private static List<double> p_acumulada = new List<double>();
        private static double fx;
        private static double Fx;
        private static List<decimal> esperadosPoisson = new List<decimal>();
        private static List<decimal> observadosPoisson = new List<decimal>();




        // variables para Normal
        private static double z;
        private static List<decimal> esperadosNormal = new List<decimal>();
        private static List<decimal> observadosNormal = new List<decimal>();


        // ---------------------------
        private static List<decimal> VariableUniforme = new List<decimal>();
        private static List<decimal> VariableExp = new List<decimal>();
        private static List<decimal> VariablePoisson = new List<decimal>();
        private static List<decimal> VariableNormal = new List<decimal>();





        public static void GenerarVariableExpNegativa(decimal lambda, List<decimal> numeros_aleatorios)
        {
            VariableExp.Clear();

            // Metodo de la transformada Inversa
            for (int i = 0; i < numeros_aleatorios.Count() - 1; i++)
            {
                VariableExp.Add((-1 / lambda) * (decimal)Math.Log(1 - (double)numeros_aleatorios[i])); // (-1/lambda) * ln(1-random)
            }
            
        }

        public static List<decimal> obtenerVariableExpNegativa()
        {
            return VariableExp;
        }

        public static void GenerarVariablePoisson(decimal lambda, List<decimal> numeros_aleatorios)
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

            
        }

        public static List<decimal> obtenerVariablePoisson()
        {
            return VariablePoisson;
        }

        public static void GenerarVariableNormal(decimal media, decimal desviacion, List<decimal> numeros_aleatorios)
        {
            VariableNormal.Clear();

            // Metodo Directo

            for (int i = 0; i < numeros_aleatorios.Count() - 1; i++)
            {
                z = Math.Sqrt(-2 * Math.Log(1 - (double)numeros_aleatorios[i])) * Math.Cos(2 * Math.PI * (double)numeros_aleatorios[i+1]);
                VariableNormal.Add(media + (decimal)z * desviacion);

            }

          
        }

        public static List<decimal> obtenerVariableNormal()
        {
            return VariableNormal;
        }

        public static decimal obtenerChiUniforme(int muestra, int intervalos)
        {
            calcularEsperadosUniforme(muestra, intervalos);
            observadosUniforme = CalcularfreqAbsolutas(VariableUniforme, intervalos, muestra);
           return ChiCuadrado.testChiCuadrado(VariableUniforme, observadosUniforme ,esperadosUniforme, muestra, intervalos);

        }


        private static void calcularEsperadosUniforme(int muestra, int intervalos)
        {
            esperadosUniforme.Clear();
            for (int i = 0; i < intervalos; i++)
            {
                esperadosUniforme.Add(muestra / intervalos);
            }


        }

        public static List<decimal> obtenerObservacionesUniforme()
        {

            return observadosUniforme;

        }

        public static List<decimal> obtenerEsperadosUniforme()
        {


            return esperadosUniforme;

        }

        public static decimal obtenerChiExponencial(decimal lambdaExp, List<decimal> limites, int muestra, int intervalos)
        {
            calcularEsperadosExponencial(lambdaExp, limites);
            observadosExponencial = CalcularfreqAbsolutas(VariableExp, intervalos, muestra);
            return ChiCuadrado.testChiCuadrado(VariableExp, observadosExponencial, esperadosExponencial, muestra, intervalos);

        }



        public static void calcularEsperadosExponencial(decimal lambdaExp, List <decimal> limites)
        {
            esperadosExponencial.Clear();
            for (int i = 1; i < limites.Count() ; i++)
            {
               esperadosExponencial.Add( (decimal) (MathNet.Numerics.Distributions.Exponential.CDF( (double)lambdaExp, (double) limites[i]) -
                 MathNet.Numerics.Distributions.Exponential.CDF((double)lambdaExp, (double)limites[i-1])));
            }

        }

        public static List<decimal> obtenerObservacionesExponencial()
        {

            return observadosExponencial;

        }

        public static List<decimal> obtenerEsperadosExponencial()
        {


            return esperadosExponencial;

        }

        public static decimal obtenerChiPoisson(decimal lambda, List<decimal> limites, int muestra, int intervalos)
        {
            calcularEsperadosPoisson(lambda, limites);
            observadosPoisson = CalcularfreqAbsolutas(VariablePoisson, intervalos, muestra);
            return ChiCuadrado.testChiCuadrado(VariablePoisson, observadosPoisson, esperadosPoisson, muestra, intervalos);

        }

        public static void calcularEsperadosPoisson(decimal lambda, List<decimal> limites)
        {
            esperadosPoisson.Clear();
            for (int i = 1; i < limites.Count(); i++)
            {
                esperadosPoisson.Add((decimal)(MathNet.Numerics.Distributions.Poisson.CDF((double)lambda, (double)limites[i]) -
                  MathNet.Numerics.Distributions.Poisson.CDF((double)lambda, (double)limites[i - 1])));
            }


        }

        public static List<decimal> obtenerObservacionesPoisson()
        {

            return observadosPoisson;

        }

        public static List<decimal> obtenerEsperadosPoisson()
        {

            return esperadosPoisson;

        }

        public static decimal obtenerChiNormal(decimal media, decimal ds ,List<decimal> limites, int muestra, int intervalos)
        {
            calcularEsperadosNormal(media, ds, limites);
            observadosNormal = CalcularfreqAbsolutas(VariableNormal, intervalos, muestra);
            return ChiCuadrado.testChiCuadrado(VariableNormal, observadosNormal, esperadosNormal, muestra, intervalos);

        }

        public static void calcularEsperadosNormal(decimal media, decimal ds ,List<decimal> limites)
        {
            esperadosNormal.Clear();
            for (int i = 1; i < limites.Count(); i++)
            {
                esperadosNormal.Add((decimal)(MathNet.Numerics.Distributions.Normal.CDF((double)media, (double)ds, (double)limites[i]) -
                  MathNet.Numerics.Distributions.Normal.CDF((double)media, (double)ds, (double)limites[i - 1])));
            }


        }

        public static List<decimal> obtenerObservacionesNormal()
        {

            return observadosNormal;

        }

        public static List<decimal> obtenerEsperadosNormal()
        {

            return esperadosPoisson;

        }

        public static List<decimal> CalcularfreqAbsolutas(List<decimal> numeros_aleatorios, int subintervalos, int muestra)
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


          return frequencias.ConvertAll(obs => (Math.Round(obs * muestra, 4, MidpointRounding.AwayFromZero))); ; //cada indice de la lista corresponde a la freq relativa de un intervalo, al multiplicarla por la muestra tenemos la absoluta

        }

    }
}
