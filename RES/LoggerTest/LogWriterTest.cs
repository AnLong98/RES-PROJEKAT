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
    public class LogWriterTest
    {


        [Test]
        [TestCase("someFilename")]
        public void Constructor_NonEmptyFileName_ObjectCreated(string filename)
        {
            LogWriter writer = new LogWriter(filename);
            Assert.AreNotEqual(writer, null);
        }


        [Test]
        [TestCase("")]
        public void Constructor_EmptyFileName_ObjectCreated(string filename)
        {
            LogWriter writer = new LogWriter(filename);
            Assert.AreNotEqual(writer, null);
        }


        [Test]
        [TestCase(null)]
        public void Constructor_NullFileName_DoesNotThrowException(string filename)
        {
            Assert.DoesNotThrow(() => new LogWriter(filename));
        }


        [Test]
        [TestCase("someNonExistingFileName")]
        public void WriteToFile_NonExistingFile_FileCreated(string filename)
        {
            LogWriter writer = new LogWriter(filename);
            writer.WriteToFile("test");

            
            Assert.True(File.Exists(filename));
            File.Delete(filename);
        }


        [Test]
        [TestCase("testFile")]
        public void WriteToFile_ExistingFile_FileNotOverwritten(string filename)
        {
            StreamWriter sw = File.CreateText("testFile");
            sw.WriteLine("some text here.");
            sw.Close();
            int initialLength = File.ReadAllText("testFile").Length;

            LogWriter writer = new LogWriter(filename);
            writer.WriteToFile("test");
            int finalLength = File.ReadAllText(filename).Length;

            File.Delete(filename);
            Assert.True(initialLength < finalLength);
            
        }


        [Test]
        [TestCase("testFile", "Message 1", "Message 2")]
        [TestCase("testFile", "Som3 rea=-=lly специфик MESEDŽ", "ИВЕН МОР SP3CIFIK MEŠIDŽ")]
        public void WriteToFile_MultipleMessages_AllMessagesWritten(string filename, string firstMessage, string secondMessage)
        {

            LogWriter writer = new LogWriter(filename);
            writer.WriteToFile(firstMessage);
            writer.WriteToFile(secondMessage);

            string[] messages = File.ReadAllLines(filename);

            File.Delete(filename);
            Assert.AreEqual(messages[0], firstMessage);
            Assert.AreEqual(messages[1], secondMessage);
            
        }
    }
}
