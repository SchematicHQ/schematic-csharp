using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record RulesengineCompany : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("base_plan_id")]
    public string? BasePlanId { get; set; }

    [JsonPropertyName("billing_product_ids")]
    public IEnumerable<string> BillingProductIds { get; set; } = new List<string>();

    [JsonPropertyName("credit_balances")]
    public Dictionary<string, double> CreditBalances { get; set; } =
        new Dictionary<string, double>();

    [JsonPropertyName("crm_product_ids")]
    public IEnumerable<string> CrmProductIds { get; set; } = new List<string>();

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("metrics")]
    public IEnumerable<RulesengineCompanyMetric> Metrics { get; set; } =
        new List<RulesengineCompanyMetric>();

    [JsonPropertyName("plan_ids")]
    public IEnumerable<string> PlanIds { get; set; } = new List<string>();

    [JsonPropertyName("rules")]
    public IEnumerable<RulesengineRule> Rules { get; set; } = new List<RulesengineRule>();

    [JsonPropertyName("subscription")]
    public RulesengineSubscription? Subscription { get; set; }

    [JsonPropertyName("traits")]
    public IEnumerable<RulesengineTrait> Traits { get; set; } = new List<RulesengineTrait>();

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
