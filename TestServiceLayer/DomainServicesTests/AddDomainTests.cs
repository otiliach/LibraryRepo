// <copyright file="AddDomainTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The add domain tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class AddDomainTests
    {
        /// <summary>The string log add null domain.</summary>
        private const string LogAddNullDomain = "Attempted to add a null domain.";

        /// <summary>The string log add invalid domain.</summary>
        private const string LogAddInvalidDomain = "Attempted to add an invalid domain.";

        /// <summary>The string log add existing domain.</summary>
        private const string LogAddExistingDomain = "Attempted to add an already existing domain.";

        /// <summary>The domain.</summary>
        private Domain domain;

        /// <summary>Setups this domain instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.domain = new Domain("Algorithm", new Domain("Informatica", null));
        }

        /// <summary>Test for adds the null domain.</summary>
        [Test]
        public void AddNullDomain()
        {
            this.domain = null;

            var domainServiceMock = new Mock<IDomainDataServices>();
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.AddDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullDomain)));
        }

        /// <summary>Test for adds the empty domain.</summary>
        [Test]
        public void AddEmptyDomain()
        {
            this.domain = new Domain();

            var domainServiceMock = new Mock<IDomainDataServices>();
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.AddDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidDomain))));
        }

        /// <summary>Test for adds the domain who has the name null.</summary>
        [Test]
        public void AddDomainNullName()
        {
            this.domain.Name = null;

            var domainServiceMock = new Mock<IDomainDataServices>();
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.AddDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidDomain))));
        }

        /// <summary>Test for adds the domain who has name too long.</summary>
        [Test]
        public void AddDomainNameTooLong()
        {
            this.domain.Name = new string('a', 51);

            var domainServiceMock = new Mock<IDomainDataServices>();
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.AddDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidDomain))));
        }

        /// <summary>Test for adds the domain with name existing.</summary>
        [Test]
        public void AddDomainNameAlreadyExist()
        {
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainByName(this.domain.Name)).Returns(this.domain);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.AddDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddExistingDomain))));
        }

        /// <summary>Test for adds the valid domain.</summary>
        [Test]
        public void AddValiddomain()
        {
            Domain nullDomain = null;
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainByName(this.domain.Name)).Returns(nullDomain);
            domainServiceMock.Setup(x => x.AddDomain(this.domain)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(domainServices.AddDomain(this.domain));
        }
    }
}
