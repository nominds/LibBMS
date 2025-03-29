using System;
using System.Collections.Generic;
using System.Data.Common;

using LibBMS.Domain.Models;
using LibBMS.Data.Repository;

namespace LibBMS.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public int AddBook(string title, string author, string isbn, string yearPublished)
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

        public void UpdateBook(int id, string title, string author, string isbn, int yearPublished)
        {
            var book = new Book
            {
                Id = id,
                Title = title,
                Author = author,
                YearPublished = yearPublished,
                ISBN = isbn
            };
            _bookRepository.Update(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepository.Delete(id);
        }

        public Book GetBookById(int id)
        {
            return _bookRepository.GetById(id);
        }
    }
}