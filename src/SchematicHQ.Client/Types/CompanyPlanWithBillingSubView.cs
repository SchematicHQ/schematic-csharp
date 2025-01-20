using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CompanyPlanWithBillingSubView
{
    [JsonPropertyName("billing_product_id")]
    public string? BillingProductId { get; init; }

    [JsonPropertyName("description")]
    public string? Description { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("image_url")]
    public string? ImageUrl { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("plan_period")]
    public string? PlanPeriod { get; init; }

    [JsonPropertyName("plan_price")]
    public int? PlanPrice { get; init; }
}
