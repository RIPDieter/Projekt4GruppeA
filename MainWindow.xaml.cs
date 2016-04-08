using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using Projekt4GruppeA.Classes;

namespace Projekt4GruppeA
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        Ellipse body1 = new Ellipse();
        int xPosition = 10;


        public MainWindow()
        {
            InitializeComponent();
        }

        public void btn1_Click(object sender, RoutedEventArgs e)
        {
            CarCasual c1 = new CarCasual();

            body1.Width = 10;
            body1.Height = 10;
            body1.Fill = Brushes.Black;

            

            RoadCasual.Children.Add(body1);
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {

            timer.Interval = TimeSpan.FromMilliseconds(40);
            timer.Tick += timerTick;
            timer.Start();
        }

        public void timerTick(object sender, EventArgs e)
        {
            Canvas.SetLeft(body1, xPosition);
            xPosition += 10;
        }
    }
}
