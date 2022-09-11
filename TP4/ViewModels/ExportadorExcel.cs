using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

//using ScottPlot.MarkerShapes;

namespace TP4.ViewModels
{
    internal class ExportadorExcel
    {

        public static bool ExportarExcel(string ruta, DataTable dataTable, string nombreArchivo)
        {
            var wb = new XLWorkbook();

            // Add a DataTable as a worksheet
            var ws = wb.Worksheets.Add(dataTable, nombreArchivo);
            ws.Cell("A1").Value = "Nº";
            ws.Cell("B1").Value = "Valor";

            wb.SaveAs(ruta + "\\" + nombreArchivo + ".xlsx");
            return true;

        }

    }
}
