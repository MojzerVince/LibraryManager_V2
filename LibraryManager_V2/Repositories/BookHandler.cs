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
            books.Add(book);
        }

        public void DeleteBook(int id)
        {
            for(int i = 0; i < books.Count; i++)
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
            for(int i = 0; i < books.Count; i++)
            {
                if (books[i].ID == id)
                {
                    return books[i];
                }
                else
                    throw new Exception("Book not found");
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
                existing.ReleaseDate = book.ReleaseDate;
                existing.Quantity = book.Quantity;
            }
            else
            {
                throw new Exception("Book not found");
            }
        }
    }
}