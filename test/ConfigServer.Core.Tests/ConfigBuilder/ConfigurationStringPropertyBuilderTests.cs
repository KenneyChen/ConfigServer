﻿using Xunit;

namespace ConfigServer.Core.Tests
{
    public class ConfigurationStringPropertyBuilderTests
    {
        private readonly ConfigurationModelBuilder<StringTestClass> target;

        public ConfigurationStringPropertyBuilderTests()
        {
            target = new ConfigurationModelBuilder<StringTestClass>(new ConfigurationModel(typeof(SimpleConfig)));
        }

        [Fact]
        public void CanBuildModelDefinition_Property()
        {
            target.Property(x => x.StringProperty);
            var result = target.Build();

            Assert.True(result.ConfigurationProperties.ContainsKey(nameof(StringTestClass.StringProperty)));
            Assert.Equal(nameof(StringTestClass.StringProperty), result.ConfigurationProperties[nameof(StringTestClass.StringProperty)].ConfigurationPropertyName);

        }

        [Fact]
        public void CanBuildModelDefinition_PropertyWithNameAndDescription()
        {
            var name = "A Name";
            var description = "A Discription";

            target.Property(x => x.StringProperty)
                .WithDisplayName(name)
                .WithDescription(description);
            var result = target.Build();

            Assert.Equal(description, result.ConfigurationProperties[nameof(StringTestClass.StringProperty)].PropertyDescription);
            Assert.Equal(name, result.ConfigurationProperties[nameof(StringTestClass.StringProperty)].PropertyDisplayName);
        }

        [Fact]
        public void CanBuildModelDefinition_PropertyWithDefaultValidation()
        {
            target.Property(x => x.StringProperty);
            var result = target.Build();

            Assert.Null(GetStringProperty(result).ValidationRules.Max);
            Assert.Null(GetStringProperty(result).ValidationRules.Min);

        }

        [Fact]
        public void CanBuildModelDefinition_PropertyWithMaxLengthValidation()
        {
            int max = 10;
            target.Property(x => x.StringProperty)
                .WithMaxLength(10);
            var result = target.Build();

            Assert.Equal(max, GetStringProperty(result).ValidationRules.MaxLength);
        }

        [Fact]
        public void CanBuildModelDefinition_PropertyWithPaternValidation()
        {
            string pattern = "apatern";
            target.Property(x => x.StringProperty)
                .WithPattern(pattern);
            var result = target.Build();

            Assert.Equal(pattern, GetStringProperty(result).ValidationRules.Pattern);
        }

        private ConfigurationPropertyModel GetStringProperty(ConfigurationModel def)
        {
            return def.ConfigurationProperties[nameof(StringTestClass.StringProperty)];
        }

        private class StringTestClass
        {
            public string StringProperty { get; set; }
        }
    }
}
