using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EazyWizy.Framework.Logging;
using EazyWizy.Framework.Caching;
using EazyWizy.Framework;

namespace LoggingUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            LogWriter.Current.WriteLog(LogSeverityType.Info, "ResponseTime final info ", " (Seconds) : " + DateTime.Now.Second.ToString());

            string xmltext = @"<test>value final gauravtest</test>";
            LogWriter.Current.WriteXml("test",xmltext);
        }

        [TestMethod]
        [TestCategory("CacheManager")]
        public void ResolveDefaultCacheManager()
        {
            ICacheManager cache = FrameworkContainer.Resolve<ICacheManager>();

            object value = cache["Test"];
            Assert.IsNull(value, "Value from cache was not null");

            cache.SetItem("Test", "Value");

            value = cache.GetItem("Test");

            Assert.IsNotNull(value, "Value from cache was null");
            Assert.AreEqual("Value", value, "Value mismatch");
        }

        [TestMethod]
        [TestCategory("CacheManager")]
        public void ContainerShouldResolveSameInstanceOfDefaultCacheManager()
        {
            ICacheManager cache1 = FrameworkContainer.Resolve<ICacheManager>();
            ICacheManager cache2 = FrameworkContainer.Resolve<ICacheManager>();
            Assert.AreEqual(cache1, cache2, "Objects are different");
        }

    }
}
