using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Models
{
    internal class DistribucionExponencial
    {

        private double lambda;

        public DistribucionExponencial(double _media)
        {
            lambda = 1 / _media;
        }


        public double generar_x_Exponencial(double random)
        {
            return ( - 1 /lambda) * Math.Log(1- random);
        }
    }
}
