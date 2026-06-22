using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyCreditBalance : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Remaining credit, excluding any open lease hold (the value SDKs gate on)
    /// </summary>
    [JsonPropertyName("remaining")]
    public required double Remaining { get; set; }

    /// <summary>
    /// Amount held by the company's open credit lease, 0 when none is open
    /// </summary>
    [JsonPropertyName("reserved")]
    public required double Reserved { get; set; }

    /// <summary>
    /// Spendable balance including the open lease hold (remaining + reserved)
    /// </summary>
    [JsonPropertyName("settled")]
    public required double Settled { get; set; }

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
