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

    /// <summary>
    /// When the company moves to the new version: the migration's date for a scheduled migration, or the end of the company's current billing period. Null when no date can be named yet, for example when the company's only subscription is past due or set to cancel; the company then moves at the next opportunity.
    /// </summary>
    [JsonPropertyName("effective_at")]
    public DateTime? EffectiveAt { get; set; }

    [JsonPropertyName("migration_id")]
    public required string MigrationId { get; set; }

    /// <summary>
    /// How the price difference is billed when the company moves. Always none for an end-of-billing-period migration.
    /// </summary>
    [JsonPropertyName("proration_behavior")]
    public MigrationProrationBehavior? ProrationBehavior { get; set; }

    /// <summary>
    /// Deprecated; use effective_at, which carries the same value.
    /// </summary>
    [JsonPropertyName("scheduled_for")]
    public DateTime? ScheduledFor { get; set; }

    /// <summary>
    /// Whether the company moves at the end of its billing period (end_of_billing_period) or on a specific date (scheduled). The type is shared with plan version migrations, but only those two values appear here: an immediate migration never pends.
    /// </summary>
    [JsonPropertyName("strategy")]
    public required PlanVersionMigrationStrategy Strategy { get; set; }

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
