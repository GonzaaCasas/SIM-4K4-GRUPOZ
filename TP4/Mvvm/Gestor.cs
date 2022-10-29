using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Documents;
using TP4.Models;
using TP4.ViewModels;



namespace TP4.Mvvm
{
    internal class Gestor
    {

        static Calculo calculo;
   


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
       // private static List<List<Actividad>> listaDatos = new List<List<Actividad>>();
        private static List<object> listaResultados = new List<object>();
        private static List<double> duracionesFinalizacionTarea = new List<double>();
        private static List<double> freqDuraciones = new List<double>();
        private static List<string> medios = new List<string>();
        private static List<double> tPromedio = new List<double>();

        private static List<double> actividadesMI_tardePromedios = new List<double>();
        private static List<double> actividadesCriticasProbalidades = new List<double>();


        // private static List<object> fila_Anterior1 = new List<object>();
        private static List<object> fila_Anterior = new List<object>();

        private static List<object> fila_Actual = new List<object>();

        private static List<List<object>> filasparaGrilla = new List<List<object>>();


        

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
           // listaDatos.Clear();
            bool primerSimulacion = true;
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

            


            for (int n_simulacion = 1; n_simulacion <= simulaciones; n_simulacion++) 
            {
                calculo = new Calculo(); // objeto calculo de la fila actual
                crearActividades();
                determinarMomentosTempranosTardes();


                if (primerSimulacion)
                {
                    calculo.min = actividadF.mf;
                    calculo.max = actividadF.mf;

                    calculo.acumDuracion = actividadF.mf;
                    calculo.mediaDuracion = actividadF.mf;
                    calculo.acumstd = Math.Pow(actividadF.mf - calculo.mediaDuracion, 2);
                    calculo.std = Math.Sqrt(calculo.acumstd / n_simulacion);

                    calculo.determinarProbDias(45);
                    calculo.determinarFecha(0.90);


                    //acumCriticoA1 = actividad1.mi == actividad1.mi_tarde ? 1 : 0;
                    //acumCriticoA2 = actividad2.mi == actividad2.mi_tarde ? 1 : 0;
                    //acumCriticoA3 = actividad3.mi == actividad3.mi_tarde ? 1 : 0;
                    //acumCriticoA4 = actividad4.mi == actividad4.mi_tarde ? 1 : 0;
                    //acumCriticoA5 = actividad5.mi == actividad5.mi_tarde ? 1 : 0;

                    calculo.determinarCaminoCritico(actividad1, actividad2, actividad3, actividad4, actividad5);

               
                    duracionesFinalizacionTarea.Add(actividadF.mf);

                }
                else
                {
                    Calculo calculofilaAnterior = fila_Anterior[7] as Calculo; // objeto calculo de la fila anterior

                    
                    calculo.acumDuracion = calculofilaAnterior.acumDuracion + actividadF.mf;
                    calculo.mediaDuracion = calculo.acumDuracion / n_simulacion;
                    calculo.acumstd = calculofilaAnterior.acumstd + Math.Pow(actividadF.mf - calculo.mediaDuracion, 2) ;
                    calculo.std = Math.Sqrt(calculo.acumstd / (n_simulacion - 1) );

                    calculo.determinarMinMax(actividadF, calculofilaAnterior.min, calculofilaAnterior.max);
                    calculo.determinarProbDias(45);
                    calculo.determinarFecha(0.90);


                    //acumCriticoA1 += actividad1.mi == actividad1.mi_tarde ? 1 : 0;
                    //acumCriticoA2 += actividad2.mi == actividad2.mi_tarde ? 1 : 0;
                    //acumCriticoA3 += actividad3.mi == actividad3.mi_tarde ? 1 : 0;
                    //acumCriticoA4 += actividad4.mi == actividad4.mi_tarde ? 1 : 0;
                    //acumCriticoA5 += actividad5.mi == actividad5.mi_tarde ? 1 : 0;

                    calculo.determinarCaminoCritico(actividad1, actividad2, actividad3, actividad4, actividad5);


                    probabCriticoA1 = acumCriticoA1 / n_simulacion;
                    probabCriticoA2 = acumCriticoA2 / n_simulacion;
                    probabCriticoA3 = acumCriticoA3 / n_simulacion;
                    probabCriticoA4 = acumCriticoA4 / n_simulacion;
                    probabCriticoA5 = acumCriticoA5 / n_simulacion;


                    if (n_simulacion <= 14)
                    {
                        duracionesFinalizacionTarea.Add(actividadF.mf);

                    }
                    else
                    {
               
                        calculo.CalcularfreqAbsolutas(actividadF.mf, duracionesFinalizacionTarea.OrderBy(duracion => duracion).ToList(), n_simulacion, calculofilaAnterior.simAnterior, calculofilaAnterior.simActual,calculofilaAnterior.intervalos);
                    }

                }
                tPromedio.Add(calculo.mediaDuracion);

                fila_Actual = new List<object> { actividadI, actividad1, actividad2, actividad3, actividad4, actividad5, actividadF, calculo }; 
                filasparaGrilla.Add(fila_Actual);

                fila_Anterior = fila_Actual;
                primerSimulacion = false;

            }

