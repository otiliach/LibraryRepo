// <copyright file="GetAuthorTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The get Author tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class GetAuthorTests
    {
        /// <summary>Gets all author.</summary>
        [Test]
        public void GetAllAuthor()
        {
            List<Author> authors = GetTestAuthors();

            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthors()).Returns(authors);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            var expected = authors;
            var actual = authorServices.GetAuthors();

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual.ElementAt(i).Id, Is.EqualTo(expected[i].Id));
                Assert.That(actual.ElementAt(i).FullName, Is.EqualTo(expected[i].FullName));
            }
        }

        /// <summary>Gets the authors none found.</summary>
        [Test]
        public void GetAuthors_NoneFound()
        {
            List<Author> authors = new List<Author>();

            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthors()).Returns(authors);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(authorServices.GetAuthors());
        }

        /// <summary>Gets the author by identifier.</summary>
        [Test]
        public void GetAuthorById()
        {
            Author author = GetTestAuthor();
            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorById(author.Id)).Returns(author);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);
            var expected = author;
            var actual = authorServices.GetAuthorById(author.Id);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.FullName, Is.EqualTo(expected.FullName));
        }

        /// <summary>Gets the author by identifier not found.</summary>
        [Test]
        public void GetAuthorById_NotFound()
        {
            Author author = GetTestAuthor();
            Author? nullAuthor = null;

            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorById(author.Id)).Returns(nullAuthor);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsNull(authorServices.GetAuthorById(author.Id));
        }

        /// <summary>Gets of the author by the full name.</summary>
        [Test]
        public void GetAuthorByFullName()
        {
            Author author = GetTestAuthor();
            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorByName(author.FullName)).Returns(author);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);
            var expected = author;
            var actual = authorServices.GetAuthorByName(author.FullName);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.FullName, Is.EqualTo(expected.FullName));
        }

        /// <summary>Gets the author by full name not found.</summary>
        [Test]
        public void GetAuthorByFullName_NotFound()
        {
            Author author = GetTestAuthor();
            Author? nullAuthor = null;

            var authorServiceMock = new Mock<IAuthorDataServices>();
            authorServiceMock.Setup(x => x.GetAuthorByName(author.FullName)).Returns(nullAuthor);
            var loggerMock = new Mock<ILog>();

            var authorServices = new AuthorServicesImplementation(authorServiceMock.Object, loggerMock.Object);

            Assert.IsNull(authorServices.GetAuthorByName(author.FullName));
        }

        /// <summary>Gets the test author.</summary>
        /// <returns>An author.</returns>
        private static Author GetTestAuthor()
        {
            return new Author("Michael T. Goodrich");
        }

        /// <summary>Gets the test authors.</summary>
        /// <returns>List of authors.</returns>
        private static List<Author> GetTestAuthors()
        {
            return new List<Author>
            {
                new Author("Mmmmm M. Mmmmmm"),
                new Author("Bbbbbb B. Bbbbb"),
                new Author("Ccccc C. Cccccc"),
                new Author("Dddd D. Ddddddd"),
            };
        }
    }
}
