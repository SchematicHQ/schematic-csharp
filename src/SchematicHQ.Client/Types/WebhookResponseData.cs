using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record WebhookResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("credit_trigger_configs")]
    public IEnumerable<CreditTriggerConfig>? CreditTriggerConfigs { get; set; }

    [JsonPropertyName("entitlement_trigger_configs")]
    public IEnumerable<EntitlementTriggerConfig>? EntitlementTriggerConfigs { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("request_types")]
    public IEnumerable<WebhookRequestType> RequestTypes { get; set; } =
        new List<WebhookRequestType>();

    [JsonPropertyName("secret")]
    public required string Secret { get; set; }

    [JsonPropertyName("status")]
    public required WebhookStatus Status { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("url")]
    public required string Url { get; set; }

    [JsonIgnore]
    public ReadOnlyAdditionalProperties AdditionalProperties { get; private set; } = new();

    void IJsonOnDeserialized.OnDeserialized() =>
        AdditionalProperties.CopyFromExtensionData(_extensionData);

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
