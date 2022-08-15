using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TP1.ViewModels
{
    public class PuntoAVM : ViewModelBase
    {
        private List<decimal> _vectorEstado;

        public List<decimal> vectorEstado
        {
            get { return _vectorEstado; }

            set { 
                _vectorEstado = value;
                OnPropertyChanged(nameof(vectorEstado));
            }
        }

        public ICommand generarCommand { get; }

    }
}
