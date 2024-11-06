using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CheckFlagRequestBody
{
    [JsonPropertyName("company")]
    public Dictionary<string, string>? Company { get; set; }

    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; set; }
}
