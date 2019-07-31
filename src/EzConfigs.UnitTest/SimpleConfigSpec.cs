using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzConfigs
{
    [TestClass]
    public class SimpleConfigSpec
    {
        [TestMethod]
        public void AddOrUpdate_NotExist_Should_Added()
        {
            var simpleConfig = CreateSimpleConfig();
            var theKey = Guid.NewGuid().ToString();
            simpleConfig.AddOrUpdate(theKey, "whatever");
            var theValue = simpleConfig.TryGet(theKey, "abc");
            theValue.ShouldEqual("whatever");
        }

        [TestMethod]
        public void AddOrUpdate_Exist_Should_Updated()
        {
            var simpleConfig = CreateSimpleConfig();
            var theKey = Guid.NewGuid().ToString();
            simpleConfig.AddOrUpdate(theKey, "whatever");
            simpleConfig.AddOrUpdate(theKey, "abc");
            var theValue = simpleConfig.TryGet(theKey, "1");
            theValue.ShouldEqual("abc");
        }
        
        [TestMethod]
        public void TryGet_NotExist_Should_Return_Default()
        {
            var simpleConfig = CreateSimpleConfig();
            var theKey = Guid.NewGuid().ToString();
            var tryGet = simpleConfig.TryGet(theKey, "whatever");
            tryGet.ShouldEqual("whatever");
        }

        [TestMethod]
        public void TryGet_Exist_Should_Return_It()
        {
            var simpleConfig = CreateSimpleConfig();
            var theKey = Guid.NewGuid().ToString();
            simpleConfig.AddOrUpdate(theKey, "abc");
            var tryGet = simpleConfig.TryGet(theKey, "whatever");
            tryGet.ShouldEqual("abc");
        }

        [TestMethod]
        public void CustomConfig_GetSet_Should_Ok()
        {
            var customConfig = new MockConfig();
            customConfig.Foo = "123";
            customConfig.AddOrUpdate("Key1", "1");
            customConfig.TryGet("key1", "").ShouldEqual("1");
            customConfig.Foo.ShouldEqual("123");
        }


        private ISimpleConfig CreateSimpleConfig()
        {
            return new SimpleConfig();
        }
    }

    public class MockConfig : SimpleConfig
    {
        public string Foo { get; set; }
    }
}
