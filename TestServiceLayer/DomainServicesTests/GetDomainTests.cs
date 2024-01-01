// <copyright file="GetDomainTests.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestServiceLayer.DomainServicesTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using ServiceLayer.Implementation;
    using ServiceLayer.Interfaces;

    /// <summary>The get domain tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class GetDomainTests
    {
        /// <summary>Test for Gets all domain.</summary>
        [Test]
        public void GetAllDomain()
        {
            List<Domain> domains = GetTestDomains();

            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomains()).Returns(domains);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            var expected = domains;
            var actual = domainServices.GetDomains();

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual.ElementAt(i).Id, Is.EqualTo(expected[i].Id));
                Assert.That(actual.ElementAt(i).Name, Is.EqualTo(expected[i].Name));
            }
        }

        /// <summary>Test for Gets the domains none found.</summary>
        [Test]
        public void GetDomains_NoneFound()
        {
            List<Domain> domains = new List<Domain>();

            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomains()).Returns(domains);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(domainServices.GetDomains());
        }

        /// <summary>Test for Gets the domain by identifier.</summary>
        [Test]
        public void GetDomainById()
        {
            Domain domain = GetTestDomain();
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainById(domain.Id)).Returns(domain);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);
            var expected = domain;
            var actual = domainServices.GetDomainById(domain.Id);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
        }

        /// <summary>Test for Gets the domain by identifier not found.</summary>
        [Test]
        public void GetDomainById_NotFound()
        {
            Domain domain = GetTestDomain();
            Domain? nullDomain = null;

            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainById(domain.Id)).Returns(nullDomain);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsNull(domainServices.GetDomainById(domain.Id));
        }

        /// <summary>Test for Gets the name of the domain by.</summary>
        [Test]
        public void GetDomainByName()
        {
            Domain domain = GetTestDomain();
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainByName(domain.Name)).Returns(domain);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);
            var expected = domain;
            var actual = domainServices.GetDomainByName(domain.Name);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
        }

        /// <summary>Test for Gets the domain by name not found.</summary>
        [Test]
        public void GetDomainByName_NotFound()
        {
            Domain domain = GetTestDomain();
            Domain? nullDomain = null;

            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainByName(domain.Name)).Returns(nullDomain);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsNull(domainServices.GetDomainByName(domain.Name));
        }

        /// <summary>Test for Gets the child domains.</summary>
        [Test]
        public void GetChildDomains()
        {
            Domain domain = GetTestDomain();
            List<Domain> domains = GetTestDomains();
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetChildDomains(domain)).Returns(domains);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);
            var expected = domain;
            var actual = domainServices.GetChildDomains(domain);

            Assert.IsNotEmpty(actual);
        }

        /// <summary>Test for Gets the child domains not found.</summary>
        [Test]
        public void GetChildDomains_NotFound()
        {
            Domain domain = GetTestDomain();
            List<Domain> domains = new List<Domain>();

            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetChildDomains(domain)).Returns(domains);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(domainServices.GetChildDomains(domain));
        }

        /// <summary>Gets the test domain.</summary>
        /// <returns>A Domain for tests.</returns>
        private static Domain GetTestDomain()
        {
            return new Domain(
                "Informatică",
                new Domain("Știință", null));
        }

        /// <summary>Gets the test domains.</summary>
        /// <returns>A List of Domains for tests.</returns>
        private static List<Domain> GetTestDomains()
        {
            Domain parentDomain = GetTestDomain();
            return new List<Domain>
            {
                new Domain("Algoritmi", parentDomain),
                new Domain("Programare", parentDomain),
                new Domain("Baze de date", parentDomain),
                new Domain("Rețele de calculatoare", parentDomain),
            };
        }
    }
}
