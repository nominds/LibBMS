using Xunit;
using System.Linq;

using LibBMS.Data.Repository;
using LibBMS.Services;

namespace LibBMS.Tests
{
    public class BookRepositoryTests
    {
        private readonly BookService _bookService;
        private readonly BookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _bookRepository = new BookRepository();
            _bookService = new BookService(_bookRepository);
        }

        [Fact]
        public void AddBook_ShouldAddBook()
        {
            int bookId = _bookService.AddBook("Test", "Test", "1231232", "2025");
            var book = _bookService.GetBookById(bookId);
            Assert.NotNull(book);
            Assert.Equal("Test", book.Title);
        }

    [Fact]
    public void GetAllBooks_ShouldReturnAllBooks()
     {
       int bookOne = _bookService.AddBook("Test", "Test", "1231232", "2025");
       int bookTwo = _bookService.AddBook("Test", "Test", "1231232", "2025");

       var books = _bookService.GetAllBooks();
       Assert.Equal(2, books.Count());
    }

    [Fact]
    public void UpdateBook_ShouldUpdateBookDetails()
     {
       int bookId = _bookService.AddBook("Test", "Test", "1231232", "2025");
       var book = _bookService.GetBookById(bookId);
       Assert.NotNull(book);
       Assert.Equal("Test", book.Title);

        book.YearPublished = 2024;
       _bookService.UpdateBook(book);
       var updatedBook = _bookService.GetBookById(book.Id);
       Assert.NotNull(updatedBook);
       Assert.Equal(2024, updatedBook.YearPublished);
    }



    }
}
