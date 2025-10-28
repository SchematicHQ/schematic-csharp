using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditLedgerEnrichedEntryResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_credit_id")]
    public required string BillingCreditId { get; set; }

    [JsonPropertyName("company")]
    public CompanyLedgerResponseData? Company { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("credit")]
    public BillingCreditLedgerResponseData? Credit { get; set; }

    [JsonPropertyName("expired_grant_count")]
    public required int ExpiredGrantCount { get; set; }

    [JsonPropertyName("feature")]
    public FeatureLedgerResponseData? Feature { get; set; }

    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    [JsonPropertyName("first_transaction_at")]
    public required DateTime FirstTransactionAt { get; set; }

    [JsonPropertyName("free_grant_count")]
    public required int FreeGrantCount { get; set; }

    [JsonPropertyName("grant_count")]
    public required int GrantCount { get; set; }

    [JsonPropertyName("last_transaction_at")]
    public required DateTime LastTransactionAt { get; set; }

    [JsonPropertyName("manually_zeroed_count")]
    public required int ManuallyZeroedCount { get; set; }

    [JsonPropertyName("net_change")]
    public required double NetChange { get; set; }

    [JsonPropertyName("plan_grant_count")]
    public required int PlanGrantCount { get; set; }

    [JsonPropertyName("purchased_grant_count")]
    public required int PurchasedGrantCount { get; set; }

    [JsonPropertyName("time_bucket")]
    public required DateTime TimeBucket { get; set; }

    [JsonPropertyName("total_consumed")]
    public required double TotalConsumed { get; set; }

    [JsonPropertyName("total_granted")]
    public required double TotalGranted { get; set; }

    [JsonPropertyName("transaction_count")]
    public required int TransactionCount { get; set; }

    [JsonPropertyName("usage_count")]
    public required int UsageCount { get; set; }

    [JsonPropertyName("zeroed_out_count")]
    public required int ZeroedOutCount { get; set; }

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
