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
      
        //Global Random Declaration
        Random rnd = new Random();
        //Global Timer Declaration
        DispatcherTimer timer = new DispatcherTimer();
        //Global ID Counter
        public static int idCounter = 0;
        // Skalierungsvariable
        int s;
        
        //Klasse TrafficlightCircuit instanziieren




         int timerCount;

        public static List<CarCasual> carList = new List<CarCasual>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {          
            

            createGrid(40, 200);
            spawntrafficlight(7,7);
            spawntrafficlight(30,7);


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
            if (!timer.IsEnabled)
            {
                startTimer(1);
                //spawnCars(Convert.ToInt16(sldSpawn.Value));
                //spawnCars(1);
                //spawntrafficlight(1);
            }
            else
            {
                MessageBox.Show("Already running!");
            }

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
            


            //switchLight();
            
            //spawnCars(Convert.ToInt16(sldSpawn.Value));
            // trafficlightCircuit.switchLight();

            int z = 10 - (Convert.ToInt16(sldSpawn.Value));

            if (rnd.Next(0, z) == 0)
            {
                spawnCars(1);
            }
           
            //if (rnd.Next(0, 3) == 2)
            //{
            //    spawnCars(1);
            //}


            moveCars();

            

        }


        #endregion TIMER RELATED

        #region SPAWN CARS

        public void spawnCars(int carsToSpawn)
        {
            if (checkSpawn() == false)
            {
                CarCasual car = new CarCasual();

                gr_mainGrid.Children.Add(car.body);
                carList.Add(car);
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

        #region  MOVE CARS

        private void moveCars()
        {
            foreach (CarCasual thisCar in carList)
            {        
                var gapSize = checkGapSize(thisCar);

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
                Console.WriteLine();
            }
        }

        #endregion  MOVE

        #region GAP

        private int checkGapSize(CarCasual thisCar)
        {
            var placeOfCarColumn = Grid.GetColumn(thisCar.body);
            var placeOfCarRow = Grid.GetRow(thisCar.body);       
                 
            for (var searchPointColumn = placeOfCarColumn + 1; searchPointColumn < gr_mainGrid.ColumnDefinitions.Count; searchPointColumn++)
            {
                for (int j = 0; j < gr_mainGrid.Children.Count; j++)
                {
                    UIElement uiE = gr_mainGrid.Children[j];
                    if (Grid.GetColumn(uiE) == searchPointColumn && Grid.GetRow(uiE) == placeOfCarRow)
                    {
                        var gapSize = Grid.GetColumn(uiE) - placeOfCarColumn - 1;
                        return gapSize;
                    }
                }
            }
            return 5;
        }

        #endregion GAP

        #region TRAFFICLIGHT



        //MainWindow main = new MainWindow();
        Trafficlight ampel = new Trafficlight();
        Trafficlight ampel12 = new Trafficlight();
        Trafficlight ampel13 = new Trafficlight();
        Trafficlight ampel14 = new Trafficlight();


        public void spawntrafficlight(int a, int b)
        {

            
            //a die Reihe der Ampel unten rechts
            //b gibt Zeile der Ampel unten rechts

            //Kreuzung 
            ampel.body = new Ellipse();
            ampel.blocker = new Ellipse();

            ampel12.body = new Ellipse();
            ampel12.blocker = new Ellipse();

            ampel13.body = new Ellipse();
            ampel13.blocker = new Ellipse();

            ampel14.body = new Ellipse();
            ampel14.blocker = new Ellipse();

            Brush[] trafficLightColors = new Brush[]
            {
                Brushes.Black,
                Brushes.Green,
                Brushes.Red,
            };

            //Kreuzung 1
            //Ampel1
            Grid.SetColumn(ampel.body, a);
            Grid.SetRow(ampel.body, b);
            ampel.body.Fill = trafficLightColors[1];

            //Set Ampel Blocker
            Grid.SetRow(ampel.blocker, Grid.GetRow(ampel.body) - 2);
            Grid.SetColumn(ampel.blocker, Grid.GetColumn(ampel.body));
            ampel.blocker.Fill = (new SolidColorBrush(Colors.Black));

            gr_mainGrid.Children.Add(ampel.body);

            //Ampel2
            Grid.SetColumn(ampel12.body, a);
            Grid.SetRow(ampel12.body, b - 5);
            ampel12.body.Fill = trafficLightColors[1];

            Grid.SetRow(ampel12.blocker, Grid.GetRow(ampel12.body));
            Grid.SetColumn(ampel12.blocker, Grid.GetColumn(ampel12.body) + 2);
            ampel12.blocker.Fill = (new SolidColorBrush(Colors.Black));

            gr_mainGrid.Children.Add(ampel12.body);

            //Ampel3
            Grid.SetColumn(ampel13.body, a + 5);
            Grid.SetRow(ampel13.body, b - 5);
            ampel13.body.Fill = trafficLightColors[1];

            Grid.SetRow(ampel13.blocker, Grid.GetRow(ampel13.body) + 2);
            Grid.SetColumn(ampel13.blocker, Grid.GetColumn(ampel13.body));
            ampel13.blocker.Fill = (new SolidColorBrush(Colors.Black));

            gr_mainGrid.Children.Add(ampel13.body);

            //Ampel4
            Grid.SetColumn(ampel14.body, a + 5);
            Grid.SetRow(ampel14.body, b);
            ampel14.body.Fill = trafficLightColors[1];

            Grid.SetRow(ampel14.blocker, Grid.GetRow(ampel14.body));
            Grid.SetColumn(ampel14.blocker, Grid.GetColumn(ampel14.body) - 2);
            ampel14.blocker.Fill = (new SolidColorBrush(Colors.Black));

            gr_mainGrid.Children.Add(ampel14.body);

            switchLight();
            
        }

        public void switchLight()
        {


            label.Content = timerCount.ToString();
            //traffic light Circuit 1
            if (cbstreet.IsChecked == true)
            {
                
                if (timerCount % 5 == 0)
                {
                    //TODO auto change color of ampel when isRed changes
                    if (ampel.isRed == true)
                    {
                        gr_mainGrid.Children.Remove(ampel.blocker);
                        ampel.isRed = false;
                        ampel.body.Fill = (new SolidColorBrush(Colors.Green));
                        
                        ampel12.isRed = true;
                        ampel12.body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(ampel12.blocker);

                        gr_mainGrid.Children.Remove(ampel13.blocker);
                        ampel13.isRed = false;
                        ampel13.body.Fill = (new SolidColorBrush(Colors.Green));
                        
                        ampel14.isRed = true;
                        ampel14.body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(ampel14.blocker);
                    }
                    else
                    {
                        
                        ampel.isRed = true;
                        ampel.body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(ampel.blocker);

                        gr_mainGrid.Children.Remove(ampel12.blocker);
                        ampel12.isRed = false;
                        ampel12.body.Fill = (new SolidColorBrush(Colors.Green));

                        ampel13.isRed = true;
                        ampel13.body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(ampel13.blocker);

                        gr_mainGrid.Children.Remove(ampel14.blocker);
                        ampel14.isRed = false;
                        ampel14.body.Fill = (new SolidColorBrush(Colors.Green));
                    }
                }

            }

            //traffic Light circuit 2
            if (cbclock.IsChecked == true)
            {

                if (timerCount % 5 == 0)
                {

                    if (ampel.isRed == true)
                    {
                        ampel.isRed = false;
                        ampel.body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(ampel.blocker);

                        ampel12.isRed = true;
                        ampel12.body.Fill = (new SolidColorBrush(Colors.Red));
                        
                        ampel13.isRed = false;
                        ampel13.body.Fill = (new SolidColorBrush(Colors.Red));
                      
                        ampel14.isRed = false;
                        ampel14.body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(ampel14.blocker);

                    }
                }
                if (timerCount%10==0)
                {
                    if (ampel12.isRed == true)
                    {
                        ampel.isRed = false;
                        ampel.body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(ampel.blocker);
                      
                        ampel12.isRed = false;
                        ampel12.body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(ampel12.blocker);

                        ampel13.isRed = true;
                        ampel13.body.Fill = (new SolidColorBrush(Colors.Red));

                        ampel14.isRed = false;
                        ampel14.body.Fill = (new SolidColorBrush(Colors.Red));
                    } 
                }
                if (timerCount%15==0)
                {
                    if (ampel13.isRed == true)
                    {
                        ampel.isRed = false;
                        ampel.body.Fill = (new SolidColorBrush(Colors.Red));

                        ampel12.isRed = false;
                        ampel12.body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(ampel12.blocker);
                        
                        ampel13.isRed = false;
                        ampel13.body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(ampel13.blocker);

                        ampel14.isRed = true;
                        ampel14.body.Fill = (new SolidColorBrush(Colors.Red));
                    } 
                }
                if (timerCount%20==0)
                {
                    if (ampel14.isRed == true)
                    {                      
                        ampel.isRed = true;
                        ampel.body.Fill = (new SolidColorBrush(Colors.Red));

                        ampel12.isRed = false;
                        ampel12.body.Fill = (new SolidColorBrush(Colors.Red));

                        ampel13.isRed = false;
                        ampel13.body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(ampel13.blocker);
                       
                        ampel14.isRed = false;
                        ampel14.body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(ampel14.blocker);
                    } 
                }

              }

          }

            



            #endregion TRAFFICLIGHT

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

        private void btnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            Analysis analysisWindow = new Analysis();
            analysisWindow.Show();
        }

        #region Checkbox
        private void cbstreet_Checked(object sender, RoutedEventArgs e)
        {
            cbclock.IsChecked = false;
            gr_mainGrid.Children.Add(ampel.blocker);
            gr_mainGrid.Children.Remove(ampel12.blocker);
            gr_mainGrid.Children.Add(ampel13.blocker);
            gr_mainGrid.Children.Remove(ampel14.blocker);

        }

        private void cbclock_Checked(object sender, RoutedEventArgs e)
        {
            cbstreet.IsChecked = false;
            

        }
        #endregion Checkbox
    }
}



