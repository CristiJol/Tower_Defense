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
    public partial class PauseForm : Form
    {
        public bool ContinueGame { get; private set; }
        private Form1 form1; // Asigură-te că ai această linie

        public PauseForm(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1; // Asigură-te că ai această linie
        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            this.Close(); // Închide fereastra de pauză
            form1.PauseGame(false); // Reia jocul
        }
        private void btnBacktoMain_Click(object sender, EventArgs e)
        {
            this.Close(); // Închide fereastra de pauză
            form1.Close(); // Închide fereastra principală (Form1)
            StartScreenForm startScreenForm = new StartScreenForm(); // Deschide fereastra de start
            startScreenForm.ShowDialog();
        }
    }
}
