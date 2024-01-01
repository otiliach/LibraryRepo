// <copyright file="UserValidation.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestDomainModel.ValidationTests
{
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using DomainModel.Enums;
    using DomainModel.Models;

    /// <summary>The User validation class.</summary>
    [ExcludeFromCodeCoverage]
    internal class UserValidation
    {
        /// <summary>The user.</summary>
        private User user;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 5, 2),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            DateTime borrowDate = DateTime.Today;

            DateTime originalReturnDate = DateTime.Today.AddDays(14);

            List<Book> books = new List<Book>
            {
                new Book("Data Structures & Algorithms in C++", authors, domains, editions),
            };

            User reader = new User(
                "Otilia",
                "Chelmus",
                "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 15",
                "OtiC@FakeMail.com",
                "0123456789",
                "Pswrd@23",
                EAccountType.Reader);

            User librarian = new User(
                "Lia",
                "Ito",
                "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 16",
                "LiaC@FakeMail.com",
                "0123456788",
                "Pswrd@32",
                EAccountType.Librarian);

            BorrowedBooks borrowedBooks = new BorrowedBooks(borrowDate, reader, editions, originalReturnDate, librarian, EBorrowStatus.Extended);

            this.user = new User(
                "Otilia",
                "Chelmus",
                "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 15",
                "OtiC@FakeMail.com",
                "0123456789",
                "Pswrd@23",
                EAccountType.Reader);

            this.user.BorrowedBooks = new List<BorrowedBooks> { borrowedBooks };

            int userId = this.user.Id;
        }

        /// <summary>Valids the user.</summary>
        [Test]
        public void ValidUser()
        {
            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(0));
        }

        /// <summary>Invalids the user who has empty user.</summary>
        [Test]
        public void InvalidUser_EmptyUser()
        {
            this.user = new User();

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.Not.EqualTo(0));
        }

        /// <summary>Invalids the user who has first name null.</summary>
        [Test]
        public void InvalidUser_FirstNameNull()
        {
            this.user.FirstName = null;

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FirstName] cannot be null."));
        }

        /// <summary>Invalids the user who has first name empty.</summary>
        [Test]
        public void InvalidUser_FirstNameEmpty()
        {
            this.user.FirstName = string.Empty;

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FirstName] cannot be null."));
        }

        /// <summary>Invalids the user who has first name too short.</summary>
        [Test]
        public void InvalidUser_FirstNameTooShort()
        {
            this.user.FirstName = "Ol";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FirstName] must be between 3 and 50 characters."));
        }

        /// <summary>Invalids the user who has first name too long.</summary>
        [Test]
        public void InvalidUser_FirstNameTooLong()
        {
            this.user.FirstName = "X" + new string('x', 50);

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FirstName] must be between 3 and 50 characters."));
        }

        /// <summary>Invalids the user who has first name no capital letter.</summary>
        [Test]
        public void InvalidUser_FirstNameNoCapitalLetter()
        {
            this.user.FirstName = new string('x', 10);

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FirstName] must be a valid firstname."));
        }

        /// <summary>Invalids the user who has first name no lowercase letters.</summary>
        [Test]
        public void InvalidUser_FirstNameNoLowercaseLetters()
        {
            this.user.FirstName = new string('X', 10);

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FirstName] must be a valid firstname."));
        }

        /// <summary>Invalids the user who has first name with numbers.</summary>
        [Test]
        public void InvalidUser_FirstNameWithNumbers()
        {
            this.user.FirstName = "Ot1lia";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FirstName] must be a valid firstname."));
        }

        /// <summary>Invalids the user who has first name with symbols.</summary>
        [Test]
        public void InvalidUser_FirstNameWithSymbols()
        {
            this.user.FirstName = "Ot!lia";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[FirstName] must be a valid firstname."));
        }

        /// <summary>Invalids the user who has last name null.</summary>
        [Test]
        public void InvalidUser_LastNameNull()
        {
            this.user.LastName = null;

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[LastName] cannot be null."));
        }

        /// <summary>Invalids the user who has last name empty.</summary>
        [Test]
        public void InvalidUser_LastNameEmpty()
        {
            this.user.LastName = string.Empty;

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[LastName] cannot be null."));
        }

        /// <summary>Invalids the user who has last name too short.</summary>
        [Test]
        public void InvalidUser_LastNameTooShort()
        {
            this.user.LastName = "Ch";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[LastName] must be between 3 and 50 characters."));
        }

        /// <summary>Invalids the user who has last name too long.</summary>
        [Test]
        public void InvalidUser_LastNameTooLong()
        {
            this.user.LastName = "X" + new string('x', 50);

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[LastName] must be between 3 and 50 characters."));
        }

        /// <summary>Invalids the user who has last name no capital letter.</summary>
        [Test]
        public void InvalidUser_LastNameNoCapitalLetter()
        {
            this.user.LastName = new string('x', 10);

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[LastName] must be a valid lastname."));
        }

        /// <summary>Invalids the user who has last name no lowercase letters.</summary>
        [Test]
        public void InvalidUser_LastNameNoLowercaseLetters()
        {
            this.user.LastName = new string('X', 10);

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[LastName] must be a valid lastname."));
        }

        /// <summary>Invalids the user who has last name with numbers.</summary>
        [Test]
        public void InvalidUser_LastNameWithNumbers()
        {
            this.user.LastName = "Ch3lmus";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[LastName] must be a valid lastname."));
        }

        /// <summary>Invalids the user who has last name with symbols.</summary>
        [Test]
        public void InvalidUser_LastNameWithSymbols()
        {
            this.user.LastName = "Chelmu$";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[LastName] must be a valid lastname."));
        }

        /// <summary>Invalids the user who has null address.</summary>
        [Test]
        public void InvalidUser_NullAddress()
        {
            this.user.Address = null;

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Address] cannot be null."));
        }

        /// <summary>Invalids the user who has address too short.</summary>
        [Test]
        public void InvalidUser_AddressTooShort()
        {
            this.user.Address = "S";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Address] must be between 2 and 100 characters."));
        }

        /// <summary>Invalids the user who has address too long.</summary>
        [Test]
        public void InvalidUser_AddressTooLong()
        {
            this.user.Address = new string('X', 101);

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Address] must be between 2 and 100 characters."));
        }

        /// <summary>Invalids the user who has null email.</summary>
        [Test]
        public void InvalidUser_NullEmail()
        {
            this.user.Email = null;

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Email] cannot be null."));
        }

        /// <summary>Invalids the user who has invalid email.</summary>
        [Test]
        public void InvalidUser_InvalidEmail()
        {
            this.user.Email = "oti.yahoo.com";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Email] is not a valid email address."));
        }

        /// <summary>Invalids the user who has email too short.</summary>
        [Test]
        public void InvalidUser_EmailTooShort()
        {
            this.user.Email = "o@gm.ro";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Email] must be between 8 and 100 characters."));
        }

        /// <summary>Invalids the user who has invalid phone number.</summary>
        [Test]
        public void InvalidUser_InvalidPhoneNumber()
        {
            this.user.PhoneNumber = "0@d4567891";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[PhoneNumber] is not a valid phone number."));
        }

        /// <summary>Invalids the user who has phone number too long.</summary>
        [Test]
        public void InvalidUser_PhoneNumberTooLong()
        {
            this.user.PhoneNumber = "02345678912";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[PhoneNumber] must be between 3 and 10 characters."));
        }

        /// <summary>Valids the user who has null phone number.</summary>
        [Test]
        public void ValidUser_NullPhoneNumber()
        {
            this.user.PhoneNumber = null;

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsTrue(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(0));
        }

        /// <summary>Invalids the user who has null password.</summary>
        [Test]
        public void InvalidUser_NullPassword()
        {
            this.user.Password = null;

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Password] cannot be null."));
        }

        /// <summary>Invalids the user who has password too short.</summary>
        [Test]
        public void InvalidUser_PasswordTooShort()
        {
            this.user.Password = "Pswd@2";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Password] must be between 8 and 30 characters."));
        }

        /// <summary>Invalids the user who has password without number.</summary>
        [Test]
        public void InvalidUser_Password_WithoutNumber()
        {
            this.user.Password = "Pasword@";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Password] is not a valid password. Must contein at least one numeber, one uppercase letter, one lowercase letter and one symbol."));
        }

        /// <summary>Invalids the user who has password without upper case letter.</summary>
        [Test]
        public void InvalidUser_Password_WithoutUpperCaseLetter()
        {
            this.user.Password = "pasword@12";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Password] is not a valid password. Must contein at least one numeber, one uppercase letter, one lowercase letter and one symbol."));
        }

        /// <summary>Invalids the user who has password without lower case letter.</summary>
        [Test]
        public void InvalidUser_Password_WithoutLowerCaseLetter()
        {
            this.user.Password = "PASSWORD@12";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Password] is not a valid password. Must contein at least one numeber, one uppercase letter, one lowercase letter and one symbol."));
        }

        /// <summary>Invalids the user who has password without symbol.</summary>
        [Test]
        public void InvalidUser_Password_WithoutSymbol()
        {
            this.user.Password = "Pasword1233";

            var context = new ValidationContext(this.user, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Assert.IsFalse(Validator.TryValidateObject(this.user, context, results, true));
            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].ErrorMessage, Is.EqualTo("[Password] is not a valid password. Must contein at least one numeber, one uppercase letter, one lowercase letter and one symbol."));
        }
    }
}
