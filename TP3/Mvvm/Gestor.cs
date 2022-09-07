using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TP3.Models;
using TP3.ViewModels;

namespace TP3.Mvvm
{
    internal class Gestor
    {
        private static List<decimal> numeros_aleatorios = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaExpNeg = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaPoisson = new List<decimal>();
        private static List<decimal> valores_variableAleatoriaNormal = new List<decimal>();
        private static int _muestra;
        private static decimal _lambda;
        private static decimal _lambdaExp;
        private static decimal _media;
        private static decimal _ds;
        private static List<decimal> chis = new List<decimal>();

        public static bool puntoA { get; set; }


        public static void generarVariablesAleatorias(decimal media, decimal ds, decimal lambda, decimal lambdaExp , int muestra)

        {
            _media = media;
            _ds = ds;
            _lambda = lambda;
            _lambdaExp = lambdaExp;
            _muestra = muestra;
            generarRandoms("Mixto", 37, 7, 19, 53, muestra + 1, 38);   // a la muestra le hago + 1 pq la distribucion normal hace uso de un random mas que el resto
          Generador.generarVariables(media, ds, lambda, lambdaExp);

        }
        public static (List<decimal>, List<decimal>, List<decimal>) obtenerVariablesAleatorias()
        {
            valores_variableAleatoriaExpNeg = Distribucion.obtenerVariableExpNegativa();
            valores_variableAleatoriaPoisson = Distribucion.obtenerVariablePoisson();
            valores_variableAleatoriaNormal = Distribucion.obtenerVariableNormal();

            return (valores_variableAleatoriaExpNeg, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal);


        }


        public static void generarRandoms(string metodo, decimal xi_1, decimal c, decimal a, decimal modulo, int muestra, decimal semilla2)

        {
            Generador.generarRandoms(metodo, xi_1, c, a, modulo, muestra, semilla2);
        }

     

        public static List<decimal> generarSiguientes(string metodo, decimal c, decimal a, decimal modulo, int muestra)

        {
            return Generador.generarSiguientes(metodo, c, a, modulo, muestra);
        }


        public static List<decimal> generadorRandomPuntoB(int muestra)
        {
            return Generador.generarRandomcSharp(muestra);
        }

        public static List<decimal> test(int subintervalos) // cambiar a List<decimal>
        {
            //decimal chiUniforme = Distribucion.obtenerChiUniforme(_muestra, subintervalos);
            //chis.Add(chiUniforme);

            chis.Clear();
            List<decimal> limites;
            limites = ChiCuadrado.calcularLimites(valores_variableAleatoriaExpNeg, subintervalos);
            decimal chiExponencial = Distribucion.obtenerChiExponencial(_lambdaExp, limites, _muestra, subintervalos);
            chis.Add(chiExponencial);

            limites = ChiCuadrado.calcularLimites(valores_variableAleatoriaPoisson, subintervalos);
            decimal chiPoissoon = Distribucion.obtenerChiPoisson(_lambda, limites, _muestra, subintervalos);
            chis.Add(chiPoissoon);

            limites = ChiCuadrado.calcularLimites(valores_variableAleatoriaNormal, subintervalos);
            decimal chiNormal = Distribucion.obtenerChiNormal(_media, _ds, limites, _muestra, subintervalos);
            chis.Add(chiNormal);

            decimal chitabulado = ChiCuadrado.obtenerChitabulado(subintervalos);
            chis.Add(chitabulado);

            return chis;
        }

        public static (List<decimal>, List<decimal>, List<decimal>) obtenerObservaciones()
        {
            
            return (Distribucion.obtenerObservacionesExponencial(), Distribucion.obtenerObservacionesPoisson(), Distribucion.obtenerObservacionesNormal());

        }

        public static (List<decimal>, List<decimal>, List<decimal>) obtenerEsperados()
        {
            return (Distribucion.obtenerEsperadosExponencial(), Distribucion.obtenerEsperadosPoisson(), Distribucion.obtenerEsperadosNormal());
        }



        public static (List<decimal>, List<decimal>, List<string>) obtenerTodoNormal(int subintervalos)
        {
            return (Distribucion.obtenerObservacionesNormal(), Distribucion.obtenerEsperadosNormal(), ChiCuadrado.obtenerMedios(valores_variableAleatoriaNormal, subintervalos));
        }

        public static (List<decimal>, List<decimal>, List<string>) obtenerTodoPoisson(int subintervalos)
        {
            return (Distribucion.obtenerObservacionesPoisson(), Distribucion.obtenerEsperadosPoisson(), ChiCuadrado.obtenerMedios(valores_variableAleatoriaPoisson, subintervalos));
        }
        public static (List<decimal>, List<decimal>, List<string>) obtenerTodoExp(int subintervalos)
        {
            return (Distribucion.obtenerObservacionesExponencial(), Distribucion.obtenerEsperadosExponencial(), ChiCuadrado.obtenerMedios(valores_variableAleatoriaExpNeg, subintervalos));
        }



