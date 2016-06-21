using System;
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

            Brush[] carColors = new Brush[]
            {
            Brushes.Red,
            Brushes.Blue,
            Brushes.Black,
            Brushes.Green,
            Brushes.Yellow,
            Brushes.Fuchsia
            };
            
            body.Height = 20;
            body.Width = 40;

            Grid.SetRowSpan(body, 20);
            Grid.SetColumnSpan(body, 40);
            body.VerticalAlignment = VerticalAlignment.Top;
            body.HorizontalAlignment = HorizontalAlignment.Left;

            ImageBrush ib = new ImageBrush();
            var x =
                new BitmapImage(new Uri("Icons/" + rnd.Next(1, 9) + ".png", UriKind.Relative));
         
            //TODO Rotationnotwurkung
            x.Rotation = Rotation.Rotate180;
            ib.ImageSource = x;
            
            
            body.Fill = ib;

            //for debugging
            //car.body.Fill = carColors[rnd.Next(carColors.Length)];
            
            Grid.SetColumn(body, columnSpawn);
            Grid.SetRow(body, rowSpawn);
        }


    }
}
