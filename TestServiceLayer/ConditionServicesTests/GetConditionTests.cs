// <copyright file="GetConditionTests.cs" company="Transilvania University of Brasov">
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

    /// <summary>The get condition tests class.</summary>
    [ExcludeFromCodeCoverage]
    internal class GetConditionTests
    {
        /// <summary>Gets all condition.</summary>
        [Test]
        public void GetAllCondition()
        {
            List<Condition> conditions = GetTestConditions();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditions()).Returns(conditions);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            var expected = conditions;
            var actual = conditionServices.GetConditions();

            Assert.That(actual.Count, Is.EqualTo(expected.Count));

            for (int i = 0; i < expected.Count; i++)
            {
                Assert.That(actual.ElementAt(i).Id, Is.EqualTo(expected[i].Id));
                Assert.That(actual.ElementAt(i).Name, Is.EqualTo(expected[i].Name));
                Assert.That(actual.ElementAt(i).Description, Is.EqualTo(expected[i].Description));
                Assert.That(actual.ElementAt(i).Value, Is.EqualTo(expected[i].Value));
            }
        }

        /// <summary>Gets the conditions none found.</summary>
        [Test]
        public void GetConditions_NoneFound()
        {
            List<Condition> conditions = new List<Condition>();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditions()).Returns(conditions);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsEmpty(conditionServices.GetConditions());
        }

        /// <summary>Gets the condition by identifier.</summary>
        [Test]
        public void GetConditionById()
        {
            Condition condition = GetTestCondition();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(condition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);
            var expected = condition;
            var actual = conditionServices.GetConditionById(condition.Id);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Description, Is.EqualTo(expected.Description));
            Assert.That(actual.Value, Is.EqualTo(expected.Value));
        }

        /// <summary>Gets the condition by identifier not found.</summary>
        [Test]
        public void GetConditionById_NotFound()
        {
            Condition condition = GetTestCondition();
            Condition? nullCondition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionById(condition.Id)).Returns(nullCondition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsNull(conditionServices.GetConditionById(condition.Id));
        }

        /// <summary>Gets the name of the condition by.</summary>
        [Test]
        public void GetConditionByName()
        {
            Condition condition = GetTestCondition();
            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionByName(condition.Name)).Returns(condition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);
            var expected = condition;
            var actual = conditionServices.GetConditionByName(condition.Name);

            Assert.IsNotNull(actual);
            Assert.That(actual.Id, Is.EqualTo(expected.Id));
            Assert.That(actual.Name, Is.EqualTo(expected.Name));
            Assert.That(actual.Description, Is.EqualTo(expected.Description));
            Assert.That(actual.Value, Is.EqualTo(expected.Value));
        }

        /// <summary>Gets the condition by name not found.</summary>
        [Test]
        public void GetConditionByName_NotFound()
        {
            Condition condition = GetTestCondition();
            Condition? nullCondition = null;

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetConditionByName(condition.Name)).Returns(nullCondition);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.IsNull(conditionServices.GetConditionByName(condition.Name));
        }

        /// <summary>Gets the domenii not found.</summary>
        [Test]
        public void GetDOMENII_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetDOMENII()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetDOMENII(), Is.EqualTo(0));
        }

        /// <summary>Gets the DOMENII condition.</summary>
        [Test]
        public void GetDOMENII()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetDOMENII()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetDOMENII(), Is.EqualTo(3));
        }

        /// <summary>Gets the NMC condition not found.</summary>
        [Test]
        public void GetNMC_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetNMC(), Is.EqualTo(0));
        }

        /// <summary>Gets the NMC condition.</summary>
        [Test]
        public void GetNMC()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNMC()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetNMC(), Is.EqualTo(3));
        }

        /// <summary>Gets the PER not found.</summary>
        [Test]
        public void GetPER_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetPER()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetPER(), Is.EqualTo(0));
        }

        /// <summary>Gets the PER.</summary>
        [Test]
        public void GetPER()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetPER()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetPER(), Is.EqualTo(3));
        }

        /// <summary>Gets the C condition not found.</summary>
        [Test]
        public void GetC_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetC()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetC(), Is.EqualTo(0));
        }

        /// <summary>Gets the C condition.</summary>
        [Test]
        public void GetC()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetC()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetC(), Is.EqualTo(3));
        }

        /// <summary>Gets the D condition not found.</summary>
        [Test]
        public void GetD_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetD()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetD(), Is.EqualTo(0));
        }

        /// <summary>Gets the D condition.</summary>
        [Test]
        public void GetD()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetD()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetD(), Is.EqualTo(3));
        }

        /// <summary>Gets the L condition not found.</summary>
        [Test]
        public void GetL_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetL()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetL(), Is.EqualTo(0));
        }

        /// <summary>Gets the L condition.</summary>
        [Test]
        public void GetL()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetL()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetL(), Is.EqualTo(3));
        }

        /// <summary>Gets the LIM condition not found.</summary>
        [Test]
        public void GetLIM_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetLIM()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetLIM(), Is.EqualTo(0));
        }

        /// <summary>Gets the LIM condition.</summary>
        [Test]
        public void GetLIM()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetLIM()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetLIM(), Is.EqualTo(3));
        }

        /// <summary>Gets the DELTA condition not found.</summary>
        [Test]
        public void GetDELTA_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetDELTA()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetDELTA(), Is.EqualTo(0));
        }

        /// <summary>Gets the DELTA condition.</summary>
        [Test]
        public void GetDELTA()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetDELTA()).Returns(60);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetDELTA(), Is.EqualTo(60));
        }

        /// <summary>Gets the NCZ condition not found.</summary>
        [Test]
        public void GetNCZ_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNCZ()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetNCZ(), Is.EqualTo(0));
        }

        /// <summary>Gets the NCZ condition.</summary>
        [Test]
        public void GetNCZ()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetNCZ()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetNCZ(), Is.EqualTo(3));
        }

        /// <summary>Gets the PERSIMP condition not found.</summary>
        [Test]
        public void GetPERSIMP_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetPERSIMP()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetPERSIMP(), Is.EqualTo(0));
        }

        /// <summary>Gets the PERSIMP condition.</summary>
        [Test]
        public void GetPERSIMP()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetPERSIMP()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetPERSIMP(), Is.EqualTo(3));
        }

        /// <summary>Gets the TIMPIMP condition not found.</summary>
        [Test]
        public void GetTIMPIMP_NotFound()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetTIMPIMP()).Returns(0);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetTIMPIMP(), Is.EqualTo(0));
        }

        /// <summary>Gets the TIMPIMP condition.</summary>
        [Test]
        public void GetTIMPIMP()
        {
            Condition condition = GetTestCondition();

            var conditionServiceMock = new Mock<IConditionDataServices>();
            conditionServiceMock.Setup(x => x.GetTIMPIMP()).Returns(3);
            var loggerMock = new Mock<ILog>();

            var conditionServices = new ConditionServicesImplementation(conditionServiceMock.Object, loggerMock.Object);

            Assert.That(conditionServices.GetTIMPIMP(), Is.EqualTo(3));
        }

        /// <summary>Gets the test condition.</summary>
        /// <returns>A Condition.</returns>
        private static Condition GetTestCondition()
        {
            return new Condition(
                "NCZ",
                "Numar maxim de carti imprumutate pe zi ",
                3);
        }

        /// <summary>Gets the test conditions.</summary>
        /// <returns>A List of Conditions.</returns>
        private static List<Condition> GetTestConditions()
        {
            return new List<Condition>
            {
                new Condition("AA", "aaa ", 1),
                new Condition("BB", "bbb ", 2),
                new Condition("CC", "CCC ", 3),
                new Condition("DD", "ddd ", 4),
            };
        }
    }
}
