namespace LibraryManager_V2.Models
{
    public class Log
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; } = String.Empty;
        public string User { get; set; } = String.Empty;

        public Log(int id, DateTime date, string message, string user)
        {
            ID = id;
            Date = date;
            Message = message;
            User = user;
        }

        public override string ToString()
        {
            return $"{ID}|{Date}|{Message}|{User}";
        }
    }
}
