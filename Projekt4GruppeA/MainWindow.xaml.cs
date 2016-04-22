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


namespace Projekt4GruppeA
{

    public partial class MainWindow : Window
    {

        /*
        <Grid Margin="0,0,0,0">
        Grid.ColumnSpan="2" Grid.Row="1" Grid.Column="1"
        Grid.Column="2" Grid.Row="1"
        Grid.Column="2" Grid.Row="1"
    */
        //Global Random Declaration
        Random rnd = new Random();
        //Global Timer Declaration
        DispatcherTimer timer = new DispatcherTimer();
        //Global ID Counter
        public static int idCounter = 0;
        

        private List<CarCasual> carList = new List<CarCasual>();

        public MainWindow()
        {
            InitializeComponent();
        }

        public void btnSpawn_Click(object sender, RoutedEventArgs e)
        {
            spawnCars(1);   
        }

        public void spawnCars(int carsToSpawn)
        {

            carsToSpawn = Convert.ToInt16(sldSpawn.Value);

        Brush[] carColors = new Brush[]
            {
                Brushes.Red,
                Brushes.Blue,
                Brushes.Black,
                Brushes.Green,
                Brushes.Yellow,
                Brushes.Fuchsia
            };

            for (int j = 0; j < carsToSpawn; j++)
            {
                CarCasual car = new CarCasual
                {
                    speed = 10,
                    body = new Ellipse(),
                };
                car.body.Width = 10;
                car.body.Height = 10;
                car.body.Fill = carColors[rnd.Next(carColors.Length)];
                carList.Add(car);
                roadHorizontal.Children.Add(car.body);
            }
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            startTimer(1);
        }

        private void startTimer(int timerSpeed)
        {
            timer.Interval = TimeSpan.FromSeconds(timerSpeed);
            timer.Tick += timerTick;
            timer.Start();
        }

        public void timerTick(object sender, EventArgs e)
        {
            spawnCars(Convert.ToInt16(sldSpawn.Value));
            moveCars();
        }

        private void moveCars()
        {
            foreach (CarCasual thisCar in carList)
            {               
                thisCar.location = thisCar.location + thisCar.speed;

                Canvas.SetLeft(thisCar.body, thisCar.location);

                //var y = carList.Find(x => x.iD.Equals(thisCar.iD));
                //if ()
                //{

                //}
            }
        }

        


        private void sldTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timer.Interval = TimeSpan.FromSeconds(sldTime.Value);
        }

        private void sldSpawn_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
           
        }
    }
}


