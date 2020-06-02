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
    public class ListDescriptionTest
    {
        ILogging mockedLogger;

        [SetUp]
        public void SetUpTests()
        {
            mockedLogger = new Mock<ILogging>().Object;
        }

        [Test]
        public void IsDatasetFull_FullDataset_ReturnsTrue()
        {
            IDescription description = MockFullDataset1();
            List<IDescription> descriptions = new List<IDescription>
            {
                description
            };

            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);

            Assert.IsTrue(listDescription.IsDatasetFull(Dataset.SET1));

        }


        [Test]
        public void IsDatasetFull_NotFullDataset_ReturnsFalse()
        {
            IDescription description = MockEmptyDataset1();
            List<IDescription> descriptions = new List<IDescription>
            {
                description
            };

            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);

            Assert.IsFalse(listDescription.IsDatasetFull(Dataset.SET1));

        }


        [Test]
        public void DoesDescriptionExist_ExistingDescription_ReturnsTrue()
        {
            IDescription description = MockFullDataset1();
            List<IDescription> descriptions = new List<IDescription>
            {
                description
            };

            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);

            Assert.IsTrue(listDescription.DoesDescriptionExist(Dataset.SET1));

        }


        [Test]
        public void DoesDescriptionExist_NonExistingDescription_ReturnsFalse()
        {
            List<IDescription> descriptions = new List<IDescription>();

            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);

            Assert.IsFalse(listDescription.DoesDescriptionExist(Dataset.SET1));

        }


        [Test]
        public void GetDescriptionByDataset_ExistingDescription_AssertContent()
        {
            IDescription description = MockFullDataset1();
            List<IDescription> descriptions = new List<IDescription>
            {
                description
            };

            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);
            Dataset dataset = Dataset.SET1;

            Assert.AreEqual(listDescription.GetDescriptionByDataset(dataset).Dataset,  dataset);

        }


        [Test]
        public void GetDescriptionByDataset_NonExistingDescription_ReturnsNull()
        {
            IDescription description = MockFullDataset1();
            List<IDescription> descriptions = new List<IDescription>
            {
                description
            };

            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);
            Dataset dataset = Dataset.SET2;

            Assert.IsNull(listDescription.GetDescriptionByDataset(dataset));

        }


        [Test]
        public void AddOrReplaceDescription_NonExistingDescription_DescriptionAdded()
        {
            IDescription description = MockEmptyDataset1();
            List<IDescription> descriptions = new List<IDescription>();
            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);

            Assert.IsEmpty(listDescription.Descriptions);

            listDescription.AddOrReplaceDescription(description);

            Assert.AreEqual(listDescription.Descriptions.Count, 1);

        }


        [Test]
        public void AddOrReplaceDescription_ExistingDescription_DescriptionReplaced()
        {
            IDescription description = MockFullDataset1();
            List<IDescription> descriptions = new List<IDescription>
            {
                description
            };

            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);

            Assert.AreEqual(listDescription.Descriptions[0].Properties.Count, 2);


            IDescription descriptionNew = MockEmptyDataset1();

            listDescription.AddOrReplaceDescription(descriptionNew);

            Assert.AreEqual(listDescription.Descriptions[0].Properties.Count, 0);

        }

        [Test]
        public void AddOrReplaceProperty_ExistingDescription_VerifyAddPropertyCalled()
        {
            Mock<IDescription> description = new Mock<IDescription>();
            description.SetupGet(x => x.Dataset).Returns(Dataset.SET1);
            List<IDescription> descriptions = new List<IDescription>
            {
                description.Object
            };

            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);

            IModule1Property newProperty = MockModule1Property(SignalCode.CODE_ANALOG, 120);
            listDescription.AddOrReplaceProperty(newProperty);

            description.Verify(x => x.AddOrReplaceProperty(newProperty), Times.Exactly(1));

        }


        [Test]
        public void AddOrReplaceProperty_NonExistingDescription_PropertyAdded()
        {
            List<IDescription> descriptions = new List<IDescription>();


            IListDescription listDescription = new ListDescription(mockedLogger, descriptions);

            Assert.AreEqual(listDescription.Descriptions.Count, 0);

            IModule1Property newProperty = MockModule1Property(SignalCode.CODE_ANALOG, 120);
            listDescription.AddOrReplaceProperty(newProperty);

            Assert.AreEqual(listDescription.Descriptions.Count, 1);
            Assert.AreEqual(listDescription.Descriptions[0].Dataset, Dataset.SET1);
            Assert.AreEqual(listDescription.Descriptions[0].Properties.Count, 1);
            Assert.AreEqual(listDescription.Descriptions[0].Properties[0].Code, SignalCode.CODE_ANALOG);
            Assert.AreEqual(listDescription.Descriptions[0].Properties[0].Module1Value, 120);

        }




        /*Helper methods*/
        public IModule1Property MockModule1Property(SignalCode code, double value)
        {
            Mock<IModule1Property> mockedProperty = new Mock<IModule1Property>();
            mockedProperty.SetupGet(x => x.Code).Returns(code);
            mockedProperty.SetupGet(x => x.Module1Value).Returns(value);

            return mockedProperty.Object;
        }

        public IDescription MockFullDataset1()
        {
            Mock<IDescription> mockedDescription = new Mock<IDescription>();

            Mock<List<IModule1Property>> mockedProperties = new Mock<List<IModule1Property>>();
            List<IModule1Property> properties = mockedProperties.Object;

            properties.Add(MockModule1Property(SignalCode.CODE_ANALOG, 100));
            properties.Add(MockModule1Property(SignalCode.CODE_DIGITAL, 200));

            mockedDescription.SetupGet(x => x.Properties).Returns(properties);
            mockedDescription.SetupGet(x => x.Dataset).Returns(Dataset.SET1);

            return mockedDescription.Object;

        }

        public IDescription MockEmptyDataset1()
        {
            Mock<IDescription> mockedDescription = new Mock<IDescription>();

            Mock<List<IModule1Property>> mockedProperties = new Mock<List<IModule1Property>>();
            List<IModule1Property> properties = mockedProperties.Object;

            mockedDescription.SetupGet(x => x.Properties).Returns(properties);
            mockedDescription.SetupGet(x => x.Dataset).Returns(Dataset.SET1);

            return mockedDescription.Object;

        }
    }
}
