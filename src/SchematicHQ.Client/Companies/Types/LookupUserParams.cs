using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record LookupUserParams
{
    [JsonPropertyName("keys")]
    public Dictionary<string, object>? Keys { get; init; }
}
