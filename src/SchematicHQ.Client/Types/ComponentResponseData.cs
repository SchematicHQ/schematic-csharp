using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ComponentResponseData
{
    [JsonPropertyName("ast")]
    public IEnumerable<int> Ast { get; init; } = new List<int>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