            // filasparaGrilla

            //indice [0] -> actividadI   }
            //indice [1] -> actividad1   }
            //indice [2] -> actividad2   }
            //indice [3] -> actividad3   }  ------ >   Actividad -> rnd, d,  mi, mf, mi_Tarde, mf_Tarde
            //indice [4] -> actividad4   }
            //indice [5] -> actividad5   }
            //indice [6] -> actividadF   }
            //indice [7] -> calculo                    Calculo -> mediaDuracion, std, min, max, prob45Dias, fechaFijar, caminoCritico

            // del punto de los 15 intervalos --> calculo.limites, calculo.frecuenciasAbsolutas, calculo.frecuenciasRelativas, calculo.probAcumuladas

            // ** probando desp lo borro **

            for (int fila = 50; fila < 100; fila++) 
            {
                Actividad actividad = filasparaGrilla[fila][1] as Actividad; // probando actividad 1 de cada fila desde la fila 50 a la 100
                Calculo calculo = filasparaGrilla[fila][7] as Calculo; // probando calculo de cada fila


                Console.WriteLine(actividad.d);
                Console.WriteLine(actividad.mi);
                Console.WriteLine(actividad.mi_tarde);
                Console.WriteLine(actividad.mf);
                Console.WriteLine(actividad.mf_tarde);
                Console.WriteLine(actividad.rnd);

                Console.WriteLine(calculo.min);
                Console.WriteLine(calculo.max);
                Console.WriteLine(calculo.mediaDuracion);
                Console.WriteLine(calculo.std);
                Console.WriteLine(calculo.probDias);
                Console.WriteLine(calculo.fechaFijar);
                Console.WriteLine(calculo.caminoCritico);

                Console.WriteLine(calculo.limites);
                Console.WriteLine(calculo.frecuenciasAbsolutas);
                Console.WriteLine(calculo.frecuenciasRelativas);
                Console.WriteLine(calculo.probAcumuladas);


            }




            medios = obtenerMedios(duracionesFinalizacionTarea, 15);

        

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

             actividadI = new Actividad();
             actividadI.d = 0;


            // Se crea  la Actividad 1 que tiene comportamiento Uniforme

            actividad1 = new Actividad();
            actividad1.rnd = random.NextDouble();
            actividad1.d = _uniformeActividad1.generar_x_uniforme(actividad1.rnd);
            
 

            // Se crea la Actividad 2 que tiene comportamiento Uniforme

            actividad2 = new Actividad();
            actividad2.rnd = random.NextDouble();
            actividad2.d= _uniformeActividad2.generar_x_uniforme(actividad2.rnd);



            // Se crea la Actividad 3 que tiene comportamiento Exponencial

            actividad3 = new Actividad();
            actividad3.rnd = random.NextDouble();
            actividad3.d = _ExponencialActividad3.generar_x_Exponencial(actividad3.rnd);


            // Se crea la actividad 4 que tiene comportamiento Uniforme

            actividad4 = new Actividad();
            actividad4.rnd = random.NextDouble();
            actividad4.d= _uniformeActividad4.generar_x_uniforme(actividad4.rnd);


            // Se crea la actividad 5 que tiene comportamiento Exponecial

