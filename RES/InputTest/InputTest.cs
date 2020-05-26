using NUnit.Framework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using InputNS;

namespace InputTest
{
    [TestFixture]
    public class InputTest
    {
        [Test]
        [TestCase(0, 10.0)]
        [TestCase(2, 319.0)]
        [TestCase(7, 999.0)]
        public void sendSignal_GoodParameters_DoesNotThrow(int signal, double value)
        {
            Mock<ILogging> log = new Mock<ILogging>();

            Mock<IModule2DirectUpdate> histprox = new Mock<IModule2DirectUpdate>();
            Mock<IModule1> modul1 = new Mock<IModule1>();

            Input input = new Input(log.Object, histprox.Object, modul1.Object);

            Assert.DoesNotThrow(() => input.sendSignal(signal, value));
            
            
        }

        [Test]
        [TestCase(-1, 10.0)]
        [TestCase(10, 231.0)]
        [TestCase(4, -213.0)]
        [TestCase(-2, -2.0)]
        public void sendSignal_BadParameters_DoesThrow(int signal, double value)
        {
            Mock<ILogging> log = new Mock<ILogging>();

            Mock<IModule2DirectUpdate> histprox = new Mock<IModule2DirectUpdate>();
            Mock<IModule1> modul1 = new Mock<IModule1>();

            Input input = new Input(log.Object, histprox.Object, modul1.Object);

            Assert.Throws<Exception>(() => input.sendSignal(signal, value));

        }
        
        //THIS IS THE METHOD I NEED HELP WITH
        [Test]
        public void StartDataFlow_CallingMethod_AreEqual()
        {
            Mock<ILogging> log = new Mock<ILogging>();

            Mock<IModule2DirectUpdate> histprox = new Mock<IModule2DirectUpdate>();
            Mock<IModule1> modul1Mock = new Mock<IModule1>();

            Input input = new Input(log.Object, histprox.Object, modul1Mock.Object);

            input.StartDataFlow();
            Thread.Sleep(9000);
            input.StopDataFlow();

            modul1Mock.Verify(modul1 => modul1.UpdateDataset(Is.))
            //Razmisljam kako ovde uporediti koliko puta se pozvala.
            //Krenuo sam sa onim modul1Mock.Setup(modul1 => modul1.UpdateDataSet()).Verifiable();
            // ovde sam zapeo kako proslediti parametre u UPDATEDATASET i nmg se raskontati.

        }
        */
    }
}
