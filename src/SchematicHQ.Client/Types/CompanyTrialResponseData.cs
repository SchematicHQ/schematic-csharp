using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CompanyTrialResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    /// <summary>
    /// What happens when the trial reaches its end date: subscribe starts billing, cancel moves the company off the plan.
    /// </summary>
    [JsonPropertyName("end_behavior")]
    public required BillingSubscriptionTrialEndSetting EndBehavior { get; set; }

    /// <summary>
    /// When the trial actually ended. Null while it runs.
    /// </summary>
    [JsonPropertyName("ended_at")]
    public DateTime? EndedAt { get; set; }

    /// <summary>
    /// When the trial is set to end.
    /// </summary>
    [JsonPropertyName("ends_at")]
    public required DateTime EndsAt { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    [JsonPropertyName("plan_version_id")]
    public string? PlanVersionId { get; set; }

    /// <summary>
    /// Where the trial was started.
    /// </summary>
    [JsonPropertyName("source")]
    public required TrialSource Source { get; set; }

    [JsonPropertyName("started_at")]
    public required DateTime StartedAt { get; set; }

    /// <summary>
    /// active while the trial runs, then converted, expired or canceled.
    /// </summary>
    [JsonPropertyName("status")]
    public required CompanyTrialStatus Status { get; set; }

    [JsonPropertyName("subscription_id")]
    public string? SubscriptionId { get; set; }

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
