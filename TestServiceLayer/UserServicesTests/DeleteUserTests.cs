// <copyright file="DeleteUserTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The delete User tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class DeleteUserTests
    {
        /// <summary>The message for log delete null user.</summary>
        private const string LogDeleteNullUser = "Attempted to delete a null user.";

        /// <summary>The message for log delete non existing user.</summary>
        private const string LogDeleteNonExistingUser = "Attempted to delete a non-existing user.";

        /// <summary>The user.</summary>
        private User? user;

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

        /// <summary>Test for Deletes the null user.</summary>
        [Test]
        public void DeleteNullUser()
        {
            this.user = null;

            var userServiceMock = new Mock<IUserDataServices>();
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.DeleteUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullUser)));
        }

        /// <summary>Test for Deletes the user doesnt exist.</summary>
        [Test]
        public void DeleteUserDoesntExist()
        {
            User? nullUser = null;

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(nullUser);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(userServices.DeleteUser(this.user));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogDeleteNonExistingUser))));
        }

        /// <summary>Test for Deletes the valid user.</summary>
        [Test]
        public void DeleteValidUser()
        {
            Assert.IsNotNull(this.user);

            var userServiceMock = new Mock<IUserDataServices>();
            userServiceMock.Setup(x => x.GetUserById(It.IsAny<int>())).Returns(this.user);
            userServiceMock.Setup(x => x.DeleteUser(this.user)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var userServices = new UserServicesImplementation(userServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(userServices.DeleteUser(this.user));
        }
    }
}
