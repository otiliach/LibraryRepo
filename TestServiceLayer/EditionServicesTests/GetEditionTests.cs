// <copyright file="GetEditionTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The get edition tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class GetEditionTests
    {
        /// <summary>Gets the test edition.</summary>
        /// <returns>An Edition for tests.</returns>
        private static Edition GetTestEdition()
        {
            Edition newEdition = new Edition(
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

            newEdition.Book = book;

            return newEdition;
        }

        /// <summary>Gets the test editions.</summary>
        /// <returns>A list of Editions for tests.</returns>
        private static List<Edition> GetTestEditions()
        {
            return new List<Edition>()
            {
                GetTestEdition(),
                GetTestEdition(),
                GetTestEdition(),
                GetTestEdition(),
                GetTestEdition(),
            };
        }

        /// <summary>Test for Gets all edition.</summary>
        [Test]
        public void GetAllEdition()
        {
            List<Edition> editions = GetTestEditions();

            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.GetEditions()).Returns(editions);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            var expected = editions;
            var actual = editionServices.GetEditions();

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual.ElementAt(i).Id, Is.EqualTo(expected[i].Id));
            }
        }

        /// <summary>Test for Gets the editions none found.</summary>
        [Test]
        public void GetEditions_NoneFound()
        {
            List<Edition> editions = new List<Edition>();

            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.GetEditions()).Returns(editions);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(editionServices.GetEditions());
        }

        /// <summary>Gets the edition by identifier.</summary>
        [Test]
        public void GetEditionById()
        {
            Edition edition = GetTestEdition();
            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.GetEditionById(edition.Id)).Returns(edition);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);
            var expected = edition;
            var actual = editionServices.GetEditionById(edition.Id);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
        }

        /// <summary>Test for Gets the edition by identifier not found.</summary>
        [Test]
        public void GetEditionById_NotFound()
        {
            Edition edition = GetTestEdition();
            Edition nullEdition = null;

            var editionServiceMock = new Mock<IEditionDataServices>();
            editionServiceMock.Setup(x => x.GetEditionById(edition.Id)).Returns(nullEdition);
            var loggerMock = new Mock<ILog>();

            var editionServices = new EditionServicesImplementation(editionServiceMock.Object, loggerMock.Object);

            Assert.IsNull(editionServices.GetEditionById(edition.Id));
        }
    }
}
