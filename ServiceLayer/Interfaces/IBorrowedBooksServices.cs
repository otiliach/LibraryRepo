// <copyright file="IBorrowedBooksServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>The BorrowedBook services interface.</summary>
    public interface IBorrowedBooksServices
    {
        /// <summary>Adds the borrowed books.</summary>
        /// <param name="borrowedBooks">The borrowed books.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool AddBorrowedBooks(BorrowedBooks borrowedBooks);

        /// <summary>Gets the borrowed books.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<BorrowedBooks> GetBorrowedBooks();

        /// <summary>Gets the borrowed books by user identifier.</summary>
        /// <param name="userId">The user identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        List<BorrowedBooks> GetBorrowedBooksByUserId(int userId);

        /// <summary>Updates the borrowed books.</summary>
        /// <param name="borrowedBooks">The borrowed books.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool UpdateBorrowedBooks(BorrowedBooks borrowedBooks);
    }
}
