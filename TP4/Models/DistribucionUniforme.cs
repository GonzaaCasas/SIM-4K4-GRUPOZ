using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Models
{
    internal class DistribucionUniforme
    {
        private double a;
        private double b;


        public DistribucionUniforme(double _a, double _b)
        {
            this.a = _a;
            this.b = _b;
        }

        public double generar_x_uniforme(double random)
        {
            return this.a + random * (this.b - this.a);
        }

    }
}
