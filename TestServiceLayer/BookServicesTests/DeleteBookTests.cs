// <copyright file="DeleteBookTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The delete book tests.</summary>
    [ExcludeFromCodeCoverage]
    internal class DeleteBookTests
    {
        /// <summary>The log delete null book.</summary>
        private const string LogDeleteNullBook = "Attempted to delete a null book.";

        /// <summary>The log delete non existing book.</summary>
        private const string LogDeleteNonExistingBook = "Attempted to delete a non-existing book.";

        /// <summary>The book.</summary>
        private Book book;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            List<Author> authors = new List<Author>()
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
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 5, 2),
            };

            this.book = new Book(
                "Data Structures & Algorithms in C++",
                authors,
                domains,
                editions);

            int bookId = this.book.Id;
        }

        /// <summary>Deletes the null book.</summary>
        [Test]
        public void DeleteNullBook()
        {
            this.book = null;

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();

            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.DeleteBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullBook)));
        }

        /// <summary>Deletes the book doesnt exist.</summary>
        [Test]
        public void DeleteBookDoesntExist()
        {
            Book nullBook = null;

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBookById(this.book.Id)).Returns(nullBook);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.DeleteBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogDeleteNonExistingBook))));
        }

        /// <summary>Deletes the valid book.</summary>
        [Test]
        public void DeleteValidBook()
        {
            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBookById(this.book.Id)).Returns(this.book);
            bookServiceMock.Setup(x => x.DeleteBook(this.book)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(bookServices.DeleteBook(this.book));
        }
    }
}
