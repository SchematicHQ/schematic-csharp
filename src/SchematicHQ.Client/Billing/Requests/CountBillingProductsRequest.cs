using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountBillingProductsRequest
{
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter products that are active. Defaults to true if not specified
    /// </summary>
    [JsonIgnore]
    public bool? IsActive { get; set; }

    [JsonIgnore]
    public string? Name { get; set; }

    [JsonIgnore]
    public BillingPriceUsageType? PriceUsageType { get; set; }

    [JsonIgnore]
    public BillingProviderType? ProviderType { get; set; }

    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Filter products that are one time charges
    /// </summary>
    [JsonIgnore]
    public bool? WithOneTimeCharges { get; set; }

    /// <summary>
    /// Filter products that have prices
    /// </summary>
    [JsonIgnore]
    public bool? WithPricesOnly { get; set; }

    /// <summary>
    /// Filter products that have zero price for free subscription type
    /// </summary>
    [JsonIgnore]
    public bool? WithZeroPrice { get; set; }

    /// <summary>
    /// Filter products that are not linked to any plan
    /// </summary>
    [JsonIgnore]
    public bool? WithoutLinkedToPlan { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonIgnore]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonIgnore]
    public int? Offset { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
