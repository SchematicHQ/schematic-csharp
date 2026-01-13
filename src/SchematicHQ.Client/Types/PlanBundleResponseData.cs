using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanBundleResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("billing_product")]
    public BillingProductPlanResponseData? BillingProduct { get; set; }

    [JsonPropertyName("credit_grants")]
    public IEnumerable<BillingPlanCreditGrantResponseData>? CreditGrants { get; set; }

    [JsonPropertyName("entitlements")]
    public IEnumerable<PlanEntitlementResponseData>? Entitlements { get; set; }

    [JsonPropertyName("plan")]
    public PlanResponseData? Plan { get; set; }

    [JsonPropertyName("traits")]
    public IEnumerable<PlanTraitResponseData>? Traits { get; set; }

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
