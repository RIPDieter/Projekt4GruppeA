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

        //
        public int timerCount = 0;

        private List<CarCasual> carList = new List<CarCasual>();

        public MainWindow()
        {
            InitializeComponent();
            spawntrafficlight();
        }

        #region BUTTON CLICK EVENTS

        public void btnSpawn_Click(object sender, RoutedEventArgs e)
        {
            //spawnCars(1);   
            //spawntrafficlight(1);
        }

        private void btnTimer_Click(object sender, RoutedEventArgs e)
        {
            startTimer(1);
        }

        #endregion BUTTON CLICK EVENTS

        #region TIMER RELATED

        private void startTimer(int timerSpeed)
        {
            timer.Interval = TimeSpan.FromSeconds(timerSpeed);
            timer.Tick += timerTick;
            timer.Start();
        }

        public void timerTick(object sender, EventArgs e)
        {
            timerCount++;
            //spawnCars(Convert.ToInt16(sldSpawn.Value));
            switchLight();
           
            if (rnd.Next(0, 3) == 2)
            {
                spawnCars(1);
            }
           
            moveCars();

        }


        #endregion TIMER RELATED


        public void spawnCars(int carsToSpawn)
        {
            //carsToSpawn = Convert.ToInt16(sldSpawn.Value);

            Brush[] carColors = new Brush[]
                {
                Brushes.Red,
                Brushes.Blue,
                Brushes.Black,
                Brushes.Green,
                Brushes.Yellow,
                Brushes.Fuchsia
                };

                CarCasual car = new CarCasual
                {                   
                    body = new Ellipse(),
                };

                car.body.Width = 10;
                car.body.Height = 10;
                car.body.Fill = carColors[rnd.Next(carColors.Length)];
                carList.Add(car);

                Grid.SetColumn(car.body, 2);
                Grid.SetRow(car.body, 5);
                gr_mainGrid.Children.Add(car.body);
        }
        
        private void moveCars()
        {
            foreach (CarCasual thisCar in carList)
            {
                var gapSize = checkGapSize(thisCar);

                //var placeOfCar = Grid.GetColumn(thisCar.body);
                //var placeOfTrafficLight = Grid.GetColumn(ampel.body);

                if (gapSize >= 1 || gapSize == null)
                {
                    var v = Grid.GetColumn(thisCar.body);
                    v++;
                    Grid.SetColumn(thisCar.body, v);
                }              
            }
        }

        private int? checkGapSize(CarCasual thisCar)
        {
            var placeOfCar = Grid.GetColumn(thisCar.body);
            var placeOfNextCar = placeOfCar + 1;

            for (int i = placeOfNextCar; i < gr_mainGrid.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < gr_mainGrid.Children.Count; j++)
                {
                    UIElement uiE = gr_mainGrid.Children[j];
                    if (Grid.GetColumn(uiE) == placeOfNextCar)
                    {
                        var gapSize = Grid.GetColumn(uiE) - placeOfCar;
                        return gapSize;
                    }
                }
            }
            return null;
        }

        #region SLIDER

        private void sldTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //TODO Umdrehen
            timer.Interval = TimeSpan.FromSeconds(sldTime.Value);
        }

        private void sldSpawn_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
        #endregion SLIDER

        #region AMPEL

        Trafficlight ampel = new Trafficlight();
        public void spawntrafficlight()
        {
            ampel.body = new Ellipse();
            ampel.blocker = new Ellipse();

            Brush[] trafficLightColors = new Brush[]
            {
                Brushes.Black,         
                Brushes.Green,
                Brushes.Red,
            };
            
            //Set Amepl Body
            Grid.SetColumn(ampel.body, 6);
            Grid.SetRow(ampel.body, 4);
            ampel.body.Fill = trafficLightColors[1];

            //Set Ampel Blocker
            Grid.SetRow(ampel.blocker, Grid.GetRow(ampel.body) + 1);
            Grid.SetColumn(ampel.blocker, Grid.GetColumn(ampel.body));
            ampel.blocker.Fill = (new SolidColorBrush(Colors.Yellow));

            gr_mainGrid.Children.Add(ampel.body);
        }

        private void switchLight()
        {
            if (timerCount % 5 == 0)
            {
                //TODO auto change color of ampel when isRed changes
                if (ampel.isRed == true)
                {
                    gr_mainGrid.Children.Remove(ampel.blocker);
                    ampel.isRed = false;
                    ampel.body.Fill = (new SolidColorBrush(Colors.Green));
                }
                else
                {
                    gr_mainGrid.Children.Add(ampel.blocker);
                    ampel.isRed = true;
                    ampel.body.Fill = (new SolidColorBrush(Colors.Red));
                }

            }
        }



        #endregion AMPEL


    }
}


