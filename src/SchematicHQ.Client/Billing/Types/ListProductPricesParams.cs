using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListProductPricesParams
{
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("price_usage_type")]
    public string? PriceUsageType { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }

    /// <summary>
    /// Filter products that have prices
    /// </summary>
    [JsonPropertyName("with_prices_only")]
    public bool? WithPricesOnly { get; init; }

    /// <summary>
    /// Filter products that have zero price for free subscription type
    /// </summary>
    [JsonPropertyName("with_zero_price")]
    public bool? WithZeroPrice { get; init; }

    /// <summary>
    /// Filter products that are not linked to any plan
    /// </summary>
    [JsonPropertyName("without_linked_to_plan")]
    public bool? WithoutLinkedToPlan { get; init; }
}
