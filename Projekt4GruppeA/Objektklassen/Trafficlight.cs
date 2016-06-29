using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Media.Imaging;

namespace Projekt4GruppeA
{
    public class Trafficlight 
    {
        //place
        public int location { get; set; }

        //shape
        public Ellipse body { get; set; }

        //UIElement on streets blocking cars
        public Ellipse blocker { get; set; }

        //state
        public bool isRed { get; set; }

        //constructor 
        public Trafficlight(int trafficLightcolumn, int trafficLightRow, int blockerColumn, int blockerRow)
        {
            Brush[] trafficLightColors = new Brush[]
           {
                Brushes.Black,
                Brushes.Green,
                Brushes.Red,
           };

            body = new Ellipse();
            body.Width = 15;
            body.Height = 15;
            isRed = true;
            
            Grid.SetRowSpan(body, trafficLightRow);
            Grid.SetColumnSpan(body,trafficLightcolumn );
           
            body.Fill = trafficLightColors[1];

            blocker = new Ellipse();
            blocker.Width = 5;
            blocker.Height = 5;

            Grid.SetColumn(blocker, blockerColumn);
            Grid.SetRow(blocker, blockerRow);

            //blocker.Fill = (new SolidColorBrush(Colors.Black));
        }
    }
}
