// <copyright file="Edition.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Enums;

    /// <summary>The Edition model.</summary>
    public class Edition
    {
        /// <summary>Initializes a new instance of the <see cref="Edition" /> class.</summary>
        /// <param name="publishingHouse">The publishing house.</param>
        /// <param name="year">The year.</param>
        /// <param name="numberOfPages">The number of pages.</param>
        /// <param name="editionNumber">The edition number.</param>
        /// <param name="bookType">Type of the book.</param>
        /// <param name="numberOfCopiesForBorrowing">The number of copies for borrowing.</param>
        /// <param name="numberOfCopiesForReading">The number of copies for reading.</param>
        public Edition(string publishingHouse, int year, int numberOfPages, int editionNumber, EBookType bookType, int numberOfCopiesForBorrowing, int numberOfCopiesForReading)
        {
            this.PublishingHouse = publishingHouse;
            this.Year = year;
            this.NumberOfPages = numberOfPages;
            this.EditionNumber = editionNumber;
            this.BookType = bookType;
            this.NumberOfCopiesForBorrowing = numberOfCopiesForBorrowing;
            this.NumberOfCopiesForReading = numberOfCopiesForReading;
            this.InitialStock = this.NumberOfCopiesForBorrowing + this.NumberOfCopiesForReading;
        }

        /// <summary>Initializes a new instance of the <see cref="Edition" /> class.</summary>
        public Edition()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the book identifier.</summary>
        /// <value>The book identifier.</value>
        public virtual int? BookId { get; set; }

        /// <summary>Gets or sets the book.</summary>
        /// <value>The book.</value>
        [Required(ErrorMessage = "[Book] cannot be null.")]
        public virtual Book Book { get; set; }

        /// <summary>Gets or sets the publishing house.</summary>
        /// <value>The publishing house.</value>
        [Required(ErrorMessage = "[PublishingHouse] cannot be null.")]
        public virtual string PublishingHouse { get; set; } = string.Empty;

        /// <summary>Gets or sets the year.</summary>
        /// <value>The year.</value>
        [Required(ErrorMessage = "[Year] cannot be null.")]
        [Range(1000, 2024, ErrorMessage = "[Year] must be between 1000 and 2024.")]
        public virtual int Year { get; set; }

        /// <summary>Gets or sets the number of pages.</summary>
        /// <value>The number of pages.</value>
        [Required(ErrorMessage = "[NumberOfPages] cannot be null.")]
        [Range(1, 5000, ErrorMessage = "[NumberOfPages] must be between 1 and 5000.")]
        public virtual int NumberOfPages { get; set; }

        /// <summary>Gets or sets the edition number.</summary>
        /// <value>The edition number.</value>
        [Required(ErrorMessage = "[EditionNumber] cannot be null.")]
        [Range(1, 30, ErrorMessage = "[EditionNumber] must be between 1 and 30.")]
        public virtual int EditionNumber { get; set; }

        /// <summary>Gets or sets the type of the book.</summary>
        /// <value>The type of the book.</value>
        [Required(ErrorMessage = "[BookType] cannot be null.")]
        [CustomValidation(typeof(Edition), "IsValidBookType", ErrorMessage= "[BookType] must be specified.")]
        public virtual EBookType BookType { get; set; }

        /// <summary>Gets or sets the number of copies for borrowing.</summary>
        /// <value>The number of copies for borrowing.</value>
        [Required(ErrorMessage = "[NumberOfCopiesForBorrowing] cannot be null.")]
        [Range(0, 100, ErrorMessage = "[NumberOfCopiesForBorrowing] must be between 0 and 100.")]
        public virtual int NumberOfCopiesForBorrowing { get; set; }

        /// <summary>Gets or sets the number of copies for reading.</summary>
        /// <value>The number of copies for reading.</value>
        [Required(ErrorMessage = "[NumberOfCopiesForReading] cannot be null.")]
        [Range(0, 100, ErrorMessage = "[NumberOfCopiesForReading] must be between 0 and 100.")]
        public virtual int NumberOfCopiesForReading { get; set; }

        /// <summary>Gets the initial stock.</summary>
        /// <value>The initial stock.</value>
        [Required(ErrorMessage = "[InitialStock] cannot be null.")]
        [Range(0, 200, ErrorMessage = "[InitialStock] must be between 0 and 100.")]
        public virtual int InitialStock { get; private set; }

        /// <summary>Gets or sets the borrowed books.</summary>
        /// <value>The borrowed books.</value>
        public virtual ICollection<BorrowedBooks> BorrowedBooks { get; set; } = new List<BorrowedBooks>();

        /// <summary>Determines whether [is valid book type] [the specified book type].</summary>
        /// <param name="bookType">Type of the book.</param>
        /// <param name="context">The context.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static ValidationResult? IsValidBookType(EBookType bookType, ValidationContext context)
        {
            return (bookType != EBookType.Unknown) ? ValidationResult.Success : new ValidationResult(null);
        }
    }
}
