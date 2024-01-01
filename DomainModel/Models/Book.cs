// <copyright file="Book.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;
    using DomainModel.ValidationAttributes;

    /// <summary>The Book model.</summary>
    public class Book
    {
        /// <summary>Initializes a new instance of the <see cref="Book" /> class.</summary>
        /// <param name="title">The title.</param>
        /// <param name="authors">The authors.</param>
        /// <param name="domains">The domains.</param>
        /// <param name="editions">The editions.</param>
        public Book(string title, List<Author> authors, List<Domain> domains, List<Edition> editions)
        {
            this.Title = title;
            this.Domains = domains;
            this.Authors = authors;
            this.Editions = editions;

            foreach (Edition edition in this.Editions)
            {
                edition.Book = this;
            }
        }

        /// <summary>Initializes a new instance of the <see cref="Book" /> class.</summary>
        public Book()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the book's title.</summary>
        /// <value>The title.</value>
        [Required(ErrorMessage = "[Title] cannot be null.")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "[Title] must be between 6 and 50 characters.")]
        public virtual string Title { get; set; } = string.Empty;

        /// <summary>Gets or sets the book's authors.</summary>
        /// <value>The authors.</value>
        [ObjectCollectionValidation(ErrorMessage = "[Book] needs at least one [author].")]
        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

        /// <summary>Gets or sets the book's editions.</summary>
        /// <value>The editions.</value>
        [ObjectCollectionValidation(ErrorMessage = "[Book] needs at least one [edition] and all editions have to be valid.")]
        public virtual ICollection<Edition> Editions { get; set; } = new List<Edition>();

        /// <summary>Gets or sets the book's domains.</summary>
        /// <value>The domains.</value>
        [ObjectCollectionValidation(ErrorMessage = "[Book] needs at least one [domain].")]
        public virtual ICollection<Domain> Domains { get; set; } = new List<Domain>();
    }
}