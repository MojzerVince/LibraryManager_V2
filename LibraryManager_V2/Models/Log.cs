using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager_V2.Models
{
    internal class Log
    {
        public int ID { get; set; }
        public string Message { get; set; } = String.Empty;
        public DateTime TimeStamp { get; set; }

        public Log(int id, string message, DateTime timeStamp)
        {
            ID = id;
            Message = message;
            TimeStamp = timeStamp;
        }
    }
}
