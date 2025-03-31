
using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Logger;

namespace LibBMS.ViewBookWorker
{

    public class BookServiceViewBookWorker : IBookServiceViewBookWorker
    {

        public void ViewBookById(BookService bookService)
        {

            ListBooksById(bookService);

            LibBMSLogger.Instance.Information("\nEnter the ID of the book to list details: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                LibBMSLogger.Instance.Information("ID: {@bookId} entered is not valid.", bookId);
                return;
            }

            var book = bookService.GetBookById(bookId);
            if (book == null || string.IsNullOrEmpty(book.ISBN))
            {
                LibBMSLogger.Instance.Information("No book found in the library with given bookId: {@bookId}.", bookId);
                return;
            }

            LibBMSLogger.Instance.Information($"{book.Id}: {book.Title} by {book.Author} with ISBN: {book.ISBN} published in year ({book.YearPublished})");

        }


        public void ViewBooks(BookService bookService)
        {
            LibBMSLogger.Instance.Information("\nList of available books in the library:");

            var books = bookService.GetAllBooks().ToList();
            if (!books.Any())
            {
                LibBMSLogger.Instance.Information("Oops ! The library has opened very recently or books available are none. Please add new books to your favorite library.");
            }
            else
            {
                LibBMSLogger.Instance.Information("List of available books in the library.");
                foreach (var book in books)
                {
                    LibBMSLogger.Instance.Information("{@Id}: {@Title} by {@Author} with ISBN: {@ISBN} publshed in year ({@YearPublished})", book.Id, book.Title, book.Author, book.ISBN, book.YearPublished);
                }
            }
        }

        public virtual void ListBooksById(BookService bookService)
        {
            LibBMSLogger.Instance.Information("\nList of all the books currently available in the library:");

            var books = bookService.GetAllBooks().ToList();
            if (!books.Any())
            {
                LibBMSLogger.Instance.Information("Oops ! The library has opened very recently or books available are none. Please add new books to your favorite library.");
                return;
            }
            else
            {

                foreach (var book in books)
                {
                    LibBMSLogger.Instance.Information($"{book.Id}");
                }
            }
        }
    }
}