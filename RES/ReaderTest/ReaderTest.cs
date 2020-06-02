using Common;
using Moq;
using NUnit.Framework;
using ReaderNS;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ReaderTest
{
    [TestFixture]
    public class ReaderTest
    {
        private ILogging mockedLogger;
        private IModule2History mockedModul2Proxy;

        [SetUp]
        public void SetUp()
        {
            Mock<ILogging> log = new Mock<ILogging>();
            mockedLogger = log.Object;

            Mock<IModule2History> historyProxy = new Mock<IModule2History>();
            mockedModul2Proxy = historyProxy.Object;
        }

        [Test]
        [TestCase("04/01/1998", "06/01/1999", 2)]
        [TestCase("04-02-1989", "05-08-2000", 3)]
        [TestCase("10/12/2019", "11/02/2020", 0)]
        [TestCase("01-02-2017", "02-02-2018", 7)]
        public void ValidateParameters_GoodParameters_DoesNotThrow(string date1, string date2, int code)
        {
            Reader reader = new Reader(mockedLogger, mockedModul2Proxy);

            Assert.DoesNotThrow(() => reader.ValidateParameters(date1, date2, code));
        }

        [Test]
        [TestCase("", "06/01/1999", 2)]
        [TestCase("04-02-1989", "", 3)]
        [TestCase("", "", 0)]
        [TestCase("01-02-2017", "02-02-2018", -1)]
        [TestCase("01-02-2017", "02-02-2018", 8)]
        [TestCase("", "", -1)]
        [TestCase("s0me r@nd txtkgd", "02-02-2018", 2)]
        [TestCase("01/05/1899", "s0me r@ndome tds", 3)]
        [TestCase("01-05-2020", "02-02-2019", 2)]
        [TestCase("05-01-1999", "05-01-1999", 5)]
        public void ValidateParameters_BadParameters_DoesThrow(string date1, string date2, int code)
        {
            Reader reader = new Reader(mockedLogger, mockedModul2Proxy);

            Assert.Throws<Exception>(() => reader.ValidateParameters(date1, date2, code));
        }


        [Test]
        [TestCase("04-01-1999", "05-02-2020", 2)]
        public void ReadFromModule2_GoodRead_Equals(string data1, string data2, int code)
        {
            Mock<IModule2History> history = new Mock<IModule2History>();
            var lista = new List<IModule2Property>();
            history.Setup(x => x.ReadHistory(DateTime.Parse(data1), DateTime.Parse(data2), (SignalCode)code)).Returns(lista);
            
            Reader reader = new Reader(mockedLogger, history.Object);
            
            string ret = reader.ReadFromModule2(data1, data2, code); //null reference, ne znam zasto?
            int num = Regex.Matches(ret, "\n").Count;
            Assert.AreEqual(num, lista.Count);
        }
        
    }
}
