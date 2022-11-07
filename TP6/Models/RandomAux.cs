using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP6.Models {
	public class RandomAux {
		public Random rnd { get; set; } = new Random();

		public double generarRandom() {
			return rnd.NextDouble();
		}

	}
}
