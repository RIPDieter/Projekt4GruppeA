﻿using System;
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

        int LeftRightColumn = 30;
        int LeftRightRow = 80;

        int RightLeftColumn = 300;
        int RightLeftRow = 75;

        int TopBottomColumn = 130;
        int TopBottomRow = 20;

        int BottomTopColumn =135;
        int BottomTopRow = 130;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            createGrid(400, 1000);
           
            //first intersection
            spawnTrafficLight(109, 180, 60, LeftRightRow);
            spawnTrafficLight(109, 140, 60, 74);
            spawnTrafficLight(149, 140, 70, RightLeftRow);
            spawnTrafficLight(149, 180, 70, 85);

            //second intersection
            spawnTrafficLight(249, 180, 129, LeftRightRow);
            spawnTrafficLight(249, 140, TopBottomColumn, 74);
            spawnTrafficLight(289, 140, 141, RightLeftRow);
            spawnTrafficLight(289, 180, BottomTopColumn, 85);

            //third intersection
            spawnTrafficLight(392, 180, 200, LeftRightRow);
            spawnTrafficLight(392, 140, 200, 74);
            spawnTrafficLight(432, 140, 220, RightLeftRow);
            spawnTrafficLight(432, 180, 220, 85);

            ImageBrush myBrush = new ImageBrush();
            myBrush.ImageSource =
                 new BitmapImage(new Uri("Icons/Street.png", UriKind.Relative));
            gr_mainGrid.Background = myBrush;

        }

        #region GRID

        private void createGrid(int rowCount, int columnCount)
        {
            for (int i = 0; i < rowCount; i++)
            {
                var gridRow = new RowDefinition();
                gridRow.Height = new GridLength(5);
                gr_mainGrid.RowDefinitions.Add(gridRow);
            }
            for (int i = 0; i < columnCount; i++)
            {
                var gridColumn = new ColumnDefinition();
                gridColumn.Width = new GridLength(5);
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

            //gr_mainGrid.Children.Add(trafficLightList[1].blocker);
            //trafficLightList[0].body.Fill = (new SolidColorBrush(Colors.Green));
            //gr_mainGrid.Children.Add(trafficLightList[2].blocker);
            //gr_mainGrid.Children.Add(trafficLightList[3].blocker);
            //trafficLightList[1].body.Fill = (new SolidColorBrush(Colors.Red));
            //trafficLightList[2].body.Fill = (new SolidColorBrush(Colors.Red));
            //trafficLightList[3].body.Fill = (new SolidColorBrush(Colors.Red));

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

            switchLight(0, 1, 2, 3, cbstreet.IsChecked, cbclock.IsChecked, Convert.ToInt32(sldLight1.Value));
            switchLight(4,5,6,7 ,cbstreet2.IsChecked, cbclock2.IsChecked, Convert.ToInt32(sldLight2.Value));
            switchLight(8, 9, 10, 11, cbstreet3.IsChecked, cbclock3.IsChecked, Convert.ToInt32(sldLight3.Value));
           
            //spawnCars(Convert.ToInt16(sldSpawn.Value));
            // trafficlightCircuit.switchLight();

            int z = 10 - (Convert.ToInt16(sldSpawn.Value));

            if (rnd.Next(0, z) == 0)
            {
                spawnCars(1);
            }

            moveCars();

            analysisWindow.txtTimer.Text = timerCount.ToString();
            

        }


        #endregion TIMER RELATED

        #region SPAWN CARS

        public void spawnCars(int carsToSpawn)
        {
            if (checkSpawn() == true)
            {   
                //cars left to right
                if (rnd.Next(0, 3) == 0)
                {
                    CarCasual car = new CarCasual(LeftRightColumn, LeftRightRow);
                 
                    gr_mainGrid.Children.Add(car.body);
                    carListLeftToRight.Add(car);
                }
                //cars right to left
                else if (rnd.Next(0, 3) == 1)
                {
                    CarCasual car = new CarCasual(RightLeftColumn, RightLeftRow);

                    gr_mainGrid.Children.Add(car.body);
                    carListRightToLeft.Add(car);
                }
                //cars top to Bottom
                else if((rnd.Next(0, 3) == 2))
                {
                    CarCasual car = new CarCasual(TopBottomColumn, TopBottomRow);
                   

                    gr_mainGrid.Children.Add(car.body);
                    carListTopToBottom.Add(car);
                }
                //cars bottom to top
                else
                {
                    CarCasual car = new CarCasual(BottomTopColumn, BottomTopRow);
                    gr_mainGrid.Children.Add(car.body);
                    carListBottomToTop.Add(car);
                }
            }
        }

        private bool checkSpawn()
        {
            for (int j = 0; j < gr_mainGrid.Children.Count; j++)
            {
                UIElement uiE = gr_mainGrid.Children[j];
                if (Grid.GetColumn(uiE) == LeftRightColumn + 10 && Grid.GetRow(uiE) == LeftRightRow)
                {
                    return false;
                }
                else if (Grid.GetColumn(uiE) == RightLeftColumn - 10  && Grid.GetRow(uiE) == RightLeftRow)
                {
                    return false;
                }
                else if (Grid.GetColumn(uiE) == TopBottomColumn && Grid.GetRow(uiE) == TopBottomRow + 10)
                {
                    return false;
                }
                else if (Grid.GetColumn(uiE) == BottomTopColumn&& Grid.GetRow(uiE) == BottomTopRow - 10)
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

                if (Grid.GetColumn(thisCar.body) <= 700)
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
                    else if (gapSize > thisCar.v && thisCar.v < 5)
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
                    else if (gapSize > thisCar.v && thisCar.v >= 5)
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

                if (gapSize == 0 && thisCar.v == 0)
                {
                    analysisWindow.label1.Content = "STAUGEFAHR!!";
                    
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

                if (gapSize == 0 && thisCar.v <= 0)
                {
                    analysisWindow.label1.Content = "STAUGEFAHR!!";
                }

            }

            #endregion RIGHTtoLEFT

            #region TOPtoBOTTOM

            foreach (CarCasual thisCar in carListTopToBottom)
            {
                var gapSize = checkGapSize(thisCar);

                //Stehen
                if (Grid.GetRow(thisCar.body) <= 300)
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

                if (gapSize == 0 && thisCar.v <= 0)
                {
                    analysisWindow.label1.Content = "STAUGEFAHR!!";
                }

            }

            #endregion TOPtoBOTTOM
          
            #region BOTTOMtoTOP

            foreach (CarCasual thisCar in carListBottomToTop)
            {
                var gapSize = checkGapSize(thisCar);

                //Stehen
                if (Grid.GetRow(thisCar.body) >= 20)
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

                    if (gapSize == 0 && thisCar.v == 0)
                    {
                        analysisWindow.label1.Content = "STAUGEFAHR!!";
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

            if (placeOfCarRow == LeftRightRow)
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
                            var gapSize = Grid.GetColumn(uiE) - placeOfCarColumn - 7;
                            return gapSize;
                        }
                    }
                }
                return 5;
            }

            #endregion  LEFTtoRIGHT

            #region  RIGHTtoLEFT

            if (placeOfCarRow == RightLeftRow)
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
                            var gapSize = placeOfCarColumn - Grid.GetColumn(uiE) - 6;
                            return gapSize;
                        }
                    }
                }
                return 5;
            }


            #endregion RIGHTtoLEFT

            #region TOPtoBOTTOM

            if (placeOfCarColumn == TopBottomColumn)
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
                            var gapSize = Grid.GetRow(uiE) - placeOfCarRow - 6;
                            return gapSize;
                        }
                    }
                }
                return 5;
            }         

            #endregion TOPtoBOTTOM

            #region BOTTOMtoTOP
            if (placeOfCarColumn == BottomTopColumn)
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
                            var gapSize = Grid.GetRow(uiE) - placeOfCarRow - 6;
                            return gapSize;
                        }
                    }
                }
                return 5;
            }

           return 5;

        }

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

        public void switchLight(int a, int b, int c, int d, bool? e, bool?f, int g)
        {
            //traffic light Circuit 1
            if (e == true)
            {
                if (timerCount%g == 0)
                {
                    //TODO auto change color of ampel when isRed changes
                    if (trafficLightList[a].isRed == true)
                    {
                        gr_mainGrid.Children.Remove(trafficLightList[a].blocker);
                        trafficLightList[a].isRed = false;
                        trafficLightList[a].body.Fill = (new SolidColorBrush(Colors.Green));

                        trafficLightList[b].isRed = true;
                        trafficLightList[b].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[b].blocker);

                        gr_mainGrid.Children.Remove(trafficLightList[c].blocker);
                        trafficLightList[c].isRed = false;
                        trafficLightList[c].body.Fill = (new SolidColorBrush(Colors.Green));

                        trafficLightList[d].isRed = true;
                        trafficLightList[d].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[d].blocker);
                    }
                    else
                    {

                        trafficLightList[a].isRed = true;
                        trafficLightList[a].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[a].blocker);

                        gr_mainGrid.Children.Remove(trafficLightList[b].blocker);
                        trafficLightList[b].isRed = false;
                        trafficLightList[b].body.Fill = (new SolidColorBrush(Colors.Green));

                        trafficLightList[c].isRed = true;
                        trafficLightList[c].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[c].blocker);

                        gr_mainGrid.Children.Remove(trafficLightList[d].blocker);
                        trafficLightList[d].isRed = false;
                        trafficLightList[d].body.Fill = (new SolidColorBrush(Colors.Green));
                    }
                }
            }


            //traffic Light circuit 2
            if (f == true)
            {

                if (timerCount%g == 0)
                {

                    if (trafficLightList[a].isRed == true)
                    {
                        trafficLightList[a].isRed = false;
                        trafficLightList[a].body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(trafficLightList[a].blocker);

                        trafficLightList[b].isRed = true;
                        trafficLightList[b].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[c].isRed = false;
                        trafficLightList[c].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[d].isRed = false;
                        trafficLightList[d].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[d].blocker);

                    }
                }
                if (timerCount%2*g == 0)
                {
                    if (trafficLightList[b].isRed == true)
                    {
                        trafficLightList[a].isRed = false;
                        trafficLightList[a].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[a].blocker);

                        trafficLightList[b].isRed = false;
                        trafficLightList[b].body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(trafficLightList[b].blocker);

                        trafficLightList[c].isRed = true;
                        trafficLightList[c].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[d].isRed = false;
                        trafficLightList[d].body.Fill = (new SolidColorBrush(Colors.Red));
                    }
                }
                if (timerCount%3*g == 0)
                {
                    if (trafficLightList[c].isRed == true)
                    {
                        trafficLightList[a].isRed = false;
                        trafficLightList[a].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[b].isRed = false;
                        trafficLightList[b].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[b].blocker);

                        trafficLightList[c].isRed = false;
                        trafficLightList[c].body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(trafficLightList[c].blocker);

                        trafficLightList[d].isRed = true;
                        trafficLightList[d].body.Fill = (new SolidColorBrush(Colors.Red));
                    }
                }
                if (timerCount%4*g == 0)
                {
                    if (trafficLightList[d].isRed == true)
                    {
                        trafficLightList[a].isRed = true;
                        trafficLightList[a].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[b].isRed = false;
                        trafficLightList[b].body.Fill = (new SolidColorBrush(Colors.Red));

                        trafficLightList[c].isRed = false;
                        trafficLightList[c].body.Fill = (new SolidColorBrush(Colors.Red));
                        gr_mainGrid.Children.Add(trafficLightList[c].blocker);

                        trafficLightList[d].isRed = false;
                        trafficLightList[d].body.Fill = (new SolidColorBrush(Colors.Green));
                        gr_mainGrid.Children.Remove(trafficLightList[d].blocker);
                    }
                }

            }

        }





    

    #endregion TRAFFICLIGHT

        #region SLIDER

        private void sldTime_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            //TODO Umdrehen
            timer.Interval = TimeSpan.FromMilliseconds(sldTime.Value);
        }

        private void sldSpawn_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
        }

        private void sldLight1_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void sldLight2_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }

        private void sldLight3_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }



        #endregion SLIDER

        #region ANALYSIS


        private void btnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            
            analysisWindow.Show();

                        
            
        }






        #endregion ANALYSIS

      
    }
}