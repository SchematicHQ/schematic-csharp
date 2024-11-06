using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CheckFlagResponseData
{
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("reason")]
    public required string Reason { get; set; }

    [JsonPropertyName("rule_id")]
    public string? RuleId { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("value")]
    public required bool Value { get; set; }
}
