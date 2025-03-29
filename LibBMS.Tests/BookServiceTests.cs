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

       _bookService.UpdateBook(bookId, "Test", "Test", "1231232", 2024);
       var updatedBook = _bookService.GetBookById(bookId);
       Assert.NotNull(updatedBook);
       Assert.Equal(2024, updatedBook.YearPublished);
    }



    }
}
