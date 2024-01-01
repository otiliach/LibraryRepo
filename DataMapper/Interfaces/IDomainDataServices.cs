// <copyright file="IDomainDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.Interfaces
{
    using DomainModel.Models;

    /// <summary>The <see cref="Domain"/> data services.</summary>
    public interface IDomainDataServices
    {
        /// <summary>Adds a new domain to the database.</summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///     <b>true</b> if the domain was added successfully to the database.
        ///     <br/>
        ///     <b>false</b> if an error occured while adding the domain to the database.
        /// </returns>
        bool AddDomain(Domain domain);

        /// <summary>Gets all the domains from the database.</summary>
        /// <returns>A list of all existing domains.</returns>
        ICollection<Domain> GetDomains();

        /// <summary>
        /// Gets the domain with the provided id from the database.
        /// </summary>
        /// <param name="id">The domain's id.</param>
        /// <returns> The domain with the provided id.</returns>
        Domain? GetDomainById(int id);

        /// <summary>
        /// Gets the domain with the provided name.
        /// </summary>
        /// <param name="name">The name of the domain.</param>
        /// <returns>The domain with the provided name.</returns>
        Domain? GetDomainByName(string name);

        /// <summary>
        /// Gets the child domains by domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>The domains with the provided domain.</returns>
        ICollection<Domain> GetChildDomains(Domain domain);

        /// <summary>
        /// Updates the domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///     <b>true</b> if the domain was updated successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while updating the domain.
        /// </returns>
        bool UpdateDomain(Domain domain);

        /// <summary>
        /// Deletes the domain.
        /// </summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///     <b>true</b> if the domain was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while deleting the domain.
        /// </returns>
        bool DeleteDomain(Domain domain);
    }
}
