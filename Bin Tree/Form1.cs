using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Bin_Tree
{
    public partial class Form1 : Form
    {
        private Tree<int, int> tree;
        private int deltax = 0;
        private int count = 1;
        public Form1()
        {
            InitializeComponent();
            tree = new Tree<int, int>();
            DoubleBuffered = true;
            panel1.AutoScroll = true;
        }

        private void DrawTree(Tree<int, int>.Node root, int count, int x, int d)
        {
            if (root == null)
                return;
            Label keylbl = new Label
            {
                Location = new Point(x + deltax, count * 50),
                Size = new Size(80, 20),
                BorderStyle = BorderStyle.FixedSingle,
                Text = $"Ключ: {root.Key}",
                TextAlign = ContentAlignment.BottomCenter
            };
            panel1.Controls.Add(keylbl);

            Label datalbl = new Label
            {
                Location = new Point(x + deltax, 20 + count * 50),
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(80, 20),
                Text = $"Значение: {root.Data}",
                TextAlign = ContentAlignment.BottomCenter
            };
            panel1.Controls.Add(datalbl);
            DrawTree(root.Left, count + 1, x - d, d / 2);
            DrawTree(root.Right, count + 1, x + d, d / 2);
        }


        private void DrawLines(Tree<int, int>.Node root, int count, int x, int d, Graphics g)
        {
            if (root == null)
                return;
            g.SmoothingMode = SmoothingMode.HighQuality;
            DrawLines(root.Left, count + 1, x - d, d / 2, g);
            DrawLines(root.Right, count + 1, x + d, d / 2, g);

            if (count != 1)
            {
                if (root == root.Pred.Left)
                {
                    g.DrawLine(new Pen(Color.Black), x + d * 2 + 40 + deltax, (count - 1) * 50 + 40, x + 40 + deltax, count * 50);
                }
                else
                {
                    g.DrawLine(new Pen(Color.Black), x - d * 2 + 40 + deltax, (count - 1) * 50 + 40, x + 40 + deltax, count * 50);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                tree.Add(Convert.ToInt32(textBox2.Text), Convert.ToInt32(textBox3.Text));
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message, "Error",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Error);
                return;
            }
            Deltax((int)Math.Pow(2, tree.depth) * 20);
            tree.Depth();
            panel1.Controls.Clear();
            DrawTree(tree.Root, count, panel1.Width / 2, (int)Math.Pow(2, tree.depth) * 20);
            panel1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                tree.Remove(Convert.ToInt32(textBox1.Text));
            }
            catch (NullReferenceException g)
            {
                MessageBox.Show(g.Message, "Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Error);
                return;
            }
            panel1.Controls.Clear();
            DrawTree(tree.Root, count, panel1.Width / 2, (int)Math.Pow(2, tree.depth) * 20);
            panel1.Refresh();
        }

        private void Deltax(int d)
        {
            for (var node = tree.Root; node != null; node = node.Left)
            {
                d += d / 2;
            }
            deltax = d - deltax;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            deltax = 0;
            tree.Clear();
            panel1.Refresh();
            panel1.Controls.Clear();
        }

        private void chlbl(object sender, EventArgs e)
        {
            label1.Text = $"Размер: {tree.Length}";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.TranslateTransform(panel1.AutoScrollPosition.X, panel1.AutoScrollPosition.Y);
            DrawLines(tree.Root, count, panel1.Width / 2, (int)Math.Pow(2, tree.depth) * 20, e.Graphics);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine("32".CompareTo("24"));
        }
    }
}
