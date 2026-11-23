using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdatePlanBundleRequestBody
{
    [JsonPropertyName("billing_product")]
    public UpsertBillingProductRequestBody? BillingProduct { get; set; }

    [JsonPropertyName("credit_grants")]
    public IEnumerable<PlanBundleCreditGrantRequestBody>? CreditGrants { get; set; }

    [JsonPropertyName("entitlements")]
    public IEnumerable<PlanBundleEntitlementRequestBody> Entitlements { get; set; } =
        new List<PlanBundleEntitlementRequestBody>();

    [JsonPropertyName("plan")]
    public required UpdatePlanRequestBody Plan { get; set; }

    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    /// <summary>
    /// Where the company moves at term end when the version's renewal_policy is migrate_to_plan. Left out, the version keeps the destination it has; refused for any other policy.
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
