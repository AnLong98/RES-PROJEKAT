using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test.ModelsTest
{
    [TestFixture]
    public class HistoricalCollectionTest
    {
        [Test]
        [TestCase()]
        public void Constructor_NoArguments_PropertiesListInitialized()
        {
            HistoricalCollection collection = new HistoricalCollection();

            Assert.IsNotNull(collection.Properties);
        }
    }
}
