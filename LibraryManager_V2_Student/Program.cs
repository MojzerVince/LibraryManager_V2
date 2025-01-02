using LibraryManager_V2.Repositories;
using LibraryManager_V2.Services;
using LibraryManager_V2_Student.Services;

namespace LibraryManager_V2_Student
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
