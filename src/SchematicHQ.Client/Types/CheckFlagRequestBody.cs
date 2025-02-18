using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CheckFlagRequestBody
{
    [JsonPropertyName("company")]
    public Dictionary<string, string>? Company { get; set; }

    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
