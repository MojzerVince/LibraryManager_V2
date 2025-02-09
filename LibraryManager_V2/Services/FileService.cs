using LibraryManager_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager_V2.Services
{
    public class FileService
    {
        public FileService()
        {
            CheckForSaveFile();
        }

        private void CheckForSaveFile()
        {
            if (!File.Exists("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\library.txt"))
            {
                FileStream fs = File.Create("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\library.txt");
                fs.Close();
            }
        }

        public void SaveLibrary(List<Book> books)
        {
            StreamWriter sw = new StreamWriter("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\library.txt");
            foreach (Book book in books)
                sw.WriteLine(book.ToString());
            sw.Close();
        }

        public List<Book> LoadLibrary()
        {
            StreamReader sr = new StreamReader("..\\..\\..\\..\\LibraryManager_V2\\bin\\Debug\\net8.0\\library.txt");
            string line;
            List<Book> books = new List<Book>();
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine();
                string[] parts = line.Split('|');
                Book book = new Book(parts[1], parts[2], (Category)Enum.Parse(typeof(Category), parts[3]), int.Parse(parts[4]));
                books.Add(book);
            }
            sr.Close();
            return books;
        }
    }
}
