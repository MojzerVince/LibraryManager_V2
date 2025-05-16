using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager_V2.Models;

namespace LibraryManager_V2.Services
{
    public class LoggerService
    {
        public LoggerService()
        {
            CheckForLogFile();
        }

        public void CheckForLogFile()
        {
            FileStream fs;

            if (!File.Exists("log.txt"))
            {
                fs = File.Create("log.txt");
                fs.Close();
            }
        }

        public List<Log> LoadLogs()
        {
            StreamReader sr = new StreamReader("log.txt");
            string line;
            List<Log> logs = new List<Log>();
            while (!sr.EndOfStream)
            {
                try 
                {
                    line = sr.ReadLine();
                    string[] parts = line.Split('|');
                    Log log = new Log(int.Parse(parts[0]), DateTime.Parse(parts[1]), parts[2], parts[3]);
                    logs.Add(log);
                }
                catch (Exception ex)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(ex.Message);
                    Console.ResetColor();
                }
                
            }
            sr.Close();
            return logs;
        }

        public void SaveToLog(Log log)
        {
            StreamWriter sw = new StreamWriter("log.txt", true); //hozzáfűzés true
            sw.WriteLine(log.ToString());
            sw.Close();
        }
    }
}
