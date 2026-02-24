using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanVersionMigrationResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("completed_at")]
    public DateTime? CompletedAt { get; set; }

    [JsonPropertyName("completed_companies")]
    public required int CompletedCompanies { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("error")]
    public string? Error { get; set; }

    [JsonPropertyName("failed_companies")]
    public required int FailedCompanies { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("plan_version_id_from")]
    public string? PlanVersionIdFrom { get; set; }

    [JsonPropertyName("plan_version_id_to")]
    public required string PlanVersionIdTo { get; set; }

    [JsonPropertyName("skipped_companies")]
    public required int SkippedCompanies { get; set; }

    [JsonPropertyName("started_at")]
    public DateTime? StartedAt { get; set; }

    [JsonPropertyName("status")]
    public required PlanVersionMigrationStatus Status { get; set; }

    [JsonPropertyName("strategy")]
    public required PlanVersionMigrationStrategy Strategy { get; set; }

    [JsonPropertyName("total_companies")]
    public required int TotalCompanies { get; set; }

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
