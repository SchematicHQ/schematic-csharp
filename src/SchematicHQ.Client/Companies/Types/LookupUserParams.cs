using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class LookupUserParams
{
    [JsonPropertyName("keys")]
    public Dictionary<string, object>? Keys { get; init; }
}
