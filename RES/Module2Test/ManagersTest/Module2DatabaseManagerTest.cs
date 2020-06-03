using Common;
using Module2;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Module2Test.ManagersTest
{
    [TestFixture]
    public class Module2DatabaseManagerTest
    {
        private string correctDatabaseName = "res_module2.db";
        private string testDatabaseName = "res_module2_test.db";
        private string wrongDatabaseName = "some_wrong_name.db";
        private string path = ""; //@"C:\Users\Predrag\Source\Repos\RES-PROJEKAT\RES\";
        Mock<ILogging> loggingMock;

        [SetUp]
        public void SetUpTests()
        {
            loggingMock = new Mock<ILogging>();
        }
        /*This code is not supposed to be here, testing database with unit tests is apparently forbidden. Check this again!!*/

        [TearDown]
        public void CleanTestDatabase()
        {
            using (SQLiteConnection databaseConnection = new SQLiteConnection(string.Format(@"Data Source={0}{1};New=False;", path, testDatabaseName)))
            {
                databaseConnection.Open();

                string[] tables = { "res_dataset1", "res_dataset2" , "res_dataset3" , "res_dataset4" };
                foreach(string tablename in tables)
                {
                    string query = "DELETE FROM " + tablename;
                    SQLiteCommand command = new SQLiteCommand(query, databaseConnection);
                    command.ExecuteNonQuery();
                }
            }
            

            
        }

        [Test]
        [TestCase]
        public void Constructor_RegularParameters_AssertDoesntThrowException()
        {
            Assert.DoesNotThrow(() => new Module2DatabaseManager(loggingMock.Object, correctDatabaseName));
        }

        [Test]
        [TestCase]
        public void Constructor_WrongDatabaseParameter_ThrowsException()
        {
            Assert.Throws<Exception>(() => new Module2DatabaseManager(loggingMock.Object, wrongDatabaseName));

        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 200)]
        [TestCase(SignalCode.CODE_DIGITAL, 0.12)]
        [TestCase(SignalCode.CODE_SIGNLENODE, 20.22029)]
        [TestCase(SignalCode.CODE_MULTIPLENODE, -15)]
        public void WriteProperty_TestDatabase_AssertNoExceptions(SignalCode code, double value)
        {
            Mock<IModule2Property> property = new Mock<IModule2Property>();
            property.SetupGet(x => x.Code).Returns(code);
            property.SetupGet(x => x.Value).Returns(value);

            Module2DatabaseManager module2DatabaseManager =  new Module2DatabaseManager(loggingMock.Object, testDatabaseName);

            Assert.DoesNotThrow(() => module2DatabaseManager.WriteProperty(property.Object));

        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 200, 800)]
        [TestCase(SignalCode.CODE_DIGITAL, 0.12, 0.57)]
        public void ReadLastByCode_MultipleEntriesOfTheFirstDatasetSameCode_AssertLastRead(SignalCode code, double firstValue, double secondValue)
        {
            /*Connect to test database to write*/
            using (SQLiteConnection databaseConnection = new SQLiteConnection(string.Format(@"Data Source={0}{1};New=False;", path, testDatabaseName)))
            {
                databaseConnection.Open();
                /*Insert test data*/
                string query = "INSERT INTO res_dataset1(signalCode, signalValue) VALUES(@code, @value)";
                using (SQLiteCommand command = new SQLiteCommand(query, databaseConnection))
                {
                    command.Parameters.AddWithValue("@code", code.ToString());
                    command.Parameters.AddWithValue("@value", firstValue);
                    command.ExecuteNonQuery();
                    Thread.Sleep(2000);
                }
                query = "INSERT INTO res_dataset1(signalCode, signalValue) VALUES(@code, @value)";
                using (SQLiteCommand command = new SQLiteCommand(query, databaseConnection))
                {
                    command.Parameters.AddWithValue("@code", code.ToString());
                    command.Parameters.AddWithValue("@value", secondValue);
                    command.ExecuteNonQuery();
                }
            }

            Module2DatabaseManager module2DatabaseManager = new Module2DatabaseManager(loggingMock.Object, testDatabaseName);
            /*Assert*/
            IModule2Property recievedProperty = module2DatabaseManager.ReadLastByCode(code);

            Assert.AreEqual(code, recievedProperty.Code);
            Assert.AreEqual(secondValue, recievedProperty.Value);


        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 200, 800)]
        [TestCase(SignalCode.CODE_DIGITAL, 0.12, 0.57)]
        public void ReadPropertiesByTimeframe_TwoPropertiesExist_AssertAllRead(SignalCode code, double firstValue, double secondValue)
        {
            DateTime periodStart = DateTime.Now;
            /*Connect to test database to write*/
            using (SQLiteConnection databaseConnection = new SQLiteConnection(string.Format(@"Data Source={0}{1};New=False;", path, testDatabaseName)))
            {
                databaseConnection.Open();
                /*Insert test data*/
                string query = "INSERT INTO res_dataset1(signalCode, signalValue) VALUES(@code, @value)";
                using (SQLiteCommand command = new SQLiteCommand(query, databaseConnection))
                {
                    command.Parameters.AddWithValue("@code", code.ToString());
                    command.Parameters.AddWithValue("@value", firstValue);
                    command.ExecuteNonQuery();
                    Thread.Sleep(2000);
                }
                query = "INSERT INTO res_dataset1(signalCode, signalValue) VALUES(@code, @value)";
                using (SQLiteCommand command = new SQLiteCommand(query, databaseConnection))
                {
                    command.Parameters.AddWithValue("@code", code.ToString());
                    command.Parameters.AddWithValue("@value", secondValue);
                    command.ExecuteNonQuery();
                }
            }

            DateTime periodEnd = DateTime.Now;
            Module2DatabaseManager module2DatabaseManager = new Module2DatabaseManager(loggingMock.Object, testDatabaseName);
            /*Assert*/
            List<IModule2Property> recievedProperty = module2DatabaseManager.ReadPropertiesByTimeframe(periodStart,periodEnd, code) ;

            Assert.AreEqual(recievedProperty.Count, 2);
            Assert.AreEqual(recievedProperty[0].Value, secondValue);
            Assert.AreEqual(recievedProperty[1].Value, firstValue);


        }
    }
}
