using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using LiveChartsCore.Defaults;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;

namespace TP6.Views
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
                    //Name = "Días",
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

        public void AgregarSerieBarras(List<double> serie, string titulo)
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

        public void AgregarSerieLinea(List<double> serie, string titulo)
        {
            Series = Series.Concat(new ISeries[]
            {
                new LineSeries<double>
                {
                    Name = titulo,
                    Values = serie
                }

            }).ToArray();
        }
        
        public void AgregarSerieXY(List<double> serieX, List<double> serieY, string titulo)
        {

            List<ObservablePoint> puntosCoord = new List<ObservablePoint>();

            for (int i = 0; i < serieX.Count; i++)
            {
                puntosCoord.Add(new ObservablePoint(serieX[i], serieY[i]));

            }

            Series = Series.Concat(new ISeries[]
            {

                new LineSeries<ObservablePoint>
                {
                    Name = titulo,
                    Values = puntosCoord.ToArray(),

                }

            }).ToArray();
        }

        public void NombreEjeX(string nombre)
        {
            XAxes = new Axis[]
            {
                new Axis
                {
                    Name = nombre,
                }
            };
        }


        public void AgregarIntervalos(string[] intervalos, bool rotacion)
        {
            if (rotacion)
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
            else
            {
               XAxes = new Axis[]
         {
               new Axis
               {
                    Labels = intervalos,
                    LabelsPaint = new SolidColorPaint(SKColors.Black),
                    TextSize = 20
                    //SeparatorsPaint = new SolidColorPaint(SKColors.Black) { StrokeThickness = 2 }
                }
         };
            }
            
        }
    }
}
