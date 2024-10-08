using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record PlanDetailResponseData
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

    [JsonPropertyName("features")]
    public IEnumerable<FeatureDetailResponseData> Features { get; init; } =
        new List<FeatureDetailResponseData>();

    [JsonPropertyName("icon")]
    public required string Icon { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("plan_type")]
    public required string PlanType { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
