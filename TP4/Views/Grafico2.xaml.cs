using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using SkiaSharp;

namespace TP4.Views
{
    /// <summary>
    /// Interaction logic for Grafico2.xaml
    /// </summary>
    public partial class Grafico2 : UserControl
    {
        public Grafico2()
        {
            InitializeComponent();
            DataContext = this;
            Series = new ISeries[0];
            Visibility = Visibility.Hidden;

        }

        public Axis[] YAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    Name = "Cantidad",
                    NamePaint = new SolidColorPaint(SKColors.Black),

                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                    TextSize = 20,

                    SeparatorsPaint = new SolidColorPaint(SKColors.DarkGray)
                    {
                        StrokeThickness = 2,
                        //PathEffect = new DashEffect(new float[] { 3, 3 })
                    }
                }
            };

        public ISeries[] Series { get; set; }

        public Axis[] XAxes { get; set; }
            = new Axis[]
            {
                new Axis
                {
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                    TextSize = 20
                }
            };

        public void AgregarSerie(List<double> serie, string titulo)
        {
            Series = Series.Concat(new ISeries[]
            {
                new ColumnSeries<double>
                {
                    Name = titulo,
                    Values = serie
                }
            }).ToArray();
        }

        public void AgregarIntervalos(string[] intervalos)
        {
            XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = intervalos,
                    LabelsRotation = 35,
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                    TextSize = 20
                    //SeparatorsPaint = new SolidColorPaint(SKColors.Black) { StrokeThickness = 2 }
                }
            };
        }
    }
}
