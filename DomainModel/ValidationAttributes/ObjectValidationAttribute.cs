// <copyright file="ObjectValidationAttribute.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>The Object Validation Attribute class.</summary>
    [ExcludeFromCodeCoverage]
    internal class ObjectValidationAttribute : ValidationAttribute
    {
        /// <inheritdoc/>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult(null);
            }

            var context = new ValidationContext(value, serviceProvider: null, items: null);
            var result = new List<ValidationResult>();

            if (Validator.TryValidateObject(value, context, result, true))
            {
                return ValidationResult.Success;
            }

            return new ValidationResult(null);
        }
    }
}
