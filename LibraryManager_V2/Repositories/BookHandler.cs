﻿using LibraryManager_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager_V2.Repositories
{
    internal class BookRepository : IBookRepository
    {
        private List<Book> books;

        public BookRepository()
        {
            books = new List<Book>();
        }

        public void AddBook(Book book)
        {
            book.ID = GenerateID();
            books.Add(book);
        }

        public void DeleteBook(int id)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ID == id)
                {
                    books.RemoveAt(i);
                    return;
                }
            }
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public Book? GetBookById(int id)
        {
            for (int i = 0; i < books.Count; i++)
            {
                if (books[i].ID == id)
                {
                    return books[i];
                }
            }
            return null;
        }

        public void UpdateBook(Book book)
        {
            Book existing = GetBookById(book.ID);
            if (existing != null)
            {
                existing.Title = book.Title;
                existing.Author = book.Author;
                existing.Genre = book.Genre;
                existing.Quantity = book.Quantity;
            }
            else
            {
                throw new Exception("Book not found");
            }
        }

        private int GenerateID()
        {
            int id = 0;
            foreach (Book b in books)
            {
                if (b.ID > id)
                {
                    id = b.ID;
                }
            }
            return ++id;
        }
    }
}
