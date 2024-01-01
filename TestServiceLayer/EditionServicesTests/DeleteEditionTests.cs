// <copyright file="DeleteEditionTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The delete edition tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class DeleteEditionTests
    {
        /// <summary>The message for log delete null edition.</summary>
        private const string LogDeleteNullEdition = "Attempted to delete a null edition.";

        /// <summary>The message for log delete non existing edition.</summary>
        private const string LogDeleteNonExistingEdition = "Attempted to delete a non-existing edition.";

        /// <summary>The edition.</summary>
        private Edition edition;

        /// <summary>Setups this Edition instance.</summary>
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

        /// <summary>Test for Deletes the null edition.</summary>
        [Test]
        public void DeleteNullEdition()
        {
            this.edition = null;

            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(editionServices.DeleteEdition(this.edition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullEdition)));
        }

        /// <summary>Test for Deletes the edition doesnt exist.</summary>
        [Test]
        public void DeleteEditionDoesntExist()
        {
            Edition nullEdition = null;

            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.GetEditionById(this.edition.Id)).Returns(nullEdition);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(editionServices.DeleteEdition(this.edition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogDeleteNonExistingEdition))));
        }

        /// <summary>Test for Deletes the valid edition.</summary>
        [Test]
        public void DeleteValidEdition()
        {
            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.GetEditionById(this.edition.Id)).Returns(this.edition);
            editionServiceMock.Setup(x => x.DeleteEdition(this.edition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(editionServices.DeleteEdition(this.edition));
        }
    }
}
