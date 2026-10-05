using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreditSpendPolicy : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// How much of the limit is already spent in the current period
    /// </summary>
    [JsonPropertyName("consumed")]
    public double? Consumed { get; set; }

    /// <summary>
    /// The credit the policy limits
    /// </summary>
    [JsonPropertyName("credit_id")]
    public required string CreditId { get; set; }

    /// <summary>
    /// The ID of the policy
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// How the limit is applied
    /// </summary>
    [JsonPropertyName("kind")]
    public required string Kind { get; set; }

    /// <summary>
    /// The name the account gave the policy
    /// </summary>
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    /// <summary>
    /// The ceiling, in credits
    /// </summary>
    [JsonPropertyName("limit")]
    public required double Limit { get; set; }

    /// <summary>
    /// For a windowed limit, when the current period ends and consumed no longer applies
    /// </summary>
    [JsonPropertyName("resets_at")]
    public DateTime? ResetsAt { get; set; }

    /// <summary>
    /// Whether the policy limits the company or one user
    /// </summary>
    [JsonPropertyName("scope")]
    public required CreditSpendPolicyScope Scope { get; set; }

    /// <summary>
    /// For a windowed limit, the period it accumulates over
    /// </summary>
    [JsonPropertyName("window")]
    public CreditSpendWindow? Window { get; set; }

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
