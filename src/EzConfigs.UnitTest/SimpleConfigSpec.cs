using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzConfigs
{
    [TestClass]
    public class SimpleConfigSpec
    {
        [TestMethod]
        public void Save_New_Should_Added()
        {
            var simpleConfig = CreateSimpleConfig();
            var theKey = Guid.NewGuid().ToString();
            simpleConfig.AddOrUpdate(theKey, "whatever");
            var theValue = simpleConfig.TryGet(theKey, "abc");
            theValue.ShouldEqual("whatever");
        }

        [TestMethod]
        public void Save_Exist_Should_Updated()
        {
            var simpleConfig = CreateSimpleConfig();
            var theKey = Guid.NewGuid().ToString();
            simpleConfig.AddOrUpdate(theKey, "whatever");
            simpleConfig.AddOrUpdate(theKey, "abc");
            var theValue = simpleConfig.TryGet(theKey, "1");
            theValue.ShouldEqual("abc");
        }


        private ISimpleConfig CreateSimpleConfig()
        {
            return new SimpleConfig();
        }
    }
}
