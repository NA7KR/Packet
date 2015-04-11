using System;
using System.Threading;
using System.Windows.Forms;

namespace Packet
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Main());

            using (Mutex mutex = new Mutex(false, "Global\\" + appGuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show("Instance already running");
                    return;
                }

                Application.Run(new Main());
            }
            
        }
        private static string appGuid = "6f02a0f5-3b66-4de6-9853-5fc5b7031e85";
    }
}