// <copyright file="AuthorServicesImplementation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Implementation
{
    using System.ComponentModel.DataAnnotations;
    using DataMapper;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Interfaces;

    /// <summary>The Author services implementation class.
    /// <seealso cref="ServiceLayer.Interfaces.IAuthorServices" />
    public class AuthorServicesImplementation : IAuthorServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The author data services.</summary>
        private IAuthorDataServices authorDataServices;

        /// <summary>Initializes a new instance of the <see cref="AuthorServicesImplementation" /> class.</summary>
        /// <param name="authorDataServices">The author data services.</param>
        /// <param name="logger">The logger.</param>
        public AuthorServicesImplementation(IAuthorDataServices authorDataServices, ILog logger)
        {
            this.authorDataServices = authorDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddAuthor(Author? author)
        {
            // Check if <author> is null.
            if (author == null)
            {
                this.logger.Warn("Attempted to add a null author.");
                return false;
            }

            // Check if <author> is invalid.
            var context = new ValidationContext(author, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(author, context, results, true))
            {
                this.logger.Warn("Attempted to add an invalid author. " + string.Join(' ', results));
                return false;
            }

            // Check if <author> already exist.
            if (this.authorDataServices.AuthorNameAlreadyExists(author.FullName))
            {
                this.logger.Warn("Attempted to add an already existing author.");
                return false;
            }

            // If all checks pass call AddAuthor.
            return this.authorDataServices.AddAuthor(author);
        }

        /// <inheritdoc/>
        public ICollection<Author> GetAuthors()
        {
            return this.authorDataServices.GetAuthors();
        }

        /// <inheritdoc/>
        public Author? GetAuthorById(int id)
        {
            return this.authorDataServices.GetAuthorById(id);
        }

        /// <inheritdoc/>
        public Author? GetAuthorByName(string name)
        {
            return this.authorDataServices.GetAuthorByName(name);
        }

        /// <inheritdoc/>
        public bool UpdateAuthor(Author author)
        {
            // Check if <author> is null.
            if (author == null)
            {
                this.logger.Warn("Attempted to update a null author.");
                return false;
            }

            // Check if <author> is invalid.
            var context = new ValidationContext(author, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(author, context, results, true))
            {
                this.logger.Warn("Attempted to update an invalid author. " + string.Join(' ', results));
                return false;
            }

            // Check if <author> exists.
            if (this.authorDataServices.GetAuthorById(author.Id) == null)
            {
                this.logger.Warn("Attempted to update a non-existing author.");
                return false;
            }

            // Check if new <author name> already exist.
            if (this.authorDataServices.GetAuthorByName(author.FullName) != null)
            {
                this.logger.Warn("Attempted to update a author name to an already existing author name.");
                return false;
            }

            // If all checks pass call UpdateAuthor.
            return this.authorDataServices.UpdateAuthor(author);
        }

        /// <inheritdoc/>
        public bool DeleteAuthor(Author author)
        {
            // Check if <author> is null.
            if (author == null)
            {
                this.logger.Warn("Attempted to delete a null author.");
                return false;
            }

            // Check if <author> exists.
            if (this.authorDataServices.GetAuthorById(author.Id) == null)
            {
                this.logger.Warn("Attempted to delete a non-existing author.");
                return false;
            }

            // If all checks pass call DeleteAuthor.
            return this.authorDataServices.DeleteAuthor(author);
        }
    }
}
