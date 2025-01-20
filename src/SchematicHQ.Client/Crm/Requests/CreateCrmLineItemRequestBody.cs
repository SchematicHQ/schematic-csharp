using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CreateCrmLineItemRequestBody
{
    [JsonPropertyName("amount")]
    public required string Amount { get; init; }

    [JsonPropertyName("discount_percentage")]
    public string? DiscountPercentage { get; init; }

    [JsonPropertyName("interval")]
    public required string Interval { get; init; }

    [JsonPropertyName("line_item_external_id")]
    public required string LineItemExternalId { get; init; }

    [JsonPropertyName("product_external_id")]
    public required string ProductExternalId { get; init; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; init; }

    [JsonPropertyName("term_month")]
    public int? TermMonth { get; init; }

    [JsonPropertyName("total_discount")]
    public string? TotalDiscount { get; init; }
}
