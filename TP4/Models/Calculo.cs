using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;


namespace TP4.Models
{
    internal class Calculo 
    {

        public double min { get; set; }
        public double max { get; set; }

        public double acumDuracion { get; set; }
        public double mediaDuracion { get; set; }

        public double acumstd { get; set; }
        public double std { get; set; }
        public double probDias { get; set; } //prob45Dias
        public double fechaFijar { get; set; } //fecha90

        public string caminoCritico { get; set; }

        public int simAnterior { get; set; }
        public int simActual { get; set; }

        //public double[] arr = new double[15];
        public List<double> intervalos = new List<double>(new double[15]); // 15 intervalos, el indice cero corresponde al intervalo 1 y asi ..

        public List<double> frecuenciasAbsolutas { get; set; }
        public List<double> frecuenciasRelativas{ get; set; }
        public List<double> probAcumuladas { get; set; }

        public List<double> limites { get; set; }

        public double acumCriticoA1 { get; set; }
        public double acumCriticoA2 { get; set; }
        public double acumCriticoA3 { get; set; }
        public double acumCriticoA4 { get; set; }
        public double acumCriticoA5 { get; set; }

        public double probabCriticoA1 { get; set; }
        public double probabCriticoA2 { get; set; }
        public double probabCriticoA3 { get; set; }
        public double probabCriticoA4 { get; set; }
        public double probabCriticoA5 { get; set; }




        public Calculo() 
        {
            simActual = 1;
            simAnterior = 0;
        }

        public void determinarMinMax(Actividad actividadF, double minAnterior, double maxAnterior)
        {
            if (actividadF.mf < minAnterior)
            {
                this.min = actividadF.mf;
            }
            else
            {
                this.min = minAnterior;
            }
            if (actividadF.mf > maxAnterior)
            {
                this.max = actividadF.mf;
            }
            else
            {
                this.max = maxAnterior;
            }
        }

        public void determinarProbDias(int dias)
        {
            this.probDias = MathNet.Numerics.Distributions.Normal.CDF(this.mediaDuracion, this.std, 45);

        }
        public void determinarFecha(double nivelConfianza)
        {
            this.fechaFijar = MathNet.Numerics.Distributions.Normal.InvCDF(this.mediaDuracion, this.std, nivelConfianza);


        }

        public void determinarCaminoCritico(Actividad actividad1, Actividad actividad2, Actividad actividad3, Actividad actividad4, Actividad actividad5)
        {
            caminoCritico = "";
        

            caminoCritico += Math.Round(actividad1.mf, 4, MidpointRounding.AwayFromZero) == Math.Round(actividad1.mf_tarde, 4, MidpointRounding.AwayFromZero) ? " Actividad 1 --->" : "";
            caminoCritico += Math.Round(actividad2.mf, 4, MidpointRounding.AwayFromZero) == Math.Round(actividad2.mf_tarde, 4, MidpointRounding.AwayFromZero) ? " Actividad 2 --->" : "";
            caminoCritico += Math.Round(actividad3.mf, 4, MidpointRounding.AwayFromZero) == Math.Round(actividad3.mf_tarde, 4, MidpointRounding.AwayFromZero) ? " Actividad 3 ---> Actividad F" : "";
            caminoCritico += Math.Round(actividad4.mf, 4, MidpointRounding.AwayFromZero) == Math.Round(actividad4.mf_tarde, 4, MidpointRounding.AwayFromZero) ? " Actividad 4 --->" : "";
            caminoCritico += Math.Round(actividad5.mf, 4, MidpointRounding.AwayFromZero) == Math.Round(actividad5.mf_tarde, 4, MidpointRounding.AwayFromZero) ? " Actividad 5 ---> Actividad F" : "";


        }

        public void CalcularfreqAbsolutas(double numero, List<double> limites, int simAnterior, int simActual, List<double> intervalos)
        {


            // intervalo 1 (indice 0) --> menores al minimo
            // .....
            // intervalo 15 (indice 14) ---> mayores al maximo

            //  - 0 - 3 - 6 - 9 - 12 - 15 - 18 - 21- 24 - 27 - 30 - 33 - 36 -39 -  


            if (numero <= limites[0]) // intervalo 1 - menor al minimo
            {
                intervalos[0] = (intervalos[0] * simAnterior + 1) / simActual; // observados

            }
            else
            {
                intervalos[0] = (intervalos[0] * simAnterior + 0) / simActual;

            }



            for (int i = 1; i <= 13; i++)
            {
                if (numero > limites[i-1] && numero <= limites[i]) // menor a algun de las otras primeras 14 simulaciones
                {
                    intervalos[i] = (intervalos[i] * simAnterior + 1) / simActual; // observados
                }
                else
                {
                    intervalos[i] = (intervalos[i] * simAnterior + 0) / simActual;
                }


            }

            if (numero > limites.Last()) // intervalo 15 - todo lo demas
            {
                intervalos[14] = (intervalos[14] * simAnterior + 1) / simActual; // observados

            }
            else
            {
                intervalos[14] = (intervalos[14] * simAnterior + 0) / simActual;

            }


            this.simActual = simActual + 1;
            this.simAnterior = this.simActual - 1 ;

            this.intervalos = intervalos;

            this.limites = limites;
            //this.frecuenciasAbsolutas = intervalos.ConvertAll(obs => (Math.Round(obs * (muestra - 14), 4, MidpointRounding.AwayFromZero)));
            this.frecuenciasAbsolutas = intervalos.ConvertAll(obs => (Math.Round(obs * simActual, 4, MidpointRounding.AwayFromZero)));
            this.frecuenciasRelativas = intervalos.ConvertAll(obs => (Math.Round(obs, 4, MidpointRounding.AwayFromZero)));

            double total = 0;
            probAcumuladas = new List<double>();
            foreach (double d in frecuenciasRelativas)
            {
               
                if (probAcumuladas.Sum() != 1)
                {
                    total += d;
                    probAcumuladas.Add(total);

                }
                else
                {
                    probAcumuladas.Add(0);  
                }
            }

        }

   




    }
}
