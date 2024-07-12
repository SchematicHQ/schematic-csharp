using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record BillingProductPlanResponseData
{
    [JsonPropertyName("account_id")]
    public required string AccountId { get; init; }

    [JsonPropertyName("billing_product_id")]
    public required string BillingProductId { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; init; }
}
