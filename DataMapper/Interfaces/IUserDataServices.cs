// <copyright file="IUserDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.Interfaces
{
    using DomainModel.Models;

    /// <summary>The <see cref="User"/> data services.</summary>
    public interface IUserDataServices
    {
        /// <summary>Adds a new user to the database.</summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///     <b>true</b> if the user was added successfully to the database.
        ///     <br/>
        ///     <b>false</b> if an error occured while adding the user to the database.
        /// </returns>
        bool AddUser(User user);

        /// <summary>Gets all the users from the database.</summary>
        /// <returns>A list of all existing users.</returns>
        ICollection<User> GetUsers();

        /// <summary>
        /// Gets the user with the provided id from the database.
        /// </summary>
        /// <param name="id">The user's id.</param>
        /// <returns> The user with the provided id.</returns>
        User? GetUserById(int id);

        /// <summary>
        /// Gets the user with the provided name.
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <returns>The user with the provided name.</returns>
        User? GetUserByEmail(string name);

        /// <summary>
        /// Updates the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///     <b>true</b> if the user was updated successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while updating the user.
        /// </returns>
        bool UpdateUser(User user);

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///     <b>true</b> if the user was deleted successfully.
        ///     <br/>
        ///     <b>false</b> if an error occured while deleting the user.
        /// </returns>
        bool DeleteUser(User user);

        /// <summary>
        /// Checks if an user with the same title already exists in the database.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>
        ///     <b>true</b> if a user with the provided email exists in the database.
        ///     <br/>
        ///     <b>false</b> if a user with the provided email doesn't exist in the database.
        /// </returns>
        bool UserEmailAlreadyExists(string email);
    }
}
