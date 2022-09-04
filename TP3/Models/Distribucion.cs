using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP3.Models
{
    internal class Distribucion
    {
        //private static decimal lambda;
        //private static decimal media;
        //private static decimal desviacion;
        private static double z;

        public static decimal DistExpNegativa(decimal lambda, decimal random)
        {
            return (-1 / lambda) * (decimal) Math.Log(1- (double)random);
        }

        public static decimal DistPoisson(decimal lambda, decimal random)
        {
            return 1;
        }

        public static decimal DistNormal(decimal media, decimal desviacion, decimal random1, decimal random2)
        {
            z =  Math.Sqrt(-2 * Math.Log(1 - (double)random1)) * Math.Cos(2* Math.PI * (double)random2) ;
            return media + (decimal)z * desviacion;
        }

    }
}
