using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module2Test.ModelsTest
{
    [TestFixture]
    public class CollectionDescriptionTest
    {

        [Test]
        [TestCase(9)]
        [TestCase(0)]
        [TestCase(-1)]
        public void CreateUniqueId_NumbersBelow10_ExceptionThrown(int staticID)
        {
            Assert.Throws<ArgumentException>(() => CollectionDescription.CreateUniqueId(staticID));

        }


        [Test]
        [TestCase(29, Result=200)]
        [TestCase(299, Result=2000)]
        [TestCase(2999, Result=20000)]
        [TestCase(29999, Result=200000)]
        public int CreateUniqueId_OverflowEdgeCases_IdIntegritySaved(int staticID)
        {
            return  CollectionDescription.CreateUniqueId(staticID);
            
        }


        [Test]
        [TestCase(20, Result = 21)]
        [TestCase(200, Result = 201)]
        [TestCase(2000, Result = 2001)]
        [TestCase(20000, Result = 20001)]
        public int CreateUniqueId_RegularCases_IdCorrect(int staticID)
        {
            return CollectionDescription.CreateUniqueId(staticID);

        }


        [Test]
        [TestCase(2, Result=22)]
        [TestCase(5, Result=25)]
        [TestCase(10, Result=200)]
        public int DefaultConstructor_MultipleObjectCreation_IdCorrect(int numberOfObjects)
        {
            CollectionDescription description = null;
            CollectionDescription.ResetStaticClassID();

            for(int i = 0; i < numberOfObjects; i++)
            {
                description = new CollectionDescription();
            }

            return description.ID;
        }


        [Test]
        [TestCase(2, Result = 22)]
        [TestCase(5, Result = 25)]
        [TestCase(10, Result = 200)]
        public int ParameterConstructor_MultipleObjectCreation_IdCorrect(int numberOfObjects)
        {
            CollectionDescription description = null;
            CollectionDescription.ResetStaticClassID();
            Dataset set = Dataset.SET1;
            var  collection = new Mock<IHistoricalCollection>().Object;

            for (int i = 0; i < numberOfObjects; i++)
            {
                description = new CollectionDescription(set,collection);
            }

            return description.ID;
        }

    }
}
