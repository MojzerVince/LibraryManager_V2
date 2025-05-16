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
            using (StreamReader sr = new StreamReader("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\log.txt"))
            {
              string line;
              List<Log> logs = new List<Log>();
              while (!sr.EndOfStream)
              {
                string line;
                string[] parts = new string[4];
                List<Log> logs = new List<Log>();
                while (!sr.EndOfStream)
                {
                    try 
                    {
                        line = sr.ReadLine();
                        parts = line.Split('|');
                        logs.Add(new Log(int.Parse(parts[0]), DateTime.Parse(parts[1]), parts[2], parts[3]));
                    }
                    catch (Exception ex)
                    {
                        logs.Add(new Log(int.Parse(parts[0]), DateTime.Parse(parts[1]), "RESTORED - " + parts[2], "Logger Service"));
                    }
                }
              }
            return logs;
            }
        }

        public void SaveToLog(Log log)
        {
            using (StreamWriter sw = new StreamWriter("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\log.txt", true))
                sw.WriteLine(log.ToString());
        }
    }
}
