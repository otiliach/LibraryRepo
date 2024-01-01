// <copyright file="AddBookTests.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestServiceLayer.BookServicesTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using ServiceLayer.Implementation;

    /// <summary>The add book tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class AddBookTests
    {
        /// <summary>The log add null book.</summary>
        private const string LogAddNullBook = "Attempted to add a null book.";

        /// <summary>The log add invalid book.</summary>
        private const string LogAddInvalidBook = "Attempted to add an invalid book.";

        /// <summary>The log add existing book.</summary>
        private const string LogAddExistingBook = "Attempted to add a duplicate book.";

        /// <summary>The log add book with too many doamins.</summary>
        private const string LogAddBookWithTooManyDoamins = "Attempted to add a book with too many domains.";

        /// <summary>The log add book ancestor descendant doamins.</summary>
        private const string LogAddBookAncestorDescendantDoamins = "Attempted to add a book with directly related domains.";

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
                new Domain("Structuri de date", null),
                new Domain("Algoritmi", null),
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

        /// <summary>Adds the null book.</summary>
        [Test]
        public void AddNullBook()
        {
            this.book = null;

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.AddBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullBook)));
        }

        /// <summary>Adds the empty book.</summary>
        [Test]
        public void AddEmptyBook()
        {
            this.book = new Book();

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.AddBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBook))));
        }

        /// <summary>Adds the book who has null title.</summary>
        [Test]
        public void AddBookNullTitle()
        {
            this.book.Title = null;

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();

            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.AddBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBook))));
        }

        /// <summary>Adds the book who has title too long.</summary>
        [Test]
        public void AddBookTitleTooLong()
        {
            this.book.Title = new string('a', 51);

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.AddBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBook))));
        }

        /// <summary>Adds the book already exist.</summary>
        [Test]
        public void AddBookAlreadyExist()
        {
            var bookServiceMock = new Mock<IBookDataServices>();
            bookServiceMock.Setup(x => x.BookAlreadyExists(this.book.Title, this.book.Authors.ToList())).Returns(true);
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetChildDomains(It.IsAny<Domain>())).Returns(new List<Domain>());
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetDOMENII()).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.AddBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddExistingBook))));
        }

        /// <summary>Adds the book who has too many domains.</summary>
        [Test]
        public void AddBookTooManyDomains()
        {
            var bookServiceMock = new Mock<IBookDataServices>();
            bookServiceMock.Setup(x => x.BookAlreadyExists(this.book.Title, this.book.Authors.ToList())).Returns(false);
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetChildDomains(It.IsAny<Domain>())).Returns(new List<Domain>());
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetDOMENII()).Returns(1);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.AddBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddBookWithTooManyDoamins))));
        }

        /// <summary>Adds the book who has ancestor-descendant domains.</summary>
        [Test]
        public void AddBookAncestorDescendantDomains()
        {
            Domain domain = new Domain("Știință", null);
            Domain domain2 = new Domain("Informatică", domain);
            Domain domain3 = new Domain("structuri de date", domain);
            Domain domain4 = new Domain("Algoritmi", domain);
            this.book.Domains = new List<Domain>()
            {
                domain2,
                domain3,
                domain4,
            };
            var bookServiceMock = new Mock<IBookDataServices>();
            bookServiceMock.Setup(x => x.BookAlreadyExists(this.book.Title, this.book.Authors.ToList())).Returns(false);
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetChildDomains(It.IsAny<Domain>())).Returns(new List<Domain>()
            {
                domain2,
                domain3,
                domain4,
            });
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetDOMENII()).Returns(10);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(bookServices.AddBook(this.book));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddBookAncestorDescendantDoamins))));
        }

        /// <summary>Adds the valid book.</summary>
        [Test]
        public void AddValidBook()
        {
            var bookServiceMock = new Mock<IBookDataServices>();
            bookServiceMock.Setup(x => x.AddBook(this.book)).Returns(true);
            bookServiceMock.Setup(x => x.BookAlreadyExists(this.book.Title, this.book.Authors.ToList())).Returns(false);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetDOMENII()).Returns(10);
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetChildDomains(It.IsAny<Domain>())).Returns(new List<Domain>());

            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(bookServices.AddBook(this.book));
        }
    }
}
