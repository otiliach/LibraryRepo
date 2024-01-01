// <copyright file="GetBookTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The get books tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class GetBookTests
    {
        /// <summary>Gets the test book.</summary>
        /// <returns>A test book.</returns>
        private static Book GetTestBook()
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
                new Edition("Litera", 2010, 714, 1, EBookType.Paperback, 5, 2),
            };

            return new Book(
                "Data Structures & Algorithms in C++",
                authors,
                domains,
                editions);
        }

        private static List<Book> GetTestBooks()
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

            return new List<Book>
            {
                new Book(
                    "Data Structures & Algorithms in C++", 
                    authors,
                    domains,
                    new List<Edition>()
                    {
                        new Edition("Litera", 2011, 714, 2, EBookType.Hardcover, 5, 2),
                        new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 5, 2),
                    }),
                new Book(
                    "Data Structures & Algorithms in C",
                    authors,
                    domains,
                    new List<Edition>() { new Edition("Litera", 2000, 650, 1, EBookType.Hardcover, 5, 2) }),
            };
        }

        /// <summary>Gets all book.</summary>
        [Test]
        public void GetAllBook()
        {
            List<Book> books = GetTestBooks();

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBooks()).Returns(books);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            var expected = books;
            var actual = bookServices.GetBooks();

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual.ElementAt(i).Id, Is.EqualTo(expected[i].Id));
                Assert.That(actual.ElementAt(i).Title, Is.EqualTo(expected[i].Title));
            }
        }

        /// <summary>Gets the books none found.</summary>
        [Test]
        public void GetBooks_NoneFound()
        {
            List<Book> books = new List<Book>();

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBooks()).Returns(books);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(bookServices.GetBooks());
        }

        /// <summary>Gets the book by identifier.</summary>
        [Test]
        public void GetBookById()
        {
            Book book = GetTestBook();
            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBookById(book.Id)).Returns(book);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);
            var expected = book;
            var actual = bookServices.GetBookById(book.Id);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
        }

        /// <summary>Gets the book by identifier not found.</summary>
        [Test]
        public void GetBookById_NotFound()
        {
            Book book = GetTestBook();
            Book nullBook = null;

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBookById(book.Id)).Returns(nullBook);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsNull(bookServices.GetBookById(book.Id));
        }

        /// <summary>Gets the book by title.</summary>
        [Test]
        public void GetBookByTitle()
        {
            Book book = GetTestBook();
            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBookByTitle(book.Title)).Returns(book);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);
            var expected = book;
            var actual = bookServices.GetBookByTitle(book.Title);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Title, Is.EqualTo(expected.Title));
        }

        /// <summary>Gets the book by title not found.</summary>
        [Test]
        public void GetBookByTitle_NotFound()
        {
            Book book = GetTestBook();
            Book nullBook = null;

            var bookServiceMock = new Mock<IBookDataServices>();
            var domainServiceMock = new Mock<IDomainDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            bookServiceMock.Setup(x => x.GetBookByTitle(book.Title)).Returns(nullBook);
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsNull(bookServices.GetBookByTitle(book.Title));
        }

        /// <summary>Gets the books by domain.</summary>
        [Test]
        public void GetBooksByDomain()
        {
            Domain domain = new Domain("Informatica", null);
            Book book = GetTestBook();
            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            var bookServiceMock = new Mock<IBookDataServices>();
            bookServiceMock.Setup(x => x.GetBooks()).Returns(new List<Book> { book });
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetChildDomains(domain)).Returns(domains);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsNotEmpty(bookServices.GetBooksByDomain(domain));
        }

        /// <summary>Gets the books by domain not found.</summary>
        [Test]
        public void GetBooksByDomain_NotFound()
        {
            Domain domain = new Domain("Domeniu", null);
            Book book = GetTestBook();

            var bookServiceMock = new Mock<IBookDataServices>();
            bookServiceMock.Setup(x => x.GetBooks()).Returns(new List<Book>());
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetChildDomains(domain)).Returns(new List<Domain>());
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var bookServices = new BookServicesImplementation(bookServiceMock.Object, domainServiceMock.Object, conditionServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(bookServices.GetBooksByDomain(domain));
        }
    }
}
