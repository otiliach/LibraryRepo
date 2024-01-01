// <copyright file="IAuthorDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.Interfaces
{
    using DomainModel.Models;

    /// <summary>The <see cref="Author"/> data services.</summary>
    public interface IAuthorDataServices
    {
        /// <summary>Adds a new author to the database.</summary>
        /// <param name="author">The author.</param>
        /// <returns>
        ///     <b>true</b> if the author was added successfully to the database.
        ///     <br/>
        ///     <b>false</b> if an error occured while adding the author to the database.
        /// </returns>
        bool AddAuthor(Author author);

        /// <summary>Gets all the authors from the database.</summary>
        /// <returns>A list of all existing authors.</returns>
        ICollection<Author> GetAuthors();

        /// <summary>
        /// Gets the author with the provided id from the database.
        /// </summary>
        /// <param name="id">The author's id.</param>
        /// <returns> The author with the provided id.</returns>
        Author? GetAuthorById(int id);

        /// <summary>
        /// Gets the author with the provided name.
        /// </summary>
        /// <param name="name">The name of the author.</param>
        /// <returns>The author with the provided name.</returns>
        Author? GetAuthorByName(string name);

        /// <summary>
        /// Updates the author.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <returns>
        ///     <b>true</b> if the author was updated successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while updating the author.
        /// </returns>
        bool UpdateAuthor(Author author);

        /// <summary>
        /// Deletes the author.
        /// </summary>
        /// <param name="author">The author.</param>
        /// <returns>
        ///     <b>true</b> if the author was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while deleting the author.
        /// </returns>
        bool DeleteAuthor(Author author);

        /// <summary>
        /// Checks if an author with the same name already exists in the database.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///     <b>true</b> if an author with the provided name exists in the database.
        ///     <br/>
        ///     <b>false</b> if an author with the provided name doesn't exist in the database.
        /// </returns>
        bool AuthorNameAlreadyExists(string name);
    }
}
