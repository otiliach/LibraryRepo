// <copyright file="AddAuthorTests.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestServiceLayer.AuthorServicesTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using ServiceLayer.Implementation;

    /// <summary>The add Author test class.</summary>
    [ExcludeFromCodeCoverage]
    internal class AddAuthorTests
    {
        /// <summary>The log add null author.</summary>
        private const string LogAddNullAuthor = "Attempted to add a null author.";

        /// <summary>The log add invalid author.</summary>
        private const string LogAddInvalidAuthor = "Attempted to add an invalid author.";

        /// <summary>The log add existing author.</summary>
        private const string LogAddExistingAuthor = "Attempted to add an already existing author.";

        /// <summary>The author.</summary>
        private Author author;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.author = new Author("Michael T. Goodrich");

            int authorId = this.author.Id;
        }

        /// <summary>Adds the null author test.</summary>
        [Test]
        public void AddNullAuthor()
        {
            this.author = null;

            var authorServiceMock = new Mock<IAuthorDataServices>();
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.AddAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullAuthor)));
        }

        /// <summary>Adds the empty author.</summary>
        [Test]
        public void AddEmptyAuthor()
        {
            this.author = new Author();

            var authorServiceMock = new Mock<IAuthorDataServices>();
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.AddAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidAuthor))));
        }

        /// <summary>Adds the name of the author null.</summary>
        [Test]
        public void AddAuthorNullName()
        {
            this.author.FullName = null;

            var authorServiceMock = new Mock<IAuthorDataServices>();
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.AddAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidAuthor))));
        }

        /// <summary>Adds the author name too long.</summary>
        [Test]
        public void AddAuthorNameTooLong()
        {
            this.author.FullName = new string('a', 12);

            var authorServiceMock = new Mock<IAuthorDataServices>();
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.AddAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidAuthor))));
        }

        /// <summary>Adds the author name avlready exist.</summary>
        [Test]
        public void AddAuthorNameAvlreadyExist()
        {
            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.AuthorNameAlreadyExists(this.author.FullName)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.AddAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddExistingAuthor))));
        }

        /// <summary>Adds the valid author.</summary>
        [Test]
        public void AddValidAuthor()
        {
            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.AuthorNameAlreadyExists(this.author.FullName)).Returns(false);
            authorServiceMock.Setup(x => x.AddAuthor(this.author)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(authorServices.AddAuthor(this.author));
        }
    }
}
