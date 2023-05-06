using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WorldBackuper3500
{
    public class Logger
    {
        private string filePath;
        private Logger logger;
        public Logger(string _filePath)
        {
            filePath = _filePath;
            try
            {
                if (!File.Exists(filePath))
                {
                    using (StreamWriter sw = File.CreateText(filePath))
                    {
                        sw.WriteLine("----- WorldBackuper3500 log file -----");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Log file path unreachable, check permissions and make sure specified directory is created");
                Environment.Exit(1);
            }
        }
        public void writeLog(string content)
        {
            using (StreamWriter sw = File.AppendText(filePath))
            {
                sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")+" "+content);
            }
        }
    }
}
