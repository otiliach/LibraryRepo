// <copyright file="UpdateDomainTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The update domain tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class UpdateDomainTests
    {
        /// <summary>The string log update null domain.</summary>
        private const string LogUpdateNullDomain = "Attempted to update a null domain.";

        /// <summary>The string log update invalid domain.</summary>
        private const string LogUpdateInvalidDomain = "Attempted to update an invalid domain.";

        /// <summary>The string  log update non existing domain.</summary>
        private const string LogUpdateNonExistingDomain = "Attempted to update a non-existing domain.";

        /// <summary>The string log update duplicated name domain.</summary>
        private const string LogUpdateDuplicatedNameDomain = "Attempted to update a domain name to an already existing domain name.";

        /// <summary>The domain.</summary>
        private Domain domain;

        /// <summary>Setups this domain instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.domain = new Domain(
                "Informatică",
                new Domain("Știință", null));

            int domainId = this.domain.Id;
        }

        /// <summary>Test for Updates the null domain.</summary>
        [Test]
        public void UpdateNullDomain()
        {
            this.domain = null;

            var domainServiceMock = new Mock<IDomainDataServices>();
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.UpdateDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullDomain)));
        }

        /// <summary>Test for Updates the empty domain.</summary>
        [Test]
        public void UpdateEmptyDomain()
        {
            this.domain = new Domain();

            var domainServiceMock = new Mock<IDomainDataServices>();
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.UpdateDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidDomain))));
        }

        /// <summary>Test for Updates the domain doesnt exist.</summary>
        [Test]
        public void UpdateDomainDoesntExist()
        {
            Domain nullDomain = null;

            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainById(this.domain.Id)).Returns(nullDomain);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.UpdateDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateNonExistingDomain))));
        }

        /// <summary>Test for Updates the domain name already exist.</summary>
        [Test]
        public void UpdateDomainNameAlreadyExist()
        {
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainById(this.domain.Id)).Returns(this.domain);
            domainServiceMock.Setup(x => x.GetDomainByName(this.domain.Name)).Returns(this.domain);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.UpdateDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateDuplicatedNameDomain))));
        }

        /// <summary>Test for Updates the valid domain.</summary>
        [Test]
        public void UpdateValidDomain()
        {
            Domain nullDomain = null;
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainById(this.domain.Id)).Returns(this.domain);
            domainServiceMock.Setup(x => x.GetDomainByName(this.domain.Name)).Returns(nullDomain);
            domainServiceMock.Setup(x => x.UpdateDomain(this.domain)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(domainServices.UpdateDomain(this.domain));
        }
    }
}
