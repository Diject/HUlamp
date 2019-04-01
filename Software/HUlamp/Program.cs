using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace HUlamp
{
    static class Program
    {
        static Mutex OneCopyCheckMutex = new Mutex(true, "3c 48556c616d70 5465726d696e616c 3c");
        [STAThread]
        static void Main()
        {
            if (OneCopyCheckMutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
            }
            else
            {
                MessageBox.Show("Приложение уже запущено", "HUlamp Terminal", MessageBoxButtons.OK);
            }
        }
    }
}
