// <copyright file="UpdateUserTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The update user tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class UpdateUserTests
    {
        /// <summary>The message for log update null user.</summary>
        private const string LogUpdateNullUser = "Attempted to update a null user.";

        /// <summary>The message for log update invalid user.</summary>
        private const string LogUpdateInvalidUser = "Attempted to update an invalid user.";

        /// <summary>The message for log update non existing user.</summary>
        private const string LogUpdateNonExistingUser = "Attempted to update a non-existing user.";

        /// <summary>The message for log update duplicated email user.</summary>
        private const string LogUpdateDuplicatedEmailUser = "Attempted to update a user email to an already existing user email.";

        /// <summary>The user.</summary>
        private User user;

        /// <summary>Setups this User instance.</summary>
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

        /// <summary>Test for Updates the null user.</summary>
        [Test]
        public void UpdateNullUser()
        {
            this.user = null;

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullUser)));
        }

        /// <summary>Test for Updates the empty user.</summary>
        [Test]
        public void UpdateEmptyUser()
        {
            this.user = new User();

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidUser))));
        }

        /// <summary>Test for Updates the user doesnt exist.</summary>
        [Test]
        public void UpdateUserDoesntExist()
        {
            User nullUser = null;

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(this.user.Id)).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateNonExistingUser))));
        }

        /// <summary>Updates the user email already exist.</summary>
        [Test]
        public void UpdateUserEmailAlreadyExist()
        {
            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(this.user.Id)).Returns(this.user);
            userServiceMock.Setup(x => x.GetUserByEmail(this.user.Email)).Returns(this.user);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.UpdateUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateDuplicatedEmailUser))));
        }

        /// <summary>Updates the valid user.</summary>
        [Test]
        public void UpdateValidUser()
        {
            User nullUser = null;
            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(this.user.Id)).Returns(this.user);
            userServiceMock.Setup(x => x.GetUserByEmail(this.user.Email)).Returns(nullUser);
            userServiceMock.Setup(x => x.UpdateUser(this.user)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(userServices.UpdateUser(this.user));
        }
    }
}
