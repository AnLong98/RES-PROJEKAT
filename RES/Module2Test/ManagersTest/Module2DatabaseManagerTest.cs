using Common;
using Module2;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test.ManagersTest
{
    [TestFixture]
    public class Module2DatabaseManagerTest
    {
        private string correctDatabaseName = "res_module2.db";
        private string testDatabaseName = "res_module2_test.db";
        private string wrongDatabaseName = "some_wrong_name.db";

        [SetUp]
        public void CreateTestDatabase()
        {
            SQLiteConnection.CreateFile(testDatabaseName);
        }

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


        /*[Test]
        [TestCase(SignalCode.CODE_ANALOG, 200)]
        [TestCase(SignalCode.CODE_DIGITAL, 300)]
        [TestCase(SignalCode.CODE_SIGNLENODE, 400)]
        [TestCase(SignalCode.CODE_MULTIPLENODE, 500)]
        public void ReadLastByCode_SingleEntry_ReturnsLastValue(SignalCode lastCode, double lastValue)
        {
            Mock<ILogging> loggingMock = new Mock<ILogging>();

            Module2DatabaseManager module2DatabaseManager =  new Module2DatabaseManager(loggingMock.Object, testDatabaseName);

            IModule2Property property =  module2DatabaseManager.ReadLastByCode(lastCode);

        }*/
    }
}
