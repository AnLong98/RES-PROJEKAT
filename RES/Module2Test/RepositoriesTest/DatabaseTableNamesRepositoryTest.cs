using Module2.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test.RepositoriesTest
{
    [TestFixture]
    public class DatabaseTableNamesRepositoryTest
    {

        [Test]
        [TestCase(Dataset.SET1, Result="DataSet1")]
        [TestCase(Dataset.SET2, Result = "DataSet2")]
        [TestCase(Dataset.SET3, Result = "DataSet3")]
        [TestCase(Dataset.SET4, Result = "DataSet4")]
        public string GetTableNameByDataset_AllDatasets_AssertName(Dataset set)
        {
            return DatabaseTableNamesRepository.GetTableNameByDataset(set);
        }
    }
}
