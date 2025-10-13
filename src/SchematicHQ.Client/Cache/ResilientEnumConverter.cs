using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Cache
{
    /// <summary>
    /// A resilient enum converter that handles unknown or blank values gracefully.
    /// Falls back to the first enum value (index 0) for unknown values.
    /// </summary>
    public class ResilientEnumConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsEnum;
        }

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var converterType = typeof(ResilientEnumConverter<>).MakeGenericType(typeToConvert);
            return (JsonConverter)Activator.CreateInstance(converterType)!;
        }
    }

    public class ResilientEnumConverter<T> : JsonConverter<T> where T : struct, Enum
    {
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var stringValue = reader.GetString();
                
                if (string.IsNullOrEmpty(stringValue))
                {
                    // Handle empty/null strings - return first enum value
                    var enumValues = Enum.GetValues<T>();
                    return enumValues.Length > 0 ? enumValues[0] : default(T);
                }

                // Try to parse the enum value (case-insensitive, snake_case to PascalCase)
                if (TryParseEnum(stringValue, out T result))
                {
                    return result;
                }
                
                // If parsing fails, log and return default
                Console.WriteLine($"Unknown enum value '{stringValue}' for type {typeof(T).Name}, using default value");
                var defaultValues = Enum.GetValues<T>();
                return defaultValues.Length > 0 ? defaultValues[0] : default(T);
            }
            
            throw new JsonException($"Unexpected token type {reader.TokenType} for enum {typeof(T).Name}");
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            // Convert enum to snake_case string for writing
            var enumName = value.ToString();
            var snakeCaseName = ConvertToSnakeCase(enumName);
            writer.WriteStringValue(snakeCaseName);
        }

        private static bool TryParseEnum(string value, out T result)
        {
            // Try direct parse first
            if (Enum.TryParse<T>(value, true, out result))
                return true;

            // Try converting snake_case to PascalCase
            var pascalCase = ConvertToPascalCase(value);
            if (Enum.TryParse<T>(pascalCase, true, out result))
                return true;

            result = default(T);
            return false;
        }

        private static string ConvertToPascalCase(string snakeCase)
        {
            if (string.IsNullOrEmpty(snakeCase))
                return snakeCase;

            var parts = snakeCase.Split('_');
            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i].Length > 0)
                {
                    parts[i] = char.ToUpper(parts[i][0]) + parts[i].Substring(1).ToLower();
                }
            }
            return string.Join("", parts);
        }

        private static string ConvertToSnakeCase(string pascalCase)
        {
            if (string.IsNullOrEmpty(pascalCase))
                return pascalCase;

            var result = new System.Text.StringBuilder();
            for (int i = 0; i < pascalCase.Length; i++)
            {
                if (i > 0 && char.IsUpper(pascalCase[i]))
                {
                    result.Append('_');
                }
                result.Append(char.ToLower(pascalCase[i]));
            }
            return result.ToString();
        }
    }
}