using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetOnboardingStateResp : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("environment_id")]
    public string? EnvironmentId { get; set; }

    [JsonPropertyName("milestones")]
    public IEnumerable<OnboardingMilestoneView> Milestones { get; set; } =
        new List<OnboardingMilestoneView>();

    [JsonPropertyName("path")]
    public OnboardingPath? Path { get; set; }

    [JsonPropertyName("requirements")]
    public IEnumerable<OnboardingRequirementView> Requirements { get; set; } =
        new List<OnboardingRequirementView>();

    [JsonPropertyName("suggested_next")]
    public IEnumerable<OnboardingRequirement> SuggestedNext { get; set; } =
        new List<OnboardingRequirement>();

    [JsonPropertyName("track")]
    public OnboardingTrack? Track { get; set; }

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
