// <copyright file="UpdateBorrowedBooksTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The update borrowedBooks tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class UpdateBorrowedBooksTests
    {
        /// <summary>The log update null borrowed books.</summary>
        private const string LogUpdateNullBorrowedBooks = "Attempted to update a null [borrowedBooks].";

        /// <summary>The log update invalid borrowed books.</summary>
        private const string LogUpdateInvalidBorrowedBooks = "Attempted to update an invalid [borrowedBooks].";

        /// <summary>The log update non existing borrowed books.</summary>
        private const string LogUpdateNonExistingBorrowedBooks = "Attempted to update a non-existing [borrowedBooks].";

        /// <summary>The log update borrowed books extended more than lim in three months.</summary>
        private const string LogUpdateBorrowedBooksExtendedMoreThanLimInThreeMonths = "Attempted to extend more than LIM in the last 3 months.";

        /// <summary>The borrowed books.</summary>
        private BorrowedBooks borrowedBooks;

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

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
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

            int borrowedBooksId = this.borrowedBooks.Id;
        }

        /// <summary>Updates the null borrowed books.</summary>
        [Test]
        public void UpdateNullBorrowedBooks()
        {
            this.borrowedBooks = null;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.UpdateBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullBorrowedBooks)));
        }

        /// <summary>Updates the borrowed books doesn't exist.</summary>
        [Test]
        public void UpdateBorrowedBooksDoesntExist()
        {
            List<BorrowedBooks> nullBorrowedBooks = null;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();

            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(this.borrowedBooks.Reader.Id)).Returns(nullBorrowedBooks);
            var loggerMock = new Mock<ILog>();

            var borrowedBooksService = new BorrowedBooksServicesImplementation(
                borrowedBooksServiceMock.Object,
                conditionServiceMock.Object,
                editionServiceMock.Object,
                loggerMock.Object);

            Assert.IsFalse(borrowedBooksService.UpdateBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateNonExistingBorrowedBooks))));
        }

        /// <summary>Updates the invalid extend borrowed books.</summary>
        [Test]
        public void UpdateInvalidExtendBorrowedBooks()
        {
            this.borrowedBooks.ActualReturnDate = this.borrowedBooks.OriginalReturnDate.AddMonths(2);
            this.borrowedBooks.Reader.AccountType = EAccountType.LibrarianReader;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.AddBorrowedBooks(this.borrowedBooks)).Returns(true);
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(new List<BorrowedBooks>());
            borrowedBooksServiceMock.Setup(x => x.UpdateBorrowedBooks(this.borrowedBooks)).Returns(true);
            var conditionServiceMock = new Mock<IConditionDataServices>();

            conditionServiceMock.Setup(x => x.GetLIM()).Returns(28);

            var editionServiceMock = new Mock<IEditionDataServices>();

            var loggerMock = new Mock<ILog>();

            var borrowedBooksService = new BorrowedBooksServicesImplementation(
                borrowedBooksServiceMock.Object,
                conditionServiceMock.Object,
                editionServiceMock.Object,
                loggerMock.Object);

            Assert.IsFalse(borrowedBooksService.UpdateBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateBorrowedBooksExtendedMoreThanLimInThreeMonths))));
        }

        /// <summary>Updates the invalid borrowed books.</summary>
        [Test]
        public void UpdateInvalidBorrowedBooks()
        {
            this.borrowedBooks.Editions = new List<Edition>();

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.AddBorrowedBooks(this.borrowedBooks)).Returns(true);
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(new List<BorrowedBooks>());
            borrowedBooksServiceMock.Setup(x => x.UpdateBorrowedBooks(this.borrowedBooks)).Returns(true);
            var conditionServiceMock = new Mock<IConditionDataServices>();

            conditionServiceMock.Setup(x => x.GetLIM()).Returns(28);

            var editionServiceMock = new Mock<IEditionDataServices>();

            var loggerMock = new Mock<ILog>();

            var borrowedBooksService = new BorrowedBooksServicesImplementation(
                borrowedBooksServiceMock.Object,
                conditionServiceMock.Object,
                editionServiceMock.Object,
                loggerMock.Object);

            Assert.IsFalse(borrowedBooksService.UpdateBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidBorrowedBooks))));
        }

        /// <summary>Updates the valid borrowed books.</summary>
        [Test]
        public void UpdateValidBorrowedBooks()
        {
            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.AddBorrowedBooks(this.borrowedBooks)).Returns(true);
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(new List<BorrowedBooks>());
            borrowedBooksServiceMock.Setup(x => x.UpdateBorrowedBooks(this.borrowedBooks)).Returns(true);
            var conditionServiceMock = new Mock<IConditionDataServices>();

            conditionServiceMock.Setup(x => x.GetLIM()).Returns(28);

            var editionServiceMock = new Mock<IEditionDataServices>();

            var loggerMock = new Mock<ILog>();

            var borrowedBooksService = new BorrowedBooksServicesImplementation(
                borrowedBooksServiceMock.Object,
                conditionServiceMock.Object,
                editionServiceMock.Object,
                loggerMock.Object);

            Assert.IsTrue(borrowedBooksService.UpdateBorrowedBooks(this.borrowedBooks));
        }
    }
}
