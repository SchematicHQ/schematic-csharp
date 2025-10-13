using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.RulesEngine.Utils;

namespace SchematicHQ.Client.Cache
{
    public class ComparableTypeConverter : JsonConverter<ComparableType>
    {
        public override ComparableType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            
            return value switch
            {
                "" or null => ComparableType.Unknown,
                "string" => ComparableType.String,
                "int" => ComparableType.Int,
                "bool" => ComparableType.Bool,
                "date" => ComparableType.Date,
                _ => ComparableType.Unknown
            };
        }

        public override void Write(Utf8JsonWriter writer, ComparableType value, JsonSerializerOptions options)
        {
            var stringValue = value switch
            {
                ComparableType.Unknown => "",
                ComparableType.String => "string",
                ComparableType.Int => "int",
                ComparableType.Bool => "bool", 
                ComparableType.Date => "date",
                _ => ""
            };
            
            writer.WriteStringValue(stringValue);
        }
    }
}