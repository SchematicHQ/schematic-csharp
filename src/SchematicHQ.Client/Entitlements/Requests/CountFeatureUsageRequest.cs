using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CountFeatureUsageRequest
{
    public string? CompanyId { get; set; }

    public Dictionary<string, string>? CompanyKeys { get; set; }

    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    public string? Q { get; set; }

    public bool? WithoutNegativeEntitlements { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    public int? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    public int? Offset { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
