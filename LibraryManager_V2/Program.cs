using LibraryManager_V2.Models;
using LibraryManager_V2.Repositories;
using LibraryManager_V2.Services;

namespace LibraryManager_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new LibraryService(new BookRepository());

            service.AddBook(new Book(
                0,
                "The Hobbit",
                "J.R.R. Tolkien",
                Category.Fantasy,
                5
            ));
        }
    }
}
