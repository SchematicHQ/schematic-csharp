using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyBillingProfileContractResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("billing_customer_id")]
    public string? BillingCustomerId { get; set; }

    /// <summary>
    /// Who invoices issued under the contract are addressed to, as the contract holds them now. Null when it holds none.
    /// </summary>
    [JsonPropertyName("billing_details")]
    public BillingInvoiceBillingDetailsResponseData? BillingDetails { get; set; }

    [JsonPropertyName("collection_method")]
    public required BillingCollectionMethod CollectionMethod { get; set; }

    [JsonPropertyName("collector")]
    public Collector? Collector { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    /// <summary>
    /// The signed order form or MSA kept with the contract. Null when none is attached.
    /// </summary>
    [JsonPropertyName("contract_document")]
    public CompanyBillingProfileDocumentResponseData? ContractDocument { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("currency")]
    public Currency? Currency { get; set; }

    [JsonPropertyName("days_until_due")]
    public long? DaysUntilDue { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("gating_days")]
    public required long GatingDays { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("is_default")]
    public required bool IsDefault { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("overdraft_limit")]
    public long? OverdraftLimit { get; set; }

    [JsonPropertyName("payment_gating")]
    public required PaymentGating PaymentGating { get; set; }

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; set; }

    /// <summary>
    /// What the company owes on postpaid usage, which the contract's overdraft_limit caps. Set only when reading the company's default contract that Schematic bills, and the company has postpaid credits.
    /// </summary>
    [JsonPropertyName("postpaid")]
    public ContractPostpaidResponseData? Postpaid { get; set; }

    [JsonPropertyName("proration_behavior")]
    public ProrationBehavior? ProrationBehavior { get; set; }

    [JsonPropertyName("provider_type")]
    public required BillingProviderType ProviderType { get; set; }

    [JsonPropertyName("purchase_order")]
    public string? PurchaseOrder { get; set; }

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
