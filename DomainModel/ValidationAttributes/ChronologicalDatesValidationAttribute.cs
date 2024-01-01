// <copyright file="ChronologicalDatesValidationAttribute.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.ValidationAttributes
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Models;

    /// <summary>The Chronological Dates Validation Attribute class.</summary>
    [ExcludeFromCodeCoverage]
    internal class ChronologicalDatesValidationAttribute : ValidationAttribute
    {
        /// <summary>Returns true if the object is valid.</summary>
        /// <param name="value">The value.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            BorrowedBooks borrowedBooks = (BorrowedBooks)context.ObjectInstance;

            DateTime borrowDate = borrowedBooks.BorrowDate;
            DateTime originalReturnDate = borrowedBooks.OriginalReturnDate;
            DateTime actualReturnDate = borrowedBooks.ActualReturnDate;

            bool validDates =
                (borrowDate <= originalReturnDate) &&
                (borrowDate <= actualReturnDate) &&
                (originalReturnDate <= actualReturnDate);

            return validDates ? ValidationResult.Success : new ValidationResult(null);
        }
    }
}
