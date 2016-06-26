﻿using ConfigServer.Core;
using System;
using System.Text;

namespace ConfigServer.Configurator.Templates
{
    public class DateTimeInputTemplate
    {
        public static string Build(object value, ConfigurationPropertyDefinition definition)
        {
            return $"<input type=\"datetime\" name=\"{definition.ConfigurationPropertyName}\" value=\"{value}\" {BuildValidationElement(definition)}>";
        }

        private static string BuildValidationElement(ConfigurationPropertyDefinition definition)
        {
            var builder = new StringBuilder();
            if (definition.ValidationRules.Min != null)
                builder.Append($"min=\"{GetDateString(definition.ValidationRules.Min)}\" ");
            if (definition.ValidationRules.Max != null)
                builder.Append($"max=\"{GetDateString(definition.ValidationRules.Max)}\" ");
            return builder.ToString();
        }

        private static string GetDateString(object value)
        {
            return ((DateTime)value).ToString("yyyy-MM-dd");
        }
    }
}