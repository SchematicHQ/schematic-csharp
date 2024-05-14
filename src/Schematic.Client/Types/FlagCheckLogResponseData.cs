using System.Text.Json.Serialization;

namespace Schematic.Client;

public class FlagCheckLogResponseData
{
    [JsonPropertyName("check_status")]
    public string CheckStatus { get; init; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment_id")]
    public string EnvironmentId { get; init; }

    [JsonPropertyName("error")]
    public string? Error { get; init; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; init; }

    [JsonPropertyName("flag_key")]
    public string FlagKey { get; init; }

    [JsonPropertyName("id")]
    public string Id { get; init; }

    [JsonPropertyName("reason")]
    public string Reason { get; init; }

    [JsonPropertyName("req_company")]
    public Dictionary<string, object>? ReqCompany { get; init; }

    [JsonPropertyName("req_user")]
    public Dictionary<string, object>? ReqUser { get; init; }

    [JsonPropertyName("rule_id")]
    public string? RuleId { get; init; }

    [JsonPropertyName("updated_at")]
    public DateTime UpdatedAt { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }

    [JsonPropertyName("value")]
    public bool Value { get; init; }
}
