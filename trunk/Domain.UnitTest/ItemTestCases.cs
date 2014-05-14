using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Domain.Managers;

namespace Domain.UnitTest
{
    [TestClass]
    public class ItemTestCases
    {
        [TestMethod]
        public void GetCategories()
        {
            var result = ItemManager.GetCategories(0);
            if (result.Count <= 0)
                Assert.Fail();
        }
    }
}
