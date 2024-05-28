using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class LookupUserParams
{
    [JsonPropertyName("keys")]
    public Dictionary<string, object>? Keys { get; init; }
}
