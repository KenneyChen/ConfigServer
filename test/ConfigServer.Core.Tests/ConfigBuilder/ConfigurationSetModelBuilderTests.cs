﻿using ConfigServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ConfigServer.Core.Tests
{
    public class ConfigurationSetModelBuilderTests
    {
        private const string setName = "Sample name";
        private const string setDescription = "Sample Description";


        [Fact]
        public void CanBuildModel_WithBasicProperties()
        {
            var builder = new ConfigurationSetModelBuilder<TestConfigSet>(setName, setDescription);
            var setModel = builder.Build();
            Assert.Equal(setName, setModel.Name);
            Assert.Equal(setDescription, setModel.Description);
            Assert.Equal(typeof(TestConfigSet), setModel.ConfigSetType);
        }

        [Fact]
        public void CanBuildModel_WithConfig_HasDefaultValues()
        {
            var builder = new ConfigurationSetModelBuilder<TestConfigSet>(setName, setDescription);
            builder.Config(x=> x.Sample);
            var setModel = builder.Build();
            var configModel = setModel.Get<SimpleConfig>();
            Assert.Equal(typeof(SimpleConfig), configModel.Type);
            Assert.Equal(string.Empty, configModel.ConfigurationDescription);
            Assert.Equal(typeof(SimpleConfig).Name, configModel.ConfigurationDisplayName);
            Assert.Equal(nameof(TestConfigSet.Sample), configModel.Name);


        }

        [Fact]
        public void CanBuildModel_WithConfig_WithName()
        {
            var builder = new ConfigurationSetModelBuilder<TestConfigSet>(setName, setDescription);
            var name = "test";
            builder.Config(x => x.Sample,name);
            var setModel = builder.Build();
            var configModel = setModel.Get<SimpleConfig>();
            Assert.Equal(typeof(SimpleConfig), configModel.Type);
            Assert.Equal(string.Empty, configModel.ConfigurationDescription);
            Assert.Equal(name, configModel.ConfigurationDisplayName);

        }

        [Fact]
        public void CanBuildModel_WithConfig_WithNameDescription()
        {
            var builder = new ConfigurationSetModelBuilder<TestConfigSet>(setName, setDescription);
            var name = "test";
            var descript = "test descript";

            builder.Config(x=> x.Sample, name, descript);
            var setModel = builder.Build();
            var configModel = setModel.Get<SimpleConfig>();
            Assert.Equal(typeof(SimpleConfig), configModel.Type);
            Assert.Equal(descript, configModel.ConfigurationDescription);
            Assert.Equal(name, configModel.ConfigurationDisplayName);

        }

        private class TestConfigSet : ConfigurationSet<TestConfigSet>
        {
            public Config<SimpleConfig> Sample { get; set; }

            public TestConfigSet() : base(setName, setDescription)
            {

            }
        }
    }
}
