using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record SearchBillingPricesParams
{
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; init; }

    [JsonPropertyName("interval")]
    public string? Interval { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("price")]
    public int? Price { get; init; }

    [JsonPropertyName("usage_type")]
    public string? UsageType { get; init; }
}
