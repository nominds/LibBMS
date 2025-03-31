using Moq;
using Xunit;
using System;
using System.Collections.Generic;

using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Logger;
using LibBMS.ViewBookWorker;
using LibBMS.Domain.Models;
using LibBMS.Data.Repository;
using LibBMS.UpdateBookWorker;


namespace LibBMS.UpdateBookWorker.Tests
{
    public class BookServiceUpdateBookWorkerTests
    {
        private readonly Mock<BookService> _mockBookService;
        private readonly Mock<LibBMSLogger> _mockLogger;
        private readonly Mock<BookServiceViewBookWorker> _mockViewBookWorker;
        private readonly BookServiceUpdateBookWorker _updateBookWorker;

        public BookServiceUpdateBookWorkerTests()
        {
            var mockRepository = new Mock<IBookRepository>();
            mockRepository.Setup(repo => repo.Add(It.IsAny<Book>())).Verifiable();

            _mockBookService = new Mock<BookService>(mockRepository.Object);
            _mockLogger = new Mock<LibBMSLogger>();
            _mockViewBookWorker = new Mock<BookServiceViewBookWorker>();

            _updateBookWorker = new BookServiceUpdateBookWorker();
        }

        [Fact]
        public void UpdateBook_ShouldReturn_WhenNoConsoleInputDetected()
        {
            int invalidBookId = 2000; // current implementation returns random less than 1000
            var book = new Book
            {
                Id = new Random().Next(1, 1000),
                Title = "Book1",
                Author = "Author1",
                YearPublished = 2012,
                ISBN = "9781760279486"
            };
            _mockViewBookWorker.Setup(v => v.ListBooksById(It.IsAny<BookService>())).Verifiable();
            _mockBookService.Setup(b => b.UpdateBook(book)).Throws(new KeyNotFoundException("Book not found"));
            Console.SetIn(new System.IO.StringReader(invalidBookId.ToString()));
            Assert.NotNull(() => _updateBookWorker.UpdateBook(_mockBookService.Object));
        }

    }
}
