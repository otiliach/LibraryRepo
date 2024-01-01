// <copyright file="IEditionDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.Interfaces
{
    using DomainModel.Models;

    /// <summary>The <see cref="Edition"/> data services.</summary>
    public interface IEditionDataServices
    {
        /// <summary>Adds a new edition to the database.</summary>
        /// <param name="edition">The edition.</param>
        /// <returns>
        ///     <b>true</b> if the edition was added successfully to the database.
        ///     <br/>
        ///     <b>false</b> if an error occured while adding the edition to the database.
        /// </returns>
        bool AddEdition(Edition edition);

        /// <summary>Gets all the editions from the database.</summary>
        /// <returns>A list of all existing editions.</returns>
        ICollection<Edition> GetEditions();

        /// <summary>
        /// Gets the edition with the provided id from the database.
        /// </summary>
        /// <param name="id">The edition's id.</param>
        /// <returns> The edition with the provided id.</returns>
        Edition? GetEditionById(int id);

        /// <summary>
        /// Updates the edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <returns>
        ///     <b>true</b> if the edition was updated successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while updating the edition.
        /// </returns>
        bool UpdateEdition(Edition edition);

        /// <summary>
        /// Deletes the edition.
        /// </summary>
        /// <param name="edition">The edition.</param>
        /// <returns>
        ///     <b>true</b> if the edition was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while deleting the edition.
        /// </returns>
        bool DeleteEdition(Edition edition);
    }
}
