using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Projekt4GruppeA
{
    public class Trafficlight 
    {
        //Ort
        public int location { get; set; }

        //Form
        public Ellipse body { get; set; }

        //Blocker UIElement
        public Ellipse blocker { get; set; }

        //Zustand
        public bool isRed { get; set; }
        
        //Konstruktor - im Moment nicht benutzt  
        public Trafficlight()
        {
            Ellipse body = new Ellipse();
            body.Width = 10;
            body.Height = 10;
            isRed = true;
        }

        public Trafficlight(int trafficLightcolumn, int trafficLightRow, int blockerColumn, int blockerRow)
        {
            Brush[] trafficLightColors = new Brush[]
           {
                Brushes.Black,
                Brushes.Green,
                Brushes.Red,
           };

            body = new Ellipse();
            body.Width = 10;
            body.Height = 10;
            isRed = true;

            Grid.SetColumn(body, trafficLightcolumn);
            Grid.SetRow(body, trafficLightRow);

            body.Fill = trafficLightColors[1];

            blocker = new Ellipse();
            blocker.Width = 10;
            blocker.Height = 10;

            Grid.SetColumn(blocker, blockerColumn);
            Grid.SetRow(blocker, blockerRow);

            blocker.Fill = (new SolidColorBrush(Colors.Black));
        }
    }
}
