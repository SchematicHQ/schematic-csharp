using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditsCreditPurchaseSuccess : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("bundle_id")]
    public required string BundleId { get; set; }

    [JsonPropertyName("bundle_name")]
    public required string BundleName { get; set; }

    [JsonPropertyName("company")]
    public CreditsWebhookCompanySummary? Company { get; set; }

    [JsonPropertyName("credit")]
    public CreditsWebhookCreditSummary? Credit { get; set; }

    [JsonPropertyName("grant_ids")]
    public IEnumerable<string> GrantIds { get; set; } = new List<string>();

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
