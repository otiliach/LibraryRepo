// <copyright file="UserServicesImplementation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace ServiceLayer.Implementation
{
    using System.ComponentModel.DataAnnotations;
    using DataMapper;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using ServiceLayer.Interfaces;

    /// <summary>The User Services Implementation class.</summary>
    /// <seealso cref="ServiceLayer.Interfaces.IUserServices" />
    public class UserServicesImplementation : IUserServices
    {
        /// <summary>The logger.</summary>
        private ILog logger;

        /// <summary>The user data services.</summary>
        private IUserDataServices userDataServices;

        /// <summary>Initializes a new instance of the <see cref="UserServicesImplementation" /> class.</summary>
        /// <param name="userDataServices">The user data services.</param>
        /// <param name="logger">The logger.</param>
        public UserServicesImplementation(IUserDataServices userDataServices, ILog logger)
        {
            this.userDataServices = userDataServices;
            this.logger = logger;
        }

        /// <inheritdoc/>
        public bool AddUser(User user)
        {
            // Check if <user> is null.
            if (user == null)
            {
                this.logger.Warn("Attempted to add a null user.");
                return false;
            }

            // Check if <user> is invalid.
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                this.logger.Warn("Attempted to add an invalid user. " + string.Join(' ', results));
                return false;
            }

            // Check if <user> already exist.
            if (this.userDataServices.UserEmailAlreadyExists(user.Email))
            {
                this.logger.Warn("Attempted to add an already existing user.");
                return false;
            }

            // If all checks pass call AddUser.
            return this.userDataServices.AddUser(user);
        }

        /// <inheritdoc/>
        public ICollection<User> GetUsers()
        {
            return this.userDataServices.GetUsers();
        }

        /// <inheritdoc/>
        public User? GetUserById(int id)
        {
            return this.userDataServices.GetUserById(id);
        }

        /// <inheritdoc/>
        public User? GetUserByEmail(string email)
        {
            return this.userDataServices.GetUserByEmail(email);
        }

        /// <inheritdoc/>
        public bool UpdateUser(User user)
        {
            // Check if <user> is null.
            if (user == null)
            {
                this.logger.Warn("Attempted to update a null user.");
                return false;
            }

            // Check if <user> is invalid.
            var context = new ValidationContext(user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            if (!Validator.TryValidateObject(user, context, results, true))
            {
                this.logger.Warn("Attempted to update an invalid user. " + string.Join(' ', results));
                return false;
            }

            // Check if <user> exists.
            if (this.userDataServices.GetUserById(user.Id) == null)
            {
                this.logger.Warn("Attempted to update a non-existing user.");
                return false;
            }

            // Check if new <user email> already exist.
            if (this.userDataServices.GetUserByEmail(user.Email) != null)
            {
                this.logger.Warn("Attempted to update a user email to an already existing user email.");
                return false;
            }

            // If all checks pass call UpdateUser.
            return this.userDataServices.UpdateUser(user);
        }

        /// <inheritdoc/>
        public bool DeleteUser(User? user)
        {
            // Check if <user> is null.
            if (user == null)
            {
                this.logger.Warn("Attempted to delete a null user.");
                return false;
            }

            // Check if <user> exists.
            if (this.userDataServices.GetUserById(user.Id) == null)
            {
                this.logger.Warn("Attempted to delete a non-existing user.");
                return false;
            }

            // If all checks pass call DeleteUser.
            return this.userDataServices.DeleteUser(user);
        }
    }
}
