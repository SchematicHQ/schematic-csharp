using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditSpendPolicyResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_credit_id")]
    public required string BillingCreditId { get; set; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Credits spent in the current window. Set only by the usage route, and only for a window cap.
    /// </summary>
    [JsonPropertyName("consumed")]
    public double? Consumed { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Credits left in the current window. Set only by the usage route, and only for a window cap.
    /// </summary>
    [JsonPropertyName("headroom")]
    public double? Headroom { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("max_per_draw")]
    public double? MaxPerDraw { get; set; }

    /// <summary>
    /// When the current window ends. Set only by the usage route, and only for a window cap.
    /// </summary>
    [JsonPropertyName("resets_at")]
    public DateTime? ResetsAt { get; set; }

    [JsonPropertyName("scope_type")]
    public required CreditSpendPolicyScope ScopeType { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("window_amount")]
    public double? WindowAmount { get; set; }

    [JsonPropertyName("window_count")]
    public required long WindowCount { get; set; }

    [JsonPropertyName("window_unit")]
    public CreditSpendWindowUnit? WindowUnit { get; set; }

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
