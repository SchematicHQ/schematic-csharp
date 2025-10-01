using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ComponentCheckoutSettings : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("collect_address")]
    public required bool CollectAddress { get; set; }

    [JsonPropertyName("collect_email")]
    public required bool CollectEmail { get; set; }

    [JsonPropertyName("collect_phone")]
    public required bool CollectPhone { get; set; }

    [JsonPropertyName("tax_collection_enabled")]
    public required bool TaxCollectionEnabled { get; set; }

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
