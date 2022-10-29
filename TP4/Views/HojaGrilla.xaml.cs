
using System.Windows.Controls;
using System.Collections.Generic;
using System.Data;
using TP4.Mvvm;
using TP4.Models;
using System;

namespace TP4.Views
{
    /// <summary>
    /// Interaction logic for HojaGrilla.xaml
    /// </summary>
    public partial class HojaGrilla : Page
    {
        public static DataTable grilla;

        public static bool tablaCreada = false;

        public HojaGrilla()
        {
            InitializeComponent();
            CargarTodo(Gestor.puntoA);
            
        }

        private void CargarTodo(bool flag)
        {
            if (flag)
            {
                DgvGrilla.DataContext = grilla;

            }
        }

        private static DataTable CrearTabla()
        {
            tablaCreada = true;
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

            //calculo
            tabla.Columns.Add("media");
            tabla.Columns.Add("std");
            tabla.Columns.Add("min");
            tabla.Columns.Add("max");
            tabla.Columns.Add("prob45d");
            tabla.Columns.Add("fecha90");
            tabla.Columns.Add("crit");
            return tabla;

        }

        public static void CargarFila(List<object> fila, int num)
        {
            if (!tablaCreada)
            {
                grilla = CrearTabla();
            }

            DataRow dr = grilla.NewRow();
            dr[0] = num;

            for (int i = 0; i <= 6; i++)
            {
                Actividad actividad = (Actividad)fila[i];

                dr[(i * 5) + 1] = Math.Round(actividad.d, 4, MidpointRounding.AwayFromZero).ToString();
                dr[(i * 5) + 2] = Math.Round(actividad.mi, 4, MidpointRounding.AwayFromZero).ToString();
                dr[(i * 5) + 3] = Math.Round(actividad.mf, 4, MidpointRounding.AwayFromZero).ToString();
                dr[(i * 5) + 4] = Math.Round(actividad.mf_tarde, 4, MidpointRounding.AwayFromZero).ToString();
                dr[(i * 5) + 5] = Math.Round(actividad.mi_tarde, 4, MidpointRounding.AwayFromZero).ToString();
            }

            Calculo calculo = (Calculo)fila[7];
            dr[36] = Math.Round(calculo.mediaDuracion, 4, MidpointRounding.AwayFromZero).ToString();
            dr[37] = Math.Round(calculo.std, 4, MidpointRounding.AwayFromZero).ToString();
            dr[38] = Math.Round(calculo.min, 4, MidpointRounding.AwayFromZero).ToString();
            dr[39] = Math.Round(calculo.max, 4, MidpointRounding.AwayFromZero).ToString();
            dr[40] = Math.Round(calculo.probDias, 4, MidpointRounding.AwayFromZero).ToString();
            dr[41] = Math.Round(calculo.fechaFijar, 4, MidpointRounding.AwayFromZero).ToString();
            dr[42] = calculo.caminoCritico;

            grilla.Rows.Add(dr);
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
