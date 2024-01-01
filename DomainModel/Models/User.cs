// <copyright file="User.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace DomainModel.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;
    using DomainModel.Enums;

    /// <summary>The User model.</summary>
    public class User
    {
        /// <summary>Initializes a new instance of the <see cref="User" /> class.</summary>
        /// <param name="firstname">The firstname.</param>
        /// <param name="lastname">The lastname.</param>
        /// <param name="address">The address.</param>
        /// <param name="email">The email.</param>
        /// <param name="phoneNumber">The phone number.</param>
        /// <param name="password">The password.</param>
        /// <param name="accountType">Type of the account.</param>
        public User(string firstname, string lastname, string address, string email, string phoneNumber, string password, EAccountType accountType)
        {
            this.FirstName = firstname;
            this.LastName = lastname;
            this.Address = address;
            this.Email = email;
            this.PhoneNumber = phoneNumber;
            this.Password = password;
            this.AccountType = accountType;
        }

        /// <summary>Initializes a new instance of the <see cref="User" /> class.</summary>
        public User()
        {
        }

        /// <summary>Gets the identifier.</summary>
        /// <value>The identifier.</value>
        public virtual int Id { get; private set; }

        /// <summary>Gets or sets the user's first name.</summary>
        /// <value>The first name.</value>
        [Required(ErrorMessage = "[FirstName] cannot be null.")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "[FirstName] must be between 3 and 50 characters.")]
        [RegularExpression(@"[A-Z][a-z]+", ErrorMessage = "[FirstName] must be a valid firstname.")]
        public virtual string FirstName { get; set; } = string.Empty;

        /// <summary>Gets or sets the user's last name.</summary>
        /// <value>The last name.</value>
        [Required(ErrorMessage = "[LastName] cannot be null.")]
        [StringLength(maximumLength: 50, MinimumLength = 3, ErrorMessage = "[LastName] must be between 3 and 50 characters.")]
        [RegularExpression(@"[A-Z][a-z]+", ErrorMessage = "[LastName] must be a valid lastname.")]
        public virtual string LastName { get; set; } = string.Empty;

        /// <summary>Gets or sets the user's address.</summary>
        /// <value>The address.</value>
        [Required(ErrorMessage = "[Address] cannot be null.")]
        [StringLength(maximumLength: 100, MinimumLength = 2, ErrorMessage = "[Address] must be between 2 and 100 characters.")]
        public virtual string Address { get; set; } = string.Empty;

        /// <summary>Gets or sets the user's email.</summary>
        /// <value>The email.</value>
        [Required(ErrorMessage = "[Email] cannot be null.")]
        [EmailAddress(ErrorMessage = "[Email] is not a valid email address.")]
        [StringLength(maximumLength: 100, MinimumLength = 8, ErrorMessage = "[Email] must be between 8 and 100 characters.")]
        public virtual string Email { get; set; } = string.Empty;

        /// <summary>Gets or sets the user's phone number.</summary>
        /// <value>The phone number.</value>
        [Phone(ErrorMessage = "[PhoneNumber] is not a valid phone number.")]
        [StringLength(maximumLength: 10, MinimumLength = 3, ErrorMessage = "[PhoneNumber] must be between 3 and 10 characters.")]
        public virtual string? PhoneNumber { get; set; }

        /// <summary>Gets or sets the password.</summary>
        /// <value>The password.</value>
        [Required(ErrorMessage = "[Password] cannot be null.")]
        [StringLength(maximumLength: 30, MinimumLength = 8, ErrorMessage = "[Password] must be between 8 and 30 characters.")]
        [CustomValidation(typeof(User), "IsValidPassword", ErrorMessage = "[Password] is not a valid password. Must contein at least one numeber, one uppercase letter, one lowercase letter and one symbol.")]
        public virtual string Password { get; set; } = string.Empty;

        /// <summary>Gets or sets the type of the user's account.</summary>
        /// <value>The type of the account.</value>
        [Required(ErrorMessage = "[AccountTupe] cannot be null.")]
        public virtual EAccountType AccountType { get; set; }

        /// <summary>Gets or sets the borrowed books.</summary>
        /// <value>The borrowed books.</value>
        public virtual ICollection<BorrowedBooks> BorrowedBooks { get; set; } = new List<BorrowedBooks>();

        /// <summary>Determines whether [is valid password] [the specified password].</summary>
        /// <param name="password">The password.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public static ValidationResult? IsValidPassword(string password, ValidationContext validationContext)
        {
            Regex hasNumber = new Regex(@"[0-9]+");
            Regex hasUpperCaseLetter = new Regex(@"[A-Z]+");
            Regex hasLowerCaseLetter = new Regex(@"[a-z]+");
            Regex hasSpecialCharacter = new Regex(@"[!@#$%^&*?_]+");

            bool isValidPassword =
                hasNumber.IsMatch(password) &&
                hasUpperCaseLetter.IsMatch(password) &&
                hasLowerCaseLetter.IsMatch(password) &&
                hasSpecialCharacter.IsMatch(password);

            return (isValidPassword == true) ? ValidationResult.Success : new ValidationResult(null);
        }
    }
}
