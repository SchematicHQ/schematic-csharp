using System.Text.Json.Serialization;

namespace Schematic.Client;

public class PreviewObject
{
    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }
}
