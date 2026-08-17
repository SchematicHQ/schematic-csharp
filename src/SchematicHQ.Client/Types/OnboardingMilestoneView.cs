using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record OnboardingMilestoneView : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("id")]
    public required OnboardingMilestone Id { get; set; }

    [JsonPropertyName("missing")]
    public IEnumerable<OnboardingRequirement> Missing { get; set; } =
        new List<OnboardingRequirement>();

    [JsonPropertyName("progress")]
    public required double Progress { get; set; }

    [JsonPropertyName("reached_at")]
    public DateTime? ReachedAt { get; set; }

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
