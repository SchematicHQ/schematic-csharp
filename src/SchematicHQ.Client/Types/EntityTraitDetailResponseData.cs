using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record EntityTraitDetailResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("definition")]
    public EntityTraitDefinitionResponseData? Definition { get; set; }

    [JsonPropertyName("definition_id")]
    public required string DefinitionId { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("value")]
    public required string Value { get; set; }
}
