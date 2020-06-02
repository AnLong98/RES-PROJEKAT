using Common;
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

            Description description = new Description(dataset, mockedLogger)
            {
                Properties = properties
            };

            Assert.IsTrue(description.DoesPropertyExist(signal));
        }


        [Test]
        [TestCase(Dataset.SET1, SignalCode.CODE_ANALOG)]
        public void DoesPropertyExist_NonExistingProperty_ReturnsFalse(Dataset dataset, SignalCode signal)
        {
            Mock<List<IModule1Property>> mockedList = new Mock<List<IModule1Property>>();
            List<IModule1Property> properties = mockedList.Object;

            Description description = new Description(dataset, mockedLogger)
            {
                Properties = properties
            };

            Assert.IsFalse(description.DoesPropertyExist(signal));
        }


        [Test]
        [TestCase(Dataset.SET1, SignalCode.CODE_ANALOG)]
        public void GetPropertyByCode_NonExistingProperty_ReturnsNull(Dataset dataset, SignalCode signal)
        {
            Mock<List<IModule1Property>> mockedList = new Mock<List<IModule1Property>>();
            List<IModule1Property> properties = mockedList.Object;

            Description description = new Description(dataset, mockedLogger)
            {
                Properties = properties
            };

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

            Description description = new Description(dataset, mockedLogger)
            {
                Properties = properties
            };

            IModule1Property receivedProperty = description.GetPropertyByCode(signal);

            Assert.AreEqual(module1Property.Code, receivedProperty.Code);
            Assert.AreEqual(module1Property.Module1Value, receivedProperty.Module1Value);
        }


        [Test]
        [TestCase(Dataset.SET1, SignalCode.CODE_ANALOG, 200)]
        public void AddOrReplaceProperty_ExistingProperty_PropertyReplaced(Dataset dataset, SignalCode signal, double value)
        {
            IModule1Property existingProperty = MockModule1Property(signal, 100);
            IModule1Property newProperty = MockModule1Property(signal, value);
            Mock<List<IModule1Property>> mockedList = new Mock<List<IModule1Property>>();

            List<IModule1Property> properties = mockedList.Object;
            properties.Add(existingProperty);
            Description description = new Description(dataset, mockedLogger)
            {
                Properties = properties
            };

            IModule1Property propertyInside = description.Properties[0];

            Assert.AreEqual(existingProperty.Code, propertyInside.Code);
            Assert.AreEqual(existingProperty.Module1Value, propertyInside.Module1Value);

            description.AddOrReplaceProperty(newProperty);

            propertyInside = description.Properties[0];

            Assert.AreEqual(newProperty.Code, propertyInside.Code);
            Assert.AreEqual(newProperty.Module1Value, propertyInside.Module1Value);
        }


        [Test]
        [TestCase(Dataset.SET1, SignalCode.CODE_ANALOG, 200)]
        public void AddOrReplaceProperty_NonExistingProperty_PropertyAddedReplaced(Dataset dataset, SignalCode signal, double value)
        {
            IModule1Property newProperty = MockModule1Property(signal, value);
            Mock<List<IModule1Property>> mockedList = new Mock<List<IModule1Property>>();

            List<IModule1Property> properties = mockedList.Object;
            Assert.IsEmpty(properties);

            Description description = new Description(dataset, mockedLogger)
            {
                Properties = properties
            };

            description.AddOrReplaceProperty(newProperty);

            IModule1Property propertyInside = description.Properties[0];

            Assert.AreEqual(newProperty.Code, propertyInside.Code);
            Assert.AreEqual(newProperty.Module1Value, propertyInside.Module1Value);
        }


        [Test]
        [TestCase(19, Result=100)]
        [TestCase(199, Result = 1000)]
        [TestCase(1999, Result = 10000)]
        public int CreateUniqueID_EdgeCases_IDIntegritySecured(int staticID)
        {
            return Description.CreateUniqueID(staticID);
        }


        [Test]
        [TestCase(0)]
        [TestCase(9)]
        [TestCase(-1)]
        public void CreateUniqueID_StaticIDBelow10_ThrowsException(int staticID)
        {
            Assert.Throws<ArgumentException>(() => Description.CreateUniqueID(staticID));
        }


        /*Helper methods*/
        public IModule1Property MockModule1Property(SignalCode code, double value)
        {
            Mock<IModule1Property> mockedProperty = new Mock<IModule1Property>();
            mockedProperty.SetupGet(x => x.Code).Returns(code);
            mockedProperty.SetupGet(x => x.Module1Value).Returns(value);

            return mockedProperty.Object;
        }

    }
}
