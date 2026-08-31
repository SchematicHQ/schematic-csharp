using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PendingMigrationResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("migration_id")]
    public required string MigrationId { get; set; }

    [JsonPropertyName("scheduled_for")]
    public DateTime? ScheduledFor { get; set; }

    [JsonPropertyName("to_plan_id")]
    public required string ToPlanId { get; set; }

    [JsonPropertyName("to_plan_name")]
    public required string ToPlanName { get; set; }

    [JsonPropertyName("to_plan_version_id")]
    public required string ToPlanVersionId { get; set; }

    [JsonPropertyName("to_plan_version_number")]
    public long? ToPlanVersionNumber { get; set; }

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
