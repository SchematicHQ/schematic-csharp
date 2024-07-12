using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListFeatureUsageParams
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("company_keys")]
    public Dictionary<string, string>? CompanyKeys { get; init; }

    [JsonPropertyName("feature_ids")]
    public IEnumerable<string>? FeatureIds { get; init; }

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

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
