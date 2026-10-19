using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record BillingNativeSubscriptionChangeResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("action")]
    public required SubscriptionChangeAction Action { get; set; }

    [JsonPropertyName("effective_at")]
    public required DateTime EffectiveAt { get; set; }

    /// <summary>
    /// The line added, or the line removed. Null on a cancellation.
    /// </summary>
    [JsonPropertyName("line")]
    public BillingNativeSubscriptionLineResponseData? Line { get; set; }

    /// <summary>
    /// The line a replacement ends. Null unless the action is replace.
    /// </summary>
    [JsonPropertyName("replaces")]
    public BillingNativeSubscriptionLineResponseData? Replaces { get; set; }

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
