using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Shapes; // für Ellipse
using System.Windows.Media; // für Brushes

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
        public CarCasual()
        {
            MainWindow.idCounter++;
            iD = MainWindow.idCounter;

            v = 1;
            location = 0;

           
        }
    }
}
