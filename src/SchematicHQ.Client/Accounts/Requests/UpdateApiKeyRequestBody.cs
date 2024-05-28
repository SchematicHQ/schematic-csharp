using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class UpdateApiKeyRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }
}
