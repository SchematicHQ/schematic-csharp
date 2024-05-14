using System.Text.Json.Serialization;

namespace Schematic.Client;

public class CompanyPlanResponseData
{
    [JsonPropertyName("company_id")]
    public string CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("plan_id")]
    public string PlanId { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }
}
