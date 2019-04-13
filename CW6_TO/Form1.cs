using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathNet.Numerics;
using MathNet.Numerics.Interpolation;
using MathNet.Numerics.Algorithms;
namespace CW6_TO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        #region
        double GDolna, GGorna, wyniknumeryczny, wynikanalityczny, dx, calka, calkaT;
        int LPrzedzialow;
        #endregion

        double h(double x) 
        {
            if (radioButton1.Checked == true) 
            {
                return -4 * Math.Pow(x, 3) + 2 * Math.Pow(x, 2) - 3;
            }
            else if (radioButton2.Checked == true) 
            {
                return Math.Sin(x);
            }
            else
            {
                return x * Math.Exp(2 * x);

            }
        }
        private static double deg2rad(double deg)  
        {
            return Math.PI * deg / 180; 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear(); 
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();

        }

        private void button1_Click(object sender, EventArgs e) 
        {
            wyniknumeryczny = 0;
            wynikanalityczny = 0;
            calka = 0;
            calkaT = 0;
            GDolna = Double.Parse(textBox1.Text); 
            GGorna = Double.Parse(textBox2.Text); 
            LPrzedzialow = int.Parse(textBox3.Text); 

            if (radioButton2.Checked == true)
            {
                wyniknumeryczny = MathNet.Numerics.Integration.Algorithms.SimpsonRule.IntegrateComposite(h, deg2rad(GDolna), deg2rad(GGorna), LPrzedzialow); // wywolywanie metody simpson jesli podane katy

            }
            else
            {
                wyniknumeryczny = MathNet.Numerics.Integration.Algorithms.SimpsonRule.IntegrateComposite(h, GDolna, GGorna, LPrzedzialow); // wywolanie metody simpson jesli liczby normalne

            }
            textBox6.Text = wyniknumeryczny.ToString(); 
            MetodaProstokatow(); 
            MetodaTrapezu(); 
            MetodaAnalityczna(GDolna, GGorna); 

        }
        private void MetodaProstokatow()
        {
            if (radioButton2.Checked == true)
            {
                dx = (deg2rad(GGorna) - deg2rad(GDolna)) / (LPrzedzialow);
            }
            else
            {
                dx = (GGorna - GDolna) / LPrzedzialow;
            }
            wyniknumeryczny = 0;
            for (int i = 1; i <= LPrzedzialow; i++)
            {
                if (radioButton2.Checked == true)
                {
                    calka += h(deg2rad(GDolna) + i * dx) * dx;
                }
                else
                {
                    calka += h(GDolna + i * dx) * dx;
                }
                textBox4.Text = calka.ToString();
            }
        }
        private void MetodaTrapezu()
        {
            if (radioButton2.Checked == true)
            {
                dx = (deg2rad(GGorna) - deg2rad(GDolna)) / (LPrzedzialow);
            }
            else
            {
                dx = (GGorna - GDolna) / LPrzedzialow; 
            }
            wyniknumeryczny = 0;
            for (int i = 0; i <= LPrzedzialow; i++)
            {
                if (radioButton2.Checked == true)
                {
                    calkaT += ((h(deg2rad(GDolna) + i * dx) + h(deg2rad(GDolna) + (i + 1) * dx)) / 2) * dx;
                }
                else
                {
                    calkaT += ((h(GDolna + i * dx) + h(GDolna + (i + 1) * dx)) / 2) * dx; 
                }
                textBox5.Text = calkaT.ToString(); 
            }
        }
        private void MetodaAnalityczna(double GDolna, double GGorna) 
        {
            if (radioButton1.Checked == true) 
            {
                wynikanalityczny = 0;
                wynikanalityczny = (-Math.Pow(GGorna, 4) + (2 * Math.Pow(GGorna, 3)) / 3 - 3 * GGorna) - (-Math.Pow(GDolna, 4) + (2 * Math.Pow(GDolna, 3)) / 3 - 3 * GDolna);
            }
            else if (radioButton2.Checked == true)
            {
                wynikanalityczny = 0;
                wynikanalityczny = (-Math.Cos(deg2rad(GGorna)) + Math.Cos(deg2rad(GDolna))); 
            }
            else
            {
                wynikanalityczny = 0;
                wynikanalityczny = (0.25 * Math.Exp(2 * GGorna) * ((2 * GGorna) - 1)) - (0.25 * Math.Exp(2 * GDolna) * ((2 * GDolna) - 1)); 
            }
            textBox7.Text = wynikanalityczny.ToString(); 
        }






        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button1_Click(object sender, EventArgs e)
        {

        }
    }
}
