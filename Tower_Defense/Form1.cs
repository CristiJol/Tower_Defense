using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_Defense
{
    public partial class Form1 : Form
    {
        public Image background = Image.FromFile("../../Images/map.jpg");
        public int ID;
        public bool isPaused;

        private int money = 150;
        public int CurrentMoney {  get=>money; set { money = value; goldLabel.Text = $"{money}g"; }}

        public Form1()
        {
            InitializeComponent();
            pictureBox5.Parent = pictureBox1;
            Engine.Initialize(this);

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            ID = 1;
            Engine.isBlur = true;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            ID = 2;
            Engine.isBlur = true;
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            ID = 3;
            Engine.isBlur = true;
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Engine.SelectTower(e.Location);
            Engine.AddNewTower(e.Location);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Engine.Tick();
            Engine.DrawEverything();
        }
        public void PauseGame(bool pause)
        {
            if (pause)
            {
                // Pauză joc și afișare fereastră de pauză
                // isPaused = true;
                timer1.Enabled = false;
                PauseForm pauseForm = new PauseForm(this); // Furnizăm instanța curentă de Form1
                pauseForm.ShowDialog();
                // isPaused = false; // Reia jocul când fereastra de pauză se închide
                timer1.Enabled = true;
            }
            else
            {
                // Reia jocul
                isPaused = false;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                PauseGame(true);
            }
        }
    }
}
