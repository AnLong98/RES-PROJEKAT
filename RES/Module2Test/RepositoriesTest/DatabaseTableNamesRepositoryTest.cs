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
        [TestCase(Dataset.SET1, Result= "res_dataset1")]
        [TestCase(Dataset.SET2, Result = "res_dataset2")]
        [TestCase(Dataset.SET3, Result = "res_dataset3")]
        [TestCase(Dataset.SET4, Result = "res_dataset4")]
        public string GetTableNameByDataset_AllDatasets_AssertName(Dataset set)
        {
            return DatabaseTableNamesRepository.GetTableNameByDataset(set);
        }
    }
}
