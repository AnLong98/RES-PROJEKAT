using Common;
using Modul1;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1Test.AdaptersTest
{
    [TestFixture]
    public class Module1DataAdapterTest
    {
        private ILogging mockedLogger;

        [SetUp]
        public void SetUpTests()
        {
            mockedLogger = new Mock<ILogging>().Object;
        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 200.32)]
        [TestCase(SignalCode.CODE_DIGITAL, 13)]
        [TestCase(SignalCode.CODE_SIGNLENODE, 10)]
        public void PackToModule1Property_RegularParameters_AssertObjectContent(SignalCode code, double value)
        {
            Module1DataAdapter dataAdapter = new Module1DataAdapter(mockedLogger);

            IModule1Property module1Property = dataAdapter.PackToModule1Property(code, value);
            Assert.AreEqual(module1Property.Code, code);
            Assert.AreEqual(module1Property.Module1Value, value);
        }
    }
}
