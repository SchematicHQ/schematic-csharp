using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class EntityTraitDetailResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("definition")]
    public EntityTraitDefinitionResponseData? Definition { get; init; }

    [JsonPropertyName("definition_id")]
    public string DefinitionId { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("value")]
    public string Value { get; init; }
}
