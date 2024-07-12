using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdateApiKeyRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
