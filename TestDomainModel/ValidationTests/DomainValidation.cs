// <copyright file="DomainValidation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestDomainModel.ValidationTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;

    /// <summary>The Domain validation class.</summary>
    [ExcludeFromCodeCoverage]
    public class DomainValidation
    {
        /// <summary>The domain.</summary>
        private Domain domain;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.domain = new Domain(
                "Algorithm",
                null);

            int domainId = this.domain.Id;
        }

        /// <summary>Valids the domain.</summary>
        [Test]
        public void ValidDomain()
        {
            var context = new ValidationContext(this.domain, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(this.domain, context, results, true));
            Assert.That(results.Count, Is.EqualTo(0));
        }

        /// <summary>Invalids the domain who has empty domain.</summary>
        [Test]
        public void InvalidDomain_EmptyDomain()
        {
            this.domain = new Domain();

            var context = new ValidationContext(this.domain, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.domain, context, results, true));
            Assert.That(results.Count, Is.Not.EqualTo(0));
        }

        /// <summary>Invalids the domain who has name null.</summary>
        [Test]
        public void InvalidDomain_NameNull()
        {
            this.domain.Name = null;

            var context = new ValidationContext(this.domain, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.domain, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Name] cannot be null."));
        }

        /// <summary>Invalids the domain who has name too short.</summary>
        [Test]
        public void InvalidDomain_NameTooShort()
        {
            this.domain.Name = "Ai";

            var context = new ValidationContext(this.domain, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.domain, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Name] must be between 3 and 50 characters."));
        }

        /// <summary>Invalids the domain who has the name invalid.</summary>
        [Test]
        public void InvalidDomain_InvalidName()
        {
            this.domain.Name = "AI & DS";

            var context = new ValidationContext(this.domain, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.domain, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Name] must be a valid name."));
        }
    }
}
