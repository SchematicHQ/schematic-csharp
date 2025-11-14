using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCompanyPlansRequestBody
{
    /// <summary>
    /// company_plan_id
    /// </summary>
    [JsonIgnore]
    public required string CompanyPlanId { get; set; }

    [JsonPropertyName("add_on_ids")]
    public IEnumerable<string> AddOnIds { get; set; } = new List<string>();

    [JsonPropertyName("base_plan_id")]
    public string? BasePlanId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
