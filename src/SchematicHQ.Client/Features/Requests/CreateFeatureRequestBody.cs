using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record CreateFeatureRequestBody
{
    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("feature_type")]
    public required CreateFeatureRequestBodyFeatureType FeatureType { get; init; }

    [JsonPropertyName("flag")]
    public CreateOrUpdateFlagRequestBody? Flag { get; init; }

    [JsonPropertyName("icon")]
    public string? Icon { get; init; }

    [JsonPropertyName("lifecycle_phase")]
    public string? LifecyclePhase { get; init; }

    [JsonPropertyName("maintainer_id")]
    public string? MaintainerId { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; init; }
}
