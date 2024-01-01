// <copyright file="Domain.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>The Condition model.</summary>
    public class Domain
    {
        /// <summary>Initializes a new instance of the <see cref="Domain" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="parentDomain">The parent domain.</param>
        public Domain(string name, Domain? parentDomain)
        {
            this.Name = name;
            this.ParentDomain = parentDomain;
        }

        /// <summary>Initializes a new instance of the <see cref="Domain" /> class.</summary>
        public Domain()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the domain's name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "[Name] cannot be null.")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "[Name] must be between 3 and 50 characters.")]
        [RegularExpression(@"[A-Za-zăâîșțĂÂÎȘȚ\s]*", ErrorMessage = "[Name] must be a valid name.")]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>Gets or sets the parent domain.</summary>
        /// <value>The parent domain.</value>
        public virtual Domain? ParentDomain { get; set; }

        /// <summary>Gets or sets the book.</summary>
        /// <value>The book.</value>
        public virtual ICollection<Book> Book { get; set; } = new List<Book>();
    }
}
