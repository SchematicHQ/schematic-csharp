using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingCreditResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("burn_strategy")]
    public required string BurnStrategy { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("default_expiry_unit")]
    public required string DefaultExpiryUnit { get; set; }

    [JsonPropertyName("default_expiry_unit_count")]
    public int? DefaultExpiryUnitCount { get; set; }

    [JsonPropertyName("default_rollover_policy")]
    public required string DefaultRolloverPolicy { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plural_name")]
    public string? PluralName { get; set; }

    [JsonPropertyName("price_per_unit")]
    public required int PricePerUnit { get; set; }

    [JsonPropertyName("singular_name")]
    public string? SingularName { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
