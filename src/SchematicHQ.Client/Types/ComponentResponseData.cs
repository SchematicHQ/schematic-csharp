using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ComponentResponseData
{
    [JsonPropertyName("ast")]
    public Dictionary<string, double>? Ast { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("state")]
    public required string State { get; init; }

    [JsonPropertyName("type")]
    public required string Type { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
