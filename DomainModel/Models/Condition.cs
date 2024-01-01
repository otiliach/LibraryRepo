// <copyright file="Condition.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>The Condition model.</summary>
    public class Condition
    {
        /// <summary>Initializes a new instance of the <see cref="Condition" /> class.</summary>
        /// <param name="name">The name.</param>
        /// <param name="description">The description.</param>
        /// <param name="value">The value.</param>
        public Condition(string name, string description, int value)
        {
            this.Name = name;
            this.Description = description;
            this.Value = value;
        }

        /// <summary>Initializes a new instance of the <see cref="Condition" /> class.</summary>
        public Condition()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the condition's name.</summary>
        /// <value>The name.</value>
        [Required(ErrorMessage = "[Name] cannot be null.")]
        [StringLength(maximumLength: 10, MinimumLength = 1, ErrorMessage = "[Name] must be between 1 and 10 characters.")]
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>Gets or sets the condition's description.</summary>
        /// <value>The description.</value>
        [Required(ErrorMessage = "[Description] cannot be null.")]
        [StringLength(maximumLength: 150, MinimumLength = 1, ErrorMessage = "[Description] must be between 1 and 100 characters.")]
        public virtual string Description { get; set; } = string.Empty;

        /// <summary>Gets or sets the condition's value.</summary>
        /// <value>The value.</value>
        [Required(ErrorMessage = "[Value] cannot be null.")]
        public virtual int Value { get; set; }
    }
}
