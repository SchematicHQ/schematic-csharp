using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingCouponResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("amount_off")]
    public int? AmountOff { get; set; }

    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    [JsonPropertyName("duration")]
    public string? Duration { get; set; }

    [JsonPropertyName("duration_in_months")]
    public int? DurationInMonths { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("external_id")]
    public required string ExternalId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("is_active")]
    public required bool IsActive { get; set; }

    [JsonPropertyName("max_redemptions")]
    public int? MaxRedemptions { get; set; }

    [JsonPropertyName("metadata")]
    public Dictionary<string, object?> Metadata { get; set; } = new Dictionary<string, object?>();

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("percent_off")]
    public double? PercentOff { get; set; }

    [JsonPropertyName("times_redeemed")]
    public required int TimesRedeemed { get; set; }

    [JsonPropertyName("valid_from")]
    public DateTime? ValidFrom { get; set; }

    [JsonPropertyName("valid_until")]
    public DateTime? ValidUntil { get; set; }

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
