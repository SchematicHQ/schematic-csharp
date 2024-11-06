using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record UpdatePlanRequestBody
{
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }
}
