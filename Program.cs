using System;
using System.Windows.Forms;

namespace Pancake_Pridction_KNN
{
    class Program
    {

        public static Common.Logging.ILog logger = Common.Logging.LogManager.GetLogger<Program>();

        [STAThread]
        static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());

            }
            catch (Exception ex)
            {
                MessageBox.Show($"exception  {ex}");
            }
        }
    }
}