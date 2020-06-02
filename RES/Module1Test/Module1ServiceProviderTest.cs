using Common;
using Modul1;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1Test
{
    [TestFixture]
    public class Module1ServiceProviderTest
    {
        private Mock<ILogging> logger;
        private Mock<IModule2Update> module2Proxy;
        private Mock<IModule1DataAdapting> dataAdapter;
        private Mock<IModule1DatabaseManagement> databaseManager;
        private Mock<IListDescription> listDescription;

        [SetUp]
        [TestCase]
        public void SetUpTest()
        {
            logger = new Mock<ILogging>();
            module2Proxy = new Mock<IModule2Update>();
            dataAdapter = new Mock<IModule1DataAdapting>();
            databaseManager = new Mock<IModule1DatabaseManagement>();
            listDescription = new Mock<IListDescription>();
        }


        [Test]
        [TestCase(SignalCode.CODE_ANALOG, 100)]
        public void UpdateDataset_CorrectParameters_VerifyFunctionCalls(SignalCode code, double value)
        {
            listDescription.Setup(x => x.IsDatasetFull(It.IsAny<Dataset>())).Returns(true);
            IModule1 module = new Module1ServiceProvider(logger.Object, module2Proxy.Object, dataAdapter.Object, listDescription.Object, databaseManager.Object);
            module.UpdateDataset(value, code);

            dataAdapter.Verify(x => x.PackToModule1Property(code, value), Times.Once);
            logger.Verify(x => x.LogNewInfo(It.IsAny<string>()), Times.Exactly(3));
            listDescription.Verify(x => x.AddOrReplaceProperty(It.IsAny<IModule1Property>()), Times.Once);
            module2Proxy.Verify(x => x.UpdateDatabase(It.IsAny<IListDescription>()), Times.Once);
            databaseManager.Verify(x => x.WriteProperty(It.IsAny<IModule1Property>()), Times.Once);
        }

    }
}
