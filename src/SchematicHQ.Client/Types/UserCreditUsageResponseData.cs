using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UserCreditUsageResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_credit_id")]
    public required string BillingCreditId { get; set; }

    /// <summary>
    /// The display name of the credit type
    /// </summary>
    [JsonPropertyName("credit_name")]
    public required string CreditName { get; set; }

    /// <summary>
    /// The user's net track-driven consumption of the credit within the window (draws minus refunds)
    /// </summary>
    [JsonPropertyName("credits_used")]
    public required double CreditsUsed { get; set; }

    /// <summary>
    /// This row's fraction (0-1) of the company's total track-driven consumption of the credit within the window, including unattributed consumption
    /// </summary>
    [JsonPropertyName("share")]
    public required double Share { get; set; }

    /// <summary>
    /// The user the consumption is attributed to; null for consumption from events sent without a user
    /// </summary>
    [JsonPropertyName("user")]
    public UserResponseData? User { get; set; }

    /// <summary>
    /// The user the consumption is attributed to; null for unattributed consumption
    /// </summary>
    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

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
