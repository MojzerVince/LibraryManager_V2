using LibraryManager_V2.Models;
using LibraryManager_V2.Repositories;
using LibraryManager_V2.Services;

namespace LibraryManager_V2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var service = new ConsoleService(new LibraryService(new BookRepository()));

            service.Run();
        }
    }
}
