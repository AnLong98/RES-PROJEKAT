using Common;
using Module2;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test
{
    [TestFixture]
    public class Module2ServiceProviderTest
    {
        private ILogging mockedLogger;
        private Module2ServiceProvider providerFullyMocked;
        private Module2ServiceProvider providerPartialyMocked;
        private Mock<IModule2DataAdapting> mockedAdapter;
        private Mock<IModule2DatabaseManagement> mockedManager;

        [SetUp]
        public void SetUpTest()
        {
            Mock<ILogging> log = new Mock<ILogging>();
            log.Setup(x => x.LogNewInfo(It.IsAny<string>()));
            log.Setup(x => x.LogNewWarning(It.IsAny<string>()));
            mockedLogger = log.Object;
            providerPartialyMocked = new Module2ServiceProvider(mockedLogger);

            mockedAdapter = new Mock<IModule2DataAdapting>();
            mockedManager = new Mock<IModule2DatabaseManagement>();

            providerFullyMocked = new Module2ServiceProvider(mockedLogger, mockedManager.Object, mockedAdapter.Object);
        }

        [Test]
        [TestCase(100, 102.1, 2, Result=true)]
        [TestCase(100, 102.001, 2, Result = true)]
        [TestCase(100, 97.999, 2, Result = true)]
        [TestCase(100, 97.9, 2, Result = true)]
        public bool IsDeadbandSatisfied_SatisfiedEgdeCases_ReturnsTrue(double oldValue, double newValue, double percentageDeadband)
        {
            IModule2Property oldProperty = MockModule2Property(SignalCode.CODE_ANALOG, oldValue);
            IModule2Property newProperty = MockModule2Property(SignalCode.CODE_ANALOG, newValue);

            return providerPartialyMocked.IsDeadbandSatisfied(oldProperty, newProperty, percentageDeadband);


        }


        [Test]
        [TestCase(100, 102.0, 2, Result = false)]
        [TestCase(100, 101.999, 2, Result = false)]
        [TestCase(100, 98.0, 2, Result = false)]
        [TestCase(100, 98.1, 2, Result = false)]
        public bool IsDeadbandSatisfied_NotSatisfiedEgdeCases_ReturnsFalse(double oldValue, double newValue, double percentageDeadband)
        {
            IModule2Property oldProperty = MockModule2Property(SignalCode.CODE_ANALOG, oldValue);
            IModule2Property newProperty = MockModule2Property(SignalCode.CODE_ANALOG, newValue);

            return providerPartialyMocked.IsDeadbandSatisfied(oldProperty, newProperty, percentageDeadband);


        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 200)]
        public void WriteToHistory_MockedAdapterAndManager_AssertNumberOfCalls(SignalCode code, double value)
        {

            providerFullyMocked.WriteToHistory(code, value);

            mockedManager.Verify(x => x.WriteProperty(It.IsAny<IModule2Property>()), Times.Exactly(1));
            mockedAdapter.Verify(x => x.PackToModule2Property(code, value), Times.Exactly(1));

        }


        [Test]
        public void ReadHistory_MockedAdapterAndManager_AssertNumberOfCalls()
        {
            DateTime invocationTime = DateTime.Now;
            Mock<IModule2DatabaseManagement> mockDatabase = new Mock<IModule2DatabaseManagement>();
            mockDatabase.Setup(x => x.ReadPropertiesByTimeframe(invocationTime, invocationTime, SignalCode.CODE_ANALOG)).Returns(new List<IModule2Property>());

            Module2ServiceProvider provider = new Module2ServiceProvider(mockedLogger, mockDatabase.Object, mockedAdapter.Object);
            provider.ReadHistory(invocationTime, invocationTime, SignalCode.CODE_ANALOG);

            mockDatabase.Verify(x => x.ReadPropertiesByTimeframe(invocationTime, invocationTime, SignalCode.CODE_ANALOG), Times.Exactly(1));
        }


        [Test]
        public void UpdateDatabase_MockedAdapterAndManagerNoPropertiesInDatabase_AssertNumberOfCalls()
        {
            IModule2Property propertyReturn = null;
            List<IModule2Property> module2Properties = new List<IModule2Property>();
            List<ICollectionDescription> collections = new List<ICollectionDescription>();
            Mock<IListDescription> mockList = new Mock<IListDescription>();
            Mock<ICollectionDescription> mockCollection = new Mock<ICollectionDescription>();
            Mock<IHistoricalCollection> mockHistorical = new Mock<IHistoricalCollection>();
            Mock<IModule2Property> mockModule2Property = new Mock<IModule2Property>();
            mockModule2Property.SetupGet(x => x.Code).Returns(SignalCode.CODE_ANALOG);
            mockModule2Property.SetupGet(x => x.Value).Returns(100);
            module2Properties.Add(mockModule2Property.Object);
            mockHistorical.SetupGet(x => x.Properties).Returns(module2Properties);
            mockCollection.SetupGet(x => x.Collection).Returns(mockHistorical.Object);
            collections.Add(mockCollection.Object);

            Mock<IModule2DataAdapting> mockAdapter = new Mock<IModule2DataAdapting>();
            mockAdapter.Setup(x => x.RepackToCollectionDescriptionArray(It.IsAny<IListDescription>())).Returns(collections);
            Mock<IModule2DatabaseManagement> mockDatabase = new Mock<IModule2DatabaseManagement>();
            mockDatabase.Setup(x => x.ReadLastByCode(It.IsAny<SignalCode>())).Returns(propertyReturn);

            Module2ServiceProvider provider = new Module2ServiceProvider(mockedLogger, mockDatabase.Object, mockAdapter.Object);
            provider.UpdateDatabase(mockList.Object);

            mockDatabase.Verify(x => x.ReadLastByCode(SignalCode.CODE_ANALOG), Times.Exactly(1));
            mockDatabase.Verify(x => x.WriteProperty(mockModule2Property.Object), Times.Exactly(1));
            mockAdapter.Verify(x => x.RepackToCollectionDescriptionArray(mockList.Object), Times.Exactly(1));
        }



        private IModule2Property MockModule2Property(SignalCode signal, double value)
        {
            Mock<IModule2Property> module2Property = new Mock<IModule2Property>();
            module2Property.SetupGet<SignalCode>(x => x.Code).Returns(signal);
            module2Property.SetupGet<double>(x => x.Value).Returns(value);
            return module2Property.Object;
        }


    }
}
