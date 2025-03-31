
using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Logger;

namespace LibBMS.ViewBookWorker
{

    public interface IBookServiceViewBookWorker
    {

        public void ViewBookById(BookService bookService);
        public void ViewBooks(BookService bookService);
        public void ListBooksById(BookService bookService);
    }
}