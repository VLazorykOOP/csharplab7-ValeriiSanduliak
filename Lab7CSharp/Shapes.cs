using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab7CSharp
{
    using System.Drawing;

    public abstract class Shape
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Color Color { get; set; }

        public Shape(int x, int y, Color color)
        {
            X = x;
            Y = y;
            Color = color;
        }

        public abstract void Draw(Graphics g);
    }

    public class Hexagon : Shape
    {
        public int Side { get; set; }

        public Hexagon(int x, int y, int side, Color color)
            : base(x, y, color)
        {
            Side = side;
        }

        public override void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            Point[] points = new Point[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = 2 * Math.PI / 6 * i;
                int x = (int)(X + Side * Math.Cos(angle));
                int y = (int)(Y + Side * Math.Sin(angle));
                points[i] = new Point(x, y);
            }
            g.FillPolygon(brush, points);
            brush.Dispose();
        }
    }

    public class Rhombus : Shape
    {
        public int Diagonal1 { get; set; }
        public int Diagonal2 { get; set; }

        public Rhombus(int x, int y, int diagonal1, int diagonal2, Color color)
            : base(x, y, color)
        {
            Diagonal1 = diagonal1;
            Diagonal2 = diagonal2;
        }

        public override void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            Point[] points = new Point[4];

            points[0] = new Point(X, Y);
            points[1] = new Point(X + Diagonal1 / 2, Y + Diagonal2 / 2);
            points[2] = new Point(X + Diagonal1, Y);
            points[3] = new Point(X + Diagonal1 / 2, Y - Diagonal2 / 2);

            g.FillPolygon(brush, points);
            brush.Dispose();
        }
    }

    public class Triangle : Shape
    {
        public int SideA { get; set; }
        public int SideB { get; set; }

        public Triangle(int x, int y, int sideA, int sideB, Color color)
            : base(x, y, color)
        {
            SideA = sideA;
            SideB = sideB;
        }

        public override void Draw(Graphics g)
        {
            Brush brush = new SolidBrush(Color);
            Point[] points = new Point[3];

            // Початкові вершини трикутника
            points[0] = new Point(X, Y + SideB); // Нижня ліва вершина
            points[1] = new Point(X + (SideA / 2), Y); // Верхня вершина
            points[2] = new Point(X + SideA, Y + SideB); // Нижня права вершина

            g.FillPolygon(brush, points);
            brush.Dispose();
        }
    }

    public class Arc : Shape
    {
        public int Radius { get; set; }
        public float StartAngle { get; set; }
        public float SweepAngle { get; set; }

        public Arc(int x, int y, int radius, float startAngle, float sweepAngle, Color color)
            : base(x, y, color)
        {
            Radius = radius;
            StartAngle = startAngle;
            SweepAngle = sweepAngle;
        }

        public override void Draw(Graphics g)
        {
            using (Pen pen = new Pen(Color))
            {
                Rectangle rect = new Rectangle(X - Radius, Y - Radius, 2 * Radius, 2 * Radius);
                g.DrawArc(pen, rect, StartAngle, SweepAngle);
            }
        }
    }
}
