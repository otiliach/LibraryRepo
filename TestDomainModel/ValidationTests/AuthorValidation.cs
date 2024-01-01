// <copyright file="AuthorValidation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestDomainModel.ValidationTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;

    /// <summary>The Author validation class.</summary>
    [ExcludeFromCodeCoverage]
    public class AuthorValidation
    {
        private Author author;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.author = new Author(
                "Michael T. Goodrich");

            int authorId = this.author.Id;
        }

        /// <summary>Valids the author.</summary>
        [Test]
        public void ValidAuthor()
        {
            var context = new ValidationContext(this.author, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(this.author, context, results, true));
            Assert.That(results.Count, Is.EqualTo(0));
        }

        /// <summary>Invalids the author with empty author.</summary>
        [Test]
        public void InvalidAuthor_EmptyAuthor()
        {
            this.author = new Author();

            var context = new ValidationContext(this.author, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.author, context, results, true));
            Assert.That(results.Count, Is.Not.EqualTo(0));
        }

        /// <summary>Invalids the author with first name null.</summary>
        [Test]
        public void InvalidAuthor_FirstNameNull()
        {
            this.author.FullName = null;

            var context = new ValidationContext(this.author, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.author, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FullName] cannot be null."));
        }

        /// <summary>Invalids the author with full name too short.</summary>
        [Test]
        public void InvalidAuthor_FullNameTooShort()
        {
            this.author.FullName = "Mi Go";

            var context = new ValidationContext(this.author, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.author, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FullName] must be between 6 and 50 characters."));
        }

        /// <summary>Invalids the full name of the author invalid.</summary>
        [Test]
        public void InvalidAuthor_InvalidFullName()
        {
            this.author.FullName = "Michael T! Goodrich";

            var context = new ValidationContext(this.author, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.author, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FullName] must be a valid fullname."));
        }
    }
}