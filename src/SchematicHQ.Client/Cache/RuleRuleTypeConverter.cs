using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Cache
{
    public class RuleRuleTypeConverter : JsonConverter<RuleRuleType>
    {
        public override RuleRuleType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            
            // Handle null - return empty string RuleRuleType
            if (value == null)
            {
                return RuleRuleType.FromCustom("");
            }
            
            // For any value (known or unknown, including empty string), just pass it through
            // RuleRuleType is a string enum that can hold any value
            return RuleRuleType.FromCustom(value);
        }

        public override void Write(Utf8JsonWriter writer, RuleRuleType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}