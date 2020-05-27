using Modul1;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1Test.ModelsTest
{
    [TestFixture]
    public class DescriptionTest
    {
        ILogging mockedLogger;

        [SetUp]
        public void SetUpTests()
        {
            mockedLogger = new Mock<ILogging>().Object;
        }

        [Test]
        [TestCase(Dataset.SET1, SignalCode.CODE_ANALOG)]
        public void DoesPropertyExist_ExistingProperty_ReturnsTrue(Dataset dataset, SignalCode signal)
        {
            IModule1Property module1Property = MockModule1Property(signal, 200);
            Mock<List<IModule1Property>> mockedList = new Mock<List<IModule1Property>>();

            List<IModule1Property> properties = mockedList.Object;
            properties.Add(module1Property);

            Description description = new Description(dataset, mockedLogger);
            description.Properties = properties;

            Assert.IsTrue(description.DoesPropertyExist(signal));
        }


        [Test]
        [TestCase(Dataset.SET1, SignalCode.CODE_ANALOG)]
        public void DoesPropertyExist_NonExistingProperty_ReturnsFalse(Dataset dataset, SignalCode signal)
        {
            Mock<List<IModule1Property>> mockedList = new Mock<List<IModule1Property>>();
            List<IModule1Property> properties = mockedList.Object;

            Description description = new Description(dataset, mockedLogger);
            description.Properties = properties;

            Assert.IsFalse(description.DoesPropertyExist(signal));
        }


        [Test]
        [TestCase(Dataset.SET1, SignalCode.CODE_ANALOG)]
        public void GetPropertyByCode_NonExistingProperty_ReturnsNull(Dataset dataset, SignalCode signal)
        {
            Mock<List<IModule1Property>> mockedList = new Mock<List<IModule1Property>>();
            List<IModule1Property> properties = mockedList.Object;

            Description description = new Description(dataset, mockedLogger);
            description.Properties = properties;

            Assert.IsNull(description.GetPropertyByCode(signal));
        }


        [Test]
        [TestCase(Dataset.SET1, SignalCode.CODE_ANALOG, 200)]
        public void GetPropertyByCode_ExistingProperty_AssertObjectContent(Dataset dataset, SignalCode signal, double value)
        {
            IModule1Property module1Property = MockModule1Property(signal, value);
            Mock<List<IModule1Property>> mockedList = new Mock<List<IModule1Property>>();

            List<IModule1Property> properties = mockedList.Object;
            properties.Add(module1Property);

            Description description = new Description(dataset, mockedLogger);
            description.Properties = properties;

            IModule1Property receivedProperty = description.GetPropertyByCode(signal);

            Assert.AreEqual(module1Property.Code, receivedProperty.Code);
            Assert.AreEqual(module1Property.Module1Value, receivedProperty.Module1Value);
        }


        public IModule1Property MockModule1Property(SignalCode code, double value)
        {
            Mock<IModule1Property> mockedProperty = new Mock<IModule1Property>();
            mockedProperty.SetupGet(x => x.Code).Returns(code);
            mockedProperty.SetupGet(x => x.Module1Value).Returns(value);

            return mockedProperty.Object;
        }

    }
}
