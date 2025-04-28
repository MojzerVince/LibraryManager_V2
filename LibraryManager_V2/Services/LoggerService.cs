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

            if (!File.Exists("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\log.txt"))
            {
                fs = File.Create("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\log.txt");
                fs.Close();
            }
        }

        public List<Log> LoadLogs()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\log.txt");
            string line;
            List<Log> logs = new List<Log>();
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] parts = line.Split('|');
                Log log = new Log(int.Parse(parts[0]), DateTime.Parse(parts[1]), parts[2]);
                logs.Add(log);
            }
            sr.Close();
            return logs;
        }

        public void SaveToLog(Log log)
        {
            StreamWriter sw = new StreamWriter("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\log.txt", true); //hozzáfűzés true
            sw.WriteLine(log.ToString());
            sw.Close();
        }
    }
}
