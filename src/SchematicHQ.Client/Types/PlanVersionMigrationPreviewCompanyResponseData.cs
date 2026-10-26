using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanVersionMigrationPreviewCompanyResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("has_billing_changes")]
    public required bool HasBillingChanges { get; set; }

    [JsonPropertyName("has_custom_pricing")]
    public required bool HasCustomPricing { get; set; }

    /// <summary>
    /// The message of the first blocking note, or of the first note when none is blocking. Kept for clients that read a single note; prefer notes.
    /// </summary>
    [JsonPropertyName("note")]
    public string? Note { get; set; }

    [JsonPropertyName("notes")]
    public IEnumerable<PlanVersionMigrationPreviewNoteResponseData> Notes { get; set; } =
        new List<PlanVersionMigrationPreviewNoteResponseData>();

    [JsonPropertyName("plan_version_id_from")]
    public string? PlanVersionIdFrom { get; set; }

    [JsonPropertyName("will_update_subscription")]
    public required bool WillUpdateSubscription { get; set; }

    [JsonPropertyName("would_fail")]
    public required bool WouldFail { get; set; }

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
