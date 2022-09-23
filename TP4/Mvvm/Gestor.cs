using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using TP4.Models;
using TP4.ViewModels;
using System.Linq;


namespace TP4.Mvvm
{
    internal class Gestor
    {
 

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
        private static List<double> duracionesFinalizacionTarea = new List<double>();
        private static List<double> freqDuraciones = new List<double>();
        private static List<string> medios = new List<string>();
        private static List<double> tPromedio = new List<double>();

        private static List<double> actividadesMI_tardePromedios = new List<double>();
        private static List<double> actividadesCriticasProbalidades = new List<double>();


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
            tPromedio.Clear();
            listaDatos.Clear();
            bool flag = false;
            min = 0;
            max = 0;
            double media;
            double acum = 0;

            double acumMI_tardeA1 = 0;
            double mediaMI_tardeA1 = 0;

            double acumMI_tardeA2 = 0;
            double mediaMI_tardeA2 = 0;

            double acumMI_tardeA3 = 0;
            double mediaMI_tardeA3 = 0;

            double acumMI_tardeA4 = 0;
            double mediaMI_tardeA4 = 0;

            double acumMI_tardeA5 = 0;
            double mediaMI_tardeA5 = 0;

            double acumCriticoA1 = 0;
            double acumCriticoA2 = 0;
            double acumCriticoA3 = 0;
            double acumCriticoA4 = 0;
            double acumCriticoA5 = 0;

            double probabCriticoA1 = 0;
            double probabCriticoA2 = 0;
            double probabCriticoA3 = 0;
            double probabCriticoA4 = 0;
            double probabCriticoA5 = 0;

            actividadesCriticasProbalidades.Clear();
            actividadesMI_tardePromedios.Clear();


            double acumstd = 0;
            double DE = 0;

