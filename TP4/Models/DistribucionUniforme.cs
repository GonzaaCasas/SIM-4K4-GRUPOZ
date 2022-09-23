using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP4.Models
{
    internal class DistribucionUniforme
    {
        private decimal a;
        private decimal b;


        public DistribucionUniforme(decimal _a, decimal _b)
        {
            this.a = _a;
            this.b = _b;
        }

        public decimal generar_x_uniforme(decimal random)
        {
            return this.a + random * (this.b - this.a);
        }

    }
}
