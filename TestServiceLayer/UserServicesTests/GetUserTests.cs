// <copyright file="GetUserTests.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestServiceLayer.UserServicesTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using ServiceLayer.Implementation;

    /// <summary>The get user tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class GetUserTests
    {
        /// <summary>Test for Gets all user.</summary>
        [Test]
        public void GetAllUser()
        {
            List<User> users = GetTestUsers();

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUsers()).Returns(users);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            var expected = users;
            var actual = userServices.GetUsers();

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual.ElementAt(i).Id, Is.EqualTo(expected[i].Id));
                Assert.That(actual.ElementAt(i).Email, Is.EqualTo(expected[i].Email));
            }
        }

        /// <summary>Test for Gets the users none found.</summary>
        [Test]
        public void GetUsers_NoneFound()
        {
            List<User> users = new List<User>();

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUsers()).Returns(users);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(userServices.GetUsers());
        }

        /// <summary>Test for Gets the user by identifier.</summary>
        [Test]
        public void GetUserById()
        {
            User user = GetTestUser();
            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(user);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);
            var expected = user;
            var actual = userServices.GetUserById(user.Id);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Email, Is.EqualTo(expected.Email));
        }

        /// <summary>Test for Gets the user by identifier not found.</summary>
        [Test]
        public void GetUserById_NotFound()
        {
            User user = GetTestUser();
            User? nullUser = null;

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(user.Id)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsNull(userServices.GetUserById(user.Id));
        }

        /// <summary>Test for Gets the user by email.</summary>
        [Test]
        public void GetUserByEmail()
        {
            User user = GetTestUser();
            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserByEmail(user.Email)).Returns(user);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);
            var expected = user;
            var actual = userServices.GetUserByEmail(user.Email);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Email, Is.EqualTo(expected.Email));
        }

        /// <summary>Test for Gets the user by email not found.</summary>
        [Test]
        public void GetUserByEmail_NotFound()
        {
            User user = GetTestUser();
            User? nullUser = null;

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserByEmail(user.Email)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsNull(userServices.GetUserByEmail(user.Email));
        }

        /// <summary>Gets the test user.</summary>
        /// <returns>An User for tests.</returns>
        private static User GetTestUser()
        {
            return new User(
                "Otilia",
                "Chelmus",
                "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 15",
                "OtiC@FakeMail.com",
                "0123456789",
                "Pswrd@23",
                EAccountType.Reader);
        }

        /// <summary>Gets the test users.</summary>
        /// <returns>A list of Users for tests.</returns>
        private static List<User> GetTestUsers()
        {
            return new List<User>
            {
                new User(
                "Otilia",
                "Chelmus",
                "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 15",
                "OtiC@FakeMail.com",
                "0123456789",
                "Pswrd@23",
                EAccountType.Reader),
                new User(
                "Otilia",
                "Matei",
                "Str. BlaBla, nr. 7, bl. C3, sc. A, et. 2, ap. 15",
                "OtiM@FakeMail.com",
                "0123456780",
                "Pswrd@23",
                EAccountType.Reader),
                new User(
                "Librarian",
                "Debu",
                "Str. BlaBlaBla, nr. 1, bl. C2, sc. A, et. 1, ap. 1",
                "Debu@FakeMail.com",
                "0123456788",
                "Libra@23",
                EAccountType.Librarian),
                new User(
                "Elena",
                "Libra",
                "Str. BlaBlaBla, nr. 1, bl. C2, sc. A, et. 1, ap. 2",
                "Elena@FakeMail.com",
                "0123456787",
                "Libra@23",
                EAccountType.LibrarianReader),
            };
        }
    }
}
