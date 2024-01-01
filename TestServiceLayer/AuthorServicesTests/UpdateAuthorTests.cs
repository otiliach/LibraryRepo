// <copyright file="UpdateAuthorTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The update Author tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class UpdateAuthorTests
    {
        /// <summary>The log update null author.</summary>
        private const string LogUpdateNullAuthor = "Attempted to update a null author.";

        /// <summary>The log update invalid author.</summary>
        private const string LogUpdateInvalidAuthor = "Attempted to update an invalid author.";

        /// <summary>The log update non existing author.</summary>
        private const string LogUpdateNonExistingAuthor = "Attempted to update a non-existing author.";

        /// <summary>The log update dvuplicated name author.</summary>
        private const string LogUpdateDuplicatedNameAuthor = "Attempted to update a author name to an already existing author name.";

        /// <summary>The author.</summary>
        private Author author;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.author = new Author(
                "Michael T. Goodrich");

            int authorId = this.author.Id;
        }

        /// <summary>Updates the null author.</summary>
        [Test]
        public void UpdateNullAuthor()
        {
            this.author = null;

            var authorServiceMock = new Mock<IAuthorDataServices>();
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.UpdateAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullAuthor)));
        }

        /// <summary>Updates the empty author.</summary>
        [Test]
        public void UpdateEmptyAuthor()
        {
            this.author = new Author();

            var authorServiceMock = new Mock<IAuthorDataServices>();
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.UpdateAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidAuthor))));
        }

        /// <summary>Updates the author doesnt exist.</summary>
        [Test]
        public void UpdateAuthorDoesntExist()
        {
            Author nullAuthor = null;

            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorById(this.author.Id)).Returns(nullAuthor);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.UpdateAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateNonExistingAuthor))));
        }

        /// <summary>Updates the author name already exist.</summary>
        [Test]
        public void UpdateAuthorNameAlreadyExist()
        {
            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorById(this.author.Id)).Returns(this.author);
            authorServiceMock.Setup(x => x.GetAuthorByName(this.author.FullName)).Returns(this.author);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(authorServices.UpdateAuthor(this.author));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateDuplicatedNameAuthor))));
        }

        /// <summary>Updates the valid author.</summary>
        [Test]
        public void UpdateValidAuthor()
        {
            Author nullAuthor = null;
            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorById(this.author.Id)).Returns(this.author);
            authorServiceMock.Setup(x => x.GetAuthorByName(this.author.FullName)).Returns(nullAuthor);
            authorServiceMock.Setup(x => x.UpdateAuthor(this.author)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(authorServices.UpdateAuthor(this.author));
        }
    }
}
