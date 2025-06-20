using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CompanyDetailResponseData
{
    [JsonPropertyName("add_ons")]
    public IEnumerable<CompanyPlanWithBillingSubView> AddOns { get; set; } =
        new List<CompanyPlanWithBillingSubView>();

    [JsonPropertyName("billing_subscription")]
    public BillingSubscriptionView? BillingSubscription { get; set; }

    [JsonPropertyName("billing_subscriptions")]
    public IEnumerable<BillingSubscriptionView> BillingSubscriptions { get; set; } =
        new List<BillingSubscriptionView>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("default_payment_method")]
    public PaymentMethodResponseData? DefaultPaymentMethod { get; set; }

    [JsonPropertyName("entity_traits")]
    public IEnumerable<EntityTraitDetailResponseData> EntityTraits { get; set; } =
        new List<EntityTraitDetailResponseData>();

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("keys")]
    public IEnumerable<EntityKeyDetailResponseData> Keys { get; set; } =
        new List<EntityKeyDetailResponseData>();

    [JsonPropertyName("last_seen_at")]
    public DateTime? LastSeenAt { get; set; }

    [JsonPropertyName("logo_url")]
    public string? LogoUrl { get; set; }

    [JsonPropertyName("metrics")]
    public IEnumerable<CompanyEventPeriodMetricsResponseData> Metrics { get; set; } =
        new List<CompanyEventPeriodMetricsResponseData>();

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("payment_methods")]
    public IEnumerable<PaymentMethodResponseData> PaymentMethods { get; set; } =
        new List<PaymentMethodResponseData>();

    [JsonPropertyName("plan")]
    public CompanyPlanWithBillingSubView? Plan { get; set; }

    [JsonPropertyName("plans")]
    public IEnumerable<GenericPreviewObject> Plans { get; set; } = new List<GenericPreviewObject>();

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public object? Traits { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("user_count")]
    public required int UserCount { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
