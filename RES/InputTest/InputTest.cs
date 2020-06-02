using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using InputNS;
using Common;

namespace InputTest
{
    [TestFixture]
    public class InputTest
    {

        private ILogging mockedLogger;
        private IModule2DirectUpdate mockedHistoryProxy;
        private IModule1 mockedModul1Proxy;

        [SetUp]
        public void SetUp()
        {
            Mock<ILogging> log = new Mock<ILogging>();
            mockedLogger = log.Object;

            Mock<IModule2DirectUpdate> historyProxy = new Mock<IModule2DirectUpdate>();
            mockedHistoryProxy = historyProxy.Object;

            Mock<IModule1> modul1Proxy = new Mock<IModule1>();
            mockedModul1Proxy = modul1Proxy.Object;
        }

        [Test]
        [TestCase(0, 10.0)]
        [TestCase(2, 319.0)]
        [TestCase(7, 999.0)]
        public void SendSignal_GoodParameters_DoesNotThrow(int signal, double value)
        {

            Input input = new Input(mockedLogger, mockedHistoryProxy, mockedModul1Proxy);

            Assert.DoesNotThrow(() => input.SendSignal(signal, value));
            
            
        }

        [Test]
        [TestCase(-1, 10.0)]
        [TestCase(10, 231.0)]
        [TestCase(4, -213.0)]
        [TestCase(-2, -2.0)]
        public void SendSignal_BadParameters_DoesThrow(int signal, double value)
        {

            Input input = new Input(mockedLogger, mockedHistoryProxy, mockedModul1Proxy);

            Assert.Throws<Exception>(() => input.SendSignal(signal, value));

        }
        

        [Test]
        public void StartDataFlow_CallingMethod_AreEqual()
        {
            Mock<IModule1> mod1 = new Mock<IModule1>();

            Input input = new Input(mockedLogger, mockedHistoryProxy, mod1.Object);

            input.StartDataFlow();
            Thread.Sleep(9000);
            input.StopDataFlow();

            mod1.Verify(modul1 => modul1.UpdateDataset(It.IsAny<double>(), It.IsAny<SignalCode>()), Times.Exactly(3));

        }
    }
}
