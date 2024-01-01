// <copyright file="BorrowedBooks.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;
    using DomainModel.Enums;
    using DomainModel.ValidationAttributes;

    /// <summary>The BorrowedBooks model.</summary>
    public class BorrowedBooks
    {
        /// <summary>Initializes a new instance of the <see cref="BorrowedBooks" /> class.</summary>
        /// <param name="barrowDate">The barrow date.</param>
        /// <param name="reader">The reader.</param>
        /// <param name="editions">The editions.</param>
        /// <param name="originalReturnDate">The original return date.</param>
        /// <param name="librarian">The librarian.</param>
        /// <param name="borrowStatus">The borrow status.</param>
        public BorrowedBooks(DateTime barrowDate, User reader, List<Edition> editions, DateTime originalReturnDate, User librarian, EBorrowStatus borrowStatus)
        {
            this.BorrowDate = barrowDate;
            this.Reader = reader;
            this.Editions = editions;
            this.OriginalReturnDate = originalReturnDate;
            this.ActualReturnDate = originalReturnDate;
            this.Librarian = librarian;
            this.BorrowStatus = borrowStatus;
        }

        /// <summary>Initializes a new instance of the <see cref="BorrowedBooks" /> class.</summary>
        public BorrowedBooks()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the borrow date.</summary>
        /// <value>The borrow date.</value>
        [Required(ErrorMessage = "[BarrowDate] cannot be null.")]
        [ChronologicalDatesValidation(ErrorMessage = "Dates are not chronological.")]
        public virtual DateTime BorrowDate { get; set; }

        /// <summary>Gets or sets the reader id.</summary>
        /// <value>The reader id.</value>
        public virtual int ReaderId { get; set; }

        /// <summary>Gets or sets the reader.</summary>
        /// <value>The reader.</value>
        [Required(ErrorMessage = "[Reader] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[Reader] must be a valid user.")]
        public virtual User Reader { get; set; }

        /// <summary>Gets or sets the editions.</summary>
        /// <value>The editions.</value>
        [ObjectCollectionValidation(ErrorMessage = "[BorrowedBooks] must have at least one edition and all editions have to be valid.")]
        public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();

        /// <summary>Gets or sets the original return date.</summary>
        /// <value>The original return date.</value>
        [Required(ErrorMessage = "[OriginalReturnDate] cannot be null.")]
        public virtual DateTime OriginalReturnDate { get; set; }

        /// <summary>Gets or sets the actual return date.</summary>
        /// <value>The actual return date.</value>
        [Required(ErrorMessage = "[ActualReturnDate] cannot be null.")]
        public virtual DateTime ActualReturnDate { get; set; }

        /// <summary>Gets or sets the librarian id.</summary>
        /// <value>The librarian id.</value>
        public virtual int LibrarianId { get; set; }

        /// <summary>Gets or sets the librarian.</summary>
        /// <value>The librarian.</value>
        [Required(ErrorMessage = "[Librarian] cannot be null.")]
        [ObjectValidation(ErrorMessage = "[Librarian] must be a valid user.")]
        public virtual User Librarian { get; set; }

        /// <summary>Gets or sets the borrow status.</summary>
        /// <value>The borrow status.</value>
        public virtual EBorrowStatus BorrowStatus { get; set; }
    }
}
