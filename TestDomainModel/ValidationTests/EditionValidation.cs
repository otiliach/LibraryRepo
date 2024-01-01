// <copyright file="EditionValidation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestDomainModel.ValidationTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Enums;
    using DomainModel.Models;

    /// <summary>The Edition validation class.</summary>
    [ExcludeFromCodeCoverage]
    internal class EditionValidation
    {
        /// <summary>The edition.</summary>
        private Edition edition;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.edition = new Edition(
                "Litera",
                2011,
                714,
                2,
                EBookType.Paperback,
                5,
                2);

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

            Book book = new Book(
                "Data Structures & Algorithms in C++",
                authors,
                domains,
                editions);

            this.edition.Book = book;

            int editionId = this.edition.Id;
            int bookId = this.edition.BookId ?? 0;
        }

        /// <summary>Valids the edition.</summary>
        [Test]
        public void ValidEdition()
        {
            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(0));
        }

        /// <summary>Invalids the edition who has the empty edition.</summary>
        [Test]
        public void InvalidEdition_EmptyEdition()
        {
            this.edition = new Edition();

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.Not.EqualTo(0));
        }

        /// <summary>Invalids the edition who has publishing house null.</summary>
        [Test]
        public void InvalidEdition_PublishingHouseNull()
        {
            this.edition.PublishingHouse = null;

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[PublishingHouse] cannot be null."));
        }

        /// <summary>Invalids the edition who has year too early.</summary>
        [Test]
        public void InvalidEdition_YearTooEarly()
        {
            this.edition.Year = 999;

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Year] must be between 1000 and 2024."));
        }

        /// <summary>Invalids the edition who has year too far in the future.</summary>
        [Test]
        public void InvalidEdition_YearTooFarInTheFuture()
        {
            this.edition.Year = 2025;

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Year] must be between 1000 and 2024."));
        }

        /// <summary>Invalids the edition who has number of pages too small.</summary>
        [Test]
        public void InvalidEdition_NumberOfPagesTooSmall()
        {
            this.edition.NumberOfPages = 0;

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[NumberOfPages] must be between 1 and 5000."));
        }

        /// <summary>Invalids the edition who has edition number too small.</summary>
        [Test]
        public void InvalidEdition_EditionNumberTooSmall()
        {
            this.edition.EditionNumber = 0;

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[EditionNumber] must be between 1 and 30."));
        }

        /// <summary>Invalids the type of the edition unknown book.</summary>
        [Test]
        public void InvalidEdition_UnknownBookType()
        {
            this.edition.BookType = EBookType.Unknown;

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[BookType] must be specified."));
        }

        /// <summary>Invalids the edition who has negative number of copies for borrowing.</summary>
        [Test]
        public void InvalidEdition_NegativeNumberOfCopiesForBorrowing()
        {
            this.edition.NumberOfCopiesForBorrowing = -1;

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[NumberOfCopiesForBorrowing] must be between 0 and 100."));
        }

        /// <summary>Invalids the edition who has negative number of copies for reading.</summary>
        [Test]
        public void InvalidEdition_NegativeNumberOfCopiesForReading()
        {
            this.edition.NumberOfCopiesForReading = -1;

            var context = new ValidationContext(this.edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.edition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[NumberOfCopiesForReading] must be between 0 and 100."));
        }
    }
}
