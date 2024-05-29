using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tower_Defense
{
    public partial class CongratulationsForm : Form
    {
        public CongratulationsForm()
        {
            InitializeComponent();
            label1.Text = "Felicitări! Ai câștigat!";
        }
    }
}
