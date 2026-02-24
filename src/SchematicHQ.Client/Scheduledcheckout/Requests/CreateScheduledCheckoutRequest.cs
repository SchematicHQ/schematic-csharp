using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateScheduledCheckoutRequest
{
    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("execute_after")]
    public required DateTime ExecuteAfter { get; set; }

    [JsonPropertyName("from_plan_id")]
    public required string FromPlanId { get; set; }

    [JsonPropertyName("to_plan_id")]
    public required string ToPlanId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
