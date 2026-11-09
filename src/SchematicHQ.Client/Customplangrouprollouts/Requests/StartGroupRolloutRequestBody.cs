using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record StartGroupRolloutRequestBody
{
    /// <summary>
    /// Companies the payer pays for whose custom plans keep their current version.
    /// </summary>
    [JsonPropertyName("excluded_company_ids")]
    public IEnumerable<string> ExcludedCompanyIds { get; set; } = new List<string>();

    [JsonPropertyName("from_plan_version_id")]
    public string? FromPlanVersionId { get; set; }

    [JsonPropertyName("to_plan_version_id")]
    public string? ToPlanVersionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
