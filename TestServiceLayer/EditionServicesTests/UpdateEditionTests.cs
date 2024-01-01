// <copyright file="UpdateEditionTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The update edition tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class UpdateEditionTests
    {
        /// <summary>The message for log update null edition.</summary>
        private const string LogUpdateNullEdition = "Attempted to update a null edition.";

        /// <summary>The message for log update invalid edition.</summary>
        private const string LogUpdateInvalidEdition = "Attempted to update an invalid edition.";

        /// <summary>The message for log update non existing edition.</summary>
        private const string LogUpdateNonExistingEdition = "Attempted to update a non-existing edition.";

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

        /// <summary>The test for Updates the null edition.</summary>
        [Test]
        public void UpdateNullEdition()
        {
            this.edition = null;

            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(editionServices.UpdateEdition(this.edition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullEdition)));
        }

        /// <summary>Test for Updates the empty edition.</summary>
        [Test]
        public void UpdateEmptyEdition()
        {
            this.edition = new Edition();

            var editionServiceMock = new Mock<IEditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(editionServices.UpdateEdition(this.edition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidEdition))));
        }

        /// <summary>Test for Updates the edition doesnt exist.</summary>
        [Test]
        public void UpdateEditionDoesntExist()
        {
            Edition nullEdition = null;

            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.GetEditionById(this.edition.Id)).Returns(nullEdition);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(editionServices.UpdateEdition(this.edition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateNonExistingEdition))));
        }

        /// <summary>Test for Updates the valid edition.</summary>
        [Test]
        public void UpdateValidEdition()
        {
            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.GetEditionById(this.edition.Id)).Returns(this.edition);
            editionServiceMock.Setup(x => x.UpdateEdition(this.edition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(editionServices.UpdateEdition(this.edition));
        }
    }
}
