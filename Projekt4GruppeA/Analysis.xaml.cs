using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Projekt4GruppeA
{
    /// <summary>
    /// Interaktionslogik für Analysis.xaml
    /// </summary>
    public partial class Analysis : Window
    {

        Bitmap bmp;
        Graphics z;
        

        public Analysis()
        {
            InitializeComponent();

            //System.Drawing.Bitmap b = new Bitmap(imag);
            //z = Graphics.FromImage(b);
        }

        private void image_Paint(object sender, PaintEventArgs e)
        {


            Graphics z = e.Graphics;
            z.SmoothingMode = SmoothingMode.AntiAlias;            

            System.Drawing.Drawing2D.Matrix myMatrix = new System.Drawing.Drawing2D.Matrix();

            System.Drawing.Pen pKoord = new System.Drawing.Pen(System.Drawing.Color.Gray, 0.01f);

                    
        }


    }

}

