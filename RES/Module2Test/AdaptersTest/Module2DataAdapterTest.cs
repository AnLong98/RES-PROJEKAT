using Common;
using Module2;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test.AdaptersTest
{
    [TestFixture]
    public class Module2DataAdapterTest
    {
        private ILogging logger;
        private Module2DataAdapter adapter;

        [SetUp]
        public void SetUpTests()
        {
            logger = new Mock<ILogging>().Object;
            adapter = new Module2DataAdapter(logger);
        }

        [TearDown]
        public void TearDownTests()
        {
            logger = null;
            adapter = null;
        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 20.5)]
        [TestCase(SignalCode.CODE_CONSUMER, 25.5)]
        public void PackToModule2Property_RegularParameters_AssertObjectContent(SignalCode signal, double value)
        {
            Module2Property module2Property = adapter.PackToModule2Property(signal, value);

            Assert.AreEqual(signal, module2Property.Code);
            Assert.AreEqual(value, module2Property.Value);
        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 20.5)]
        [TestCase(SignalCode.CODE_CONSUMER, 25.5)]
        public void RepackToModule2Property_RegularParameters_AssertObjectContent(SignalCode signal, double value)
        {
            IModule1Property property = MockModule1Property(signal, value);

            Module2Property module2Property = adapter.RepackToModule2Property(property);

            Assert.AreEqual(signal, module2Property.Code);
            Assert.AreEqual(value, module2Property.Value);
        }


        [Test]
        [TestCase(11, Dataset.SET1, SignalCode.CODE_ANALOG, 20.34, SignalCode.CODE_DIGITAL, 300.22)]
        [TestCase(120, Dataset.SET2, SignalCode.CODE_CUSTOM, 253.34, SignalCode.CODE_LIMITSET, 100.18)]
        [TestCase(125, Dataset.SET3, SignalCode.CODE_SIGNLENODE, 20, SignalCode.CODE_MULTIPLENODE, 50)]
        [TestCase(1009, Dataset.SET4, SignalCode.CODE_CONSUMER, 100, SignalCode.CODE_SOURCE, 200)]
        public void RepackToCollectionDescription_RegularParameters_AssertObjectContent
            (int id, Dataset dataset, SignalCode code1, double value1, SignalCode code2, double value2)
        {
            List<IModule1Property> properties = new List<IModule1Property>();
            properties.Add(MockModule1Property(code1, value1));
            properties.Add(MockModule1Property(code2, value2));
            IDescription description = MockDescription(id, dataset, properties);

            CollectionDescription collectionDescription = adapter.RepackToCollectionDescription(description);

            Assert.AreEqual(dataset, collectionDescription.Dataset);
            Assert.AreEqual(id, collectionDescription.ID);

            List<IModule2Property> historicalData = collectionDescription.Collection.Properties;
            Assert.AreEqual(code1, historicalData[0].Code);
            Assert.AreEqual(code2, historicalData[1].Code);
            Assert.AreEqual(value1, historicalData[0].Value);
            Assert.AreEqual(value2, historicalData[1].Value);
        }


        [Test]
        [TestCase(11, Dataset.SET1, SignalCode.CODE_ANALOG, 20.34, SignalCode.CODE_LIMITSET, 300.22)]
        [TestCase(120, Dataset.SET2, SignalCode.CODE_MULTIPLENODE, 253.34, SignalCode.CODE_LIMITSET, 100.18)]
        [TestCase(125, Dataset.SET3, SignalCode.CODE_SIGNLENODE, 20, SignalCode.CODE_DIGITAL, 50)]
        [TestCase(1009, Dataset.SET4, SignalCode.CODE_SOURCE, 100, SignalCode.CODE_CUSTOM, 200)]
        public void RepackToCollectionDescription_WrongDatasetForCode_ThrowsException
            (int id, Dataset dataset, SignalCode code1, double value1, SignalCode code2, double value2)
        {
            List<IModule1Property> properties = new List<IModule1Property>();
            properties.Add(MockModule1Property(code1, value1));
            properties.Add(MockModule1Property(code2, value2));
            IDescription description = MockDescription(id, dataset, properties);

            Assert.Throws<ArgumentException>(() => adapter.RepackToCollectionDescription(description));

        }


        [Test]
        [TestCase(0)]
        [TestCase(5)]
        public void RepackToCollectionDescriptionArray_RegularParameters_AssertListLength(int listLength)
        {
            IListDescription listDescription = MockRegularListDescription(listLength);

            Assert.AreEqual(listLength, listDescription.Descriptions.Count);

        }




        /*Helper methods*/
        private IModule1Property MockModule1Property(SignalCode signal, double value)
        {
            Mock<IModule1Property> module1Property = new Mock<IModule1Property>();
            module1Property.SetupGet<SignalCode>(x => x.Code).Returns(signal);
            module1Property.SetupGet<double>(x => x.Module1Value).Returns(value);
            return module1Property.Object;
        }


        private IDescription MockDescription(int id, Dataset dataset, List<IModule1Property> properties)
        {
            Mock<IDescription> mockDescription = new Mock<IDescription>();
            mockDescription.SetupGet(x => x.Dataset).Returns(dataset);
            mockDescription.SetupGet(x => x.ID).Returns(id);
            mockDescription.Setup(x => x.Properties()).Returns(properties);

            return mockDescription.Object;
        }


        //Mocks a ListDescription of given length with copies of the same description, list description is made with valid values
        private IListDescription MockRegularListDescription(int listLength)
        {
            Mock<IListDescription> mockList = new Mock<IListDescription>();
            List<IDescription> descriptions = new List<IDescription>();
            

            for (int i = 0; i < listLength; i++)
            {
                List<IModule1Property> properties = new List<IModule1Property>();
                properties.Add(MockModule1Property(SignalCode.CODE_ANALOG, 200));
                properties.Add(MockModule1Property(SignalCode.CODE_DIGITAL, 100));
                IDescription description = MockDescription(10, Dataset.SET1, properties);
                descriptions.Add(description);
            }

            mockList.SetupGet(x => x.Descriptions).Returns(descriptions);
            return mockList.Object;
        }
    }
}
