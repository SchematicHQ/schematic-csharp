using System.Text.Json.Serialization;
using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreateFeatureRequestBody
{
    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; init; }

    [JsonPropertyName("feature_type")]
    public CreateFeatureRequestBodyFeatureType FeatureType { get; init; }

    [JsonPropertyName("flag")]
    public CreateOrUpdateFlagRequestBody? Flag { get; init; }

    [JsonPropertyName("lifecycle_phase")]
    public string? LifecyclePhase { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; init; }
}
