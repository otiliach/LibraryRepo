// <copyright file="IUserServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Interfaces
{
    using DomainModel.Models;

    /// <summary>The User services interface.</summary>
    public interface IUserServices
    {
        /// <summary>Adds the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool AddUser(User user);

        /// <summary>Gets the all users.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        ICollection<User> GetUsers();

        /// <summary>Gets the user by identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <returns>A user. </returns>
        User? GetUserById(int id);

        /// <summary>Gets the user by email.</summary>
        /// <param name="email">The email.</param>
        /// <returns>A user.</returns>
        User? GetUserByEmail(string email);

        /// <summary>Updates the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool UpdateUser(User user);

        /// <summary>Deletes the user.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        bool DeleteUser(User? user);
    }
}
