using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager_V2.Models
{
    public enum Category
    {
        Fantasy,
        ScienceFiction,
        Mystery,
        Romance,
        Horror,
        Thriller,
        HistoricalFiction,
        NonFiction,
        Biography,
        Autobiography,
        Poetry,
        Drama,
        Comedy,
        Satire,
        Tragedy,
        Action,
        Adventure,
        Fable,
        FairyTale,
        Mythology,
        Legend,
        Science,
    }

    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Author { get; set; } = String.Empty;
        public Category Genre { get; set; }
        public int Quantity { get; set; }

        public Book(string title, string author, Category genre, int quantity)
        {
            Title = title;
            Author = author;
            Genre = genre;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{ID}|{Title}|{Author}|{Genre}|{Quantity}";
        }
    }
}
