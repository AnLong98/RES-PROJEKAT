using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InputTest
{
    [TestFixture]
    public class InputTest
    {
        [Test]
        [TestCase(0, 10)]
        [TestCase(2, 319)]
        [TestCase(7, 999)]
        public void sendSignalTest()
        {

        }

    }
}
