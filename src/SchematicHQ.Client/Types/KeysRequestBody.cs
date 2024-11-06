using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record KeysRequestBody
{
    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();
}
