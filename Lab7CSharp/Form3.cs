﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab7CSharp
{
    public partial class Form3 : Form
    {
        private List<Shape> shapes;
        private Random random = new Random();

        private Graphics graphics;

        public Form3()
        {
            InitializeComponent();
            InitializeComboBox();

            shapes = new List<Shape>();
            graphics = pictureBox1.CreateGraphics();

            radioButton1.Checked = true;
            this.FormClosing += Form3_FormClosing;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void InitializeComboBox()
        {
            comboBox1.DrawMode = DrawMode.OwnerDrawFixed;
            comboBox1.DrawItem += ComboBoxColors_DrawItem;

            List<Color> colorList = new List<Color>
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
            };

            foreach (Color color in colorList)
            {
                comboBox1.Items.Add(color);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void ComboBoxColors_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index >= 0)
            {
                System.Windows.Forms.ComboBox comboBox = (System.Windows.Forms.ComboBox)sender;
                Color itemColor = (Color)comboBox.Items[e.Index];

                e.Graphics.FillRectangle(
                    new SolidBrush(itemColor),
                    e.Bounds.Left + 1,
                    e.Bounds.Top + 1,
                    20,
                    e.Bounds.Height - 2
                );

                e.Graphics.DrawString(
                    itemColor.Name,
                    comboBox.Font,
                    Brushes.Black,
                    e.Bounds.Left + 25,
                    e.Bounds.Top + 1
                );

                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
                {
                    e.Graphics.DrawRectangle(
                        Pens.Black,
                        e.Bounds.Left,
                        e.Bounds.Top,
                        e.Bounds.Width - 1,
                        e.Bounds.Height - 1
                    );
                }

                e.DrawFocusRectangle();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "Ширина:";
            label5.Visible = false;
            label6.Visible = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            textBox2.Visible = false;
            textBox3.Visible = false;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "Діагональ 1:";
            label5.Text = "Діагональ 2:";
            label5.Visible = true;
            label6.Visible = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            textBox2.Visible = true;
            textBox3.Visible = false;
        }

        private void radioButton3_CheckedChanged_1(object sender, EventArgs e)
        {
            label4.Text = "Сторона A:";
            label5.Text = "Сторона B:";
            label5.Visible = true;
            label6.Visible = false;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            textBox2.Visible = true;
            textBox3.Visible = false;
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            label4.Text = "Радіус:";
            label5.Text = "Початковий кут:";
            label6.Text = "Кут розгортки:";
            label5.Visible = true;
            label6.Visible = true;

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";

            textBox2.Visible = true;
            textBox3.Visible = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedRadioButton = groupBox1
                .Controls.OfType<RadioButton>()
                .FirstOrDefault(r => r.Checked);
            string selectedShapes = selectedRadioButton?.Text;

            if (selectedShapes != null)
            {
                Color selectedColor = (Color)comboBox1.SelectedItem;

                for (int i = 0; i < Convert.ToInt32(txtCount.Text); i++)
                {
                    Shape newShape = CreateShape(selectedShapes, selectedColor);

                    shapes.Add(newShape);
                }

                DrawShapes(shapes, graphics);
            }
        }

        private Shape CreateShape(string shapeType, Color color)
        {
            int x = random.Next(0, pictureBox1.Width - 1);
            int y = random.Next(0, pictureBox1.Height - 1);

            switch (shapeType)
            {
                case "Шестикутник":
                    int side = int.Parse(textBox1.Text);
                    return new Hexagon(x, y, side, color);
                case "Ромб":
                    int diagonal1 = int.Parse(textBox1.Text);
                    int diagonal2 = int.Parse(textBox2.Text);
                    return new Rhombus(x, y, diagonal1, diagonal2, color);
                case "Трикутник":
                    int sideA = int.Parse(textBox1.Text);
                    int sideB = int.Parse(textBox2.Text);
                    return new Triangle(x, y, sideA, sideB, color);
                case "Дуга":
                    int radius = int.Parse(textBox1.Text);
                    float startAngle = float.Parse(textBox2.Text);
                    float sweepAngle = float.Parse(textBox3.Text);
                    return new Arc(x, y, radius, startAngle, sweepAngle, color);

                default:
                    return null;
            }
        }

        private void DrawShapes(List<Shape> shapes, Graphics graphics)
        {
            pictureBox1.BackColor = Color.White;

            foreach (Shape shape in shapes)
            {
                shape.Draw(graphics);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            DrawShapes(shapes, e.Graphics);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.formMenu.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            shapes.Clear();
        }
    }
}
