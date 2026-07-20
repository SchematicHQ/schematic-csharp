using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpsertCompanyRequestBody : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Assign this base plan when creating the company (starts with plan_). Takes precedence over the environment's initial plan and must be provisionable without a payment method.
    /// </summary>
    [JsonPropertyName("base_plan_id")]
    public string? BasePlanId { get; set; }

    /// <summary>
    /// The Schematic price to provision for base_plan_id (starts with bilpp_). Required and must be $0 for a billing-linked plan; omit for a plan that is not billing-linked.
    /// </summary>
    [JsonPropertyName("base_plan_price_id")]
    public string? BasePlanPriceId { get; set; }

    /// <summary>
    /// If you know the Schematic ID, you can use that here instead of keys
    /// </summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>
    /// See [Key Management](https://docs.schematichq.com/developer_resources/key_management) for more information
    /// </summary>
    [JsonPropertyName("keys")]
    public Dictionary<string, string> Keys { get; set; } = new Dictionary<string, string>();

    [JsonPropertyName("last_seen_at")]
    public DateTime? LastSeenAt { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("prevent_key_remap")]
    public bool? PreventKeyRemap { get; set; }

    /// <summary>
    /// A map of trait names to trait values
    /// </summary>
    [JsonPropertyName("traits")]
    public Dictionary<string, object?>? Traits { get; set; }

    [JsonPropertyName("update_only")]
    public bool? UpdateOnly { get; set; }

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
