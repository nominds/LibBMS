using System.Collections.Generic;

using LibBMS.Services;

namespace LibBMS.DeleteBookWorker
{
    public interface IBookServiceDeleteBookWorker
    {
        void DeleteBook(BookService bookService);
    }
}