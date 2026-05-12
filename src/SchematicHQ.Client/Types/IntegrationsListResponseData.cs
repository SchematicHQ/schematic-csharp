using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record IntegrationsListResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("capabilities")]
    public required IntegrationCapabilities Capabilities { get; set; }

    [JsonPropertyName("config")]
    public IntegrationConfig? Config { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("is_app_install")]
    public required bool IsAppInstall { get; set; }

    [JsonPropertyName("is_connect_install")]
    public required bool IsConnectInstall { get; set; }

    [JsonPropertyName("state")]
    public required IntegrationState State { get; set; }

    [JsonPropertyName("type")]
    public required IntegrationType Type { get; set; }

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
