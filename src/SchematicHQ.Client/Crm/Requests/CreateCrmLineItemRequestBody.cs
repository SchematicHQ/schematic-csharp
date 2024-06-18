using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CreateCrmLineItemRequestBody
{
    [JsonPropertyName("TermMonth")]
    public int? TermMonth { get; init; }

    [JsonPropertyName("amount")]
    public string Amount { get; init; }

    [JsonPropertyName("discount_percentage")]
    public string? DiscountPercentage { get; init; }

    [JsonPropertyName("interval")]
    public string Interval { get; init; }

    [JsonPropertyName("line_item_external_id")]
    public string LineItemExternalId { get; init; }

    [JsonPropertyName("product_external_id")]
    public string ProductExternalId { get; init; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; init; }

    [JsonPropertyName("total_discount")]
    public string? TotalDiscount { get; init; }
}
