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

        public Form1()
        {
            InitializeComponent();
            Engine.Initialize(this);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Engine.Blurbackground();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Engine.Blurbackground();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            Engine.Blurbackground();
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            Engine.AddNewTower(e.Location);
        }

       
    }
}
