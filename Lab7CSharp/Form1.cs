using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public partial class Form1 : Form
    {
        private Timer timer;
        private int angle;

        public Form1()
        {
            InitializeComponent();

            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += Timer_Tick;

            angle = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            angle += 5;
            panel1.Location = CalculateNewLocation(button1.Location, angle);
            panel1.BackColor = GenerateRandomColor();
            button1.BackColor = GenerateRandomColor();
            panel1.Width = GenerateRandomWidth();
            button1.Width = GenerateRandomWidth();
        }

        private Point CalculateNewLocation(Point center, int angle)
        {
            double radians = angle * Math.PI / 180;
            int radius = 100;
            int x = center.X + (int)(radius * Math.Cos(radians));
            int y = center.Y + (int)(radius * Math.Sin(radians));
            return new Point(x, y);
        }

        private Color GenerateRandomColor()
        {
            Random random = new Random();
            return Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
        }

        private int GenerateRandomWidth()
        {
            Random random = new Random();
            return random.Next(50, 100);
        }
    }
}
