using System;
using System.IO;

namespace Pancake_Pridction_KNN
{
    public static class Log
    {
        public static void ShowInUI(string msg)
        {
            var timemsg = $"{Extend.nowTime} {msg}{Environment.NewLine}";
            if (MainForm.self.rickTb.InvokeRequired)
            {
                MainForm.self.rickTb.Invoke(new Action(() =>
                {
                    ShowInMainFormRickTextBox(timemsg);
                }));
            }
            else
            {
                ShowInMainFormRickTextBox(timemsg);
            }
            Program.logger.Info(msg);
        }

        private static void ShowInMainFormRickTextBox(string msg)
        {
            var richtextbox = MainForm.self.rickTb;
            richtextbox.AppendText(msg);
            richtextbox.SelectionStart = richtextbox.TextLength;
            richtextbox.Focus();
        }
    }

}
