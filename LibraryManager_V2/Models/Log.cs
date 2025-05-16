namespace LibraryManager_V2.Models
{
    public class Log
    {
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; } = String.Empty;

        public Log(int id, DateTime date, string message)
        {
            ID = id;
            Date = date;
            Message = message;
        }

        public override string ToString()
        {
            return $"{ID}|{Date}|{Message}";
        }
    }
}
