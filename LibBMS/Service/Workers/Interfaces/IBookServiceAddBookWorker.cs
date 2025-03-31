
using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Logger;

namespace LibBMS.AddBookWorker
{
    public interface IBookServiceAddBookWorker
    {
        public void AddBook(BookService bookService);

    }
}