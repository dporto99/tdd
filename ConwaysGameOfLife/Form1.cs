using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConwaysGameOfLife
{
    public partial class Form1 : Form
    {
        private Life life;
        private int lifeSize;
        private bool running = false;

        public Form1()
        {
            InitializeComponent();
            lifeSize = 30;
            life = new Life(lifeSize, true);
        }

        private void DrawLife(PaintEventArgs e)
        {
            var panelWidth = panel1.Size.Width;
            var matrixWidth = life.Matrix.GetLength(0);
            var cellSize = panelWidth / matrixWidth;

            using (var brush = new SolidBrush(Color.Black))
            {
                for (int i = 0; i < life.Matrix.GetLength(0); i++)
                {
                    for (int j = 0; j < life.Matrix.GetLength(0); j++)
                    {
                        if (life.Matrix[i, j])
                            e.Graphics.FillRectangle(brush, new Rectangle(i * cellSize, j * cellSize, cellSize, cellSize));
                    }
                }
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            DrawLife(e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            life = new Life(lifeSize, false);
            panel1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            life = new Life(lifeSize, true);
            panel1.Refresh();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                button3.Text = "Stop";
                button1.Enabled = button2.Enabled = button4.Enabled = false;
                Run();
            }
            else
            {
                button3.Text = "Run";
                button1.Enabled = button2.Enabled = button4.Enabled = true;
            }
            running = !running;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                life.ExecuteOneCycle();
                panel1.Refresh();
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {
            if (!running)
            {
                MouseEventArgs e2 = (MouseEventArgs)e;
                var panelWidth = panel1.Size.Width;
                var matrixWidth = life.Matrix.GetLength(0);
                var cellSize = panelWidth / matrixWidth;
                var x = e2.X / cellSize;
                var y = e2.Y / cellSize;
                life.Matrix[x, y] = !life.Matrix[x, y];
                panel1.Refresh();
            }
        }

        private async void Run()
        {
            await Task.Factory.StartNew(() => RunGame());
        }

        private void RunGame()
        {
            while (running)
            {
                life.ExecuteOneCycle();
                System.Threading.Thread.Sleep(1000);
            }
        }
    }

    public class Life
    {

        #region Constructor and Properties
        private bool[,] matrix;

        public Life(bool[,] matrix)
        {
            this.matrix = matrix;
        }

        public Life(int size, bool fillRandom)
        {
            matrix = new bool[size, size];
            if (fillRandom)
            {
                var random = new Random();
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        matrix[i, j] = random.Next(100) <= 10;
                    }
                }
            }
        }

        public bool[,] Matrix
        {
            get { return matrix; }
            set { matrix = value; }
        }
        #endregion

        #region Methods
        public void ExecuteOneCycle()
        {
            //MessageBox.Show("asdsadasd");
            var newMatrix = new bool[matrix.GetLength(0), matrix.GetLength(0)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    var count = LiveNeighboursCount(i, j);
                    newMatrix[i, j] = DetermineStatus(matrix[i, j], count);
                    var finalValue = newMatrix[i, j];
                }
            }
            matrix = newMatrix;
        }

        public static bool DetermineStatus(bool cellStatus, int liveNeighboursCount)
        {
            return (cellStatus && (liveNeighboursCount == 2 || liveNeighboursCount == 3))
                || (!cellStatus && liveNeighboursCount == 3);
        }

        public bool IsInBound(int x, int y)
        {
            var lenght = matrix.GetLength(0);
            return x > -1 && x < lenght && y > -1 && y < lenght;
        }

        public int LiveNeighboursCount(int x, int y)
        {
            var count = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (i != 1 || j != 1)
                    {
                        if (IsInBound((x+i-1),(y+j-1)))
                        {
                            count = count + (matrix[(x + i - 1), (y + j - 1)] ? 1 : 0); 
                        }
                    }
                }
            }
            return count;
        }

        #endregion
    }
}
