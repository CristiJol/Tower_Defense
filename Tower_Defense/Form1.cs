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
            Engine.AddNewTower(e.Location);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            Engine.Tick();
            Engine.DrawEverything();
        }
    }
}
