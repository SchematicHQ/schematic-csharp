using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateWebhookRequestBody
{
    [JsonPropertyName("credit_trigger_configs")]
    public IEnumerable<CreditTriggerConfig>? CreditTriggerConfigs { get; set; }

    [JsonPropertyName("entitlement_trigger_configs")]
    public IEnumerable<EntitlementTriggerConfig>? EntitlementTriggerConfigs { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("request_types")]
    public IEnumerable<WebhookRequestType>? RequestTypes { get; set; }

    [JsonPropertyName("status")]
    public WebhookStatus? Status { get; set; }

    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
