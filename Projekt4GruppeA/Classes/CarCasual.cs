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
    /// <summary>
    /// Klasse für Autos
    /// </summary>


    public class CarCasual 
    {
        //ID
        public double iD { get; set; }

        //Geschwindigkeit
        public int speed { get; set; }

        //Ort
        public int location { get; set; }

        //Form
        public Ellipse body { get; set; }

        /// <summary>
        /// Konstruktor
        /// - ID hochzählen
        /// - Standardwerte
        /// </summary>
        
        public CarCasual()
        {
            MainWindow.idCounter++;
            iD = MainWindow.idCounter;

            speed = 10;
            location = 0;
        }
    }
}
