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
        private decimal xi_1;

        public static List<decimal> generar(string metodo, decimal xi_1, decimal cIndependiente, decimal cMultiplicadora, decimal modulo, int muestra)
        {

            return Generador.generar( metodo, xi_1, cIndependiente, cMultiplicadora, modulo, muestra);
        }
    }
}
