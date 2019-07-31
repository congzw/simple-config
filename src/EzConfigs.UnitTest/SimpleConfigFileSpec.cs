﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EzConfigs
{
    [TestClass]
    public class SimpleConfigFileSpec
    {
        [TestMethod]
        public async Task ReadFile_NotExist_Should_Return_Default()
        {
            var simpleConfig = CreateSimpleConfigFile();
            var defaultValue = new SimpleConfig();
            var config = await simpleConfig.ReadFile("not_exist.json", defaultValue);
            config.ShouldSame(defaultValue);
        }

        [TestMethod]
        public async Task ReadFile_Exist_Should_Return_It()
        {
            var simpleConfig = CreateSimpleConfigFile();
            var defaultValue = new SimpleConfig();
            var config = await simpleConfig.ReadFile("exist.json", defaultValue);
            config.ShouldNotSame(defaultValue);
            config.TryGet("key1", 0).ShouldEqual(1);
            config.TryGet("key2", "").ShouldEqual("2");
            config.TryGet("key3", new DateTime(1999, 1, 1)).ShouldEqual(new DateTime(2000, 1, 1));
        }

        [TestMethod]
        public void GetDefaultConfigFilePath_DiffModel_Should_Diff()
        {
            var simpleConfigFile = CreateSimpleConfigFile();
            var defaultConfigFilePath = simpleConfigFile.GetDefaultConfigFilePath<SimpleConfig>();
            var defaultConfigFilePath2 = simpleConfigFile.GetDefaultConfigFilePath<MockConfig>();
            defaultConfigFilePath.ShouldNotEqual(defaultConfigFilePath2);
        }

        private ISimpleConfigFile CreateSimpleConfigFile()
        {
            var mockJsonFile = new MockJsonFile();
            var mockConfig = new SimpleConfig();
            mockConfig.AddOrUpdate("key1", 1);
            mockConfig.AddOrUpdate("key2", "2");
            mockConfig.AddOrUpdate("key3", new DateTime(2000, 1, 1));
            mockJsonFile.MockConfig = mockConfig;
            mockJsonFile.MockConfigFilePath = "exist.json";
            return new SimpleConfigFile(mockJsonFile);
        }
    }

    internal class MockJsonFile : ISimpleJsonFile
    {
        public ISimpleConfig MockConfig { get; set; }
        public string MockConfigFilePath { get; set; }

        public Task<IList<T>> ReadFile<T>(string filePath)
        {
            IList<T> defaultResult = new List<T>();
            if (filePath != MockConfigFilePath)
            {
                return Task.FromResult((IList<T>)defaultResult);
            }

            defaultResult.Add((T)MockConfig);
            return Task.FromResult((IList<T>)defaultResult);
        }

        public Task SaveFile<T>(string filePath, IList<T> list, bool indented)
        {
            if (filePath != MockConfigFilePath)
            {
                return Task.FromResult(0);
            }

            var theOne = list.SingleOrDefault();
            MockConfig = theOne as ISimpleConfig;
            return Task.FromResult(0);
        }
    }
}
