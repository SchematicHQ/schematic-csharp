using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountFeatureUsageRequest
{
    [JsonIgnore]
    public string? CompanyId { get; set; }

    [JsonIgnore]
    public Dictionary<string, string>? CompanyKeys { get; set; }

    [JsonIgnore]
    public IEnumerable<string> FeatureIds { get; set; } = new List<string>();

    [JsonIgnore]
    public string? Q { get; set; }

    [JsonIgnore]
    public bool? WithoutNegativeEntitlements { get; set; }

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
