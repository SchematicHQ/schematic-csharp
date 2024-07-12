using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record FeatureDetailResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("event_summary")]
    public EventSummaryResponseData? EventSummary { get; init; }

    [JsonPropertyName("feature_type")]
    public required string FeatureType { get; init; }

    [JsonPropertyName("flags")]
    public IEnumerable<FlagDetailResponseData> Flags { get; init; } =
        new List<FlagDetailResponseData>();

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("lifecycle_phase")]
    public string? LifecyclePhase { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("plans")]
    public IEnumerable<PreviewObject> Plans { get; init; } = new List<PreviewObject>();

    [JsonPropertyName("trait")]
    public EntityTraitDefinitionResponseData? Trait { get; init; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
