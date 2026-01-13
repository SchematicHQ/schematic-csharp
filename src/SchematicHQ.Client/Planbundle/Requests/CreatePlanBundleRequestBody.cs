using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreatePlanBundleRequestBody
{
    [JsonPropertyName("billing_product")]
    public UpsertBillingProductRequestBody? BillingProduct { get; set; }

    [JsonPropertyName("credit_grants")]
    public IEnumerable<PlanBundleCreditGrantRequestBody>? CreditGrants { get; set; }

    [JsonPropertyName("entitlements")]
    public IEnumerable<PlanBundleEntitlementRequestBody> Entitlements { get; set; } =
        new List<PlanBundleEntitlementRequestBody>();

    [JsonPropertyName("plan")]
    public CreatePlanRequestBody? Plan { get; set; }

    [JsonPropertyName("traits")]
    public IEnumerable<UpdatePlanTraitTraitRequestBody>? Traits { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
