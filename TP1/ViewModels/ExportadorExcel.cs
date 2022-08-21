using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ClosedXML.Excel;
using ScottPlot.MarkerShapes;

namespace TP1.ViewModels
{
    internal class ExportadorExcel
    {
        
        public static bool ExportarExcel(string ruta, DataTable dataTable)
        {
            var wb = new XLWorkbook();

            // Add a DataTable as a worksheet
            wb.Worksheets.Add(dataTable,"Serie Generada");

            wb.SaveAs( ruta + "\\Serie generada.xlsx");
            return true;

        }

    }
}
