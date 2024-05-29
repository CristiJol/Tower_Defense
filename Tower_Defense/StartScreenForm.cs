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
    public partial class StartScreenForm : Form
    {
        public StartScreenForm()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            // Deschide fereastra principală sau orice altceva necesar pentru a începe jocul.
            Engine.towers.Clear();
            Engine.enemies.Clear();
            Engine.projectiles.Clear();
            Engine.time = 0;
            Engine.castleHealth = 100;
            Form1 gameForm = new Form1(); // Înlocuiește cu numele corect al ferestrei principale
            gameForm.Show();
            this.Hide(); // Ascunde fereastra de start
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            // Închide aplicația când butonul Exit este apăsat
            Application.Exit();
        }
    }
}
