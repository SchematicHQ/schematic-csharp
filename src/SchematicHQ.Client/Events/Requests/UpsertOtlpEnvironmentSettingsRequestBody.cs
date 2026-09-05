using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpsertOtlpEnvironmentSettingsRequestBody
{
    [JsonPropertyName("company_attribute")]
    public string? CompanyAttribute { get; set; }

    [JsonPropertyName("company_key")]
    public string? CompanyKey { get; set; }

    [JsonPropertyName("tool_events_enabled")]
    public required bool ToolEventsEnabled { get; set; }

    [JsonPropertyName("user_attribute")]
    public string? UserAttribute { get; set; }

    [JsonPropertyName("user_key")]
    public string? UserKey { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
