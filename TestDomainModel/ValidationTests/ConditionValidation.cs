// <copyright file="ConditionValidation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestDomainModel.ValidationTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;

    /// <summary>The Condition validation class.</summary>
    [ExcludeFromCodeCoverage]
    internal class ConditionValidation
    {
        /// <summary>The condition.</summary>
        private Condition condition;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.condition = new Condition(
                "NCZ",
                "Numar maxim de carti imprumutate pe zi ",
                3);

            int conditionId = this.condition.Id;
        }

        /// <summary>Valids the condition.</summary>
        [Test]
        public void ValidCondition()
        {
            var context = new ValidationContext(this.condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(this.condition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(0));
        }

        /// <summary>Invalids the condition who has empty condition.</summary>
        [Test]
        public void InvalidCondition_EmptyCondition()
        {
            this.condition = new Condition();

            var context = new ValidationContext(this.condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.condition, context, results, true));
            Assert.That(results.Count, Is.Not.EqualTo(0));
        }

        /// <summary>Invalids the condition who has name null.</summary>
        [Test]
        public void InvalidCondition_NameNull()
        {
            this.condition.Name = null;

            var context = new ValidationContext(this.condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.condition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Name] cannot be null."));
        }

        /// <summary>Invalids the condition who has name too long.</summary>
        [Test]
        public void InvalidCondition_NameTooLong()
        {
            this.condition.Name = new string('a', 12);

            var context = new ValidationContext(this.condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.condition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Name] must be between 1 and 10 characters."));
        }

        /// <summary>Invalids the condition who has description null.</summary>
        [Test]
        public void InvalidCondition_DescriptionNull()
        {
            this.condition.Description = null;

            var context = new ValidationContext(this.condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.condition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Description] cannot be null."));
        }

        /// <summary>Invalids the condition who has description too long.</summary>
        [Test]
        public void InvalidCondition_DescriptionTooLong()
        {
            this.condition.Description = new string('a', 160);

            var context = new ValidationContext(this.condition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.condition, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Description] must be between 1 and 100 characters."));
        }
    }
}
