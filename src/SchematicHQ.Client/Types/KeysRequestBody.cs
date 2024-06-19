using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class KeysRequestBody
{
    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; init; }
}
