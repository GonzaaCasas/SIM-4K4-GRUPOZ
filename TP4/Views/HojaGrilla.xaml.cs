
using System.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TP4.Mvvm;

namespace TP4.Views
{
    /// <summary>
    /// Interaction logic for HojaGrilla.xaml
    /// </summary>
    public partial class HojaGrilla : Page
    {
        DataTable grilla;

        public HojaGrilla()
        {
            InitializeComponent();
            CargarTodo(Gestor.puntoA);
        }

        private void CargarTodo(bool flag)
        {
            if (flag)
            {
                grilla = CrearTabla();
                DgvGrilla.DataContext = grilla;

            }
        }

        private DataTable CrearTabla()
        {
            DataTable tabla = new DataTable();
            tabla.Columns.Add("sim");

            //ai
            tabla.Columns.Add("ti");
            tabla.Columns.Add("mii");
            tabla.Columns.Add("mfi");
            tabla.Columns.Add("mfit");
            tabla.Columns.Add("miit");

            //a1
            tabla.Columns.Add("t1");
            tabla.Columns.Add("mi1");
            tabla.Columns.Add("mf1");
            tabla.Columns.Add("mf1t");
            tabla.Columns.Add("mi1t");

            //a2
            tabla.Columns.Add("t2");
            tabla.Columns.Add("mi2");
            tabla.Columns.Add("mf2");
            tabla.Columns.Add("mf2t");
            tabla.Columns.Add("mi2t");

            //a3
            tabla.Columns.Add("t3");
            tabla.Columns.Add("mi3");
            tabla.Columns.Add("mf3");
            tabla.Columns.Add("mf3t");
            tabla.Columns.Add("mi3t");

            //a4
            tabla.Columns.Add("t4");
            tabla.Columns.Add("mi4");
            tabla.Columns.Add("mf4");
            tabla.Columns.Add("mf4t");
            tabla.Columns.Add("mi4t");

            //a5
            tabla.Columns.Add("t5");
            tabla.Columns.Add("mi5");
            tabla.Columns.Add("mf5");
            tabla.Columns.Add("mf5t");
            tabla.Columns.Add("mi5t");


            //f
            tabla.Columns.Add("tf");
            tabla.Columns.Add("mif");
            tabla.Columns.Add("mff");
            tabla.Columns.Add("mfft");
            tabla.Columns.Add("mift");


            return tabla;

        }

        
        //    for (int i = 0; i< 3; i++)
        //    {
        //        DataRow _row = tabla.NewRow();
        //_row[0] = "1";
        //        _row[1] = "20";
        //        _row[2] = "15";
        //        _row[3] = "10";
        //        _row[4] = "5";
        //        _row[5] = "0";

        //        _row[6] = "1";
        //        _row[7] = "20";
        //        _row[8] = "15";
        //        _row[9] = "10";
        //        _row[10] = "5";
        //        _row[11] = "0";

        //        _row[12] = "1";
        //        _row[13] = "20";
        //        _row[14] = "15";
        //        _row[15] = "10";
        //        _row[16] = "5";
        //        _row[17] = "0";

        //        tabla.Rows.Add(_row);
        //    }

    }
}
