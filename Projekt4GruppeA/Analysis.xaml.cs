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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Wpf;


namespace Projekt4GruppeA
{
    /// <summary>
    /// Interaktionslogik für Analysis.xaml
    /// </summary>
    public partial class Analysis : Window
    {
        //Optimale Methode hier wäre DataBinding, dann müsste man aber die Listen in ObservableCollections umbauen
        DispatcherTimer timer = new DispatcherTimer();

        public Analysis()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += tickThat;
            timer.Start();


            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Anzahl Autos",
                    Values = new ChartValues<double> {10}
                }
            };

            Labels = new[] {"CARS"};
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


        private void tickThat(object sender, EventArgs e)
        {
            var totalCarsOnMap = (MainWindow.carListLeftToRight.Count + MainWindow.carListRightToLeft.Count +
                                  MainWindow.carListTopToBottom.Count + MainWindow.carListBottomToTop.Count);

            tb_totalCarCountOnMap.Text = totalCarsOnMap.ToString();
            tb_leftToRightCars.Text = MainWindow.carListLeftToRight.Count.ToString();
            tb_rightToLeftCars.Text = MainWindow.carListRightToLeft.Count.ToString();
            lstOnaU.Text = MainWindow.carListTopToBottom.Count.ToString();
            lstUnaO.Text = MainWindow.carListBottomToTop.Count.ToString();

            updateChart(totalCarsOnMap);
        }

        private void updateChart(int value)
        {
            SeriesCollection[0].Values.Add(Convert.ToDouble(value));
        }

    }
}