            actividad5 = new Actividad();
            actividad5.rnd = random.NextDouble();
            actividad5.d = _ExponencialActividad5.generar_x_Exponencial(actividad5.rnd);
  

            // Se crea la actividad F

            actividadF = new Actividad();
            actividadF.d = 0;


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

            actividadF.mi = actividad5.mf > actividad3.mf ? actividad5.mf : actividad3.mf;
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

            actividad3.mf_tarde = actividadF.mi_tarde; /// (?)
            actividad3.mi_tarde = actividad3.mf_tarde - actividad3.d; // (?)

            // momentos mas tarde la actividad2

            actividad2.mf_tarde = actividad5.mi_tarde;
            actividad2.mi_tarde = actividad2.mf_tarde - actividad2.d;

            // momentos mas tarde la actividad1


            actividad1.mf_tarde = actividad4.mi_tarde;
            actividad1.mi_tarde = actividad1.mf_tarde - actividad1.d;

            // momentos mas tarde la actividadI

            actividadI.mf_tarde = actividad1.mi_tarde < actividad2.mi_tarde && actividad1.mi_tarde < actividad3.mi_tarde ? actividad1.mi_tarde : actividad2.mi_tarde <                       actividad3.mi_tarde ? actividad2.mi_tarde : actividad3.mi_tarde;

            actividadI.mi_tarde = actividadI.mf_tarde - actividadI.d;


        }


        public static List<double> CalcularfreqAbsolutas(int numero, List<double> limites, int muestra, int simAnterior, int simActual)
        {


            double[] arr = new double[15]; 
            List<double> frequencias = new List<double>(arr); // 15 intervalos, el indice cero corresponde al intervalo 1 y asi ..

            // 0 - 3 - 6 - 9 - 12 - 15 - 18 - 21- 24 - 27 - 30 - 33 - 36 -39 - 42  

                for (int i = 0; i < 15; i++)
                {
                    if (numero > limites[i] && numero <= limites[i+1]) // menor a algun de las primeras 14 simulaciones
                    {
                        frequencias[i] = (frequencias[i] * simAnterior + 1) / simActual; // observados
                    }
                    else
                    {
                        frequencias[i] = (frequencias[i] * simAnterior + 0) / simActual;
                    }
                   

            }

            if (numero > limites.Last()) // intervalo 15 - todo lo demas
            {
                frequencias[14] = (frequencias[14] * simAnterior + 1) / simActual; // observados

            }
            else
            {
                frequencias[14] = (frequencias[14] * simAnterior + 0) / simActual;

            }


            simAnterior = simActual;
            simActual++;



            return frequencias.ConvertAll(obs => (Math.Round(obs * muestra, 4, MidpointRounding.AwayFromZero))); ; //cada indice de la lista corresponde a la freq relativa de un intervalo, al multiplicarla por la muestra tenemos la absoluta

        }


        //public static List<double> CalcularfreqAbsolutas(List<double> serie, int subintervalos, int muestra)
        //{


        //    double[] arr = new double[subintervalos];
        //    List<double> frequencias = new List<double>(arr);


        //    double min = serie.Min();
        //    double max = serie.Max();


        //    double paso = (max - min) / subintervalos;

        //    double lim_inferior = min;
        //    double lim_superior = lim_inferior + paso;

        //    int simActual = 1;
        //    int simAnterior = 0;



        //    foreach (var random in serie)
        //    {
        //        for (int i = 0; i < subintervalos; i++)
        //        {
        //            if (random >= lim_inferior && random <= lim_superior)
        //            {
        //                frequencias[i] = (frequencias[i] * simAnterior + 1) / simActual; // observados
        //            }
        //            else
        //            {
        //                frequencias[i] = (frequencias[i] * simAnterior + 0) / simActual;
        //            }

        //            lim_inferior = lim_superior;
        //            lim_superior = lim_inferior + paso;


        //        }

        //        simAnterior = simActual;
        //        simActual++;
        //        lim_inferior = min;
        //        lim_superior = lim_inferior + paso;

        //    }


        //    return frequencias.ConvertAll(obs => (Math.Round(obs * muestra, 4, MidpointRounding.AwayFromZero))); ; //cada indice de la lista corresponde a la freq relativa de un intervalo, al multiplicarla por la muestra tenemos la absoluta

        //}

    }

}
