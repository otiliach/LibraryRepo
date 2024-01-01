// <copyright file="DeleteAuthorTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The delete Author tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class DeleteAuthorTests
    {
        /// <summary>The log delete null author.</summary>
        private const string LogDeleteNullAuthor = "Attempted to delete a null author.";

        /// <summary>The log delete non existing author.</summary>
        private const string LogDeleteNonExistingAuthor = "Attempted to delete a non-existing author.";

        /// <summary>The author.</summary>
        private Author author;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.author = new Author("Michael T.Goodrich");

            int authorId = this.author.Id;
        }

        /// <summary>Deletes the null author.</summary>
        [Test]
        public void DeleteNullAuthor()
        {
            this.author = null;

            var authorServiceMock = new Mock<IAuthorDataServices>();
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.DeleteAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullAuthor)));
        }

        /// <summary>Deletes the author doesnt exist.</summary>
        [Test]
        public void DeleteAuthorDoesntExist()
        {
            Author nullAuthor = null;

            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorById(this.author.Id)).Returns(nullAuthor);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.DeleteAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogDeleteNonExistingAuthor))));
        }

        /// <summary>Deletes the valid author.</summary>
        [Test]
        public void DeleteValidAuthor()
        {
            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorById(this.author.Id)).Returns(this.author);
            authorServiceMock.Setup(x => x.DeleteAuthor(this.author)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(authorServices.DeleteAuthor(this.author));
        }
    }
}
