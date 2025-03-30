
using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Logger;

namespace LibBMS.AddBookWorker
{

    public class BookServiceAddBookWorker()
    {

        public void AddBook(BookService bookService)
        {
            
            LibBMSLogger.Instance.Information("\nEnter the book details:");

            Console.Write("Title: ");
            var title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                LibBMSLogger.Instance.Error("\nInvalid book title {@title}. Empty or only whitespaces not allowed.", title);
                return;
            }

            Console.Write("Author: ");
            var author = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(author))
            {
                LibBMSLogger.Instance.Error("\nInvalid author name {@authorName}. Empty or only whitespaces not allowed.", author);
                return;
            }

            Console.Write("ISBN (Supports only ISBN-10, or ISBN-13 format): ");
            var isbn = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(isbn))
            {
                LibBMSLogger.Instance.Error("\nInvalid book ISBN {@isbn}. Empty or only whitespaces not allowed. ", isbn);
                return;
            }

            if (!(Utilities.IsValidISBN(isbn)))
            {
                LibBMSLogger.Instance.Error("\nISBN validation failed on the entered ISBN: {@isbn}. Doesn't comply with ISBN-10 or ISBN-13 format. ", isbn);
                return;
            }

            Console.Write("Year Published (YYYY): ");
            var yearPublished = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(yearPublished))
            {
                LibBMSLogger.Instance.Error("\nInvalid publishing year {@year}. Empty or only whitespaces not allowed.  ", yearPublished);
                return;
            }

            if (!(Utilities.IsValidYear(yearPublished)))
            {
                int currentYear = DateTime.Now.Year;
                LibBMSLogger.Instance.Error("\nPublishing year validation failed. The publishing year should be less than or equal to the current year: {@presentYear}. ", currentYear);
                return;
            }

            int bookId = bookService.AddBook(title, author, isbn, yearPublished);
            LibBMSLogger.Instance.Information("\nA new book added successfully. The new books id : {@bookId}", bookId);
        }

    }
}