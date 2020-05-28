using Module1.Repositories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module1Test.RepositoriesTest
{
    [TestFixture]
    public class DatabaseTableNamesRepositoryTest
    {

        [Test]
        [TestCase(Dataset.SET1, Result ="res_module1_dataset1")]
        [TestCase(Dataset.SET2, Result = "res_module1_dataset2")]
        [TestCase(Dataset.SET3, Result = "res_module1_dataset3")]
        [TestCase(Dataset.SET4, Result = "res_module1_dataset4")]
        public string GetTableNameByDataset_AllDatasets_AssertTableName(Dataset set)
        {
            return DatabaseTableNamesRepository.GetTableNameByDataset(set);
        }

    }
}
