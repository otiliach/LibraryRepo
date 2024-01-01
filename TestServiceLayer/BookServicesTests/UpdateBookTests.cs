// <copyright file="UpdateBookTests.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestServiceLayer.BookServicesTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using ServiceLayer.Implementation;

    /// <summary>The update Book tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class UpdateBookTests
    {
        /// <summary>The log update null book.</summary>
        private const string LogUpdateNullBook = "Attempted to update a null book.";

        /// <summary>The log update invalid book.</summary>
        private const string LogUpdateInvalidBook = "Attempted to update an invalid book.";

        /// <summary>The log update non existing book.</summary>
        private const string LogUpdateNonExistingBook = "Attempted to update a non-existing book.";

        /// <summary>The book.</summary>
        private Book book;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            List<Author> books = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2010, 714, 1, EBookType.Paperback, 5, 2),
            };

            this.book = new Book(
                "Data Structures & Algorithms in C++",
                books,
                domains,
                editions);

            int bookId = this.book.Id;
        }

        /// <summary>Updates the null book.</summary>
        [Test]
        public void UpdateNullBook()
        {
            this.book = null;

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.UpdateBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullBook)));
        }

        /// <summary>Updates the empty book.</summary>
        [Test]
        public void UpdateEmptyBook()
        {
            this.book = new Book();

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.UpdateBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidBook))));
        }

        /// <summary>Updates the book doesnt exist.</summary>
        [Test]
        public void UpdateBookDoesntExist()
        {
            Book nullBook = null;

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();

            bookServiceMock.Setup(x => x.GetBookById(this.book.Id)).Returns(nullBook);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.UpdateBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateNonExistingBook))));
        }

        /// <summary>Updates the valid book.</summary>
        [Test]
        public void UpdateValidBook()
        {
            Book nullBook = null;
            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBookById(this.book.Id)).Returns(this.book);
            bookServiceMock.Setup(x => x.GetBookByTitle(this.book.Title)).Returns(nullBook);
            bookServiceMock.Setup(x => x.UpdateBook(this.book)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(bookServices.UpdateBook(this.book));
        }
    }
}
