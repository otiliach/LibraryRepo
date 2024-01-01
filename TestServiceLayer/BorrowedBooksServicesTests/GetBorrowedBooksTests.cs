// <copyright file="GetBorrowedBooksTests.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestServiceLayer.BorrowedBooksServicesTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using ServiceLayer.Implementation;

    /// <summary>The get borrowed books tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class GetBorrowedBooksTests
    {
        /// <summary>The borrowed books.</summary>
        private BorrowedBooks borrowedBooks;

        /// <summary>Gets the test borrowed books.</summary>
        /// <returns>A BorrowedBooks.</returns>
        private BorrowedBooks GetTestBorrowedBooks()
        {
            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 5, 2),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            DateTime borrowDate = DateTime.Today;

            DateTime originalReturnDate = DateTime.Today.AddDays(14);

            List<Book> books = new List<Book>
            {
                new Book("Data Structures & Algorithms in C++", authors, domains, editions),
            };

            User reader = new User(
                "Otilia",
                "Chelmus",
                "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 15",
                "OtiC@FakeMail.com",
                "0123456789",
                "Pswrd@23",
                EAccountType.Reader);

            User librarian = new User(
                "Lia",
                "Ito",
                "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 16",
                "LiaC@FakeMail.com",
                "0123456788",
                "Pswrd@32",
                EAccountType.Librarian);

            this.borrowedBooks = new BorrowedBooks(borrowDate, reader, editions, originalReturnDate, librarian, EBorrowStatus.Extended);

            return this.borrowedBooks;
        }

        /// <summary>Gets the test borrowed bookss.</summary>
        /// <returns>A List of BorrowedBooks.</returns>
        private List<BorrowedBooks> GetTestBorrowedBookss()
        {
            return new List<BorrowedBooks>
            {
                this.GetTestBorrowedBooks(),
                this.GetTestBorrowedBooks(),
                this.GetTestBorrowedBooks(),
            };
        }

        /// <summary>Gets all borrowed books.</summary>
        [Test]
        public void GetAllBorrowedBooks()
        {
            List<BorrowedBooks> borrowedBookss = this.GetTestBorrowedBookss();

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();

            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooks()).Returns(borrowedBookss);
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            var expected = borrowedBookss;
            var actual = borrowedBooksServices.GetBorrowedBooks();

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual.ElementAt(i).Id, Is.EqualTo(expected[i].Id));
                Assert.That(actual.ElementAt(i).Reader.Id, Is.EqualTo(expected[i].Reader.Id));
            }
        }

        /// <summary>Gets the borrowed bookss none found.</summary>
        [Test]
        public void GetBorrowedBookss_NoneFound()
        {
            List<BorrowedBooks> borrowedBookss = new List<BorrowedBooks>();

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooks()).Returns(borrowedBookss);
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(borrowedBooksServices.GetBorrowedBooks());
        }

        /// <summary>Gets the borrowed books by user identifier.</summary>
        [Test]
        public void GetBorrowedBooksByUserId()
        {
            List<BorrowedBooks> borrowedBooks = this.GetTestBorrowedBookss();
            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(borrowedBooks);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);
            var expected = borrowedBooks;
            var actual = borrowedBooksServices.GetBorrowedBooksByUserId(It.IsAny<int>());

            Assert.IsNotNull(actual);

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual[i].Id, Is.EqualTo(expected[i].Id));
                Assert.That(actual[i].Reader.Id, Is.EqualTo(expected[i].Reader.Id));
            }
        }

        /// <summary>Gets the borrowed books by identifier not found.</summary>
        [Test]
        public void GetBorrowedBooksById_NotFound()
        {
            BorrowedBooks borrowedBooks = this.GetTestBorrowedBooks();

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.AddBorrowedBooks(this.borrowedBooks)).Returns(true);
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(new List<BorrowedBooks>());
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();

            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(
                borrowedBooksServiceMock.Object,
                conditionServiceMock.Object,
                editionServiceMock.Object,
                loggerMock.Object);

            Assert.IsEmpty(borrowedBooksServices.GetBorrowedBooksByUserId(borrowedBooks.Id));
        }
    }
}
