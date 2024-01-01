// <copyright file="IAuthorServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>The Author service interface.</summary>
    public interface IAuthorServices
    {
        /// <summary>Adds the author.</summary>
        /// <param name="author">The author.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool AddAuthor(Author? author);

        /// <summary>Gets the authors.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<Author> GetAuthors();

        /// <summary>Gets the author by id.</summary>
        /// <param name="id">The id.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Author? GetAuthorById(int id);

        /// <summary>Gets the name of the author by.</summary>
        /// <param name="name">The name.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Author? GetAuthorByName(string name);

        /// <summary>Updates the author.</summary>
        /// <param name="author">The author.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool UpdateAuthor(Author author);

        /// <summary>Deletes the author.</summary>
        /// <param name="author">The author.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool DeleteAuthor(Author author);
    }
}
