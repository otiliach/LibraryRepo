// <copyright file="IBookServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>The Book services interface.</summary>
    public interface IBookServices
    {
        /// <summary>Adds the book.</summary>
        /// <param name="book">The book.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool AddBook(Book book);

        /// <summary>Books the already exists.</summary>
        /// <param name="title">The title.</param>
        /// <param name="authorList">The author list.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool BookAlreadyExists(string title, List<Author> authorList);

        /// <summary>Gets the books.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<Book> GetBooks();

        /// <summary>Gets the book by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Book? GetBookById(int id);

        /// <summary>Gets the book by title.</summary>
        /// <param name="title">The title.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Book? GetBookByTitle(string title);

        /// <summary>Gets the books by domain.</summary>
        /// <param name="domain">The domain.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<Book> GetBooksByDomain(Domain domain);

        /// <summary>Updates the book.</summary>
        /// <param name="book">The book.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool UpdateBook(Book book);

        /// <summary>Deletes the book.</summary>
        /// <param name="book">The book.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool DeleteBook(Book book);
    }
}
