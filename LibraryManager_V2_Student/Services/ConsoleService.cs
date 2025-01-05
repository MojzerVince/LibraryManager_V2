using LibraryManager_V2.Models;
using LibraryManager_V2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager_V2_Student.Services
{
    internal class ConsoleService
    {
        private LibraryService service;

        public ConsoleService(LibraryService service)
        {
            this.service = service;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Welcome to the Library!");
            Console.ResetColor();

            //TEST ONLY
            service.AddBook(new Book("The Hobbit", "J.R.R. Tolkien", Category.Fantasy, 5));
            service.AddBook(new Book("The Lord of the Rings", "J.R.R. Tolkien", Category.Fantasy, 3));
        }

        public void Run()
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("0. Books");
            Console.WriteLine("1. Lend a book");
            Console.WriteLine("2. Return a book");
            Console.WriteLine("3. Message to admins");
            Console.WriteLine("4. Exit\n");

            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    GetAllBooks();
                    Run();
                    break;
                case "1":
                    LendBook();
                    break;
                case "2":
                    //ReturnBook();
                    break;
                case "3":
                    //SendMessage();
                    break;
                case "4":
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
            }
        }

        private void LendBook()
        {
            GetAllBooks();
            Console.Write("\nPlease enter the ID of the book you want to lend: ");
            int id = int.Parse(Console.ReadLine());
            if(service.rep.GetBookById(id) != null)
            {
                if(service.rep.GetBookById(id).Quantity == 0)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Out of stock!");
                    Console.ResetColor();
                    Console.WriteLine();
                    Run();
                }
                else
                {
                    service.LendBook(service.rep.GetBookById(id));
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write("Successfully lended the book!");
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write("Invalid ID. Please try again.");
                Console.ResetColor();
                Console.WriteLine();
            }
            Run();
        }
    }
}
