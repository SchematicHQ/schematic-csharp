using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CountCompanyOverridesParams
{
    /// <summary>
    /// Filter company overrides by a single company ID (starting with comp\_)
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter company overrides by multiple company IDs (starting with comp\_)
    /// </summary>
    [JsonPropertyName("company_ids")]
    public IEnumerable<string>? CompanyIds { get; set; }

    /// <summary>
    /// Filter company overrides by a single feature ID (starting with feat\_)
    /// </summary>
    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    /// <summary>
    /// Filter company overrides by multiple feature IDs (starting with feat\_)
    /// </summary>
    [JsonPropertyName("feature_ids")]
    public IEnumerable<string>? FeatureIds { get; set; }

    /// <summary>
    /// Filter company overrides by multiple company override IDs (starting with cmov\_)
    /// </summary>
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

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

    /// <summary>
    /// Search for company overrides by feature or company name
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
