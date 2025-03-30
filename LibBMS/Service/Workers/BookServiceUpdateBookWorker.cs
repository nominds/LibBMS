using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Logger;
using LibBMS.ViewBookWorker;

namespace LibBMS.UpdateBookWorker
{

    public class BookServiceUpdateBookWorker()
    {

        public void UpdateBook(BookService bookService)
        {
            var viewBookWorker = new BookServiceViewBookWorker();
            viewBookWorker.ListBooksById(bookService);

            LibBMSLogger.Instance.Information("\nEnter the ID of the book which you would like to update. ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                LibBMSLogger.Instance.Information("ID: {@bookId} entered is not valid.", bookId);
                return;
            }

            try
            {

                var book = bookService.GetBookById(bookId);

                if (book == null || string.IsNullOrEmpty(book.ISBN))
                {
                    LibBMSLogger.Instance.Information("No book found in the library with given bookId: {@bookId}.", bookId);
                    throw new KeyNotFoundException("Book not found in the library.");
                }

                LibBMSLogger.Instance.Information("\nEnter the book details:");

                Console.Write("Title: (Note: press return key if no change is desired)");
                var title = Console.ReadLine();

                if (!(string.IsNullOrWhiteSpace(title)))
                {
                    book.Title = title;
                }

                Console.Write("Author: (Note: press return key if no change is desired)");
                var author = Console.ReadLine();
                if (!(string.IsNullOrWhiteSpace(author)))
                {
                    book.Author = author;
                }

                Console.Write("ISBN:  (Note: press return key if no change is desired)");
                var isbn = Console.ReadLine();
                if (!(string.IsNullOrWhiteSpace(isbn)))
                {
                    if (!(Utilities.IsValidISBN(isbn)))
                    {
                        LibBMSLogger.Instance.Error("\nISBN validation failed on the entered ISBN: {@isbn}. Doesn't comply with ISBN-10 or ISBN-13 format. ", isbn);
                        throw new Exception("Invalid ISBN");
                    }
                    book.ISBN = isbn;
                }

                Console.Write("Year Published (YYYY): ");
                var yearPublished = Console.ReadLine();
                if (!(string.IsNullOrWhiteSpace(yearPublished)))
                {
                    if (!(Utilities.IsValidYear(yearPublished)))
                    {
                        int currentYear = DateTime.Now.Year;
                        LibBMSLogger.Instance.Error("\nPublishing year validation failed. The publishing year should be less than or equal to the current year: {@presentYear}. ", currentYear);
                        throw new Exception("Invalid year of publication.");
                    }
                    book.YearPublished = int.Parse(yearPublished);
                }

                bookService.UpdateBook(book);
                LibBMSLogger.Instance.Information("\nBook with id {@bookId} successfully updated.", bookId);
            }
            catch (KeyNotFoundException ex)
            {
                LibBMSLogger.Instance.Error("Book with id {@bookId} not found in the library. Exception: {@exceptionMessage}", bookId, ex.Message);
                throw new KeyNotFoundException(bookId.ToString());
            }
            catch (Exception ex)
            {
                LibBMSLogger.Instance.Error("Update to book with id {@bookId} failed.  Exception: {@exceptionMessage}", bookId, ex.Message);
                throw new KeyNotFoundException(bookId.ToString());
            }

        }

    }
}