// <copyright file="IEditionServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>The Edition service interface.</summary>
    public interface IEditionServices
    {
        /// <summary>Adds the edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool AddEdition(Edition edition);

        /// <summary>Gets the editions.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<Edition> GetEditions();

        /// <summary>Gets the edition by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Edition? GetEditionById(int id);

        /// <summary>Updates the edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool UpdateEdition(Edition edition);

        /// <summary>Deletes the edition.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool DeleteEdition(Edition edition);
    }
}
