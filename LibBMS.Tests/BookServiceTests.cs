using Xunit;
using System.Linq;

using LibBMS.Data.Repository;
using LibBMS.Services;

namespace LibBMS.BookServiceTests
{
    public class BookServiceTests
    {
        private readonly BookService _bookService;
        private readonly BookRepository _bookRepository;

        public BookServiceTests()
        {
            _bookRepository = new BookRepository();
            _bookService = new BookService(_bookRepository);
        }

        [Fact]
        public void AddBook_ShouldAddBook()
        {
            int bookId = _bookService.AddBook("Book1", "Author1", "9781760279486", "2025");
            var book = _bookService.GetBookById(bookId);
            Assert.NotNull(book);
            Assert.Equal("Book1", book.Title);
        }

        [Fact]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            int bookOne = _bookService.AddBook("Book1", "Author1", "9781760279486", "1919");
            int bookTwo = _bookService.AddBook("Book2", "Author2", "9789383095384", "2024");

            var books = _bookService.GetAllBooks();
            Assert.Equal(2, books.Count());
        }

        [Fact]
        public void UpdateBook_ShouldUpdateBookDetails()
        {
            int bookId = _bookService.AddBook("Book1", "Author1", "9781760279486", "2025");
            var book = _bookService.GetBookById(bookId);
            Assert.NotNull(book);
            Assert.Equal("Book1", book.Title);

            book.YearPublished = 2024;
            _bookService.UpdateBook(book);
            var updatedBook = _bookService.GetBookById(book.Id);
            Assert.NotNull(updatedBook);
            Assert.Equal(2024, updatedBook.YearPublished);
        }

        [Fact]
        public void DeleteBook_ShouldDeleteBookDetails()
        {
            int bookOne = _bookService.AddBook("Book1", "Author1", "9781760279486", "1919");
            int bookTwo = _bookService.AddBook("Book2", "Author2", "9789383095384", "2024");
            var book = _bookService.GetBookById(bookOne);
            Assert.NotNull(book);
            Assert.Equal("Book1", book.Title);


            _bookService.DeleteBook(bookOne);
            book = _bookService.GetBookById(bookOne);
            Assert.Null(book);
            book = _bookService.GetBookById(bookTwo);
            Assert.NotNull(book);
            Assert.Equal("Author2", book.Author);
        }



    }
}
