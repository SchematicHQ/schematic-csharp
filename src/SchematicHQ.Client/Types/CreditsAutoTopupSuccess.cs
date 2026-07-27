using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditsAutoTopupSuccess : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company")]
    public CreditsAutoTopupCompanySummary? Company { get; set; }

    [JsonPropertyName("credit")]
    public CreditsAutoTopupCreditSummary? Credit { get; set; }

    [JsonPropertyName("grant_id")]
    public required string GrantId { get; set; }

    [JsonPropertyName("quantity")]
    public required long Quantity { get; set; }

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
