using Modul1;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1Test.ManagersTest
{
    [TestFixture]
    public class Module1DatabaseManagerTest
    {
        private ILogging mockedLogger;

        [SetUp]
        public void SetUpTests()
        {
            mockedLogger = new Mock<ILogging>().Object;
        }

        [Test]
        public void Constructor_NonExistingDatabase_ThrowsException()
        {
            string databaseName = "some_bogus_db.db";

            Assert.Throws<Exception>(() => new Module1DatabaseManager(mockedLogger, databaseName));
            
        }
        /*We Need to decide how to test the direct database connection without unit tests
        [Test]
        public void WriteNew()
        {
            Module1DatabaseManager manager = new Module1DatabaseManager(mockedLogger, "res_module1.db");
            manager.WriteProperty(new Module1Property(SignalCode.CODE_CONSUMER, 666));
        }

        [Test]
        public void RewriteExisting()
        {
            Module1DatabaseManager manager = new Module1DatabaseManager(mockedLogger, "res_module1.db");
            manager.WriteProperty(new Module1Property(SignalCode.CODE_CONSUMER, 222));
        }*/
    }
}
