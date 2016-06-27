﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes; // für Ellipse
using System.Windows.Media;
using System.Windows.Media.Imaging;

// für Brushes

namespace Projekt4GruppeA
{

    public class CarCasual 

    {
        //ID
        public double iD { get; set; }

        //Geschwindigkeit
        public int v { get; set; }

        //Ort
        public int location { get; set; }

        //Form
        public Ellipse body { get; set; }

        //Konstruktor, ID hochzählen, Standardparameter  
        public CarCasual(int columnSpawn, int rowSpawn)
        {
            Random rnd = new Random();
            
            MainWindow.idCounter++;
            iD = MainWindow.idCounter;
            v = 1;
            location = 0;

            body = new Ellipse();
            
            body.Height = 40;
            body.Width = 40;
            
            Grid.SetRowSpan(body, 40);
            Grid.SetColumnSpan(body, 40);
            body.VerticalAlignment = VerticalAlignment.Top;
            body.HorizontalAlignment = HorizontalAlignment.Left;

            ImageBrush ib = new ImageBrush();

            
                // x = original image
                var x = new BitmapImage(new Uri("Icons/" + rnd.Next(1, 9) + ".png", UriKind.Relative));
                ib.ImageSource = x;
                body.Fill = ib;
            
            
            if (columnSpawn == 30)
            {

                // y = rotated image
                var y = new BitmapImage();
                y.BeginInit();
                y.UriSource = x.UriSource;
                y.Rotation = Rotation.Rotate180;
                y.EndInit();

                ib.ImageSource = y;
                body.Fill = ib;
            }

            else if (columnSpawn == 130)
            {              
                var y = new BitmapImage();
                y.BeginInit();
                y.UriSource = x.UriSource;
                y.Rotation = Rotation.Rotate270;
                y.EndInit();

                ib.ImageSource = y;
                body.Fill = ib;
            }

            else if (columnSpawn == 140)
            {
                var y = new BitmapImage();
                y.BeginInit();
                y.UriSource = x.UriSource;
                y.Rotation = Rotation.Rotate90;
                y.EndInit();

                ib.ImageSource = y;
                body.Fill = ib;
            }

            Grid.SetColumn(body, columnSpawn);
            Grid.SetRow(body, rowSpawn);
        }


    }
}
