using System;
using System.Collections.Generic;
using System.Data.Common;

using LibBMS.Domain.Models;
using LibBMS.Data.Repository;

namespace LibBMS.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public virtual int AddBook(string title, string author, string isbn, string yearPublished)
        {
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = title,
                Author = author,
                YearPublished = int.Parse(yearPublished),
                ISBN = isbn
            };
            _bookRepository.Add(book);
            return book.Id;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _bookRepository.GetAll();
        }

        public virtual void UpdateBook(Book book)
        {
            _bookRepository.Update(book);
        }

        public virtual void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
        }

        public virtual Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }
    }
}