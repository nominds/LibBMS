using System;
using System.Collections.Generic;
using System.Data.Common;

using LibBMS.Domain.Models;
using LibBMS.Data.Repository;

namespace LibBMS.Services
{
    public interface IBookService
    {
        public int AddBook(string title, string author, string isbn, string yearPublished);
        public IEnumerable<Book> GetAllBooks();
        public void UpdateBook(Book book);
        public void DeleteBook(int id);
        public Book GetBookById(int id);
    }
}