        public static List<decimal> obtenerConjuntoRandomGenerado()
        {
            return numeros_aleatorios;  

        }


        public static List<decimal> probabilidad(List<decimal> numeros_aleatorios)
        {
            List<decimal> array = new List<decimal>(); //aca se crea unna nueva lista donde contendra los valores de las probabilidades de cada intervalo
            int simActual = 1;
            int simAnterior = 0;
            decimal p1 = 0;
            decimal p2 = 0;
            decimal p3 = 0;
            decimal p4 = 0;
            decimal p5 = 0;
            decimal p6 = 0;
            decimal p7 = 0;
            decimal p8 = 0;
            decimal p9 = 0;
            decimal p10 = 0;
            foreach (decimal i in numeros_aleatorios)
            {

                if (i * 10000 <= 1000) { p1 = (p1 * simAnterior + 1) / simActual; }                    // no Ame dejaba comparar decimales asi que los paso a entero y los comparo
                else { p1 = (p1 * simAnterior + 0) / simActual; }

                if (1000 <= i * 10000 & i * 10000 < 2000) { p2 = (p2 * simAnterior + 1) / simActual; }
                else { p2 = (p2 * simAnterior + 0) / simActual; }

                if (2000 <= i * 10000 & i * 10000 < 3000) { p3 = (p3 * simAnterior + 1) / simActual; }
                else { p3 = (p3 * simAnterior + 0) / simActual; }

                if (3000 <= i * 10000 & i * 10000 < 4000) { p4 = (p4 * simAnterior + 1) / simActual; }
                else { p4 = (p4 * simAnterior + 0) / simActual; }

                if (4000 <= i * 10000 & i * 10000 < 5000) { p5 = (p5 * simAnterior + 1) / simActual; }
                else { p5 = (p5 * simAnterior + 0) / simActual; }

                if (5000 <= i * 10000 & i * 10000 < 6000) { p6 = (p6 * simAnterior + 1) / simActual; }
                else { p6 = (p6 * simAnterior + 0) / simActual; }

                if (6000 <= i * 10000 & i * 10000 < 7000) { p7 = (p7 * simAnterior + 1) / simActual; }
                else { p7 = (p7 * simAnterior + 0) / simActual; }

                if (7000 <= i * 10000 & i * 10000 < 8000) { p8 = (p8 * simAnterior + 1) / simActual; }
                else { p8 = (p8 * simAnterior + 0) / simActual; }

                if (8000 <= i * 10000 & i * 10000 < 9000) { p9 = (p9 * simAnterior + 1) / simActual; }
                else { p9 = (p9 * simAnterior + 0) / simActual; }

                if (9000 <= i * 10000 & i * 10000 < 10000) { p10 = (p10 * simAnterior + 1) / simActual; }
                else { p10 = (p10 * simAnterior + 0) / simActual; }
                simAnterior = simActual;
                simActual++;

            }
            array.Add(Math.Round(p1, 4, MidpointRounding.AwayFromZero)); // va agregando al fnal del forech el reultado de cada probabiliddad seguro hay una forma de hacerlo en 1 linea
            array.Add(Math.Round(p2, 4, MidpointRounding.AwayFromZero));
            array.Add(Math.Round(p3, 4, MidpointRounding.AwayFromZero));
            array.Add(Math.Round(p4, 4, MidpointRounding.AwayFromZero));
            array.Add(Math.Round(p5, 4, MidpointRounding.AwayFromZero));
            array.Add(Math.Round(p6, 4, MidpointRounding.AwayFromZero));
            array.Add(Math.Round(p7, 4, MidpointRounding.AwayFromZero));
            array.Add(Math.Round(p8, 4, MidpointRounding.AwayFromZero));
            array.Add(Math.Round(p9, 4, MidpointRounding.AwayFromZero));
            array.Add(Math.Round(p10, 4, MidpointRounding.AwayFromZero));

            //Console.WriteLine(array.Sum());

            return array; // esto habria que mostrar u de ultima para cada pN dentro de un label entonces el return seria cada probabilidad y no una lista
        }

        public static (List<string>, List<string>, List<string>) obtenerMedioIntervalos(int subintervalos)
        {

            return (ChiCuadrado.obtenerMedios(valores_variableAleatoriaExpNeg, subintervalos),
            ChiCuadrado.obtenerMedios(valores_variableAleatoriaPoisson, subintervalos),
            ChiCuadrado.obtenerMedios(valores_variableAleatoriaNormal, subintervalos));

        }


        public static bool ExportarExcel(string ruta, DataTable tabla, string nombreArchivo)
        {
            return ExportadorExcel.ExportarExcel(ruta, tabla, nombreArchivo);
        }


    }

}
