using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TP1.ViewModels
{
    public class PuntoAVM : ViewModelBase
    {
        private string _vectorEstado;

        public string vectorEstado
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
