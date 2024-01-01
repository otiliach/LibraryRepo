// <copyright file="Author.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>The Author model.</summary>
    public class Author
    {
        /// <summary>Initializes a new instance of the <see cref="Author" /> class.</summary>
        /// <param name="fullname">The fullname of the author.</param>
        public Author(string fullname)
        {
            this.FullName = fullname;
        }

        /// <summary>Initializes a new instance of the <see cref="Author" /> class.</summary>
        public Author()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the full name.</summary>
        /// <value>The full name.</value>
        [Required(ErrorMessage = "[FullName] cannot be null.")]
        [StringLength(maximumLength: 50, MinimumLength = 6, ErrorMessage = "[FullName] must be between 6 and 50 characters.")]
        [RegularExpression(@"[A-Z][a-z]+(\s[A-Z]\.)?\s[A-Z][a-z]+", ErrorMessage = "[FullName] must be a valid fullname.")]
        public virtual string FullName { get; set; } = string.Empty;

        /// <summary>Gets or sets the author's books.</summary>
        /// <value>The author's books.</value>
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
