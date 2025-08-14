using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

namespace SchematicHQ.Client.RulesEngine
{
    public class SnakeCaseEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            if (value == null)
                throw new JsonException();

            // Convert snake_case to PascalCase
            var pascalCase = string.Join("", value.Split('_', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => char.ToUpperInvariant(s[0]) + s.Substring(1)));

            if (Enum.TryParse<T>(pascalCase, out var result))
                return result;

            throw new JsonException($"Unable to convert '{value}' to enum '{typeof(T).Name}'");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            // Convert PascalCase to snake_case
            var str = value.ToString();
            var snake = string.Concat(str.Select((x, i) => i > 0 && char.IsUpper(x) ? "_" + char.ToLowerInvariant(x) : char.ToLowerInvariant(x).ToString()));
            writer.WriteStringValue(snake);
        }
    }
}
