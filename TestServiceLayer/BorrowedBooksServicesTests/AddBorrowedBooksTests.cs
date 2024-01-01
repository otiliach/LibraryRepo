// <copyright file="AddBorrowedBooksTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The add borrowedBooks tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class AddBorrowedBooksTests
    {
        /// <summary>The log add null borrowed books.</summary>
        private const string LogAddNullBorrowedBooks = "Attempted to add a null borrowedBooks.";

        /// <summary>The log add invalid borrowed books.</summary>
        private const string LogAddInvalidBorrowedBooks = "Attempted to add an invalid borrowedBooks.";

        /// <summary>The log out of stock books for borrowed.</summary>
        private const string LogOutOfStockBooksForBorrowed = "Attempted to borrow out of stock books.";

        /// <summary>The log not enough books for borrowed.</summary>
        private const string LogNotEnoughBooksForBorrowed = "Attempted to borrow with not enough remaining books.";

        /// <summary>The log borrowed more than NMC books.</summary>
        private const string LogBorrowedMoreThanNMCBooks = "Attempted to borrow more than NMC books in PER.";

        /// <summary>The log borrowed more than c books.</summary>
        private const string LogBorrowedMoreThanCBooks = "Attempted to borrow more than C books at once.";

        /// <summary>The log borrowed no distinct domains books.</summary>
        private const string LogBorrowedNoDistinctDomainsBooks = "Attempted to borrow books with not enough distinct domains.";

        /// <summary>The log borrowed more than d books from same domain.</summary>
        private const string LogBorrowedMoreThanDBooksFromSameDomain = "Attempted to borrow more than D books with the same domain in L months.";

        /// <summary>The log borrowed same books in delta time.</summary>
        private const string LogBorrowedSameBooksInDeltaTime = "Attempted to borrow same book in DELTA time.";

        /// <summary>The log borrowed more than NCZ books in a day.</summary>
        private const string LogBorrowedMoreThanNCZBooksInADay = "Attempted to borrow more than NCZ books in a day.";

        /// <summary>The log gave more than persimp books in a day.</summary>
        private const string LogGaveMoreThanPERSIMPBooksInADay = "Attempted to lend more than PERSIMP books in a day.";

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

            editions[0].Book = books[0];

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

        /// <summary>Adds the null borrowed books.</summary>
        [Test]
        public void AddNullBorrowedBooks()
        {
            this.borrowedBooks = null;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullBorrowedBooks)));
        }

        /// <summary>Adds the empty borrowed books.</summary>
        [Test]
        public void AddEmptyBorrowedBooks()
        {
            this.borrowedBooks = new BorrowedBooks();

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidBorrowedBooks))));
        }

        /// <summary>Adds the borrowed books out of stock.</summary>
        [Test]
        public void AddBorrowedBooksOutOfStock()
        {
            this.borrowedBooks.Editions.ElementAt(0).NumberOfCopiesForBorrowing = 0;
            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogOutOfStockBooksForBorrowed))));
        }

        /// <summary>Adds the borrowed not enough books.</summary>
        [Test]
        public void AddBorrowedNotEnoughBooks()
        {
            this.borrowedBooks.Editions.ElementAt(0).NumberOfCopiesForBorrowing = 1;
            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogNotEnoughBooksForBorrowed))));
        }

        /// <summary>Adds the borrowed books more than maximum.</summary>
        [Test]
        public void AddBorrowedBooksMoreThanMax()
        {
            BorrowedBooks borrowedBooks = this.borrowedBooks;
            borrowedBooks.BorrowDate = DateTime.Today.AddDays(-5);

            List<BorrowedBooks> userHistory = new List<BorrowedBooks>()
            {
                borrowedBooks,
                borrowedBooks,
                borrowedBooks,
                borrowedBooks,
                borrowedBooks,
                borrowedBooks,
                borrowedBooks,
                borrowedBooks,
                borrowedBooks,
            };

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(userHistory);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogBorrowedMoreThanNMCBooks))));
        }

        /// <summary>Adds the borrowed more than C books.</summary>
        [Test]
        public void AddBorrowedMoreThanCBooks()
        {
            BorrowedBooks borrowedBooks = this.borrowedBooks;
            borrowedBooks.BorrowDate = DateTime.Today.AddDays(-5);

            List<BorrowedBooks> userHistory = new List<BorrowedBooks>()
            {
                borrowedBooks,
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Edition> bookEditions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            Book book = new Book("Data Structures & Algorithms in C++", authors, domains, editions);

            foreach (Edition edition in editions)
            {
                edition.Book = book;
            }

            this.borrowedBooks.Editions = editions;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(userHistory);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogBorrowedMoreThanCBooks))));
        }

        /// <summary>Adds the borrowed not distinct domains books.</summary>
        [Test]
        public void AddBorrowedNotDistinctDomainsBooks()
        {
            BorrowedBooks borrowedBooks = this.borrowedBooks;
            borrowedBooks.BorrowDate = DateTime.Today.AddDays(-5);

            List<BorrowedBooks> userHistory = new List<BorrowedBooks>()
            {
                borrowedBooks,
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Edition> bookEditions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
            };

            Book book = new Book("Data Structures & Algorithms in C++", authors, domains, editions);

            foreach (Edition edition in editions)
            {
                edition.Book = book;
            }

            this.borrowedBooks.Editions = editions;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(userHistory);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogBorrowedNoDistinctDomainsBooks))));
        }

        /// <summary>Adds the borrowed too many books from one domain.</summary>
        [Test]
        public void AddBorrowedTooManyBooksFromOneDomain()
        {
            BorrowedBooks borrowedBooks = this.borrowedBooks;
            borrowedBooks.BorrowDate = DateTime.Today.AddDays(-45);

            List<BorrowedBooks> userHistory = new List<BorrowedBooks>()
            {
                borrowedBooks,
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Edition> bookEditions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            Book book = new Book("Data Structures & Algorithms in C++", authors, domains, editions);

            foreach (Edition edition in editions)
            {
                edition.Book = book;
            }

            this.borrowedBooks.Editions = editions;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(userHistory);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetD()).Returns(5);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            conditionServiceMock.Setup(x => x.GetL()).Returns(2);
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogBorrowedMoreThanDBooksFromSameDomain))));
        }

        /// <summary>Adds the book borrowed too many times in DELTA time.</summary>
        [Test]
        public void AddBookBorrowedTooManyTimesInDeltaTime()
        {
            BorrowedBooks borrowedBooks = this.borrowedBooks;
            borrowedBooks.BorrowDate = DateTime.Today.AddDays(-45);

            List<BorrowedBooks> userHistory = new List<BorrowedBooks>()
            {
                borrowedBooks,
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Edition> bookEditions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            Book book = new Book("Data Structures & Algorithms in C++", authors, domains, editions);

            foreach (Edition edition in editions)
            {
                edition.Book = book;
            }

            this.borrowedBooks.Editions = editions;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(userHistory);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetD()).Returns(9);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            conditionServiceMock.Setup(x => x.GetL()).Returns(2);
            conditionServiceMock.Setup(x => x.GetDELTA()).Returns(60);
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogBorrowedSameBooksInDeltaTime))));
        }

        /// <summary>Adds the borrowed books more than NCZ books in a day.</summary>
        [Test]
        public void AddBorrowedBooksMoreThanNCZBooksInADay()
        {
            BorrowedBooks borrowedBooks = new BorrowedBooks(
                this.borrowedBooks.BorrowDate,
                this.borrowedBooks.Reader,
                this.borrowedBooks.Editions.ToList(),
                this.borrowedBooks.OriginalReturnDate,
                this.borrowedBooks.Librarian,
                this.borrowedBooks.BorrowStatus);
            borrowedBooks.BorrowDate = DateTime.Today;

            List<BorrowedBooks> userHistory = new List<BorrowedBooks>()
            {
                borrowedBooks,
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Edition> bookEditions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            var bookMock = new Mock<Book>("Data Structures & Algorithms in C#", authors, domains, editions);
            bookMock.SetupGet(x => x.Id).Returns(17);
            bookMock.SetupGet(x => x.Title).Returns("Data Structures & Algorithms in C#");
            bookMock.SetupGet(x => x.Authors).Returns(authors);
            bookMock.SetupGet(x => x.Domains).Returns(domains);
            bookMock.SetupGet(x => x.Editions).Returns(editions);

            foreach (Edition edition in editions)
            {
                edition.Book = bookMock.Object;
            }

            this.borrowedBooks.Editions = editions;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(userHistory);
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetD()).Returns(9);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            conditionServiceMock.Setup(x => x.GetL()).Returns(2);
            conditionServiceMock.Setup(x => x.GetDELTA()).Returns(60);
            conditionServiceMock.Setup(x => x.GetNCZ()).Returns(1);
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogBorrowedMoreThanNCZBooksInADay))));
        }

        /// <summary>Adds the borrowed books more than persimp books in a day.</summary>
        [Test]
        public void AddBorrowedBooksMoreThanPERSIMPBooksInADay()
        {
            BorrowedBooks borrowedBooks = this.borrowedBooks;
            borrowedBooks.BorrowDate = DateTime.Today;

            List<BorrowedBooks> userHistory = new List<BorrowedBooks>()
            {
                borrowedBooks,
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Edition> bookEditions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 25, 25),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            Book book = new Book("Data Structures & Algorithms in C++", authors, domains, editions);

            foreach (Edition edition in editions)
            {
                edition.Book = book;
            }

            this.borrowedBooks.Editions = editions;

            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(new List<BorrowedBooks>());
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetD()).Returns(9);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            conditionServiceMock.Setup(x => x.GetPERSIMP()).Returns(0);
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            conditionServiceMock.Setup(x => x.GetL()).Returns(2);
            conditionServiceMock.Setup(x => x.GetDELTA()).Returns(60);
            conditionServiceMock.Setup(x => x.GetNCZ()).Returns(2);
            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogGaveMoreThanPERSIMPBooksInADay))));
        }

        /// <summary>Adds the valid borrowed books.</summary>
        [Test]
        public void AddValidBorrowedBooks()
        {
            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.AddBorrowedBooks(this.borrowedBooks)).Returns(true);
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(new List<BorrowedBooks>());
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            conditionServiceMock.Setup(x => x.GetD()).Returns(9);
            conditionServiceMock.Setup(x => x.GetL()).Returns(2);
            conditionServiceMock.Setup(x => x.GetLIM()).Returns(28);
            conditionServiceMock.Setup(x => x.GetDELTA()).Returns(60);
            conditionServiceMock.Setup(x => x.GetNCZ()).Returns(5);
            conditionServiceMock.Setup(x => x.GetPERSIMP()).Returns(100);

            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
        }

        /// <summary>Adds the valid borrowed books ordered by librarian reader.</summary>
        [Test]
        public void AddValidBorrowedBooksLibrarianReader()
        {
            this.borrowedBooks.Reader.AccountType = EAccountType.LibrarianReader;
            var borrowedBooksServiceMock = new Mock<IBorrowedBooksDataServices>();
            borrowedBooksServiceMock.Setup(x => x.AddBorrowedBooks(this.borrowedBooks)).Returns(true);
            borrowedBooksServiceMock.Setup(x => x.GetBorrowedBooksByUserId(It.IsAny<int>())).Returns(new List<BorrowedBooks>());
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(9);
            conditionServiceMock.Setup(x => x.GetPER()).Returns(30);
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            conditionServiceMock.Setup(x => x.GetD()).Returns(9);
            conditionServiceMock.Setup(x => x.GetL()).Returns(2);
            conditionServiceMock.Setup(x => x.GetLIM()).Returns(28);
            conditionServiceMock.Setup(x => x.GetDELTA()).Returns(60);
            conditionServiceMock.Setup(x => x.GetNCZ()).Returns(5);
            conditionServiceMock.Setup(x => x.GetPERSIMP()).Returns(100);

            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var borrowedBooksServices = new BorrowedBooksServicesImplementation(borrowedBooksServiceMock.Object, conditionServiceMock.Object, editionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(borrowedBooksServices.AddBorrowedBooks(this.borrowedBooks));
        }
    }
}
