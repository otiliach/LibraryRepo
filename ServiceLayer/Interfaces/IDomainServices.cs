// <copyright file="IDomainServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>The Domain services interface.</summary>
    public interface IDomainServices
    {
        /// <summary>Adds the domain.</summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool AddDomain(Domain domain);

        /// <summary>Gets the domains.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<Domain> GetDomains();

        /// <summary>Gets the domain by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Domain? GetDomainById(int id);

        /// <summary>Gets the domain by name of the domain.</summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Domain? GetDomainByName(string name);

        /// <summary>Gets the child domains.</summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<Domain> GetChildDomains(Domain domain);

        /// <summary>Updates the domain.</summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool UpdateDomain(Domain domain);

        /// <summary>Deletes the domain.</summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool DeleteDomain(Domain domain);
    }
}
