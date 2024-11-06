using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record FeatureDetailResponseData
{
    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    [JsonPropertyName("event_summary")]
    public EventSummaryResponseData? EventSummary { get; set; }

    [JsonPropertyName("feature_type")]
    public required string FeatureType { get; set; }

    [JsonPropertyName("flags")]
    public IEnumerable<FlagDetailResponseData> Flags { get; set; } =
        new List<FlagDetailResponseData>();

    [JsonPropertyName("icon")]
    public required string Icon { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("lifecycle_phase")]
    public string? LifecyclePhase { get; set; }

    [JsonPropertyName("maintainer_id")]
    public string? MaintainerId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plans")]
    public IEnumerable<PreviewObject> Plans { get; set; } = new List<PreviewObject>();

    [JsonPropertyName("trait")]
    public EntityTraitDefinitionResponseData? Trait { get; set; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }
}
