using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ListCompanyOverridesRequest
{
    /// <summary>
    /// Filter company overrides by a single company ID (starting with comp_)
    /// </summary>
    [JsonIgnore]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Filter company overrides by multiple company IDs (starting with comp_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> CompanyIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter company overrides by a single feature ID (starting with feat_)
    /// </summary>
    [JsonIgnore]
    public string? FeatureId { get; set; }

    /// <summary>
    /// Filter company overrides by multiple feature IDs (starting with feat_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    /// <summary>
    /// Filter company overrides by multiple company override IDs (starting with cmov_)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Filter company overrides by whether they have not expired
    /// </summary>
    [JsonIgnore]
    public bool? WithoutExpired { get; set; }

    /// <summary>
    /// Search for company overrides by feature or company name
    /// </summary>
    [JsonIgnore]
    public string? Q { get; set; }

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
