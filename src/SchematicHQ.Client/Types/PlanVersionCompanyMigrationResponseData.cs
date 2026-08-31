using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanVersionCompanyMigrationResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("company_name")]
    public required string CompanyName { get; set; }

    [JsonPropertyName("completed_at")]
    public DateTime? CompletedAt { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("error_code")]
    public MigrationErrorCode? ErrorCode { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("migration_id")]
    public required string MigrationId { get; set; }

    [JsonPropertyName("plan_version_id_from")]
    public string? PlanVersionIdFrom { get; set; }

    /// <summary>
    /// When this company is expected to migrate, for a migration scheduled at the end of the billing period: the end of the company's current billing period. Only set while both the company and the migration are still pending. A value at or before the time of the request means the company has no active subscription and migrates as soon as processing runs. Null means no upcoming renewal could be determined from the company's current billing status (for example, a past-due subscription or one set to cancel); it does not mean the company will never migrate.
    /// </summary>
    [JsonPropertyName("scheduled_for")]
    public DateTime? ScheduledFor { get; set; }

    [JsonPropertyName("started_at")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("status")]
    public required PlanVersionCompanyMigrationStatus Status { get; set; }

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
