using System;
using System.Windows.Forms;

namespace Conway
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VisualUniverse());
        }
    }
}