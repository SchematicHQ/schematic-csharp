using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CreateFeatureRequestBody
{
    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("event_subtype")]
    public string? EventSubtype { get; set; }

    [JsonPropertyName("feature_type")]
    public required CreateFeatureRequestBodyFeatureType FeatureType { get; set; }

    [JsonPropertyName("flag")]
    public CreateOrUpdateFlagRequestBody? Flag { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("lifecycle_phase")]
    public string? LifecyclePhase { get; set; }

    [JsonPropertyName("maintainer_id")]
    public string? MaintainerId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
