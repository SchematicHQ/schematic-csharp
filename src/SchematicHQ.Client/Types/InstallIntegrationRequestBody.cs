using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record InstallIntegrationRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_matching_criteria")]
    public CompanyMatchingCriteria? CompanyMatchingCriteria { get; set; }

    [JsonPropertyName("company_matching_field")]
    public string? CompanyMatchingField { get; set; }

    [JsonPropertyName("config")]
    public Dictionary<string, object?>? Config { get; set; }

    [JsonPropertyName("is_sandbox")]
    public bool? IsSandbox { get; set; }

    [JsonPropertyName("live_mode")]
    public bool? LiveMode { get; set; }

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
