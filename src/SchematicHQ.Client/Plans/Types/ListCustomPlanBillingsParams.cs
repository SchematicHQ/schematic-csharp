using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

/// <summary>
/// Input parameters
/// </summary>
[Serializable]
public record ListCustomPlanBillingsParams : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    /// <summary>
    /// Filter by company ID
    /// </summary>
    [JsonPropertyName("company_id")]
    public string? CompanyId { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public long? Limit { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public long? Offset { get; set; }

    /// <summary>
    /// Filter by plan ID
    /// </summary>
    [JsonPropertyName("plan_id")]
    public string? PlanId { get; set; }

    /// <summary>
    /// Filter by billing status
    /// </summary>
    [JsonPropertyName("status")]
    public CustomPlanBillingStatus? Status { get; set; }

    /// <summary>
    /// Filter by multiple billing statuses
    /// </summary>
    [JsonPropertyName("statuses")]
    public IEnumerable<CustomPlanBillingStatus>? Statuses { get; set; }

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
