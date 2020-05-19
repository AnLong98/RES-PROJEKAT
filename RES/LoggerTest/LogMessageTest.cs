using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoggerTest
{
    [TestFixture]
    public class LogMessageTest
    {

        [Test]
        [TestCase("Server aktiviran")]
        [TestCase("NeKa__Mnogo!@##_ cudna !@3 poruka")]
        public void GetInfoMessage_NonEmptyMessage_InfoAtTheStart(string message)
        {
            LogMessage log = new LogMessage(message);

            string expectedValue = string.Format("[INFO] {0}", message);
            string receivedValue = log.GetInfoMessage();

            Assert.AreEqual(expectedValue, receivedValue);
        }


        [Test]
        [TestCase("")]
        public void GetInfoMessage_EmptyMessage_InfoAtTheStart(string message)
        {
            LogMessage log = new LogMessage(message);

            string expectedValue = string.Format("[INFO] {0}", message);
            string receivedValue = log.GetInfoMessage();

            Assert.AreEqual(expectedValue, receivedValue);
        }


        [Test]
        [TestCase(null)]
        public void GetInfoMessage_NullMessage_DoesNotThrowException(string message)
        { 
            LogMessage log = new LogMessage(message);
            Assert.DoesNotThrow(() => log.GetInfoMessage());
        }

        [Test]
        [TestCase("Server aktiviran")]
        [TestCase("NeKa__Mnogo!@##_ cudna !@3 poruka")]
        public void GetWarningMessage_NonEmptyMessage_WarningAtTheStart(string message)
        {
            LogMessage log = new LogMessage(message);

            string expectedValue = string.Format("[WARNING] {0}", message);
            string receivedValue = log.GetWarningMessage();

            Assert.AreEqual(expectedValue, receivedValue);
        }


        [Test]
        [TestCase("")]
        public void GetWarningMessage_EmptyMessage_WarningAtTheStart(string message)
        {
            LogMessage log = new LogMessage(message);

            string expectedValue = string.Format("[WARNING] {0}", message);
            string receivedValue = log.GetWarningMessage();

            Assert.AreEqual(expectedValue, receivedValue);
        }


        [Test]
        [TestCase(null)]
        public void GetWarningMessage_NullMessage_DoesNotThrowException(string message)
        {
            LogMessage log = new LogMessage(message);
            Assert.DoesNotThrow(() => log.GetWarningMessage());

        }


        [Test]
        [TestCase(null)]
        public void Constructor_NullMessage_DoesNotThrowException(string message)
        {
            Assert.DoesNotThrow(() => new LogMessage(message));
        }
    }
}
