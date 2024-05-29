using System;
using System.Windows.Forms;

namespace Tower_Defense
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Înlocuiește Form1 cu StartScreenForm
            Application.Run(new StartScreenForm());
        }
    }
}
