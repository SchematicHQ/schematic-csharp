using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class FeatureDetailResponseData
{
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("event_summary")]
    public EventSummaryResponseData? EventSummary { get; init; }

    [JsonPropertyName("feature_type")]
    public string FeatureType { get; init; }

    [JsonPropertyName("flags")]
    public List<FlagDetailResponseData> Flags { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("lifecycle_phase")]
    public string? LifecyclePhase { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("plans")]
    public List<PreviewObject> Plans { get; init; }

    [JsonPropertyName("trait")]
    public EntityTraitDefinitionResponseData? Trait { get; init; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
