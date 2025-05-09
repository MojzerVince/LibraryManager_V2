using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManager_V2.Models;

namespace LibraryManager_V2.Services
{
    internal class ConsoleService
    {
        private LibraryService service;

        private void ExecuteWithLogging(Action action, string message)
        {
            try
            {
                Console.WriteLine(message);
                action();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }

            try
            {
                Console.WriteLine("Loading book...");
                service.LoadBooks();
                Thread.Sleep(500);
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
            }
            Console.Clear();

            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Welcome to the Library Manager!");
            Console.ResetColor();

            //TEST ONLY
            //service.AddBook(new Book("The Hobbit", "J.R.R. Tolkien", Category.Fantasy, 5));
            //service.AddBook(new Book("The Lord of the Rings", "J.R.R. Tolkien", Category.Fantasy, 3));
        }

        public void Run()
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("0. Books");
            Console.WriteLine("1. Add book");
            Console.WriteLine("2. Modify book");
            Console.WriteLine("3. Delete book");
            Console.WriteLine("4. Check logs");
            Console.WriteLine("5. Check messages");
            Console.WriteLine("6. Save and Exit\n");

            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);
            Console.WriteLine();
            switch (consoleKeyInfo.Key)
            {
                case ConsoleKey.D0:
                case ConsoleKey.NumPad0:
                    GetAllBooks();
                    Run();
                    break;
                case ConsoleKey.D1:
                case ConsoleKey.NumPad1:
                    AddBook();
                    break;
                case ConsoleKey.D2:
                case ConsoleKey.NumPad2:
                    ModifyBook();
                    break;
                case ConsoleKey.D3:
                case ConsoleKey.NumPad3:
                    DeleteBookById();
                    break;
                case ConsoleKey.D4:
                case ConsoleKey.NumPad4:
                    LogCheck();
                    break;
                case ConsoleKey.D5:
                case ConsoleKey.NumPad5:
                    MessageCheck();
                    Run();
                    break;
                case ConsoleKey.D6:
                case ConsoleKey.NumPad6:
                    service.SaveBooks();
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(500);
                    Environment.Exit(0);
                    break;
                default:
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Invalid option. Please try again.");
                    Console.ResetColor();
                    Console.WriteLine();
                    Run();
                    break;
            }
        }

        private void GetAllCategories()
        {
            Console.Clear();
            foreach (var c in Enum.GetValues(typeof(Category)))
            {
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine(c);
                Console.ResetColor();
                Thread.Sleep(10);
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.White;
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        private void GetAllBooks()
        {
            Console.Clear();
            foreach (Book b in service.rep.GetAllBooks())
            {
                Console.Write($"{b.ID} | ");
                Console.Write($"{b.Title} | ");
                Console.Write($"{b.Author} | ");
                Console.Write($"{b.Genre} | ");
                Console.Write($"{b.Quantity} left\n");
                Thread.Sleep(15);
            }
        }

        private void DeleteBookById()
        {
            GetAllBooks();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Delete book");
            Console.ResetColor();
            Console.Write("ID: #");
            int id = int.Parse(Console.ReadLine());
            if(service.rep.GetBookById(id) != null)
                service.DeleteBook(id);
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Invalid ID!");
                Console.ResetColor();
                Console.WriteLine();
            }
            Run();
        }

        private void AddBook()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Add book");
            Console.ResetColor();
            Console.WriteLine();
            Console.Write("Title: ");
            string title = Console.ReadLine();
            Console.Write("Author: ");
            string author = Console.ReadLine();
            GetAllCategories();
            Console.Write("Genre: ");
            string genre = Console.ReadLine();
            Console.Write("Quantity available: ");
            int quantity = int.Parse(Console.ReadLine());

            service.AddBook(new Book(title, author, (Category)Enum.Parse(typeof(Category), genre), quantity));
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write("Book successfully added!");
            Console.ResetColor();
            Console.WriteLine();
            Run();
        }

        private void ModifyBook()
        {
            GetAllBooks();
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Modify book");
            Console.ResetColor();
            Console.Write("ID: #");
            int id = int.Parse(Console.ReadLine());
            if (service.rep.GetBookById(id) != null)
            {
                Book cBook = service.rep.GetBookById(id); //current book

                Console.WriteLine();
                Console.WriteLine($"Title: {cBook.Title}");
                Console.Write("New title: ");
                string title = Console.ReadLine();
                Console.Clear();
                Console.WriteLine($"Author: {cBook.Author}");
                Console.Write("New author: ");
                string author = Console.ReadLine();
                GetAllCategories();
                Console.WriteLine($"Genre: {cBook.Genre}");
                Console.Write("New genre: ");
                Category genre = (Category)Enum.Parse(typeof(Category), Console.ReadLine());
                Console.Clear();
                Console.WriteLine($"Quantity: {cBook.Quantity}");
                Console.Write("New quantity: ");
                int quantity = int.Parse(Console.ReadLine());

                service.ModifyBook(id, new Book(title, author, genre, quantity));

                Console.Clear();
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Book successfully modified!");
                Console.ResetColor();
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Invalid ID!");
                Console.ResetColor();
                Console.WriteLine();
            }
            Run();
        }

        private void LogCheck()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Logs");
            Console.ResetColor();
            Console.WriteLine();
            foreach (Log l in service.ReturnLogs())
            {
                Console.WriteLine(l);
                Thread.Sleep(15);
            }
            Run();
        }

        private void MessageCheck()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("This feature is not yet implemented!");
            Console.ResetColor();
        }
    }
}
