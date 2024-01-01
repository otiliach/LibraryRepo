// <copyright file="BookValidation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestDomainModel.ValidationTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Enums;
    using DomainModel.Models;

    /// <summary>The book validation class.</summary>
    [ExcludeFromCodeCoverage]
    public class BookValidation
    {
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

        /// <summary>Valids the book.</summary>
        [Test]
        public void ValidBook()
        {
            var context = new ValidationContext(this.book, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(this.book, context, results, true));
            Assert.That(results.Count, Is.EqualTo(0));
        }

        /// <summary>Invalids the book with empty book.</summary>
        [Test]
        public void InvalidBook_EmptyBook()
        {
            this.book = new Book();

            var context = new ValidationContext(this.book, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.book, context, results, true));
            Assert.That(results.Count, Is.Not.EqualTo(0));
        }

        /// <summary>Invalids the book with title null.</summary>
        [Test]
        public void InvalidBook_TitleNull()
        {
            this.book.Title = null;

            var context = new ValidationContext(this.book, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.book, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Title] cannot be null."));
        }

        /// <summary>Invalids the book with title too short.</summary>
        [Test]
        public void InvalidBook_TitleTooShort()
        {
            this.book.Title = "DS&A";

            var context = new ValidationContext(this.book, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.book, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Title] must be between 6 and 50 characters."));
        }

        /// <summary>Invalids the book with domains null.</summary>
        [Test]
        public void InvalidBook_DomainsNull()
        {
            this.book.Domains = new List<Domain>()
            {
            };

            var context = new ValidationContext(this.book, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.book, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Book] needs at least one [domain]."));
        }
    }
}
