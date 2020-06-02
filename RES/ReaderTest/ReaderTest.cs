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
        public void ValidateParameters_GoodDateParametersWithoutTime_DoesNotThrow(string beginDate, string endDate, int code)
        {
            Reader reader = new Reader(mockedLogger, mockedModul2Proxy);

            Assert.DoesNotThrow(() => reader.ValidateParameters(beginDate, endDate, code));
        }

        [Test]
        [TestCase("04/01/1998 20:00:00", "06/01/1999 21:35:00", 2)]
        [TestCase("04-02-1989 00:00:00", "05-08-2000 23:59:59", 3)]
        [TestCase("10/12/2019 01:20:00", "11/02/2020 10:00:00", 0)]
        [TestCase("01-02-2017 23:24:59", "02-02-2018 22:21:44", 7)]
        public void ValidateParameters_GoodDateWithGoodTime_DoesNotThrow(string beginDate, string endDate, int code)
        {
            Reader reader = new Reader(mockedLogger, mockedModul2Proxy);

            Assert.DoesNotThrow(() => reader.ValidateParameters(beginDate, endDate, code));
        }

        [Test]
        [TestCase("04/01/1998 25:00:00", "06/01/1999 21:35:00", 2)]
        [TestCase("04-02-1989 00:61:00", "05-08-2000 23:59:59", 3)]
        [TestCase("10/12/2019 01:20:65", "11/02/2020 10:00:00", 0)]
        [TestCase("01-02-2017 23:24:59", "02-02-2018 24:00:00", 7)]
        [TestCase("02-03-2018 22:22:22", "02-02-2019 05:65:00", 4)]
        [TestCase("02-02-2015 23:21:20", "01-05-2019 052:65:00", 4)]
        [TestCase("01-01-2018 18:21:22", "05-06-2019 05-11:00", 4)]
        public void ValidateParameters_GoodDateWithBadTime_DoesThrow(string beginDate, string endDate, int code)
        {
            Reader reader = new Reader(mockedLogger, mockedModul2Proxy);

            Assert.Throws<Exception>(() => reader.ValidateParameters(beginDate, endDate, code));
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
        public void ValidateParameters_BadDateWithoutTime_DoesThrow(string beginDate, string endDate, int code)
        {
            Reader reader = new Reader(mockedLogger, mockedModul2Proxy);

            Assert.Throws<Exception>(() => reader.ValidateParameters(beginDate, endDate, code));
        }
        

        [Test]
        [TestCase("04-01-1999", "05-02-2020", 2)]
        public void ReadFromHistory_GoodReadEmptyList_Equals(string beginDate, string endDate, int code)
        {
            Mock<IModule2History> history = new Mock<IModule2History>();
            var lista = new List<IModule2Property>();
            history.Setup(x => x.ReadHistory(DateTime.Parse(beginDate), DateTime.Parse(endDate), (SignalCode)code)).Returns(lista);
            
            Reader reader = new Reader(mockedLogger, history.Object);
            
            string ret = reader.ReadFromHistory(beginDate, endDate, code);
            int num = Regex.Matches(ret, "\n").Count;
            Assert.AreEqual(num, lista.Count);
        }

        [Test]
        [TestCase("04-01-1999", "05-02-2020", 2)]
        public void ReadFromHistory_GoodReadNotEmptyList_Equals(string beginDate, string endDate, int code)
        {
            Mock<IModule2History> history = new Mock<IModule2History>();

            Mock<IModule2Property> prop1 = new Mock<IModule2Property>();
            Mock<IModule2Property> prop2 = new Mock<IModule2Property>();
            Mock<IModule2Property> prop3 = new Mock<IModule2Property>();

            var lista = new List<IModule2Property>();
            lista.Add(prop1.Object);
            lista.Add(prop2.Object);
            lista.Add(prop3.Object);
            history.Setup(x => x.ReadHistory(DateTime.Parse(beginDate), DateTime.Parse(endDate), (SignalCode)code)).Returns(lista);

            Reader reader = new Reader(mockedLogger, history.Object);

            string ret = reader.ReadFromHistory(beginDate, endDate, code);
            int num = Regex.Matches(ret, "\n").Count;
            Assert.AreEqual(num, lista.Count);
        }

    }
}
