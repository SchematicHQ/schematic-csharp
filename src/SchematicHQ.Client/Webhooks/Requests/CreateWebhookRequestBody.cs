using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateWebhookRequestBody
{
    [JsonPropertyName("entitlement_trigger_configs")]
    public IEnumerable<EntitlementTriggerConfig>? EntitlementTriggerConfigs { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("request_types")]
    public IEnumerable<CreateWebhookRequestBodyRequestTypesItem> RequestTypes { get; set; } =
        new List<CreateWebhookRequestBodyRequestTypesItem>();

    [JsonPropertyName("url")]
    public required string Url { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
