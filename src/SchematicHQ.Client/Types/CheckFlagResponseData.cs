using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CheckFlagResponseData
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; init; }

    [JsonPropertyName("error")]
    public string? Error { get; init; }

    [JsonPropertyName("reason")]
    public required string Reason { get; init; }

    [JsonPropertyName("rule_id")]
    public string? RuleId { get; init; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; init; }

    [JsonPropertyName("value")]
    public required bool Value { get; init; }
}
