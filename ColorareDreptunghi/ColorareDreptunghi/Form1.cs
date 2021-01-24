using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace ColorareDreptunghi
{
    public partial class Form1 : Form
    {
        static Bitmap bitmap;
        static Graphics graphics;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bitmap);

            List<Color> colors = new List<Color>();

            foreach (PropertyInfo property in typeof(Color).GetProperties())
            {
                if (property.PropertyType == typeof(Color))
                {
                    fillColor.Items.Add((Color)property.GetValue(null));
                }
            }

            X.Text = "32";
            Y.Text = "64";
            rectangleWidth.Text = "100";
            rectangleHeight.Text = "200";
        }

        private void Go_Click(object sender, EventArgs e)
        {
            try
            {
                Color color = (Color)fillColor.Items[fillColor.SelectedIndex];

                int x = int.Parse(X.Text);
                int y = int.Parse(Y.Text);

                int width = int.Parse(rectangleWidth.Text);
                int height = int.Parse(rectangleHeight.Text);

                graphics.Clear(Color.White);

                DrawMyRectangle(x, y, width, height);
                FillMyRectangle(x, y, width, height, color);

                pictureBox1.Image = bitmap;
            }
            catch
            {
                if (fillColor.SelectedIndex == -1)
                {
                    MessageBox.Show("Culoarea trebuie selectata!");
                }
                else if (!int.TryParse(X.Text, out int x) || !int.TryParse(Y.Text, out int y))
                {
                    MessageBox.Show("Coordonatele trebuie sa fie numere intregi!");
                }
                else if (!int.TryParse(rectangleWidth.Text, out int width) || !int.TryParse(rectangleHeight.Text, out int height))
                {
                    MessageBox.Show("Valorile pentru latime si inaltime trebuie sa fie numere intregi!");
                }
            }
        }

        private void DrawMyRectangle(int x, int y, int width, int height)
        {
            for (int i = x; i <= x + width; i++)
            {
                bitmap.SetPixel(i, y, Color.Black);
                bitmap.SetPixel(i, y + height, Color.Black);
            }
            for (int i = y; i <= y + height; i++)
            {
                bitmap.SetPixel(x + width, i, Color.Black);
                bitmap.SetPixel(x, i, Color.Black);
            }
        }

        private void FillMyRectangle(int x, int y, int width, int height, Color color)
        {
            for (int i = y + 1; i < y + height; i++)
            {
                for (int j = x + 1; j < x + width; j++)
                {
                    bitmap.SetPixel(j, i, color);
                }
            }
        }
    }
}
