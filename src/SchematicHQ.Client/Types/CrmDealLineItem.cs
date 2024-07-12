using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CrmDealLineItem
{
    [JsonPropertyName("billing_frequency")]
    public required string BillingFrequency { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("currency")]
    public required string Currency { get; init; }

    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; init; }

    [JsonPropertyName("description")]
    public required string Description { get; init; }

    [JsonPropertyName("discount_percentage")]
    public Dictionary<string, object>? DiscountPercentage { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("price")]
    public required double Price { get; init; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; init; }

    [JsonPropertyName("term_month")]
    public int? TermMonth { get; init; }

    [JsonPropertyName("total_discount")]
    public Dictionary<string, object>? TotalDiscount { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }
}
