// <copyright file="DomainServicesImplementation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Implementation
{
    using System.ComponentModel.DataAnnotations;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Interfaces;

    /// <summary>The Domain Services Implementation class.</summary>
    /// <seealso cref="ServiceLayer.Interfaces.IDomainServices" />
    public class DomainServicesImplementation : IDomainServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The domain data services.</summary>
        private IDomainDataServices domainDataServices;

        /// <summary>Initializes a new instance of the <see cref="DomainServicesImplementation" /> class.</summary>
        /// <param name="domainDataServices">The domain data services.</param>
        /// <param name="logger">The logger.</param>
        public DomainServicesImplementation(IDomainDataServices domainDataServices, ILog logger)
        {
            this.domainDataServices = domainDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddDomain(Domain domain)
        {
            // Check if <domain> is null.
            if (domain == null)
            {
                this.logger.Warn("Attempted to add a null domain.");
                return false;
            }

            // Check if <domain> is invalid.
            var context = new ValidationContext(domain, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(domain, context, results, true))
            {
                this.logger.Warn("Attempted to add an invalid domain. " + string.Join(' ', results));
                return false;
            }

            // Check if <domain> already exist.
            if (this.domainDataServices.GetDomainByName(domain.Name) != null)
            {
                this.logger.Warn("Attempted to add an already existing domain.");
                return false;
            }

            // If all checks pass call AddDomain.
            return this.domainDataServices.AddDomain(domain);
        }

        /// <inheritdoc/>
        public ICollection<Domain> GetDomains()
        {
            return this.domainDataServices.GetDomains();
        }

        /// <inheritdoc/>
        public Domain? GetDomainById(int id)
        {
            return this.domainDataServices.GetDomainById(id);
        }

        /// <inheritdoc/>
        public Domain? GetDomainByName(string name)
        {
            return this.domainDataServices.GetDomainByName(name);
        }

        /// <inheritdoc/>
        public ICollection<Domain> GetChildDomains(Domain domain)
        {
            return this.domainDataServices.GetChildDomains(domain);
        }

        /// <inheritdoc/>
        public bool UpdateDomain(Domain domain)
        {
            // Check if <domain> is null.
            if (domain == null)
            {
                this.logger.Warn("Attempted to update a null domain.");
                return false;
            }

            // Check if <domain> is invalid.
            var context = new ValidationContext(domain, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(domain, context, results, true))
            {
                this.logger.Warn("Attempted to update an invalid domain. " + string.Join(' ', results));
                return false;
            }

            // Check if <domain> exists.
            if (this.domainDataServices.GetDomainById(domain.Id) == null)
            {
                this.logger.Warn("Attempted to update a non-existing domain.");
                return false;
            }

            // Check if new <condition name> already exist.
            if (this.domainDataServices.GetDomainByName(domain.Name) != null)
            {
                this.logger.Warn("Attempted to update a domain name to an already existing domain name.");
                return false;
            }

            // If all checks pass call UpdateDomain.
            return this.domainDataServices.UpdateDomain(domain);
        }

        /// <inheritdoc/>
        public bool DeleteDomain(Domain domain)
        {
            // Check if <domain> is null.
            if (domain == null)
            {
                this.logger.Warn("Attempted to delete a null domain.");
                return false;
            }

            // Check if <domain> exists.
            if (this.domainDataServices.GetDomainById(domain.Id) == null)
            {
                this.logger.Warn("Attempted to delete a non-existing domain.");
                return false;
            }

            return this.domainDataServices.DeleteDomain(domain);
        }
    }
}
