using System.Text.Json.Serialization;

namespace SchematicHQ.Client;

public class KeysRequestBody
{
    [JsonPropertyName("keys")]
    public Dictionary<string, object> Keys { get; init; }
}
