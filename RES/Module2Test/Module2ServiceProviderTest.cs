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
        
        private Module2ServiceProvider providerPartialyMocked;

        [SetUp]
        public void SetUpTest()
        {
            Mock<ILogging> log = new Mock<ILogging>();
            log.Setup(x => x.LogNewInfo(It.IsAny<string>()));
            log.Setup(x => x.LogNewWarning(It.IsAny<string>()));
            mockedLogger = log.Object;
            providerPartialyMocked = new Module2ServiceProvider(mockedLogger);
        }

        [Test]
        [TestCase(100, 102.1, 2, Result=true)]
        [TestCase(100, 102.001, 2, Result = true)]
        [TestCase(100, 97.999, 2, Result = true)]
        [TestCase(100, 97.9, 2, Result = true)]
        public bool IsDeadbandSatisfied_SatisfiedEgdeCases_ReturnsTrue(double oldValue, double newValue, double percentageDeadband)
        {
            Module2Property oldProperty = new Module2Property(SignalCode.CODE_ANALOG, oldValue);
            Module2Property newProperty = new Module2Property(SignalCode.CODE_ANALOG, newValue);

            return providerPartialyMocked.IsDeadbandSatisfied(oldProperty, newProperty, percentageDeadband);


        }


        [Test]
        [TestCase(100, 102.0, 2, Result = false)]
        [TestCase(100, 101.999, 2, Result = false)]
        [TestCase(100, 98.0, 2, Result = false)]
        [TestCase(100, 98.1, 2, Result = false)]
        public bool IsDeadbandSatisfied_NotSatisfiedEgdeCases_ReturnsFalse(double oldValue, double newValue, double percentageDeadband)
        {
            Module2Property oldProperty = new Module2Property(SignalCode.CODE_ANALOG, oldValue);
            Module2Property newProperty = new Module2Property(SignalCode.CODE_ANALOG, newValue);

            return providerPartialyMocked.IsDeadbandSatisfied(oldProperty, newProperty, percentageDeadband);


        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 200)]
        public void WriteToHistory_MockedAdapterAndManager_AssertNumberOfCalls(SignalCode code, double value)
        {
            Mock<IModule2DataAdapting> mockedAdapter = new Mock<IModule2DataAdapting>();
            Mock<IModule2DatabaseManagement> mockedManager = new Mock<IModule2DatabaseManagement>();

            Module2ServiceProvider providerFullyMocked = new Module2ServiceProvider(mockedLogger,mockedManager.Object, mockedAdapter.Object);

            providerFullyMocked.WriteToHistory(code, value);

            mockedManager.Verify(x => x.WriteProperty(It.IsAny<IModule2Property>(), It.IsAny<DateTime>()), Times.Exactly(1));
            mockedAdapter.Verify(x => x.PackToModule2Property(code, value), Times.Exactly(1));

        }

    }
}
