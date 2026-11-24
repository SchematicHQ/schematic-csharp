using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Exports one row per user, oldest first. Omitting every filter exports every user in the environment.
/// </summary>
[Serializable]
public record UserExportMetadata : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Restrict the export to members of this company ID (starting with 'comp_')
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// User key definition IDs (starting with 'ekt_') to include as columns, one per key
    /// </summary>
    [JsonPropertyName("entity_key_definition_ids")]
    public IEnumerable<string>? EntityKeyDefinitionIds { get; set; }

    /// <summary>
    /// User trait definition IDs (starting with 'trt_') to include as columns, one per trait
    /// </summary>
    [JsonPropertyName("entity_trait_definition_ids")]
    public IEnumerable<string>? EntityTraitDefinitionIds { get; set; }

    /// <summary>
    /// Restrict the export to these user IDs (starting with 'user_')
    /// </summary>
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

    /// <summary>
    /// Account member emails to notify when the export completes; empty means the artifact is only retrievable via the API
    /// </summary>
    [JsonPropertyName("notification_email_recipient_email_addresses")]
    public IEnumerable<string>? NotificationEmailRecipientEmailAddresses { get; set; }

    /// <summary>
    /// Restrict the export to users in a company on this plan or add-on ID (starting with 'plan_')
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    /// <summary>
    /// Search users by name, keys or string traits
    /// </summary>
    [JsonPropertyName("q")]
    public string? Q { get; set; }

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
