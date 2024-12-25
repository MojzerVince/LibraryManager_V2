using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager_V2.Models
{
    internal class Book
    {
        public int ID { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Author { get; set; } = String.Empty;
        public string ReleaseDate { get; set; } = String.Empty;
        public int Quantity { get; set; }

        public Book(int id, string title, string author, string releaseDate, int quantity)
        {
            ID = id;
            Title = title;
            Author = author;
            ReleaseDate = releaseDate;
            Quantity = quantity;
        }
    }
}
