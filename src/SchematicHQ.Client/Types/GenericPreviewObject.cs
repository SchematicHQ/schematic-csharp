using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GenericPreviewObject
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
