using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager_V2.Models
{
    public class Log
    {
        public int ID { get; set; }
        public string Message { get; set; } = String.Empty;
        public DateTime Date { get; set; }

        public Log(int id, string message, DateTime date)
        {
            ID = id;
            Message = message;
            Date = date;
        }
    }
}
