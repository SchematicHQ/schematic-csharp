using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record KeysRequestBody
{
    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
