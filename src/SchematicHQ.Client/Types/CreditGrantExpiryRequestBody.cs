using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditGrantExpiryRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("expiry_type")]
    public CreditGrantExpiryRequestBodyExpiryType? ExpiryType { get; set; }

    [JsonPropertyName("expiry_unit")]
    public CreditGrantExpiryRequestBodyExpiryUnit? ExpiryUnit { get; set; }

    [JsonPropertyName("expiry_unit_count")]
    public int? ExpiryUnitCount { get; set; }

    [JsonPropertyName("reset_cadence")]
    public required CreditGrantExpiryRequestBodyResetCadence ResetCadence { get; set; }

    [JsonPropertyName("reset_start")]
    public required CreditGrantExpiryRequestBodyResetStart ResetStart { get; set; }

    [JsonPropertyName("reset_type")]
    public CreditGrantExpiryRequestBodyResetType? ResetType { get; set; }

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
