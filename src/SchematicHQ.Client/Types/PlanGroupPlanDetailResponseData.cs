using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record PlanGroupPlanDetailResponseData
{
    [JsonPropertyName("audience_type")]
    public string? AudienceType { get; init; }

    [JsonPropertyName("billing_product")]
    public BillingProductDetailResponseData? BillingProduct { get; init; }

    [JsonPropertyName("company_count")]
    public required int CompanyCount { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("entitlements")]
    public IEnumerable<PlanEntitlementResponseData> Entitlements { get; init; } =
        new List<PlanEntitlementResponseData>();

    [JsonPropertyName("features")]
    public IEnumerable<FeatureDetailResponseData> Features { get; init; } =
        new List<FeatureDetailResponseData>();

    [JsonPropertyName("icon")]
    public required string Icon { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("is_default")]
    public required bool IsDefault { get; init; }

    [JsonPropertyName("is_free")]
    public required bool IsFree { get; init; }

    [JsonPropertyName("is_trialable")]
    public required bool IsTrialable { get; init; }

    [JsonPropertyName("monthly_price")]
    public BillingPriceResponseData? MonthlyPrice { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("plan_type")]
    public required string PlanType { get; init; }

    [JsonPropertyName("trial_days")]
    public int? TrialDays { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("yearly_price")]
    public BillingPriceResponseData? YearlyPrice { get; init; }
}
