using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CountFeaturesRequest
{
    [JsonIgnore]
    public IEnumerable<string> Ids { get; set; } = new List<string>();

    /// <summary>
    /// Search by feature name or ID
    /// </summary>
    [JsonIgnore]
    public string? Q { get; set; }

    /// <summary>
    /// Filter out features that already have a company override for the specified company ID
    /// </summary>
    [JsonIgnore]
    public string? WithoutCompanyOverrideFor { get; set; }

    /// <summary>
    /// Filter out features that already have a plan entitlement for the specified plan ID
    /// </summary>
    [JsonIgnore]
    public string? WithoutPlanEntitlementFor { get; set; }

    /// <summary>
    /// Filter by one or more feature types (boolean, event, trait)
    /// </summary>
    [JsonIgnore]
    public IEnumerable<string> FeatureType { get; set; } = new List<string>();

    /// <summary>
    /// Only return boolean features if there is an associated event. Automatically includes boolean in the feature types filter.
    /// </summary>
    [JsonIgnore]
    public bool? BooleanRequireEvent { get; set; }

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
