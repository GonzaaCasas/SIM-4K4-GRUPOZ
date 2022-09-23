using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using TP4.Models;
using TP4.ViewModels;
using System.Linq;


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

        //--------------------- Lo de arriba es del TP pasado

        static Actividad actividadI;
        static Actividad actividad1;
        static Actividad actividad2;
        static Actividad actividad3;
        static Actividad actividad4;
        static Actividad actividad5;
        static Actividad actividadF;



        private static DistribucionUniforme _uniformeActividad1;
        private static DistribucionUniforme _uniformeActividad2;
        private static DistribucionExponencial _ExponencialActividad3;
        private static DistribucionUniforme _uniformeActividad4;
        private static DistribucionExponencial _ExponencialActividad5;



        private static List<object> vectorEstado = new List<object>();
        private static List<object> fila = new List<object>();
        private static List<List<Actividad>> listaDatos = new List<List<Actividad>>();
        private static List<object> listaResultados = new List<object>();
        private static List<double> tPromedio = new List<double>();

        private static double min = 0;
        private static double max = 0;
        private static double prob45d;
        private static double fecha90;

        public static bool puntoA { get; set; }
        static Random random = new Random();

        public static void inicializarDistribuciones( double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5)
        {
            _uniformeActividad1 = new DistribucionUniforme(a1, b1);
            _uniformeActividad2 = new DistribucionUniforme(a2, b2);
            _ExponencialActividad3 = new DistribucionExponencial(media3);
            _uniformeActividad4 = new DistribucionUniforme(a4, b4);
            _ExponencialActividad5 = new DistribucionExponencial(media5);


        }

        public static void simular(double simulaciones, double a1, double b1, double a2, double b2, double a4, double b4, double media3, double media5)
        {
            inicializarDistribuciones( a1,  b1,  a2,  b2,  a4,  b4,  media3,  media5);

            bool flag = false;
            //decimal min = 0;
            //decimal max = 0;
            double acum = 0;
            double media = 0;
            double acumstd = 0;
            double DE = 0;

            for (int i = 1; i <= simulaciones; i++) // despues cambiar simulaciones / 2
            {
                
                // actualizarVectorEstado(i);
                crearActividades();
                determinarMomentosTempranosTardes();
                //TerminarDeCalcularActividades(?)
                //Hacer el resto de calculos (promedio duracion, max y min, probabilidad de 45 dias , etc)

                listaDatos.Add(new List<Actividad> {actividadI, actividad1,actividad2, actividad3, actividad4, actividad5, actividadF });

                if (!flag)
                {
                    min = actividadF.mf;
                    max = actividadF.mf;
                    acum = actividadF.mf;
                    media = actividadF.mf;
                    acumstd = actividadF.mf;
                    flag = true;
                    listaResultados = new List<object> { min, max };
                }
                else
                {
                    acum += actividadF.mf;
                    media = acum / listaDatos.Count;
                    //acumstd += (decimal)Math.Pow((double)(actividadF.mf - media),2);
                    //DE = acumstd / (i - 1);
                    if (actividadF.mf < min)
                    {
                        min = actividadF.mf;
                    }
                    if (actividadF.mf > max)
                    {
                        max = actividadF.mf;
                    }
                }
                tPromedio.Add(media);
                // vectorEstado[i] = new List<List<decimal>> { listaActividades, listaResultados};

                vectorEstado.Add(fila);
            }

            for (int i = 1; i <= listaDatos.Count; i++)
            {
                acumstd += Math.Pow(listaDatos[i-1][6].mf - tPromedio.Last(), 2);

                if (i == 1)
                {
                    DE = Math.Sqrt((double)(acumstd / i));
                }
                else
                {
                    DE = Math.Sqrt((double)(acumstd / (i-1)));
                }               
            }
            //listaResultados = new List<object> { min, max };
            Console.WriteLine(vectorEstado);

            prob45d = MathNet.Numerics.Distributions.Normal.CDF((double)tPromedio.Last(), (double)DE, 45);
            fecha90 = MathNet.Numerics.Distributions.Normal.InvCDF((double)tPromedio.Last(), (double)DE, 0.9);

            puntoA = true; //flag
        }

        #region obtener variables
        public static List<double> ObtenerTiempoPromedio()
        {
            return tPromedio;
        }

        public static double ObtenerMinimo()
        {
            return min;
        }
        public static double ObtenerMaximo()
        {
            return max;
        }

        public static double ObtenerProb45d()
        {
            return prob45d;
        }
        public static double Obtenerfecha90()
        {
            return fecha90;
        }
        #endregion




        public static void actualizarVectorEstado(int j)
        {


            for (int i = 0; i <= 1; i++) // actualiza las dos filas del vector estado
            {

                //crearActividades();
                //determinarMomentosTempranosTardes();
                ////TerminarDeCalcularActividades(?)
                ////Hacer el resto de calculos (promedio duracion, max y min, probabilidad de 45 dias , etc)
                //fila = new List<decimal> { j, actividadI.d, actividadI.mi, actividadI.mf, actividadI.mf_tarde, actividadI.mi_tarde,
                //                              actividad1.d, actividad1.mi, actividad1.mf, actividad1.mf_tarde, actividad1.mi_tarde,
                //                              actividad2.d, actividad2.mi, actividad2.mf, actividad2.mf_tarde, actividad2.mi_tarde,
                //                              actividad3.d, actividad3.mi, actividad3.mf, actividad3.mf_tarde, actividad3.mi_tarde,
                //                              actividad4.d, actividad4.mi, actividad4.mf, actividad4.mf_tarde, actividad4.mi_tarde,
                //                              actividad5.d, actividad5.mi, actividad5.mf, actividad5.mf_tarde, actividad5.mi_tarde,
                //                              actividadF.d, actividadF.mi, actividadF.mf, actividadF.mf_tarde, actividadF.mi_tarde,

              
                //fila.AddRange(fila) ;

            }

        }

        public static void crearActividades()
        {

            // Se crea  la Actividad I 

             actividadI = new Actividad(0);
            

            // Se crea  la Actividad 1 que tiene comportamiento Uniforme

            double t1 = _uniformeActividad1.generar_x_uniforme(random.NextDouble());
            actividad1 = new Actividad(t1);


            // Se crea la Actividad 2 que tiene comportamiento Uniforme

            double t2 = _uniformeActividad2.generar_x_uniforme(random.NextDouble());
            actividad2 = new Actividad(t2);


            // Se crea la Actividad 3 que tiene comportamiento Exponencial

            double t3 = _ExponencialActividad3.generar_x_Exponencial(random.NextDouble());
            actividad3 = new Actividad(t3);

            // Se crea la actividad 4 que tiene comportamiento Uniforme

            double t4 = _uniformeActividad4.generar_x_uniforme(random.NextDouble());
            actividad4 = new Actividad(t4);

            // Se crea la actividad 5 que tiene comportamiento Exponecial

            double t5 = _ExponencialActividad5.generar_x_Exponencial(random.NextDouble());
            actividad5 = new Actividad(t5);

            // Se crea la actividad F

            actividadF = new Actividad(0);


        }

        public static void determinarMomentosTempranosTardes()
        {
            //  momentos (?) de la actividadI

            actividadI.mi = 0;
            actividadI.mf = actividadI.d + actividadI.mi;
            actividadI.mf_tarde = actividad1.mi_tarde < actividad2.mi_tarde ? actividad1.mi_tarde : actividad2.mi_tarde;
            actividadI.mi_tarde = actividadI.mf_tarde - actividadI.d;


            //  momentos (?) de la actividad1 

            actividad1.mi = actividadI.mf;
            actividad1.mf = actividad1.d + actividad1.mi;
            actividad1.mf_tarde = actividad4.mi_tarde;
            actividad1.mi_tarde = actividad1.mf_tarde - actividad1.d;


            //  momentos (?) de la actividad2

            actividad2.mi = actividadI.mf;
            actividad2.mf = actividad2.d + actividad2.mi;
            actividad2.mf_tarde = actividad5.mi_tarde < actividadF.mi_tarde ? actividad5.mi_tarde : actividadF.mi_tarde;
            actividad2.mi_tarde = actividad2.mf_tarde - actividad2.d;


            //  momentos (?) de la actividad3

            actividad3.mi = actividadI.mf;
            actividad3.mf = actividad3.d + actividad3.mi;
            actividad3.mf_tarde = 0; /// (?)
            actividad3.mi_tarde = 0; // (?)


            //  momentos (?) de la actividad4

            actividad4.mi = actividad1.mf;
            actividad4.mf = actividad4.d + actividad4.mi;
            actividad4.mf_tarde = actividad5.mi_tarde;
            actividad4.mi_tarde = actividad4.mf_tarde - actividad4.d;


            //  momentos (?) de la actividad5

            actividad5.mi = actividad4.mf > actividad2.mf ? actividad4.mf : actividad2.mf;
            actividad5.mf = actividad5.d + actividad5.mi;
            actividad5.mf_tarde = actividadF.mi_tarde;
            actividad5.mi_tarde = actividad5.mf_tarde - actividad5.d;

            //  momentos (?) de la actividadF

            actividadF.mi = actividad5.mf > actividad2.mf ? actividad5.mf : actividad2.mf;
            actividadF.mf = actividadF.d + actividadF.mi;
            actividadF.mf_tarde = actividadF.mf;
            actividadF.mi_tarde = actividadF.mf_tarde - actividadF.d;
        }


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
