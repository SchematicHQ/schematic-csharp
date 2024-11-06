using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateApiKeyRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }
}
