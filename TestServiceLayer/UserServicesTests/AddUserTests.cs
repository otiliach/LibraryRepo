// <copyright file="AddUserTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The add user tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class AddUserTests
    {
        /// <summary>The message for log add null user.</summary>
        private const string LogAddNullUser = "Attempted to add a null user.";

        /// <summary>The message for log add invalid user.</summary>
        private const string LogAddInvalidUser = "Attempted to add an invalid user.";

        /// <summary>The message for log add existing user.</summary>
        private const string LogAddExistingUser = "Attempted to add an already existing user.";

        /// <summary>The user.</summary>
        private User user;

        /// <summary>Setups this user instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.user = new User(
                "Otilia",
                "Chelmus",
                "Str. BlaBlaBla, nr. 3, bl. C23, sc. A, et. 2, ap. 15",
                "OtiC@FakeMail.com",
                "0123456789",
                "Pswrd@23",
                EAccountType.Reader);

            int userId = this.user.Id;
        }

        /// <summary>Test for Adds the null user.</summary>
        [Test]
        public void AddNullUser()
        {
            this.user = null;

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.AddUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullUser)));
        }

        /// <summary>Test for Adds the empty user.</summary>
        [Test]
        public void AddEmptyUser()
        {
            this.user = new User();

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.AddUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>Test for Adds the user null email.</summary>
        [Test]
        public void AddUserNullEmail()
        {
            this.user.Email = null;

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.AddUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidUser))));
        }

        /// <summary>Test for Adds the user email already exist.</summary>
        [Test]
        public void AddUserEmailAlreadyExist()
        {
            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.UserEmailAlreadyExists(this.user.Email)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.AddUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddExistingUser))));
        }

        /// <summary>Test for Adds the valid user.</summary>
        [Test]
        public void AddValidUser()
        {
            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.UserEmailAlreadyExists(this.user.Email)).Returns(false);
            userServiceMock.Setup(x => x.AddUser(this.user)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(userServices.AddUser(this.user));
        }
    }
}
