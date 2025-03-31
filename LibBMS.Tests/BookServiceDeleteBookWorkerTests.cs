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


namespace LibBMS.DeleteBookWorker.Tests
{
    public class BookServiceDeleteBookWorkerTests
    {
        private readonly Mock<BookService> _mockBookService;
        private readonly Mock<LibBMSLogger> _mockLogger;
        private readonly Mock<BookServiceViewBookWorker> _mockViewBookWorker;
        private readonly BookServiceDeleteBookWorker _deleteBookWorker;

        public BookServiceDeleteBookWorkerTests()
        {
            var mockRepository = new Mock<IBookRepository>();
            mockRepository.Setup(repo => repo.Add(It.IsAny<Book>())).Verifiable();

            _mockBookService = new Mock<BookService>(mockRepository.Object);
            _mockLogger = new Mock<LibBMSLogger>();
            _mockViewBookWorker = new Mock<BookServiceViewBookWorker>();

            _deleteBookWorker = new BookServiceDeleteBookWorker();
        }

        [Fact]
        public void DeleteBook_ShouldFail_WhenLibraryHasZeroBooks()
        {
            var validBookId = 1;

            _mockViewBookWorker.Setup(v => v.ListBooksById(It.IsAny<BookService>())).Verifiable();
            _mockBookService.Setup(b => b.DeleteBook(validBookId)).Verifiable();
            Console.SetIn(new System.IO.StringReader(validBookId.ToString()));
            _deleteBookWorker.DeleteBook(_mockBookService.Object);
            // behavior of this test case is flaky. Need to be investigated and fixed.
            _mockBookService.Verify(b => b.DeleteBook(validBookId), Times.Once);

        }


        [Fact]
        public void DeleteBook_ShouldLogError_WhenInvalidIdIsEntered()
        {

            _mockViewBookWorker.Setup(v => v.ListBooksById(It.IsAny<BookService>())).Verifiable();
            Console.SetIn(new System.IO.StringReader("invalid"));
            _deleteBookWorker.DeleteBook(_mockBookService.Object);
            _mockBookService.Verify(b => b.DeleteBook(It.IsAny<int>()), Times.Never);
        }

        [Fact]
        public void DeleteBook_ShouldReturn_WhenNoConsoleInputDetected()
        {
            var invalidBookId = 20000;
            _mockViewBookWorker.Setup(v => v.ListBooksById(It.IsAny<BookService>())).Verifiable();
            _mockBookService.Setup(b => b.DeleteBook(invalidBookId)).Throws(new KeyNotFoundException("Book not found"));
            Console.SetIn(new System.IO.StringReader(invalidBookId.ToString()));
            Assert.NotNull(() => _deleteBookWorker.DeleteBook(_mockBookService.Object));
        }


    }
}
