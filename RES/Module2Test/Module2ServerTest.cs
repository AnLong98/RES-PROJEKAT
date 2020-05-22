using Common;
using Module2;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test
{
    [TestFixture]
    public class Module2ServerTest
    {

        [Test]
        public void Start_ServerWithAllActiveEndpoints_DoesntThrowExceptions()
        {
            ILogging logger = new Mock<ILogging>().Object;

            Module2Server server = new Module2Server(logger);

            Assert.DoesNotThrow(() => server.Start());
        }


        [Test]
        public void Stop_StartedServerWithAllActiveEndpoints_DoesntThrowExceptions()
        {
            ILogging logger = new Mock<ILogging>().Object;

            Module2Server server = new Module2Server(logger);
            server.Start();

            Assert.DoesNotThrow(() => server.Stop());
        }
    }
}
