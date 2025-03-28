using System.Collections.Generic;

using LibBMS.Domain.Models;

namespace LibBMS.Data.Repository
{
    public interface IBookRepository
    {
        int Add(Book book);
        Book GetById(int id);
        IEnumerable<Book> GetAll();
        void Update(Book book);
        void Delete(int id);
    }

}