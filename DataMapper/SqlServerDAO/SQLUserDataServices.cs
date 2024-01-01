// <copyright file="SQLUserDataServices.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DataMapper.SqlServerDAO
{
    using System.Data.Entity;
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;

    /// <summary>The user data services.</summary>
    [ExcludeFromCodeCoverage]
    public class SQLUserDataServices : IUserDataServices
    {
        private static readonly ILog Logger = LogManager.GetLogger(System.Environment.MachineName);

        /// <inheritdoc/>
        public bool AddUser(User user)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Users.Add(user);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while adding a new user: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("User added successfully!");
            return true;
        }

        /// <inheritdoc/>
        public ICollection<User> GetUsers()
        {
            ICollection<User> users = new List<User>();

            using (LibraryContext libraryContext = new LibraryContext())
            {
                users = libraryContext.Users.OrderBy((user) => user.Id).ToList();
            }

            return users;
        }

        /// <inheritdoc/>
        public User? GetUserById(int id)
        {
            User? user = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                user = libraryContext.Users.FirstOrDefault((user) => user.Id == id);
            }

            return user;
        }

        /// <inheritdoc/>
        public User? GetUserByEmail(string email)
        {
            User? user = null;

            using (LibraryContext libraryContext = new LibraryContext())
            {
                user = libraryContext.Users.FirstOrDefault((user) => user.Email == email);
            }

            return user;
        }

        /// <inheritdoc/>
        public bool UpdateUser(User user)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Users.Attach(user);
                    libraryContext.Entry(user).State = EntityState.Modified;
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while updating a user: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("User updated successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool DeleteUser(User user)
        {
            using (LibraryContext libraryContext = new LibraryContext())
            {
                try
                {
                    libraryContext.Users.Attach(user);
                    libraryContext.Users.Remove(user);
                    libraryContext.SaveChanges();
                }
                catch (Exception exception)
                {
                    Logger.Error("Error while deleting a new user: " + exception.Message.ToString());
                    return false;
                }
            }

            Logger.Info("User deleted successfully!");
            return true;
        }

        /// <inheritdoc/>
        public bool UserEmailAlreadyExists(string email)
        {
            if (this.GetUserByEmail(email) != null)
            {
                return true;
            }

            return false;
        }
    }
}
