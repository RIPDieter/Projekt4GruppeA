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
      
        //Global Random Declaration
        Random rnd = new Random();
        //Global Timer Declaration
        DispatcherTimer timer = new DispatcherTimer();
        //Global ID Counter
        public static int idCounter = 0;


        public int timerCount = 0;

        private List<CarCasual> carList = new List<CarCasual>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {          
            createGrid(20, 40);
            spawntrafficlight();
           
            
        }
        #region GRID

        private void createGrid(int rowCount, int columnCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                var gridRow = new RowDefinition();
                gridRow.Height = new GridLength(20);
                gr_mainGrid.RowDefinitions.Add(gridRow);
            }
            for (int i = 0; i < columnCount; i++)
            {
                var gridColumn = new ColumnDefinition();
                gridColumn.Width = new GridLength(20);
                gr_mainGrid.ColumnDefinitions.Add(gridColumn);
            }
        }
        #endregion GRID

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

        #region SPAWN

        public void spawnCars(int carsToSpawn)
        {
            //carsToSpawn = Convert.ToInt16(sldSpawn.Value);

            if (checkSpawn() == false)
            {
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
        }

        private bool checkSpawn()
        {
            for (int j = 0; j < gr_mainGrid.Children.Count; j++)
            {
                UIElement uiE = gr_mainGrid.Children[j];
                if (Grid.GetColumn(uiE) == 2 && Grid.GetRow(uiE) == 5)
                {
                    return false;
                }
            }
            return true;
        }

        #endregion SPAWN

        #region  MOVE

        private void moveCars()
        {
            foreach (CarCasual thisCar in carList)
            {
                var gapSize = checkGapSize(thisCar);

                //var placeOfCar = Grid.GetColumn(thisCar.body);
                //var placeOfTrafficLight = Grid.GetColumn(ampel.body);


                //Stehen
                if (gapSize == 0)
                {
                    thisCar.v = 0;
                }
                // Bremsen
                else if (gapSize <= thisCar.v)
                {
                    thisCar.v = gapSize;
                    var CurrentColumn = Grid.GetColumn(thisCar.body);
                    CurrentColumn += thisCar.v;
                    Grid.SetColumn(thisCar.body, CurrentColumn);
                }
                //Beschleunigen
                else if (gapSize > thisCar.v && thisCar.v < 2)
                {
                    var CurrentColumn = Grid.GetColumn(thisCar.body);
                    CurrentColumn += thisCar.v;
                    Grid.SetColumn(thisCar.body, CurrentColumn);

                    if (rnd.Next(0, 3) == 2 && thisCar.v > 0)
                    {
                        thisCar.v--;
                    }
                    thisCar.v++;
                }
                // Höchstgeschw.
                else if (gapSize > thisCar.v && thisCar.v >= 2)
                {        
                    var CurrentColumn = Grid.GetColumn(thisCar.body);
                    CurrentColumn += thisCar.v;
                    Grid.SetColumn(thisCar.body, CurrentColumn);

                    if (rnd.Next(0, 3) == 2)
                    {
                        thisCar.v--;
                    }
                }  
            }
        }

        #endregion  MOVE

        #region GAP
        private int checkGapSize(CarCasual thisCar)
        {
            var placeOfCarColumn = Grid.GetColumn(thisCar.body);
            var firstSearchPointColumn = placeOfCarColumn + 1;
            var placeOfCarRow = Grid.GetRow(thisCar.body);         

            for (int i = firstSearchPointColumn; i < gr_mainGrid.ColumnDefinitions.Count; i++)
            {
                for (int j = 0; j < gr_mainGrid.Children.Count; j++)
                {
                    UIElement uiE = gr_mainGrid.Children[j];
                    if (Grid.GetColumn(uiE) == firstSearchPointColumn && Grid.GetRow(uiE) == placeOfCarRow)
                    {
                        var gapSize = Grid.GetColumn(uiE) - placeOfCarColumn - 1;
                        return gapSize;
                    }
                }
            }
            return 5;
        }
        #endregion GAP

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
        Trafficlight ampel2 = new Trafficlight();
        Trafficlight ampel3 = new Trafficlight();
        public void spawntrafficlight()
        {
            ampel.body = new Ellipse();
            ampel.blocker = new Ellipse();

            ampel2.body = new Ellipse();
            ampel2.blocker = new Ellipse();

            ampel3.body = new Ellipse();
            ampel3.blocker = new Ellipse();

            Brush[] trafficLightColors = new Brush[]
            {
                Brushes.Black,         
                Brushes.Green,
                Brushes.Red,
            };
            
            //Set Ampel Body
            Grid.SetColumn(ampel.body, 10);
            Grid.SetRow(ampel.body, 7);
            ampel.body.Fill = trafficLightColors[1];

            //Set Ampel Blocker
            Grid.SetRow(ampel.blocker, Grid.GetRow(ampel.body) - 2);
            Grid.SetColumn(ampel.blocker, Grid.GetColumn(ampel.body));
            ampel.blocker.Fill = (new SolidColorBrush(Colors.Black));

            gr_mainGrid.Children.Add(ampel.body);
           
            // Ampel 2
            //Set Ampel Body
            Grid.SetColumn(ampel2.body, 20);
            Grid.SetRow(ampel2.body, 7);
            ampel2.body.Fill = trafficLightColors[1];

            //Set Ampel Blocker
            Grid.SetRow(ampel2.blocker, Grid.GetRow(ampel2.body) - 2);
            Grid.SetColumn(ampel2.blocker, Grid.GetColumn(ampel2.body));
            ampel2.blocker.Fill = (new SolidColorBrush(Colors.Black));

            gr_mainGrid.Children.Add(ampel2.body);
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

                if (rdb1street.IsChecked == true)
                {

                }


            }

            if (timerCount % 5 == 0)
            {
                //TODO auto change color of ampel when isRed changes
                if (ampel2.isRed == true)
                {
                    gr_mainGrid.Children.Remove(ampel2.blocker);
                    ampel2.isRed = false;
                    ampel2.body.Fill = (new SolidColorBrush(Colors.Green));
                }
                else
                {
                    gr_mainGrid.Children.Add(ampel2.blocker);
                    ampel2.isRed = true;
                    ampel2.body.Fill = (new SolidColorBrush(Colors.Red));
                }

            }

        }


        #endregion AMPEL

       
    }
}