            for (int i = 1; i <= simulaciones; i++) 
            {
                
                
                crearActividades();
                determinarMomentosTempranosTardes();
    

                listaDatos.Add(new List<Actividad> {actividadI, actividad1,actividad2, actividad3, actividad4, actividad5, actividadF });

                if (!flag)
                {
                    min = actividadF.mf;
                    max = actividadF.mf;
                    acum = actividadF.mf;
                    acumMI_tardeA1 = actividad1.mi_tarde;
                    acumMI_tardeA2 = actividad2.mi_tarde;
                    acumMI_tardeA3 = actividad3.mi_tarde;
                    acumMI_tardeA4 = actividad4.mi_tarde;
                    acumMI_tardeA5 = actividad5.mi_tarde;

                    acumCriticoA1 = actividad1.mi == actividad1.mi_tarde ? 1 : 0;
                    acumCriticoA2 = actividad2.mi == actividad2.mi_tarde ? 1 : 0;
                    acumCriticoA3 = actividad3.mi == actividad3.mi_tarde ? 1 : 0;
                    acumCriticoA4 = actividad4.mi == actividad4.mi_tarde ? 1 : 0;
                    acumCriticoA5 = actividad5.mi == actividad5.mi_tarde ? 1 : 0;


                    media = actividadF.mf;
                    acumstd = actividadF.mf;
                    flag = true;
                    //listaResultados = new List<object> { min, max };
                }
                else
                {
                    acum += actividadF.mf;
                    media = acum / listaDatos.Count;

                    acumMI_tardeA1 += actividad1.mi_tarde;
                    acumMI_tardeA2 += actividad2.mi_tarde;
                    acumMI_tardeA3 += actividad3.mi_tarde;
                    acumMI_tardeA4 += actividad4.mi_tarde;
                    acumMI_tardeA5 += actividad5.mi_tarde;


                    mediaMI_tardeA1 = acumMI_tardeA1 / listaDatos.Count;
                    mediaMI_tardeA2 = acumMI_tardeA2 / listaDatos.Count;
                    mediaMI_tardeA3 = acumMI_tardeA3 / listaDatos.Count;
                    mediaMI_tardeA4 = acumMI_tardeA4 / listaDatos.Count;
                    mediaMI_tardeA5 = acumMI_tardeA5 / listaDatos.Count;


                    acumCriticoA1 += actividad1.mi == actividad1.mi_tarde ? 1 : 0;
                    acumCriticoA2 += actividad2.mi == actividad2.mi_tarde ? 1 : 0;
                    acumCriticoA3 += actividad3.mi == actividad3.mi_tarde ? 1 : 0;
                    acumCriticoA4 += actividad4.mi == actividad4.mi_tarde ? 1 : 0;
                    acumCriticoA5 += actividad5.mi == actividad5.mi_tarde ? 1 : 0;


                    probabCriticoA1 = acumCriticoA1 / listaDatos.Count;
                    probabCriticoA2 = acumCriticoA2 / listaDatos.Count;
                    probabCriticoA3 = acumCriticoA3 / listaDatos.Count;
                    probabCriticoA4 = acumCriticoA4 / listaDatos.Count;
                    probabCriticoA5 = acumCriticoA5 / listaDatos.Count;

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
       
             
                if (i <= 14)
                {
                    duracionesFinalizacionTarea.Add(actividadF.mf);

                }

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
            freqDuraciones = CalcularfreqAbsolutas(duracionesFinalizacionTarea, 15, 14);
            medios = obtenerMedios(duracionesFinalizacionTarea, 15);
            Console.WriteLine(vectorEstado);

            prob45d = MathNet.Numerics.Distributions.Normal.CDF((double)tPromedio.Last(), (double)DE, 45);
            fecha90 = MathNet.Numerics.Distributions.Normal.InvCDF((double)tPromedio.Last(), (double)DE, 0.9);

            actividadesMI_tardePromedios.AddRange(new List<double> { mediaMI_tardeA1, mediaMI_tardeA2, mediaMI_tardeA3,
                                                                    mediaMI_tardeA4, mediaMI_tardeA5});
            actividadesCriticasProbalidades.AddRange(new List<double> { probabCriticoA1, probabCriticoA2, 0,                                                                             probabCriticoA4, probabCriticoA5 });

            puntoA = true; //flag
        }

        #region obtener variables
        public static List<double> ObtenerTiempoPromedio()
        {
            return tPromedio;
        }

        public static List<double> ObtenerMITardio()
        {
            return actividadesMI_tardePromedios;
        }

        public static List<double> ObtenerActCriticas()
        {
            return actividadesCriticasProbalidades;
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

        public static (List<double>,List<string>) obtenerDatosIntervalos()
        {
            return (freqDuraciones, medios);
        }
        #endregion




        public static List<string> obtenerMedios(List<double> conjuntoNumeros, int intervalos)
        {
            // 0 - 1 , 1 - 5, 5, 9
            List<string> valoresMediosIntervalos = new List<string>();

            List<double> limites = calcularLimites(conjuntoNumeros, intervalos);

            for (int i = 0; i < limites.Count() - 1; i++)
            {
                valoresMediosIntervalos.Add(Math.Round(((limites[i] + limites[i + 1]) / 2), 4, MidpointRounding.AwayFromZero).ToString()); // (limite inferior + limite superior) / 2
            }

            return valoresMediosIntervalos;
        }

        public static List<double> calcularLimites(List<double> conjuntoNumeros, int subintervalos)
        {
            double min = conjuntoNumeros.Min();
            double max = conjuntoNumeros.Max();
            double paso = (max - min) / subintervalos;

            double lim_inferior = min;
            double lim_superior = lim_inferior + paso;


            List<double> array = new List<double>();

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
            //  momentos mas temprano de la actividadI 

            actividadI.mi = 0;
            actividadI.mf = actividadI.d + actividadI.mi;

            //  momentos mas temprano de la actividad1 

            actividad1.mi = actividadI.mf;
            actividad1.mf = actividad1.d + actividad1.mi;

  
            //  momentos mas temprano de la actividad2

            actividad2.mi = actividadI.mf;
            actividad2.mf = actividad2.d + actividad2.mi;


            //  momentos mas temprano de la actividad3

            actividad3.mi = actividadI.mf;
            actividad3.mf = actividad3.d + actividad3.mi;

        
            //  momentos mas temprano de la actividad4

            actividad4.mi = actividad1.mf;
            actividad4.mf = actividad4.d + actividad4.mi;


            //  momentos mas tempranos de la actividad5

            actividad5.mi = actividad4.mf > actividad2.mf ? actividad4.mf : actividad2.mf;
            actividad5.mf = actividad5.d + actividad5.mi;


            //  momentos mas tempranos de la actividadF

            actividadF.mi = actividad5.mf > actividad2.mf ? actividad5.mf : actividad2.mf;
            actividadF.mf = actividadF.d + actividadF.mi;

            // momentos mas tarde de la actividadF

            actividadF.mf_tarde = actividadF.mf;
            actividadF.mi_tarde = actividadF.mf_tarde - actividadF.d;

            // momentos mas tarde la actividad5

            actividad5.mf_tarde = actividadF.mi_tarde;
            actividad5.mi_tarde = actividad5.mf_tarde - actividad5.d;

            // momentos mas tarde la actividad4


            actividad4.mf_tarde = actividad5.mi_tarde;
            actividad4.mi_tarde = actividad4.mf_tarde - actividad4.d;

            // momentos mas tarde la actividad3

            actividad3.mf_tarde = 0; /// (?)
            actividad3.mi_tarde = 0; // (?)

            // momentos mas tarde la actividad2

            actividad2.mf_tarde = actividad5.mi_tarde < actividadF.mi_tarde ? actividad5.mi_tarde : actividadF.mi_tarde;
            actividad2.mi_tarde = actividad2.mf_tarde - actividad2.d;

            // momentos mas tarde la actividad1


            actividad1.mf_tarde = actividad4.mi_tarde;
            actividad1.mi_tarde = actividad1.mf_tarde - actividad1.d;

            // momentos mas tarde la actividadI

            actividadI.mf_tarde = actividad1.mi_tarde < actividad2.mi_tarde ? actividad1.mi_tarde : actividad2.mi_tarde;
            actividadI.mi_tarde = actividadI.mf_tarde - actividadI.d;


        }

        public static List<double> CalcularfreqAbsolutas(List<double> serie, int subintervalos, int muestra)
        {


            double[] arr = new double[subintervalos];
            List<double> frequencias = new List<double>(arr);


            double min = serie.Min();
            double max = serie.Max();


            double paso = (max - min) / subintervalos;

            double lim_inferior = min;
            double lim_superior = lim_inferior + paso;

            int simActual = 1;
            int simAnterior = 0;



            foreach (var random in serie)
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
