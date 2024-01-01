// <copyright file="ObjectCollectionValidationAttribute.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.ValidationAttributes
{
    using System.Collections;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>The Object Collection ValidationAttribut class.</summary>
    [ExcludeFromCodeCoverage]
    internal class ObjectCollectionValidationAttribute : ValidationAttribute
    {
        /// <summary>Returns true if ... is valid.</summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="T:System.ComponentModel.DataAnnotations.ValidationResult">ValidationResult</see> class.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var list = value as IEnumerable;

            // Check if the list is null.
            if (list == null)
            {
                return new ValidationResult(null);
            }

            // Check if the list is empty
            if (list.Cast<object>().Count() == 0)
            {
                return new ValidationResult(null);
            }

            // Check for invalid list items.
            foreach (var item in list)
            {
                var context = new ValidationContext(item, serviceProvider: null, items: null);
                var result = new List<ValidationResult>();

                if (!Validator.TryValidateObject(item, context, result, true))
                {
                    return new ValidationResult(null);
                }
            }

            return ValidationResult.Success;
        }
    }
}
