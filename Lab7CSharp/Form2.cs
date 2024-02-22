using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab7CSharp
{
    public partial class Form2 : Form
    {
        private Color pickedColor;

        public Form2()
        {
            InitializeComponent();

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            this.FormClosing += Form2_FormClosing;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e) { }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter =
                "Image Files (*.bmp;*.jpg;*.png)|*.bmp;*.jpg;*.png|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    Image image = Image.FromFile(openFileDialog.FileName);
                    pictureBox1.Image = image;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("No file selected");
            }
        }

        private void pictureBox1_Click_EventHandler(object sender, EventArgs e)
        {
            MouseEventArgs mouseEventArgs = (MouseEventArgs)e;
            pictureBox1_Click(sender, mouseEventArgs);
        }

        private void pictureBox1_Click(object sender, MouseEventArgs e)
        {
            Bitmap bitmap = new Bitmap(pictureBox1.Image);
            pickedColor = bitmap.GetPixel(e.X, e.Y);
            DisplayPickedColor();
        }

        private void DisplayPickedColor()
        {
            pickedColorLabel.Text = $"RGB: ({pickedColor.R}, {pickedColor.G}, {pickedColor.B})";
            pictureBox2.BackColor = pickedColor;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("No image loaded to save.");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter =
                "Bitmap Image (*.bmp)|*.bmp|JPEG Image (*.jpg)|*.jpg|PNG Image (*.png)|*.png";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string fileName = saveFileDialog.FileName;
                    ImageFormat imageFormat;

                    switch (Path.GetExtension(fileName).ToLower())
                    {
                        case ".bmp":
                            imageFormat = ImageFormat.Bmp;
                            break;
                        case ".jpg":
                            imageFormat = ImageFormat.Jpeg;
                            break;
                        case ".png":
                            imageFormat = ImageFormat.Png;
                            break;
                        default:
                            MessageBox.Show("Invalid file format.");
                            return;
                    }

                    pictureBox1.Image.Save(fileName, imageFormat);
                    MessageBox.Show("Image saved successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving image: " + ex.Message);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Program.formMenu.Show();
            this.Hide();
        }
    }
}
