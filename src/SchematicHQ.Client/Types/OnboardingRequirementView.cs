using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record OnboardingRequirementView : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("blocked_by")]
    public IEnumerable<OnboardingRequirement>? BlockedBy { get; set; }

    [JsonPropertyName("id")]
    public required OnboardingRequirement Id { get; set; }

    [JsonPropertyName("reached_at")]
    public DateTime? ReachedAt { get; set; }

    [JsonPropertyName("satisfied_by")]
    public string? SatisfiedBy { get; set; }

    [JsonPropertyName("status")]
    public required OnboardingRequirementStatus Status { get; set; }

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
