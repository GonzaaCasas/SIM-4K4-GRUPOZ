using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Windows;
using System.Windows.Controls;

namespace TP4.Views
{
    /// <summary>
    /// Interaction logic for Grafico.xaml
    /// </summary>
    public partial class Grafico : UserControl
    {
        public Grafico()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection();
            Visibility = Visibility.Hidden;
            //{
            //    new ColumnSeries
            //    {
            //        Title = "2015",
            //        Values = new ChartValues<double> { 10, 50, 39, 50 }
            //    }
            //};

            ////adding series will update and animate the chart automatically
            //SeriesCollection.Add(new ColumnSeries
            //{
            //    Title = "2016",
            //    Values = new ChartValues<double> { 11, 56, 42 }
            //});

            ////also adding values updates and animates the chart automatically
            //SeriesCollection[1].Values.Add(48d);

            //Labels = new[] { "Maria", "Susan", "Charles", "Frida" };
            Formatter = value => value.ToString("N");

            DataContext = this;

            //ToolTip =

        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<decimal, string> Formatter { get; set; }

        public void AgregarColeccion(decimal[] valores, string titulo)
        {
            SeriesCollection.Add(new ColumnSeries
            {
                Title = titulo,
                Values = new ChartValues<decimal>(valores)
            });
        }

        public void AgregarIntervalos(string[] labels)
        {
            Labels = labels;
        }

        public void Visible(bool visible)
        {
            if (visible)
            {
                Visibility = Visibility.Visible;
            }
            else
            {
                Visibility = Visibility.Hidden;
            }

        }

    }
}
