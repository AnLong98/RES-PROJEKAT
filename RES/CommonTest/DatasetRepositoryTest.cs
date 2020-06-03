using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonTest
{
    [TestFixture]
    public class DatasetRepositoryTest
    {
        [Test]
        [TestCase(SignalCode.CODE_ANALOG, Result=Dataset.SET1)]
        [TestCase(SignalCode.CODE_DIGITAL, Result=Dataset.SET1)]
        [TestCase(SignalCode.CODE_CUSTOM, Result=Dataset.SET2)]
        [TestCase(SignalCode.CODE_LIMITSET, Result=Dataset.SET2)]
        [TestCase(SignalCode.CODE_SIGNLENODE, Result=Dataset.SET3)]
        [TestCase(SignalCode.CODE_MULTIPLENODE, Result=Dataset.SET3)]
        [TestCase(SignalCode.CODE_SOURCE, Result = Dataset.SET4)]
        [TestCase(SignalCode.CODE_CONSUMER, Result = Dataset.SET4)]
        public Dataset GetDataset_AllSignals_AssertReturnedDataset(SignalCode signalCode)
        {
            return DatasetRepository.GetDataset(signalCode);
        }
    }
}
