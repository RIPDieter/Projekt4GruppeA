using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Projekt4GruppeA.Classes
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
            body.Width = 25;
            body.Height = 25;
            isRed = false;
        }
    }
}
