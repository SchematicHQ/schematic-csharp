using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
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
    public CreateFeatureRequestBodyLifecyclePhase? LifecyclePhase { get; set; }

    [JsonPropertyName("maintainer_id")]
    public string? MaintainerId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plural_name")]
    public string? PluralName { get; set; }

    [JsonPropertyName("singular_name")]
    public string? SingularName { get; set; }

    [JsonPropertyName("trait_id")]
    public string? TraitId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
