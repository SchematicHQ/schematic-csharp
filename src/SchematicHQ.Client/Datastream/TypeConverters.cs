using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Datastream
{
    public class EntityTypeConverter : JsonConverter<EntityType>
    {
        private readonly Dictionary<string, EntityType> _nameToEnum;
        private readonly Dictionary<EntityType, string> _enumToName;

        public EntityTypeConverter()
        {
            _nameToEnum = new Dictionary<string, EntityType>(StringComparer.OrdinalIgnoreCase);
            _enumToName = new Dictionary<EntityType, string>();
            
            // Initialize the mappings
            foreach (var enumValue in Enum.GetValues(typeof(EntityType)).Cast<EntityType>())
            {
                var memberInfo = typeof(EntityType).GetMember(enumValue.ToString())[0];
                var attribute = memberInfo.GetCustomAttribute<JsonStringEnumMemberNameAttribute>();
                
                string name = attribute?.Name ?? enumValue.ToString().ToLowerInvariant();
                _nameToEnum[name] = enumValue;
                _enumToName[enumValue] = name;
            }
        }

        public override EntityType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? stringValue = reader.GetString();
            
            if (string.IsNullOrEmpty(stringValue))
            {
                return EntityType.Unknown;
            }

            if (_nameToEnum.TryGetValue(stringValue, out var enumValue))
            {
                return enumValue;
            }

            return EntityType.Unknown;
        }

        public override void Write(Utf8JsonWriter writer, EntityType value, JsonSerializerOptions options)
        {
            if (_enumToName.TryGetValue(value, out var name))
            {
                writer.WriteStringValue(name);
            }
            else
            {
                writer.WriteStringValue(value.ToString().ToLowerInvariant());
            }
        }
    }
}