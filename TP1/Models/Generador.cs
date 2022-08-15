using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP1.Models
{
    internal class Generador
    {
        private static ArrayList numeros_aleatorios = new ArrayList();
        private static decimal xi;
        public static ArrayList generar(decimal xi_1, decimal cIndependiente, decimal cMultiplicadora, decimal modulo, int muestra)
        {
            numeros_aleatorios.Add(xi_1 / modulo); // agrega el primer random que corresponde a la semilla

            for (int i = 0; i < muestra; i++)
            {

                xi = generadorCongruenteMultiplicativo(xi_1, cMultiplicadora, modulo);
                numeros_aleatorios.Add(xi / modulo); // agrega los random
                xi_1 = xi;


            }

            return numeros_aleatorios;
        }

        private static decimal generadorCongruenteMixto(decimal xi_1, decimal a, decimal c, decimal m)
        {
            return (a * xi_1 + c) % m;
		    }

        private static decimal generadorCongruenteMultiplicativo(decimal xi_1, decimal a, decimal m)
        {
            return (a * xi_1) % m;
        }

        private static decimal generadorCongruenteAditivo(decimal xi_1, decimal xi_0, decimal m)
        {
            return (xi_1 + xi_0) % m;
        }
    }
}
