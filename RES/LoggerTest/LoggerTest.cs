using LoggerSpace;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerTest
{
    [TestFixture]
    public class LoggerTest
    {
        [Test]
        [TestCase(null)]
        [TestCase("")]
        public void Constructor_NullOrEmptyParameter_ObjectCreated(string filename)
        {
            Logger log = new Logger(filename);
            Assert.IsNotNull(log);
            Assert.IsInstanceOf(typeof(Logger), log);
            
        }


        [Test]
        [TestCase("Somefile", "Some message")]
        [TestCase("Somefile", "Some message\r\nnewlinemessage")]
        public void LogNewInfo_NonEmptyMessage_MessageLogged(string filename, string message)
        {
            Logger loger = new Logger(filename);
            loger.LogNewInfo(message);

            string log = File.ReadAllText(filename);
            string expectedValue = string.Format("[INFO] {0}\r\n", message);
            string receivedValue = log;

            File.Delete(filename);
            Assert.AreEqual(expectedValue, receivedValue);
            

        }


        [Test]
        [TestCase("Somefile", "Some message")]
        [TestCase("Somefile", "Some message\r\nnewlinemessage")]
        public void LogNewWarning_NonEmptyMessage_MessageLogged(string filename, string message)
        {
            Logger loger = new Logger(filename);
            loger.LogNewWarning(message);

            string log = File.ReadAllText(filename);
            string expectedValue = string.Format("[WARNING] {0}\r\n", message);
            string receivedValue = log;

            File.Delete(filename);
            Assert.AreEqual(expectedValue, receivedValue);


        }

    }
}
