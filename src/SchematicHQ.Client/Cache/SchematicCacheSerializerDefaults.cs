using System.Text.Json;

namespace SchematicHQ.Client.Cache;

public static class SchematicCacheSerializerDefaults
{
    public static readonly JsonSerializerOptions Options = new JsonSerializerOptions
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower,
        Converters =
        {
            new ComparableTypeConverter(), // Specific handler for ComparableType empty strings
            new ResilientEnumConverter() // Fallback for all other enums
        }
    };
}