using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record FlagInPlanResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// Deprecated. The value a check returns in the requesting environment when no other rule matches: its default rule, or the flag's seed value while it has none. Read the default rule instead
    /// </summary>
    [JsonPropertyName("default_value")]
    public bool? DefaultValue { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("feature")]
    public FeatureResponseData? Feature { get; set; }

    [JsonPropertyName("feature_id")]
    public string? FeatureId { get; set; }

    [JsonPropertyName("flag_type")]
    public string FlagType { get; set; } = "boolean";

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("key")]
    public required string Key { get; set; }

    [JsonPropertyName("last_checked_at")]
    public DateTime? LastCheckedAt { get; set; }

    [JsonPropertyName("maintainer_account_member_id")]
    public string? MaintainerAccountMemberId { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("rules")]
    public IEnumerable<RuleDetailResponseData> Rules { get; set; } =
        new List<RuleDetailResponseData>();

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

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
