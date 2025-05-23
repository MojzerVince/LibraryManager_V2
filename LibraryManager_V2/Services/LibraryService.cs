﻿using LibraryManager_V2.Repositories;
using LibraryManager_V2.Models;

namespace LibraryManager_V2.Services
{
    public class LibraryService(IBookRepository rep) : LoggerService
    {
        public IBookRepository rep = rep;
        private LoggerService logger = new LoggerService();
        private FileService files = new FileService();
        private List<Log> logs = new List<Log>();

        public void AddBook(Book book)
        {
            rep.AddBook(book);
            Log log = new Log(logs.Count + 1, DateTime.Now, $"Book '#{book.ID}' '{book.Title}' was added to library");
            logs.Add(log);
            logger.SaveToLog(log);
        }

        public void ModifyBook(int id, Book book)
        {
            Log log = new Log(logs.Count + 1, DateTime.Now, $"Book '#{rep.GetBookById(id).ID}' was modified");
            logs.Add(log);
            logger.SaveToLog(log);
            rep.ModifyBook(id, book);
        }

        public void DeleteBook(int id)
        {
            Log log = new Log(logs.Count + 1, DateTime.Now, $"Book '#{rep.GetBookById(id).ID}' '{rep.GetBookById(id).Title}' was removed from library");
            logs.Add(log);
            logger.SaveToLog(log);
            rep.DeleteBook(id);
        }

        public void CreateCustomLog(string message)
        {
            Log log = new Log(logs.Count + 1, DateTime.Now, message);
            logs.Add(log);
            logger.SaveToLog(log);
        }

        public void LendBook(Book book)
        {
            foreach (Book b in rep.GetAllBooks())
            {
                if(b.ID == book.ID && b.Quantity > 0)
                {
                    b.Quantity--;
                    Log log = new Log(logs.Count + 1, DateTime.Now, $"Book '{book.Title}' was lended");
                    logs.Add(log);
                    logger.SaveToLog(log);
                }
                else if(b.ID == book.ID && b.Quantity == 0)
                {
                    Log log = new Log(logs.Count + 1, DateTime.Now, $"Book '{book.Title}' is out of stock");
                    logs.Add(log);
                    logger.SaveToLog(log);
                }
            }
        }

        public void ReturnBook(Book book)
        {
            foreach (Book b in rep.GetAllBooks())
            {
                if (b.ID == book.ID && b.Quantity > 0)
                {
                    b.Quantity++;
                    Log log = new Log(logs.Count + 1, DateTime.Now, $"Book '{book.Title}' was returned");
                    logs.Add(log);
                    logger.SaveToLog(log);
                }
            }
        }

        public void LoadBooks()
        {
            foreach(Book b in files.LoadLibrary())
            {
                rep.AddBook(b);
            }
        }

        public void SaveBooks()
        {
            files.SaveLibrary(rep.GetAllBooks());
        }

        public List<Log> ReturnLogs()
        {
            return logs;
        }

        public new void LoadLogs()
        {
            logs = logger.LoadLogs();
        }
    }
}
