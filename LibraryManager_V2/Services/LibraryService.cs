using LibraryManager_V2.Repositories;
using LibraryManager_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager_V2.Services
{
    public class LibraryService(IBookRepository rep)
    {
        public IBookRepository rep = rep;
        private List<Log> logs = new List<Log>();

        public void AddBook(Book book)
        {
            rep.AddBook(book);
            logs.Add(new Log(logs.Count + 1, $"Book '{book.Title}' was added to library", DateTime.Now));
        }

        public void DeleteBook(int id)
        {
            logs.Add(new Log(logs.Count + 1, $"Book '{rep.GetBookById(id).Title}' was removed from library", DateTime.Now));
            rep.DeleteBook(id);
        }

        public void LendBook(Book book)
        {
            foreach (Book b in rep.GetAllBooks())
            {
                if(b.ID == book.ID && b.Quantity > 0)
                {
                    b.Quantity--;
                    logs.Add(new Log(logs.Count + 1, $"Book '{book.Title}' was lended", DateTime.Now));
                }
                else if(b.ID == book.ID && b.Quantity == 0)
                {
                    //OUT OF STOCK
                    logs.Add(new Log(logs.Count + 1, $"Book '{book.Title}' is out of stock", DateTime.Now));
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
                    logs.Add(new Log(logs.Count + 1, $"Book '{book.Title}' was returned", DateTime.Now));
                }
            }
        }

        public List<Log> ReturnLogs()
        {
            return logs;
        }
    }
}
