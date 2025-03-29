using System;
using System.Collections.Generic;
using System.Linq;

using LibBMS.Domain.Models;

namespace LibBMS.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly List<Book> _books = new List<Book>();

        public int Add(Book book)
        {
            if (book == null) throw new ArgumentNullException(nameof(book));
            _books.Add(book);
            return book.Id;
        }

        public Book GetById(int id)
        {
            return _books.SingleOrDefault(book => book.Id == id) ?? (Book) new Object();
           
        }

        public IEnumerable<Book> GetAll()
        {
            return _books;
        }

        public void Update(Book book)
        {
            var existingBook = GetById(book.Id);
            if (existingBook == null) throw new KeyNotFoundException("Book not found.");

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.YearPublished = book.YearPublished;
            existingBook.ISBN = book.ISBN;
        }

        public void Delete(int id)
        {
            var book = GetById(id);
            if (book == null) throw new KeyNotFoundException("Book not found.");

            _books.Remove(book);
        }
    }

}