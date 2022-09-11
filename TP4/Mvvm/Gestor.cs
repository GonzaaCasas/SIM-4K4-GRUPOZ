using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TP4.Models;
using TP4.ViewModels;

namespace TP4.Mvvm
{
    internal class Gestor
    {
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


        public static void generarVariablesAleatorias(decimal media, decimal ds, decimal lambda, decimal lambdaExp, int muestra)

        {
            _media = media;
            _ds = ds;
            _lambda = lambda;
            _lambdaExp = lambdaExp;
            _muestra = muestra;
            generadorRandoms(muestra + 1);   // a la muestra le hago + 1 pq la distribucion normal hace uso de un random mas que el resto
            Generador.generarVariables(media, ds, lambda, lambdaExp);

        }
        public static (List<decimal>, List<decimal>, List<decimal>) obtenerVariablesAleatorias()
        {
            valores_variableAleatoriaExpNeg = Distribucion.obtenerVariableExpNegativa();
            valores_variableAleatoriaPoisson = Distribucion.obtenerVariablePoisson();
            valores_variableAleatoriaNormal = Distribucion.obtenerVariableNormal();

            return (valores_variableAleatoriaExpNeg, valores_variableAleatoriaPoisson, valores_variableAleatoriaNormal);


        }

        public static void generadorRandoms(int muestra)
        {
            Generador.generarRandomcSharp(muestra);
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


        public static bool ExportarExcel(string ruta, DataTable tabla, string nombreArchivo)
        {
            return ExportadorExcel.ExportarExcel(ruta, tabla, nombreArchivo);
        }


    }

}
