using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Windows;

namespace Pancake_Pridction_KNN
{
    public static class Config
    {
        public static AppConfig cfg;
        public class AppConfig
        {
            public AppConfig()
            {
                faildCount = 0;
                betAmountInBNB = 0.01;
                AutoBet = false;
                Proxy = "";
            }
            public int faildCount { get; set; }
            public double betAmountInBNB { get; set; }
            public bool AutoBet { get;  set; }

            /// <summary>
            /// set your http proxy here. like http://127.0.0.1:7890
            /// </summary>
            public string Proxy { get; set; }

            /// <summary>
            /// Set your sql server here
            /// </summary>
            public string DbServerName { get; set; }
            public string dbName { get; set; }
            public string dbUser { get; set; }
            public string dbPassword { get; set; }
            public string websocketNode { get; set; }
            public string wallletPrivateKey { get;  set; }
            public string rpc_Endpoint { get;  set; }
        }
        public static void ReadConfig()
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + @"\Config\AppConfig.json";
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                if (!File.Exists(path))
                    SaveConfig();

                var jt = JToken.Parse(File.ReadAllText(path));
                cfg = jt.ToObject<AppConfig>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"exception on readconfig!{ex}");
            }
        }
        public static void SaveConfig()
        {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + @"\Config\AppConfig.json";
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                if (cfg == null) cfg = new AppConfig();
                var jt = JToken.FromObject(cfg);
                File.WriteAllText(path, jt.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"exception on saveconfig!{ex}");
            }
        }

    }
}
