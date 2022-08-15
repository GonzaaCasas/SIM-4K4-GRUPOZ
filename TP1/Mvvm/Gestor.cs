using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TP1.Models;

namespace TP1.Mvvm
{
    internal class Gestor
    {
        private ArrayList numeros_aleatorios = new ArrayList();

        decimal esperado;
        decimal lim_inferior;
        decimal lim_superior;
        decimal paso;
        private List<decimal> e_oExpDosDividoEsperado = new List<decimal>(); // desp le ppongo otro nombre a la lista no sabia q poner 

        public static List<decimal> generar(string metodo, decimal xi_1, decimal c, decimal a, decimal modulo, int muestra)
        {
            return Generador.generar(metodo, xi_1, c, a, modulo, muestra);
        }

        public void testChiCuadrado(List<decimal>  numeros_aleatorios,int muestra, int subintervalos)
        {

            decimal min = numeros_aleatorios.Min();
            decimal max = numeros_aleatorios.Max();

            esperado = muestra / subintervalos;
            paso = (max - min) / subintervalos;

            this.lim_inferior = min;
            this.lim_superior = lim_inferior + paso;

            int[] observados = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }; // cada indice corresponde a un intervalo de los 10

            for (int i = 0; i < subintervalos - 1; i++)
            {
                foreach (var random in numeros_aleatorios)
                {
                    if (random >= lim_inferior && random <= lim_superior)
                    {
                        observados[i]++;
                    }
                }

                this.lim_inferior = this.lim_superior;
                this.lim_superior = this.lim_superior + paso;

            }

            for (int i = 0; i < observados.Count(); i++)
            {
                double e_o = (double)(this.esperado - observados[i]);
                decimal e_oExpDos = (decimal)Math.Pow(e_o, 2);  // lo puse en  varios paso para q se entienda al leerlo 
                this.e_oExpDosDividoEsperado.Add(e_oExpDos / this.esperado);
            }

            decimal chiCuadrado = e_oExpDosDividoEsperado.Sum();

            double gradosLibertad = subintervalos - 1;
            double alfa = 0.05;

            decimal chiSquareTeorico = (decimal)MathNet.Numerics.Distributions.ChiSquared.InvCDF(gradosLibertad, 1 - alfa);

            if (chiCuadrado < chiSquareTeorico)
            {
                // tirar que esta todo ok
            }

        }

        public static List<decimal> probabilidad(List<decimal> numeros_aleatorios)
        {
            List<decimal> array= new List<decimal>(); //aca se crea unna nueva lista donde contendra los valores de las probabilidades de cada intervalo
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

                if (i * 10000 < 1000) { p1 = (p1 * simAnterior + 1) / simActual; }                    // no me dejaba comparar decimales asi que los paso a entero y los comparo
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
            array.Add(p1); // va agregando al fnal del forech el reultado de cada probabiliddad seguro hay una forma de hacerlo en 1 linea
            array.Add(p2);
            array.Add(p3);
            array.Add(p4);
            array.Add(p5);
            array.Add(p6);
            array.Add(p7);
            array.Add(p8);
            array.Add(p9);
            array.Add(p10);

            return array; // esto habria que mostrar u de ultima para cada pN dentro de un label entonces el return seria cada probabilidad y no una lista
        }
    }
}
