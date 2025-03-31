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



namespace LibBMS.ViewBookWorker.Tests
{
    public class BookServiceViewBookWorkerTests
    {
        private readonly Mock<BookService> _mockBookService;
        private readonly Mock<LibBMSLogger> _mockLogger;
        private readonly Mock<BookServiceViewBookWorker> _mockViewBookWorker;
        private readonly BookServiceViewBookWorker _viewBookWorker;

        public BookServiceViewBookWorkerTests()
        {
            var mockRepository = new Mock<IBookRepository>();
            mockRepository.Setup(repo => repo.Add(It.IsAny<Book>())).Verifiable();

            _mockBookService = new Mock<BookService>(mockRepository.Object);
            _mockLogger = new Mock<LibBMSLogger>();
            _mockViewBookWorker = new Mock<BookServiceViewBookWorker>();

            _viewBookWorker = new BookServiceViewBookWorker();
        }

        [Fact]
        public void ViewBook_ShouldReturn_WhenNoConsoleInputDetected()
        {
            var invalidBookId = 20000;
            _mockViewBookWorker.Setup(v => v.ListBooksById(It.IsAny<BookService>())).Verifiable();
            _mockBookService.Setup(b => b.GetBookById(invalidBookId)).Throws(new KeyNotFoundException("Book not found"));
            Console.SetIn(new System.IO.StringReader(invalidBookId.ToString()));
            Assert.NotNull(() => _viewBookWorker.ViewBookById(_mockBookService.Object));
        }

    }
}
