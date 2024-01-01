// <copyright file="IBorrowedBooksDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.Interfaces
{
    using DomainModel.Models;

    /// <summary>The <see cref="BorrowedBooks"/> data services.</summary>
    public interface IBorrowedBooksDataServices
    {
        /// <summary>Adds a new borrowedBooks to the database.</summary>
        /// <param name="borrowedBooks">The borrowedBooks.</param>
        /// <returns>
        ///     <b>true</b> if the borrowedBooks was added successfully to the database.
        ///     <br/>
        ///     <b>false</b> if an error occured while adding the borrowedBooks to the database.
        /// </returns>
        bool AddBorrowedBooks(BorrowedBooks borrowedBooks);

        /// <summary>Gets all the borrowedBooks from the database.</summary>
        /// <returns>A list of all existing borrowedBooks.</returns>
        ICollection<BorrowedBooks> GetBorrowedBooks();

        /// <summary>
        /// Gets the borrowedBooks with the provided user id from the database.
        /// </summary>
        /// <param name="userId">The id of the user.</param>
        /// <returns> The borrowedBooks with the provided user id.</returns>
        List<BorrowedBooks> GetBorrowedBooksByUserId(int userId);

        /// <summary>
        /// Updates the borrowedBooks.
        /// </summary>
        /// <param name="borrowedBooks">The borrowedBooks.</param>
        /// <returns>
        ///     <b>true</b> if the borrowedBooks was updated successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while updating the borrowedBooks.
        /// </returns>
        bool UpdateBorrowedBooks(BorrowedBooks borrowedBooks);
    }
}
