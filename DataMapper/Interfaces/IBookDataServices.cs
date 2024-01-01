// <copyright file="IBookDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.Interfaces
{
    using DomainModel.Models;

    /// <summary>The <see cref="Book"/> data services.</summary>
    public interface IBookDataServices
    {
        /// <summary>Adds a new book to the database.</summary>
        /// <param name="book">The book.</param>
        /// <returns>
        ///     <b>true</b> if the book was added successfully to the database.
        ///     <br/>
        ///     <b>false</b> if an error occured while adding the book to the database.
        /// </returns>
        bool AddBook(Book book);

        /// <summary>Gets all the books from the database.</summary>
        /// <returns>A list of all existing books.</returns>
        ICollection<Book> GetBooks();

        /// <summary>
        /// Gets the book with the provided id from the database.
        /// </summary>
        /// <param name="id">The book's id.</param>
        /// <returns> The book with the provided id.</returns>
        Book? GetBookById(int id);

        /// <summary>
        /// Gets the book with the provided title.
        /// </summary>
        /// <param name="title">The title of the book.</param>
        /// <returns>The book with the provided title.</returns>
        Book? GetBookByTitle(string title);

        /// <summary>
        /// Updates the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        ///     <b>true</b> if the book was updated successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while updating the book.
        /// </returns>
        bool UpdateBook(Book book);

        /// <summary>
        /// Deletes the book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <returns>
        ///     <b>true</b> if the book was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while deleting the book.
        /// </returns>
        bool DeleteBook(Book book);

        /// <summary>
        /// Checks if an book with the same title already exists in the database.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="authorList">The authors.</param>
        /// <returns>
        ///     <b>true</b> if a book with the provided title exists in the database.
        ///     <br/>
        ///     <b>false</b> if a book with the provided title doesn't exist in the database.
        /// </returns>
        bool BookAlreadyExists(string title, List<Author> authorList);
    }
}
