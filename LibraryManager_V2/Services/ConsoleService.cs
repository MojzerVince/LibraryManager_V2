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

        public ConsoleService(LibraryService service)
        {
            this.service = service;
        }

        public void Run()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Welcome to the Library Manager!");
            Console.ResetColor();
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("0. Books");
            Console.WriteLine("1. Add a book");
            Console.WriteLine("2. Delete a book");
            Console.WriteLine("3. Lend a book");
            Console.WriteLine("4. Return a book");
            Console.WriteLine("5. Exit\n");

            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    GetAllBooks();
                    break;
                case "1":
                    AddBook();
                    break;
                case "2":
                    DeleteBookById();
                    break;
                case "3":
                    //LendBook();
                    break;
                case "4":
                    //ReturnBook();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    Run();
                    break;
            }
        }

        private void GetAllCategories()
        {
            Console.WriteLine();
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            foreach (Category c in Enum.GetValues(typeof(Category)))
            {
                Console.WriteLine(c);
            }
            Console.ResetColor();
            Console.WriteLine();
        }

        private void GetAllBooks()
        {
            Console.WriteLine();
            foreach (Book b in service.rep.GetAllBooks())
            {
                Console.Write($"{b.ID} | ");
                Console.Write($"{b.Title} | ");
                Console.Write($"{b.Author} | ");
                Console.Write($"{b.Genre} | ");
                Console.Write($"{b.Quantity} left\n");
            }
            Run();
        }

        private void DeleteBookById()
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Delete book");
            Console.ResetColor();
            Console.WriteLine();
            GetAllBooks();
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            service.DeleteBook(id);
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

            service.AddBook(new Book(0, title, author, (Category)Enum.Parse(typeof(Category), genre), quantity));
            Run();
        }
    }
}
