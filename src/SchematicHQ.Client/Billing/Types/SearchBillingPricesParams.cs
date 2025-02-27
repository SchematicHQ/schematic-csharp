using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record SearchBillingPricesParams
{
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    [JsonPropertyName("interval")]
    public string? Interval { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("price")]
    public int? Price { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }

    [JsonPropertyName("usage_type")]
    public SearchBillingPricesResponseParamsUsageType? UsageType { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
