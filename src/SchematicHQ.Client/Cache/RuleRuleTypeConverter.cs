using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Cache
{
    public class RuleTypeConverter : JsonConverter<RuleType>
    {
        public override RuleType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            
            // Handle null - return empty string RuleType
            if (value == null)
            {
                return RuleType.FromCustom("");
            }
            
            // For any value (known or unknown, including empty string), just pass it through
            // RuleType is a string enum that can hold any value
            return RuleType.FromCustom(value);
        }

        public override void Write(Utf8JsonWriter writer, RuleType value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.Value);
        }
    }
}