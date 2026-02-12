using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ConditionGroupView : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("account_id")]
    public required string AccountId { get; set; }

    [JsonPropertyName("conditions")]
    public IEnumerable<ConditionView> Conditions { get; set; } = new List<ConditionView>();

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("flag_id")]
    public string? FlagId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    [JsonPropertyName("rule_id")]
    public required string RuleId { get; set; }

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
