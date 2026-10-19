using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingNativeSubscriptionResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("cancel_at")]
    public DateTime? CancelAt { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("currency")]
    public required Currency Currency { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The lines in effect now.
    /// </summary>
    [JsonPropertyName("lines")]
    public IEnumerable<BillingNativeSubscriptionLineResponseData> Lines { get; set; } =
        new List<BillingNativeSubscriptionLineResponseData>();

    [JsonPropertyName("period_end")]
    public required DateTime PeriodEnd { get; set; }

    [JsonPropertyName("period_start")]
    public required DateTime PeriodStart { get; set; }

    /// <summary>
    /// Changes that take effect later, earliest first.
    /// </summary>
    [JsonPropertyName("scheduled_changes")]
    public IEnumerable<BillingNativeSubscriptionChangeResponseData> ScheduledChanges { get; set; } =
        new List<BillingNativeSubscriptionChangeResponseData>();

    [JsonPropertyName("started_at")]
    public DateTime? StartedAt { get; set; }

    /// <summary>
    /// The charges not yet invoiced on the company's contract. Null when the company has no billing profile.
    /// </summary>
    [JsonPropertyName("statement")]
    public BillingStatementResponseData? Statement { get; set; }

    [JsonPropertyName("status")]
    public required SubscriptionStatus Status { get; set; }

    /// <summary>
    /// The contract terms in force. Null when the company has no billing profile.
    /// </summary>
    [JsonPropertyName("terms")]
    public BillingInvoiceTermsResponseData? Terms { get; set; }

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
