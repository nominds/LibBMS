using Moq;
using Xunit;
using System;
using System.Collections.Generic;

using LibBMS.Common;
using LibBMS.Services;
using LibBMS.Domain.Models;
using LibBMS.Data.Repository;
using LibBMS.AddBookWorker;


namespace LibBMS.AddBookWorker.Tests
{
    public class BookServiceAddBookWorkerTests
    {
        private readonly Mock<BookService> _mockBookService;
        private readonly BookServiceAddBookWorker _addBookWorker;

        public BookServiceAddBookWorkerTests()
        {
            var mockRepository = new Mock<IBookRepository>();
            mockRepository.Setup(repo => repo.Add(It.IsAny<Book>())).Verifiable();
            _mockBookService = new Mock<BookService>(mockRepository.Object);
            _addBookWorker = new BookServiceAddBookWorker();
        }


        [Fact]
        public void AddBook_InvalidBookTitle_ShouldLogFailure()
        {
            string sampleString = "\nAuthor\n9781760279486\n2024";
            StringReader stringReader = new StringReader(sampleString);
            Console.SetIn(stringReader);
            string title = Console.ReadLine();
            string author = Console.ReadLine();
            string isbn = Console.ReadLine();
            string yearPublished = Console.ReadLine();

            _mockBookService.Setup(x => x.AddBook(It.Is<string>(title => title == ""),
                                        It.Is<string>(author => author == "Author"),
                                        It.Is<string>(isbn => isbn == "9781760279486"),
                                        It.Is<string>(yearPublished => yearPublished == "2024")))
                           .Returns(1);

            _addBookWorker.AddBook(_mockBookService.Object);


            _mockBookService.Verify(x => x.AddBook(title, author, isbn, yearPublished), Times.Never);
        }


        [Fact]
        public void AddBook_InvalidBookAuthor_ShouldLogFailure()
        {
            string sampleString = "Title\n\n9781760279486\n2024";
            StringReader stringReader = new StringReader(sampleString);
            Console.SetIn(stringReader);
            string title = Console.ReadLine();
            string author = Console.ReadLine();
            string isbn = Console.ReadLine();
            string yearPublished = Console.ReadLine();

            _mockBookService.Setup(x => x.AddBook(It.Is<string>(title => title == ""),
                                        It.Is<string>(author => author == "Author"),
                                        It.Is<string>(isbn => isbn == "9781760279486"),
                                        It.Is<string>(yearPublished => yearPublished == "2024")))
                           .Returns(1);

            _addBookWorker.AddBook(_mockBookService.Object);


            _mockBookService.Verify(x => x.AddBook(title, author, isbn, yearPublished), Times.Never);
        }

        [Fact]
        public void AddBook_InvalidYearPublishedBookDetails_ShouldLogFailure()
        {
            string sampleString = "Title\nAuthor\n9781760279486\n2027";
            StringReader stringReader = new StringReader(sampleString);
            Console.SetIn(stringReader);
            string title = Console.ReadLine();
            string author = Console.ReadLine();
            string isbn = Console.ReadLine();
            string yearPublished = Console.ReadLine();

            _mockBookService.Setup(x => x.AddBook(It.Is<string>(title => title == ""),
                                        It.Is<string>(author => author == "Author"),
                                        It.Is<string>(isbn => isbn == "9781760279486"),
                                        It.Is<string>(yearPublished => yearPublished == "2027")))
                           .Returns(1);

            _addBookWorker.AddBook(_mockBookService.Object);


            _mockBookService.Verify(x => x.AddBook(title, author, isbn, yearPublished), Times.Never);
        }


        [Fact]
        public void AddBook_InvalidISBNBookDetails_ShouldLogFailure()
        {
            string sampleString = "Title\nAuthor\n111111\n2024";
            StringReader stringReader = new StringReader(sampleString);
            Console.SetIn(stringReader);
            string title = Console.ReadLine();
            string author = Console.ReadLine();
            string isbn = Console.ReadLine();
            string yearPublished = Console.ReadLine();

            _mockBookService.Setup(x => x.AddBook(It.Is<string>(title => title == ""),
                                        It.Is<string>(author => author == "Author"),
                                        It.Is<string>(isbn => isbn == "111111"),
                                        It.Is<string>(yearPublished => yearPublished == "2024")))
                           .Returns(1);

            _addBookWorker.AddBook(_mockBookService.Object);


            _mockBookService.Verify(x => x.AddBook(title, author, isbn, yearPublished), Times.Never);
        }

    }
}
