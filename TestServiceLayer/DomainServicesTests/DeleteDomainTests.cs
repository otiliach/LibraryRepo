// <copyright file="DeleteDomainTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The delete domain tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class DeleteDomainTests
    {
        /// <summary>The message log delete null domain.</summary>
        private const string LogDeleteNullDomain = "Attempted to delete a null domain.";

        /// <summary>The message log delete non existing domain.</summary>
        private const string LogDeleteNonExistingDomain = "Attempted to delete a non-existing domain.";

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

        /// <summary>Test for Deletes the null domain.</summary>
        [Test]
        public void DeleteNullDomain()
        {
            this.domain = null;

            var domainServiceMock = new Mock<IDomainDataServices>();
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.DeleteDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullDomain)));
        }

        /// <summary>Test for Deletes the domain doesnt exist.</summary>
        [Test]
        public void DeleteDomainDoesntExist()
        {
            Domain nullDomain = null;

            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainById(this.domain.Id)).Returns(nullDomain);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(domainServices.DeleteDomain(this.domain));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogDeleteNonExistingDomain))));
        }

        /// <summary>Test for Deletes the valid domain.</summary>
        [Test]
        public void DeleteValidDomain()
        {
            var domainServiceMock = new Mock<IDomainDataServices>();
            domainServiceMock.Setup(x => x.GetDomainById(this.domain.Id)).Returns(this.domain);
            domainServiceMock.Setup(x => x.DeleteDomain(this.domain)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var domainServices = new DomainServicesImplementation(domainServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(domainServices.DeleteDomain(this.domain));
        }
    }
}
