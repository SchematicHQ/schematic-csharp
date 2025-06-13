using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
public record ListBillingProductsParams
{
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Filter products that are active
    /// </summary>
    [JsonPropertyName("is_active")]
    public bool? IsActive { get; set; }

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
    public ListBillingProductsResponseParamsPriceUsageType? PriceUsageType { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    /// <summary>
    /// Filter products that are one time charges
    /// </summary>
    [JsonPropertyName("with_one_time_charges")]
    public bool? WithOneTimeCharges { get; set; }

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

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
