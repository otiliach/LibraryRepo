// <copyright file="EditionServicesImplementation.cs" company="Transilvania University of Brasov">
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

    /// <summary>The Edition Services Implementation class.</summary>
    /// <seealso cref="ServiceLayer.Interfaces.IEditionServices" />
    public class EditionServicesImplementation : IEditionServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The edition data services.</summary>
        private IEditionDataServices editionDataServices;

        /// <summary>Initializes a new instance of the <see cref="EditionServicesImplementation" /> class.</summary>
        /// <param name="editionDataServices">The edition data services.</param>
        /// <param name="logger">The logger.</param>
        public EditionServicesImplementation(IEditionDataServices editionDataServices, ILog logger)
        {
            this.editionDataServices = editionDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddEdition(Edition edition)
        {
            // Check if <edition> is null.
            if (edition == null)
            {
                this.logger.Warn("Attempted to add a null edition.");
                return false;
            }

            // Check if <edition> is invalid.
            var context = new ValidationContext(edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(edition, context, results, true))
            {
                this.logger.Warn("Attempted to add an invalid edition. " + string.Join(' ', results));
                return false;
            }

            // TODO: Check if <edition> already exist.

            // If all checks pass call AddEdition.
            return this.editionDataServices.AddEdition(edition);
        }

        /// <inheritdoc/>
        public ICollection<Edition> GetEditions()
        {
            return this.editionDataServices.GetEditions();
        }

        /// <inheritdoc/>
        public Edition? GetEditionById(int id)
        {
            return this.editionDataServices.GetEditionById(id);
        }

        /// <inheritdoc/>
        public bool UpdateEdition(Edition edition)
        {
            // Check if <edition> is null.
            if (edition == null)
            {
                this.logger.Warn("Attempted to update a null edition.");
                return false;
            }

            // Check if <edition> is invalid.
            var context = new ValidationContext(edition, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(edition, context, results, true))
            {
                this.logger.Warn("Attempted to update an invalid edition. " + string.Join(' ', results));
                return false;
            }

            // Check if <edition> exists.
            if (this.editionDataServices.GetEditionById(edition.Id) == null)
            {
                this.logger.Warn("Attempted to update a non-existing edition.");
                return false;
            }

            // If all checks pass call UpdateEdition.
            return this.editionDataServices.UpdateEdition(edition);
        }

        /// <inheritdoc/>
        public bool DeleteEdition(Edition edition)
        {
            // Check if <edition> is null.
            if (edition == null)
            {
                this.logger.Warn("Attempted to delete a null edition.");
                return false;
            }

            // Check if <edition> exists.
            if (this.editionDataServices.GetEditionById(edition.Id) == null)
            {
                this.logger.Warn("Attempted to delete a non-existing edition.");
                return false;
            }

            // If all checks pass call DeleteEdition.
            return this.editionDataServices.DeleteEdition(edition);
        }
    }
}
