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

        //Bitmap bmp;
        //Graphics z;

        //const float minX = -0.5f;
        //const float maxX = 7.5f;
        //const float minY = -1.1f;
        //const float maxY = 1.1f;


        public Analysis()
        {
            InitializeComponent();         
            
        }





        //private void ImageSource(object sender, PaintEventArgs e)
        //{
        //    float breite = maxX - minX;
        //    float hoehe = maxY - minY;

        //    z.SmoothingMode = SmoothingMode.AntiAlias;

        //    Matrix chart = new Matrix();
        //    chart.Scale((float)image.ActualHeight / hoehe, (float)image.ActualWidth / breite);

        //    chart.Translate(-minX, -minY, MatrixOrder.Prepend);

        //    z.Transform = chart;

        //    Pen pkoord = new Pen(Color.Black, 0.01f);

        //    z.DrawLine(pkoord, 0, 0, 7, 0);
        //    z.DrawLine(pkoord, 0, -1, 0, -1);

        //    PointF[] kurve = new PointF[1000];
        //    for (int i = 0; i < kurve.Length; i++)
        //    {
        //        kurve[i].X = (float)i * 7 / 1000f;
        //        kurve[i].Y = -(float)Math.Sin(kurve[i].X * 2 / 10f);
        //    }

        //    Pen pKurve = new Pen(Color.Red, 0.01f);
        //    z.DrawLines(pKurve, kurve);


        //}





        //{
        //    float minX = -1.1f;
        //    float maxX = 1.1f;

        //    float minY = -1.1f;
        //    float maxY = 1.1f;

        //    float breite = maxX - minX;
        //    float hoehe = maxY - minY; // Deklarieren von Maßen

        //System.Drawing.Drawing2D.Matrix myMatrix = new System.Drawing.Drawing2D.Matrix();
        //myMatrix.Scale(image.RenderSize.Width / (double)breite, image.RenderSize.Height / (double)hoehe); // Skalierung der Matrix
        //myMatrix.Translate(-minX, -minY, MatrixOrder.Prepend);




    }



        //private void image_Paint(object sender, PaintEventArgs e)
        //{
        //    //const float a = 0;
        //    //const float b = 20; // for trying

        //    //Graphics z = e.Graphics;
        //    //z.SmoothingMode = SmoothingMode.AntiAlias;            

        //    //System.Drawing.Drawing2D.Matrix myMatrix = new System.Drawing.Drawing2D.Matrix(); // generating a matrix

        //    //System.Drawing.Pen pKoord = new System.Drawing.Pen(System.Drawing.Color.Black, 10f);

        //    //z.DrawLine(pKoord, a, b , b, a);


        //    Line objline = new Line();

        //    objline.Stroke = System.Windows.Media.Brushes.Black; 
        //    objline.Fill = System.Windows.Media.Brushes.Black;

        //    objline.X1 = (image.ActualWidth + image.Margin.Left);
        //    objline.Y1 = (image.ActualHeight + image.Margin.Bottom);

        //    objline.X2 = image.ActualWidth + 20;
        //    objline.Y2 = image.ActualHeight + 20;

        //#############################################################################

        #region GRID_CHART

        //private void grid(int row, int column)
        //{
        //    var Crow = new RowDefinition();
        //    Crow.Height = new GridLength(20);
        //    Grid1.RowDefinitions.Add(Crow);

        //    var Chcolumn = new ColumnDefinition();
        //    Chcolumn.Width = new GridLength(10);
        //    Grid1.ColumnDefinitions.Add(Chcolumn);

        //}

        #endregion GRID_CHART

        //##########################

        #region XY_Axis




        #endregion XY_Axis


    }


    


    



