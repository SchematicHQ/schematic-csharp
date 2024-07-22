using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record LookupCompanyParams
{
    [JsonPropertyName("keys")]
    public Dictionary<string, object>? Keys { get; init; }
}
