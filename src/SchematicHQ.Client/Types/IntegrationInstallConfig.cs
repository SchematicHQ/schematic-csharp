using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record IntegrationInstallConfig : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_matching_criteria")]
    public CompanyMatchingCriteria? CompanyMatchingCriteria { get; set; }

    [JsonPropertyName("company_matching_field")]
    public string? CompanyMatchingField { get; set; }

    [JsonPropertyName("config")]
    public IntegrationConfig? Config { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("integration_id")]
    public required string IntegrationId { get; set; }

    [JsonPropertyName("is_app_install")]
    public required bool IsAppInstall { get; set; }

    [JsonPropertyName("is_connect_install")]
    public required bool IsConnectInstall { get; set; }

    [JsonPropertyName("live_mode")]
    public required bool LiveMode { get; set; }

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

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
