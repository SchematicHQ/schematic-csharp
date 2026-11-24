using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateCustomPlanBundleRequestBody
{
    [JsonPropertyName("billing_product")]
    public required UpsertBillingProductRequestBody BillingProduct { get; set; }

    [JsonPropertyName("credit_grants")]
    public IEnumerable<PlanBundleCreditGrantRequestBody>? CreditGrants { get; set; }

    [JsonPropertyName("entitlements")]
    public IEnumerable<PlanBundleEntitlementRequestBody> Entitlements { get; set; } =
        new List<PlanBundleEntitlementRequestBody>();

    [JsonPropertyName("plan")]
    public required CreateCustomPlanBundlePlanRequestBody Plan { get; set; }

    /// <summary>
    /// Where the company moves at term end when renewal_policy is migrate_to_plan. Required for that policy and refused for any other.
    /// </summary>
    [JsonPropertyName("renewal_destination")]
    public RenewalDestinationRequestBody? RenewalDestination { get; set; }

    [JsonPropertyName("renewal_policy")]
    public RenewalPolicy? RenewalPolicy { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
