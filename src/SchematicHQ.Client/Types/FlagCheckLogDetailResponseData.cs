using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public record FlagCheckLogDetailResponseData
{
    [JsonPropertyName("check_status")]
    public required string CheckStatus { get; init; }

    [JsonPropertyName("company")]
    public CompanyResponseData? Company { get; init; }

    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; init; }

    [JsonPropertyName("environment")]
    public EnvironmentResponseData? Environment { get; init; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; init; }

    [JsonPropertyName("error")]
    public string? Error { get; init; }

    [JsonPropertyName("flag")]
    public FlagResponseData? Flag { get; init; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; init; }

    [JsonPropertyName("flag_key")]
    public required string FlagKey { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    [JsonPropertyName("reason")]
    public required string Reason { get; init; }

    [JsonPropertyName("req_company")]
    public Dictionary<string, string?>? ReqCompany { get; init; }

    [JsonPropertyName("req_user")]
    public Dictionary<string, string?>? ReqUser { get; init; }

    [JsonPropertyName("rule")]
    public RuleResponseData? Rule { get; init; }

    [JsonPropertyName("rule_id")]
    public string? RuleId { get; init; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; init; }

    [JsonPropertyName("user")]
    public UserResponseData? User { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }

    [JsonPropertyName("value")]
    public required bool Value { get; init; }
}
