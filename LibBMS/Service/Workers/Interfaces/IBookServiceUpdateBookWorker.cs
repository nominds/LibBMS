using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Logger;
using LibBMS.ViewBookWorker;

namespace LibBMS.UpdateBookWorker
{

    public interface IBookServiceUpdateBookWorker
    {

        public void UpdateBook(BookService bookService);

    }
}