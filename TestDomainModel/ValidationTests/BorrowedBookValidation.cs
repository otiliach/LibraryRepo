// <copyright file="BorrowedBookValidation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestDomainModel.ValidationTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Enums;
    using DomainModel.Models;

    /// <summary>The BorrowedBook Validation class.</summary>
    [ExcludeFromCodeCoverage]
    public class BorrowedBookValidation
    {
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

            int borrowedBooksId = this.borrowedBooks.Id;
            int readerId = this.borrowedBooks.ReaderId;
            int librarianId = this.borrowedBooks.LibrarianId;
        }

        /// <summary>Valids the borrowed books.</summary>
        [Test]
        public void ValidBorrowedBooks()
        {
            var context = new ValidationContext(this.borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(this.borrowedBooks, context, results, true));
            Assert.That(results.Count, Is.EqualTo(0));
        }

        /// <summary>Invalids the borrowed books with empty borrowed.</summary>
        [Test]
        public void InvalidBorrowedBooks_EmptyBorrowed()
        {
            this.borrowedBooks = new BorrowedBooks();

            var context = new ValidationContext(this.borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.borrowedBooks, context, results, true));
            Assert.That(results.Count, Is.Not.EqualTo(0));
        }

        /// <summary>Invalids the borrowed books with reader null.</summary>
        [Test]
        public void InvalidBorrowedBooks_ReaderNull()
        {
            this.borrowedBooks.Reader = null;

            var context = new ValidationContext(this.borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.borrowedBooks, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Reader] cannot be null."));
        }

        /// <summary>Invalids the borrowed books who has librarian null.</summary>
        [Test]
        public void InvalidBorrowedBooks_LibrarianNull()
        {
            this.borrowedBooks.Librarian = null;

            var context = new ValidationContext(this.borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.borrowedBooks, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Librarian] cannot be null."));
        }

        /// <summary>Invalids the borrowed books who has non chronological dates.</summary>
        [Test]
        public void InvalidBorrowedBooks_NonChronologicalDates()
        {
            this.borrowedBooks.OriginalReturnDate = DateTime.Today.AddDays(-7);

            var context = new ValidationContext(this.borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.borrowedBooks, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("Dates are not chronological."));
        }

        /// <summary>Invalids the borrowed books who has reader first name too short.</summary>
        [Test]
        public void InvalidBorrowedBooks_ReaderFirstNameTooShort()
        {
            this.borrowedBooks.Reader.FirstName = "O";

            var context = new ValidationContext(this.borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.borrowedBooks, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Reader] must be a valid user."));
        }

        /// <summary>Invalids the borrowed books who has librarian first name too short.</summary>
        [Test]
        public void InvalidBorrowedBooks_LibrarianFirstNameTooShort()
        {
            this.borrowedBooks.Librarian.FirstName = "T";

            var context = new ValidationContext(this.borrowedBooks, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.borrowedBooks, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Librarian] must be a valid user."));
        }
    }
}
