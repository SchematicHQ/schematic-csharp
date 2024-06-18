using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CrmDealLineItem
{
    [JsonPropertyName("billing_frequency")]
    public string BillingFrequency { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("currency")]
    public string Currency { get; init; }

    [JsonPropertyName("deleted_at")]
    public DateTime? DeletedAt { get; init; }

    [JsonPropertyName("description")]
    public string Description { get; init; }

    [JsonPropertyName("discount_percentage")]
    public Dictionary<string, object>? DiscountPercentage { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("price")]
    public double Price { get; init; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }

    [JsonPropertyName("term_month")]
    public int? TermMonth { get; init; }

    [JsonPropertyName("total_discount")]
    public Dictionary<string, object>? TotalDiscount { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
