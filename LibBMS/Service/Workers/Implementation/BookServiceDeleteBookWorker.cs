
using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Logger;
using LibBMS.ViewBookWorker;

namespace LibBMS.DeleteBookWorker
{

    public class BookServiceDeleteBookWorker : IBookServiceDeleteBookWorker
    {

        public void DeleteBook(BookService bookService)
        {
            var viewBookWorker = new BookServiceViewBookWorker();
            viewBookWorker.ListBooksById(bookService);

            LibBMSLogger.Instance.Information("\nEnter the ID of the book which you would like to delete: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                LibBMSLogger.Instance.Information("ID: {@bookId} entered is not valid.", bookId);
                return;
            }

            try
            {
                bookService.DeleteBook(bookId);
                LibBMSLogger.Instance.Information("Successfully deleted book with id {@bookId}. ", bookId);
            }
            catch (KeyNotFoundException ex)
            {
                LibBMSLogger.Instance.Error("Book with id {@bookId} not found in the library. Exception: {@exceptionMessage} ", bookId, ex.Message);
                throw new KeyNotFoundException(bookId.ToString());
            }

        }

    }
}