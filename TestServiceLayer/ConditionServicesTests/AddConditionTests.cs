// <copyright file="AddConditionTests.cs" company="Transilvania University of Brasov">
// Chelmus Otilia-Elena
// </copyright>

namespace TestServiceLayer.ConditionServicesTests
{
    using System.Diagnostics.CodeAnalysis;
    using DataMapper.Interfaces;
    using DomainModel.Models;
    using log4net;
    using Moq;
    using ServiceLayer.Implementation;

    /// <summary>Te add conditions tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class AddConditionTests
    {
        /// <summary>The log add null condition.</summary>
        private const string LogAddNullCondition = "Attempted to add a null condition.";

        /// <summary>The log add invalid condition.</summary>
        private const string LogAddInvalidCondition = "Attempted to add an invalid condition.";

        /// <summary>The log add existing condition.</summary>
        private const string LogAddExistingCondition = "Attempted to add an already existing condition.";

        /// <summary>The condition.</summary>
        private Condition condition;

        /// <summary>Setups this instance.</summary>
        [SetUp]
        public void Setup()
        {
            this.condition = new Condition(
                "NCZ",
                "Numar maxim de carti imprumutate pe zi ",
                3);

            int conditionId = this.condition.Id;
        }

        /// <summary>Adds the null condition.</summary>
        [Test]
        public void AddNullCondition()
        {
            this.condition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogAddNullCondition)));
        }

        /// <summary>Adds the empty condition.</summary>
        [Test]
        public void AddEmptyCondition()
        {
            this.condition = new Condition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>Adds the name of the condition null.</summary>
        [Test]
        public void AddConditionNullName()
        {
            this.condition.Name = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>Adds the condition name too long.</summary>
        [Test]
        public void AddConditionNameTooLong()
        {
            this.condition.Name = new string('a', 12);

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddInvalidCondition))));
        }

        /// <summary>Adds the condition name already exist.</summary>
        [Test]
        public void AddConditionNameAlreadyExist()
        {
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.ConditionNameAlreadyExists(this.condition.Name)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.AddCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogAddExistingCondition))));
        }

        /// <summary>Adds the valid condition.</summary>
        [Test]
        public void AddValidCondition()
        {
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.ConditionNameAlreadyExists(this.condition.Name)).Returns(false);
            conditionServiceMock.Setup(x => x.AddCondition(this.condition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(conditionServices.AddCondition(this.condition));
        }
    }
}
