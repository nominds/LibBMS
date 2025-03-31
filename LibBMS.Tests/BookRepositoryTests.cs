using Xunit;
using System.Linq;

using LibBMS.Data.Repository;
using LibBMS.Domain.Models;

namespace LibBMS.BookRepositoryTests
{
    public class BookRepositoryTests
    {
        private readonly BookRepository _bookRepository;

        public BookRepositoryTests()
        {
            _bookRepository = new BookRepository();
        }

        [Fact]
        public void AddBook_ShouldAddBook()
        {
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = "Book1",
                Author = "Author1",
                YearPublished = 2012,
                ISBN = "9781760279486"
            };

            var bookId = _bookRepository.Add(book);

            var bookOne = _bookRepository.GetById(bookId);
            Assert.NotNull(bookOne);
            Assert.Equal("Author1", bookOne.Author);
            Assert.Equal("9781760279486", bookOne.ISBN);
        }


        [Fact]
        public void GetByNonExistentId_ShouldReturnNull()
        {
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = "Book1",
                Author = "Author1",
                YearPublished = 2012,
                ISBN = "9781760279486"
            };

            var bookId = _bookRepository.Add(book);

            var bookOne = _bookRepository.GetById(bookId + 1);
            Assert.Null(bookOne);
        }

        [Fact]
        public void GetByExistentId_ShouldReturnBook()
        {
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = "Book1",
                Author = "Author1",
                YearPublished = 2012,
                ISBN = "9781760279486"
            };

            var bookId = _bookRepository.Add(book);

            var bookOne = _bookRepository.GetById(bookId);
            Assert.NotNull(bookOne);
            Assert.Equal("2012", (bookOne.YearPublished).ToString());
            Assert.Equal("Book1", bookOne.Title);
        }


        [Fact]
        public void UpdateByExistentId_ShouldUpdateBook()
        {
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = "Book1",
                Author = "Author1",
                YearPublished = 2012,
                ISBN = "9781760279486"
            };

            var bookId = _bookRepository.Add(book);

            var bookOne = _bookRepository.GetById(bookId);
            Assert.NotNull(bookOne);
            Assert.Equal("2012", (bookOne.YearPublished).ToString());
            Assert.Equal("Book1", bookOne.Title);

            bookOne.Title = "NewBook1";
            bookOne.YearPublished = 2013;

            _bookRepository.Update(bookOne);

            bookOne = _bookRepository.GetById(bookId);
            Assert.NotNull(bookOne);
            Assert.Equal("2013", (bookOne.YearPublished).ToString());
            Assert.Equal("NewBook1", bookOne.Title);

        }

        [Fact]
        public void DeleteByExistentId_ShouldDeleteBook()
        {
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = "Book1",
                Author = "Author1",
                YearPublished = 2012,
                ISBN = "9781760279486"
            };

            var bookId = _bookRepository.Add(book);

            var bookOne = _bookRepository.GetById(bookId);
            Assert.NotNull(bookOne);
            Assert.Equal("2012", (bookOne.YearPublished).ToString());
            Assert.Equal("Book1", bookOne.Title);

            _bookRepository.Delete(bookId);
            bookOne = _bookRepository.GetById(bookId);
            Assert.Null(bookOne);

        }

        [Fact]
        public void DeleteNonExistentId_ShouldThrowException()
        {
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = "Book1",
                Author = "Author1",
                YearPublished = 2012,
                ISBN = "9781760279486"
            };

            var bookId = _bookRepository.Add(book);

            var bookOne = _bookRepository.GetById(bookId);
            Assert.NotNull(bookOne);
            Assert.Equal("2012", (bookOne.YearPublished).ToString());
            Assert.Equal("Book1", bookOne.Title);

            try
            {
                _bookRepository.Delete(bookId + 1);
            }
            catch (KeyNotFoundException knfe)
            {
                Assert.Equal("Book not found.", knfe.Message);

            }

        }

        [Fact]
        public void UpdateNonExistentId_ShouldThrowException()
        {
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = "Book1",
                Author = "Author1",
                YearPublished = 2012,
                ISBN = "9781760279486"
            };

            var bookId = _bookRepository.Add(book);

            var bookOne = _bookRepository.GetById(bookId);
            Assert.NotNull(bookOne);
            Assert.Equal("2012", (bookOne.YearPublished).ToString());
            Assert.Equal("Book1", bookOne.Title);

            bookOne.Id = bookId + 1;

            try
            {
                _bookRepository.Update(bookOne);
            }
            catch (KeyNotFoundException knfe)
            {
                Assert.Equal("Book not found.", knfe.Message);

            }

        }




    }
}
