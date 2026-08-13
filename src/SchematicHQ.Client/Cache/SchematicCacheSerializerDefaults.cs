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
            new ResilientEnumConverter() // Fallback for all enums
        }
    };
}