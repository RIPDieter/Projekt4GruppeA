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

        Analysis analysisWindow = new Analysis();

        //Klasse TrafficlightCircuit instanziieren

        int timerCount;

        public static List<CarCasual> carListLeftToRight = new List<CarCasual>();
        public static List<CarCasual> carListRightToLeft = new List<CarCasual>();
        public static List<CarCasual> carListTopToBottom = new List<CarCasual>();
        public static List<CarCasual> carListBottomToTop = new List<CarCasual>();

        public static List<Trafficlight> trafficLightList = new List<Trafficlight>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            createGrid(40, 200);

            //spawntrafficlight(30,7);
            spawnTrafficLight(30, 7, 30, 5);
            spawnTrafficLight(30, 2, 32, 2);
            spawnTrafficLight(35, 2, 35, 4);
            spawnTrafficLight(35, 7, 33, 7);
            drawstreet();
           
        }
        public void drawstreet()
        {
           

            int number = 20;
            int width = 20;
            int height = 20;
            int top = 20;
            int left = 20;

            for (int i = 0; i < number; i++)
            {
                // Create the rectangle
                Rectangle rec = new Rectangle()
                {
                    Width = width,
                    Height = height,
                    Fill = Brushes.Gray,
                    
                 
                };

                Grid.SetRow(rec, 8);
                Grid.SetColumn(rec, i);
                draw_Grid.Children.Add(rec);
                
            }



            //for (int i = 2; i < 20; i++)
            //{

            //    r[i].Height = 10;
            //    r[i].Width = 10;
            //    r[i].Fill = new SolidColorBrush(Colors.Gray);
            //    Grid.SetRow(r[i], i);
            //    Grid.SetColumn(r[i], 9);
            //    gr_mainGrid.Children.Add(r[i]);
            //}



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

            for (int i = 0; i < rowCount; i++)
            {
                var gridRow = new RowDefinition();
                gridRow.Height = new GridLength(20);
                draw_Grid.RowDefinitions.Add(gridRow);
            }
            for (int i = 0; i < columnCount; i++)
            {
                var gridColumn = new ColumnDefinition();
                gridColumn.Width = new GridLength(20);
                draw_Grid.ColumnDefinitions.Add(gridColumn);
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

        private void cbstreet_Click(object sender, RoutedEventArgs e)
        {
            cbclock.IsChecked = false;
            gr_mainGrid.Children.Remove(trafficLightList[0].blocker);
            gr_mainGrid.Children.Remove(trafficLightList[1].blocker);
            gr_mainGrid.Children.Remove(trafficLightList[2].blocker);
            gr_mainGrid.Children.Remove(trafficLightList[3].blocker);


            gr_mainGrid.Children.Add(trafficLightList[0].blocker);
            trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Red));
            gr_mainGrid.Children.Add(trafficLightList[2].blocker);
            trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Red));


        }

        private void cbclock_Click(object sender, RoutedEventArgs e)
        {
            cbstreet.IsChecked = false;
            gr_mainGrid.Children.Remove(trafficLightList[0].blocker);
            gr_mainGrid.Children.Remove(trafficLightList[1].blocker);
            gr_mainGrid.Children.Remove(trafficLightList[2].blocker);
            gr_mainGrid.Children.Remove(trafficLightList[3].blocker);

            gr_mainGrid.Children.Add(trafficLightList[1].blocker);
            trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Green));
            gr_mainGrid.Children.Add(trafficLightList[2].blocker);
            gr_mainGrid.Children.Add(trafficLightList[3].blocker);
            trafficLightList[1].body.Fill = (new SolidColorBrush(Colors.Red));
            trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Red));
            trafficLightList[3].body.Fill = (new SolidColorBrush(Colors.Red));

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

           
            switchLight();

            //spawnCars(Convert.ToInt16(sldSpawn.Value));
            // trafficlightCircuit.switchLight();

            int z = 10 - (Convert.ToInt16(sldSpawn.Value));

            if (rnd.Next(0, z) == 0)
            {
                spawnCars(1);
            }

            moveCars();

        }


        #endregion TIMER RELATED

        #region SPAWN CARS

        public void spawnCars(int carsToSpawn)
        {
            if (checkSpawn() == false)
            {   
                //cars left to right
                if (rnd.Next(0, 3) == 0)
                {
                    CarCasual car = new CarCasual(2, 5);                   
                    gr_mainGrid.Children.Add(car.body);
                    carListLeftToRight.Add(car);
                }
                //cars right to left
                else if (rnd.Next(0, 3) == 1)
                {
                    CarCasual car = new CarCasual(30, 4);

                    gr_mainGrid.Children.Add(car.body);
                    carListRightToLeft.Add(car);
                }
                //cars bottom to top, to to bottom
                else
                {
                    CarCasual car = new CarCasual(31, 0);
                    CarCasual car1 = new CarCasual(34, 15);
                    gr_mainGrid.Children.Add(car1.body);
                    carListBottomToTop.Add(car1);

                    gr_mainGrid.Children.Add(car.body);
                    carListTopToBottom.Add(car);
                }
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
                else if (Grid.GetColumn(uiE) == 30 && Grid.GetRow(uiE) == 4)
                {
                    return false;
                }
                else if (Grid.GetColumn(uiE) == 31 && Grid.GetRow(uiE) == 0)
                {
                    return false;
                }
                else if (Grid.GetColumn(uiE) == 34 && Grid.GetRow(uiE) == 15)
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
            #region  LEFTtoRIGHT

            foreach (CarCasual thisCar in carListLeftToRight)
            {
                var gapSize = checkGapSize(thisCar);

                if (Grid.GetColumn(thisCar.body) <= 40)
                {
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
                else
                {
                    //carListLeftToRight.Remove(thisCar);
                    gr_mainGrid.Children.Remove(thisCar.body);
                }

                if (gapSize == 0  && thisCar.v <= 0)
                {
                    analysisWindow.label.Content = "ACHTUNG STAU!!";
                }
            }

            #endregion  LEFTtoRIGHT 

            #region RIGHTtoLEFT

            foreach (CarCasual thisCar in carListRightToLeft)
            {
                var gapSize = checkGapSize(thisCar);

                if (Grid.GetColumn(thisCar.body) >= 3)
                {
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
                        CurrentColumn -= thisCar.v;
                        Grid.SetColumn(thisCar.body, CurrentColumn);
                    }
                    //Beschleunigen
                    else if (gapSize > thisCar.v && thisCar.v < 2)
                    {
                        var CurrentColumn = Grid.GetColumn(thisCar.body);
                        CurrentColumn -= thisCar.v;
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
                        CurrentColumn -= thisCar.v;
                        Grid.SetColumn(thisCar.body, CurrentColumn);

                        if (rnd.Next(0, 3) == 2)
                        {
                            thisCar.v--;
                        }
                    }

                }
                else
                {
                    //carListRightToLeft.Remove(thisCar);
                    gr_mainGrid.Children.Remove(thisCar.body);
                }

            }

            #endregion RIGHTtoLEFT

            #region TOPtoBOTTOM

            foreach (CarCasual thisCar in carListTopToBottom)
            {
                var gapSize = checkGapSize(thisCar);

                //Stehen
                if (Grid.GetRow(thisCar.body) <= 15)
                {
                    if (gapSize == 0)
                    {
                        thisCar.v = 0;
                    }
                    // Bremsen
                    else if (gapSize <= thisCar.v)
                    {
                        thisCar.v = gapSize;
                        var CurrentRow = Grid.GetRow(thisCar.body);
                        CurrentRow += thisCar.v;
                        Grid.SetRow(thisCar.body, CurrentRow);
                    }
                    //Beschleunigen
                    else if (gapSize > thisCar.v && thisCar.v < 2)
                    {
                        var CurrentRow = Grid.GetRow(thisCar.body);
                        CurrentRow += thisCar.v;
                        Grid.SetRow(thisCar.body, CurrentRow);

                        if (rnd.Next(0, 3) == 2 && thisCar.v > 0)
                        {
                            thisCar.v--;
                        }
                        thisCar.v++;

                    }
                    // Höchstgeschw.
                    else if (gapSize > thisCar.v && thisCar.v >= 2)
                    {
                        var CurrentRow = Grid.GetRow(thisCar.body);
                        CurrentRow += thisCar.v;
                        Grid.SetRow(thisCar.body, CurrentRow);

                        if (rnd.Next(0, 3) == 2)
                        {
                            thisCar.v--;
                        }
                    } 
                }
                else
                {
                    gr_mainGrid.Children.Remove(thisCar.body);
                }

            }

            #endregion TOPtoBOTTOM
          
            #region BOTTOMtoTOP

            foreach (CarCasual thisCar in carListBottomToTop)
            {
                var gapSize = checkGapSize(thisCar);

                //Stehen
                if (Grid.GetRow(thisCar.body) >= 3)
                {
                    if (gapSize == 0)
                    {
                        thisCar.v = 0;
                    }
                    // Bremsen
                    else if (gapSize <= thisCar.v)
                    {
                        thisCar.v = gapSize;
                        var CurrentRow = Grid.GetRow(thisCar.body);
                        CurrentRow -= thisCar.v;
                        Grid.SetRow(thisCar.body, CurrentRow);
                    }
                    //Beschleunigen
                    else if (gapSize > thisCar.v && thisCar.v < 2)
                    {
                        var CurrentRow = Grid.GetRow(thisCar.body);
                        CurrentRow -= thisCar.v;
                        Grid.SetRow(thisCar.body, CurrentRow);

                        if (rnd.Next(0, 3) == 2 && thisCar.v > 0)
                        {
                            thisCar.v--;
                        }
                        thisCar.v++;

                    }
                    // Höchstgeschw.
                    else if (gapSize > thisCar.v && thisCar.v >= 2)
                    {
                        var CurrentRow = Grid.GetRow(thisCar.body);
                        CurrentRow -= thisCar.v;
                        Grid.SetRow(thisCar.body, CurrentRow);

                        if (rnd.Next(0, 3) == 2)
                        {
                            thisCar.v--;
                        }
                    } 

                }
                else
                {
                    gr_mainGrid.Children.Remove(thisCar.body);
                }

            }
            #endregion BOTTOMtoTop

        }


        #endregion  MOVE

        #region GAP

        private int checkGapSize(CarCasual thisCar)
        {
            var placeOfCarColumn = Grid.GetColumn(thisCar.body);
            var placeOfCarRow = Grid.GetRow(thisCar.body);

            #region  LEFTtoRIGHT

            if (placeOfCarRow == 5)
            {

                for (var searchPointColumn = placeOfCarColumn + 1;
                    searchPointColumn < gr_mainGrid.ColumnDefinitions.Count;
                    searchPointColumn++)
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

            #endregion  LEFTtoRIGHT

            #region  RIGHTtoLEFT

            if (placeOfCarRow == 4)
            {
                for (var searchPointColumn = placeOfCarColumn - 1;
                    searchPointColumn >= 0;
                    searchPointColumn--)
                {
                    for (int j = 0; j < gr_mainGrid.Children.Count; j++)
                    {
                        UIElement uiE = gr_mainGrid.Children[j];
                        if (Grid.GetColumn(uiE) == searchPointColumn && Grid.GetRow(uiE) == placeOfCarRow)
                        {
                            var gapSize = placeOfCarColumn - Grid.GetColumn(uiE) - 1;
                            return gapSize;
                        }
                    }
                }
                return 5;
            }


            #endregion RIGHTtoLEFT

            #region TOPtoBOTTOM

            if (placeOfCarColumn == 30)
            {

                for (var searchPointRow = placeOfCarRow + 1;
                    searchPointRow < gr_mainGrid.RowDefinitions.Count;
                    searchPointRow++)
                {
                    for (int j = 0; j < gr_mainGrid.Children.Count; j++)
                    {
                        UIElement uiE = gr_mainGrid.Children[j];
                        if (Grid.GetRow(uiE) == searchPointRow && Grid.GetColumn(uiE) == placeOfCarColumn)
                        {
                            var gapSize = Grid.GetRow(uiE) - placeOfCarRow - 1;
                            return gapSize;
                        }
                    }
                }
                return 5;
            }


            return 5;
        }
        #endregion TOPtoBOTTOM
        #region BOTTOMtoTOP



        #endregion BOTTOMtoTOP
        #endregion GAP

        #region TRAFFICLIGHT


        public void spawnTrafficLight(int trafficLightColumn, int trafficLightRow, int blockerColumn, int blockerRow)
        {
            Trafficlight ampel = new Trafficlight(trafficLightColumn, trafficLightRow, blockerColumn, blockerRow);
            trafficLightList.Add(ampel);
            gr_mainGrid.Children.Add(ampel.body);
            //gr_mainGrid.Children.Add(ampel.blocker);
        }

        public void switchLight()
        {
            //traffic light Circuit 1
            if (cbstreet.IsChecked == true)
            {
                if (timerCount%5 == 0)
                {
                    //TODO auto change color of ampel when isRed changes
                    if (trafficLightList[0].isRed == true)
                    {
                        gr_mainGrid.Children.Remove(trafficLightList[0].blocker);
                        trafficLightList[0].isRed = false;
                        trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Green));

                        trafficLightList[1].isRed = true;
                        trafficLightList[1].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[1].blocker);

                        gr_mainGrid.Children.Remove(trafficLightList[2].blocker);
                        trafficLightList[2].isRed = false;
                        trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Green));

                        trafficLightList[3].isRed = true;
                        trafficLightList[3].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[3].blocker);
                    }
                    else
                    {

                        trafficLightList[0].isRed = true;
                        trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[0].blocker);

                        gr_mainGrid.Children.Remove(trafficLightList[1].blocker);
                        trafficLightList[1].isRed = false;
                        trafficLightList[1].body.Fill = (new SolidColorBrush(Colors.Green));

                        trafficLightList[2].isRed = true;
                        trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[2].blocker);

                        gr_mainGrid.Children.Remove(trafficLightList[3].blocker);
                        trafficLightList[3].isRed = false;
                        trafficLightList[3].body.Fill = (new SolidColorBrush(Colors.Green));
                    }
                }
            }


            //traffic Light circuit 2
            if (cbclock.IsChecked == true)
            {

                if (timerCount%5 == 0)
                {

                    if (trafficLightList[0].isRed == true)
                    {
                        trafficLightList[0].isRed = false;
                        trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(trafficLightList[0].blocker);

                        trafficLightList[1].isRed = true;
                        trafficLightList[1].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[2].isRed = false;
                        trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[3].isRed = false;
                        trafficLightList[3].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[3].blocker);

                    }
                }
                if (timerCount%10 == 0)
                {
                    if (trafficLightList[1].isRed == true)
                    {
                        trafficLightList[0].isRed = false;
                        trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[0].blocker);

                        trafficLightList[1].isRed = false;
                        trafficLightList[1].body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(trafficLightList[1].blocker);

                        trafficLightList[2].isRed = true;
                        trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[3].isRed = false;
                        trafficLightList[3].body.Fill = (new SolidColorBrush(Colors.Red));
                    }
                }
                if (timerCount%15 == 0)
                {
                    if (trafficLightList[2].isRed == true)
                    {
                        trafficLightList[0].isRed = false;
                        trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[1].isRed = false;
                        trafficLightList[1].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[1].blocker);

                        trafficLightList[2].isRed = false;
                        trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(trafficLightList[2].blocker);

                        trafficLightList[3].isRed = true;
                        trafficLightList[3].body.Fill = (new SolidColorBrush(Colors.Red));
                    }
                }
                if (timerCount%20 == 0)
                {
                    if (trafficLightList[3].isRed == true)
                    {
                        trafficLightList[0].isRed = true;
                        trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[1].isRed = false;
                        trafficLightList[1].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[2].isRed = false;
                        trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[2].blocker);

                        trafficLightList[3].isRed = false;
                        trafficLightList[3].body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(trafficLightList[3].blocker);
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


        #region ANALYSIS


        private void btnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            
            analysisWindow.Show();

            analysisWindow.label1.Content = timerCount.ToString();


            
            
            
        }


        #endregion ANALYSIS

    }
}