using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pancake_Pridction_KNN
{
    public static class LogHelper
    {
        public static string LogPath = AppDomain.CurrentDomain.BaseDirectory + "Log";
        private static bool LogDirExists = false;
        public static void LogToFile(string msg)
        {
            try
            {
                var fn = LogPath + $"\\{DateTime.Now.ToString("yyyy-MM-dd")}.txt";
                if (LogDirExists == false)
                    Directory.CreateDirectory(LogPath);
                File.AppendAllText(fn, msg);
            }
            catch (Exception)
            {

            }
        }
    }
}
