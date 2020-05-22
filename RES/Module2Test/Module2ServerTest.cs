using Common;
using Module2;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test
{
    [TestFixture]
    public class Module2ServerTest
    {

        [Test]
        public void Start_ServerWithAllActiveEndpoints_LoggerCalledWithSuccessMessage()
        {
            Mock<ILogging> logMock = new Mock<ILogging>();

            Module2Server server = new Module2Server(logMock.Object);
            server.Start();

            logMock.Verify(x => x.LogNewInfo("Server started"), Times.Exactly(1));
            server.Stop();//Stop server to prevent further tests crashing
        }


        [Test]
        public void Stop_StartedServerWithAllActiveEndpoints_LoggerCalledWithSuccessMessage()
        {
            Mock<ILogging> logMock = new Mock<ILogging>();

            Module2Server server = new Module2Server(logMock.Object);
            server.Start();
            server.Stop();

            logMock.Verify(x => x.LogNewInfo("Server stopped"), Times.Exactly(1));
        }


        [Test]
        [TestCase("net.tcp://localhost:10100/Module2DirectUpdate", "net.tcp://localhost:10100/Module2Update", "net.tcp://localhost:10100/Module2History")]
        public void Ping_StartedServerWithAllActiveEndpoints_AssertAllConnectionsAlive(string directUpdateEndpoint, string updateEndpoint, string historyEndpoint)
        {
            ILogging logger = new Mock<ILogging>().Object;
            IModule2DirectUpdate directProxy = null;
            IModule2History historyProxy = null;
            IModule2Update updateProxy = null;

            Module2Server server = new Module2Server(logger);
            server.Start();
            
            ChannelFactory<IModule2History> historyChannelFactory = new ChannelFactory<IModule2History>(new NetTcpBinding());
            ChannelFactory<IModule2Update> updateFactory = new ChannelFactory<IModule2Update>(new NetTcpBinding());
            ChannelFactory<IModule2DirectUpdate> directUpdateFactory = new ChannelFactory<IModule2DirectUpdate>(new NetTcpBinding());

            Assert.DoesNotThrow(() => historyProxy = historyChannelFactory.CreateChannel(new EndpointAddress(historyEndpoint)));
            Assert.DoesNotThrow(() => updateProxy =  updateFactory.CreateChannel(new EndpointAddress(updateEndpoint)));
            Assert.DoesNotThrow(() => directProxy = directUpdateFactory.CreateChannel(new EndpointAddress(directUpdateEndpoint)));

            Assert.IsTrue(historyProxy.Ping());
            Assert.IsTrue(directProxy.Ping());
            Assert.IsTrue(updateProxy.Ping());

            server.Stop();
        }







    }
}
