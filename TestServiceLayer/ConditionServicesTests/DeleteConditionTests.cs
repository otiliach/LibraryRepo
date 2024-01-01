// <copyright file="DeleteConditionTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The delete condition tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class DeleteConditionTests
    {
        /// <summary>The log delete null condition.</summary>
        private const string LogDeleteNullCondition = "Attempted to delete a null condition.";

        /// <summary>The log delete non existing condition.</summary>
        private const string LogDeleteNonExistingCondition = "Attempted to delete a non-existing condition.";

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

        /// <summary>Deletes the null condition.</summary>
        [Test]
        public void DeleteNullCondition()
        {
            this.condition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.DeleteCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message == LogDeleteNullCondition)));
        }

        /// <summary>Deletes the condition doesnt exist.</summary>
        [Test]
        public void DeleteConditionDoesntExist()
        {
            Condition nullCondition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(this.condition.Id)).Returns(nullCondition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsFalse(conditionServices.DeleteCondition(this.condition));
            loggerMock.Verify(logger => logger.Warn(It.Is<string>(message => message.Contains(LogDeleteNonExistingCondition))));
        }

        /// <summary>Deletes the valid condition.</summary>
        [Test]
        public void DeleteValidCondition()
        {
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(this.condition.Id)).Returns(this.condition);
            conditionServiceMock.Setup(x => x.DeleteCondition(this.condition)).Returns(true);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsTrue(conditionServices.DeleteCondition(this.condition));
        }
    }
}
