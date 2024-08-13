using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdatePlanRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }
}
