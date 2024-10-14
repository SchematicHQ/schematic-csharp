using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CreateCrmLineItemRequestBody
{
    [JsonPropertyName("TermMonth")]
    public int? TermMonth { get; set; }

    [JsonPropertyName("amount")]
    public required string Amount { get; set; }

    [JsonPropertyName("discount_percentage")]
    public string? DiscountPercentage { get; set; }

    [JsonPropertyName("interval")]
    public required string Interval { get; set; }

    [JsonPropertyName("line_item_external_id")]
    public required string LineItemExternalId { get; set; }

    [JsonPropertyName("product_external_id")]
    public required string ProductExternalId { get; set; }

    [JsonPropertyName("quantity")]
    public required int Quantity { get; set; }

    [JsonPropertyName("total_discount")]
    public string? TotalDiscount { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
