using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateBillingCreditRequestBody
{
    [JsonPropertyName("burn_strategy")]
    public CreateBillingCreditRequestBodyBurnStrategy? BurnStrategy { get; set; }

    [JsonPropertyName("default_expiry_unit")]
    public string? DefaultExpiryUnit { get; set; }

    [JsonPropertyName("default_expiry_unit_count")]
    public int? DefaultExpiryUnitCount { get; set; }

    [JsonPropertyName("default_rollover_policy")]
    public CreateBillingCreditRequestBodyDefaultRolloverPolicy? DefaultRolloverPolicy { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("icon")]
    public string? Icon { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plural_name")]
    public string? PluralName { get; set; }

    [JsonPropertyName("singular_name")]
    public string? SingularName { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
