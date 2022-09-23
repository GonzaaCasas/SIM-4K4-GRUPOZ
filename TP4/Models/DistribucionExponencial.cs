using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Models
{
    internal class DistribucionExponencial
    {

        private decimal lambda;

        public DistribucionExponencial(decimal _media)
        {
            lambda = 1 / _media;
        }


        public decimal generar_x_Exponencial(decimal random)
        {
            return ( - 1 /lambda) * (decimal)Math.Log(1- (double)random);
        }
    }
}
