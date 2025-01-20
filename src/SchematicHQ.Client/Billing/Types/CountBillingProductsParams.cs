using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CountBillingProductsParams
{
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("price_usage_type")]
    public string? PriceUsageType { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter products that have prices
    /// </summary>
    [JsonPropertyName("with_prices_only")]
    public bool? WithPricesOnly { get; set; }

    /// <summary>
    /// Filter products that have zero price for free subscription type
    /// </summary>
    [JsonPropertyName("with_zero_price")]
    public bool? WithZeroPrice { get; set; }

    /// <summary>
    /// Filter products that are not linked to any plan
    /// </summary>
    [JsonPropertyName("without_linked_to_plan")]
    public bool? WithoutLinkedToPlan { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
