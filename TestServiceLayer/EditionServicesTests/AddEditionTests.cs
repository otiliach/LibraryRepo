// <copyright file="AddEditionTests.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestServiceLayer.EditionServicesTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Enums;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using ServiceLayer.Implementation;

    /// <summary>The add edition tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class AddEditionTests
    {
        /// <summary>The message for log add null edition.</summary>
        private const string LogAddNullEdition = "Attempted to add a null edition.";

        /// <summary>The message for log add invalid edition.</summary>
        private const string LogAddInvalidEdition = "Attempted to add an invalid edition.";

        /// <summary>The message for log add existing edition.</summary>
        private const string LogAddExistingEdition = "Attempted to add an already existing edition.";

        /// <summary>The edition.</summary>
        private Edition edition;

        /// <summary>Setups this edition instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.edition = new Edition(
                "Litera",
                2011,
                714,
                2,
                EBookType.Paperback,
                5,
                2);

            List<Author> authors = new List<Author>()
            {
                new Author("Michael T. Goodrich"),
                new Author("Roberto Tamassia"),
                new Author("David Mount"),
            };

            List<Domain> domains = new List<Domain>()
            {
                new Domain("Data Structures", null),
                new Domain("Algorithms", null),
            };

            List<Edition> editions = new List<Edition>()
            {
                new Edition("Litera", 2011, 714, 2, EBookType.Paperback, 5, 2),
            };

            Book book = new Book(
                "Data Structures & Algorithms in C++",
                authors,
                domains,
                editions);

            this.edition.Book = book;

            int editionId = this.edition.Id;
        }

        /// <summary>Test for Adds the null edition.</summary>
        [Test]
        public void AddNullEdition()
        {
            this.edition = null;

            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(editionServices.AddEdition(this.edition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullEdition)));
        }

        /// <summary>Test for Adds the empty edition.</summary>
        [Test]
        public void AddEmptyEdition()
        {
            this.edition = new Edition();

            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(editionServices.AddEdition(this.edition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidEdition))));
        }

        /// <summary>Test for Adds the valid edition.</summary>
        [Test]
        public void AddValidEdition()
        {
            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.AddEdition(this.edition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(editionServices.AddEdition(this.edition));
        }
    }
}
