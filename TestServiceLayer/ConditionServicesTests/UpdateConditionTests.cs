// <copyright file="UpdateConditionTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The update condition tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class UpdateConditionTests
    {
        /// <summary>The log update null condition.</summary>
        private const string LogUpdateNullCondition = "Attempted to update a null condition.";

        /// <summary>The log update invalid condition.</summary>
        private const string LogUpdateInvalidCondition = "Attempted to update an invalid condition.";

        /// <summary>The log update non existing condition.</summary>
        private const string LogUpdateNonExistingCondition = "Attempted to update a non-existing condition.";

        /// <summary>The log update duplicated name condition.</summary>
        private const string LogUpdateDuplicatedNameCondition = "Attempted to update a condition name to an already existing condition name.";

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

        /// <summary>Updates the null condition.</summary>
        [Test]
        public void UpdateNullCondition()
        {
            this.condition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogUpdateNullCondition)));
        }

        /// <summary>Updates the empty condition.</summary>
        [Test]
        public void UpdateEmptyCondition()
        {
            this.condition = new Condition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateInvalidCondition))));
        }

        /// <summary>Updates the condition doesnt exist.</summary>
        [Test]
        public void UpdateConditionDoesntExist()
        {
            Condition nullCondition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(this.condition.Id)).Returns(nullCondition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateNonExistingCondition))));
        }

        /// <summary>Updates the condition name already exist.</summary>
        [Test]
        public void UpdateConditionNameAlreadyExist()
        {
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(this.condition.Id)).Returns(this.condition);
            conditionServiceMock.Setup(x => x.GetConditionByName(this.condition.Name)).Returns(this.condition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.UpdateCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogUpdateDuplicatedNameCondition))));
        }

        /// <summary>Updates the valid condition.</summary>
        [Test]
        public void UpdateValidCondition()
        {
            Condition nullCondition = null;
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(this.condition.Id)).Returns(this.condition);
            conditionServiceMock.Setup(x => x.GetConditionByName(this.condition.Name)).Returns(nullCondition);
            conditionServiceMock.Setup(x => x.UpdateCondition(this.condition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(conditionServices.UpdateCondition(this.condition));
        }
    }
}
