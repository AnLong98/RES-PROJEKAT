using Common;
using Module2;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test.ManagersTest
{
    [TestFixture]
    public class Module2DatabaseManagerTest
    {
        private string correctDatabaseName = "res_module2.db";
        private string wrongDatabaseName = "some_wrong_name.db";

        [Test]
        [TestCase]
        public void Constructor_RegularParameters_AssertDoesntThrowException()
        {
            Mock<ILogging> loggingMock = new Mock<ILogging>();


            Assert.DoesNotThrow(() => new Module2DatabaseManager(loggingMock.Object, correctDatabaseName));


        }

        [Test]
        [TestCase]
        public void Constructor_WrongDatabaseParameter_ThrowsException()
        {
            Mock<ILogging> loggingMock = new Mock<ILogging>();


            Assert.Throws<Exception>(() => new Module2DatabaseManager(loggingMock.Object, wrongDatabaseName));


        }
    }
}
