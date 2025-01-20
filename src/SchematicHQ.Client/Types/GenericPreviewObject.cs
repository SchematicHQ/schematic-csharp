using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GenericPreviewObject
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
