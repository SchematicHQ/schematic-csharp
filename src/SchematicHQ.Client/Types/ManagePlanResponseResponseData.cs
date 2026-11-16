using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ManagePlanResponseResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("checkout_id")]
    public string? CheckoutId { get; set; }

    [JsonPropertyName("company")]
    public CompanyDetailResponseData? Company { get; set; }

    [JsonPropertyName("problems")]
    public IEnumerable<CheckoutProblemResponseData> Problems { get; set; } =
        new List<CheckoutProblemResponseData>();

    /// <summary>
    /// When a scheduled plan change takes effect. The company stays on its current plan until then. Empty when the change took effect straight away.
    /// </summary>
    [JsonPropertyName("scheduled_change_at")]
    public DateTime? ScheduledChangeAt { get; set; }

    [JsonPropertyName("success")]
    public required bool Success { get; set; }

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
