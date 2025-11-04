using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record PlanChangeResponseData : IJsonOnDeserialized
{
    [JsonExtensionData]
    private readonly IDictionary<string, JsonElement> _extensionData =
        new Dictionary<string, JsonElement>();

    [JsonPropertyName("action")]
    public required PlanChangeResponseDataAction Action { get; set; }

    [JsonPropertyName("actor_type")]
    public required PlanChangeResponseDataActorType ActorType { get; set; }

    [JsonPropertyName("add_ons_added")]
    public IEnumerable<PlanSnapshotView> AddOnsAdded { get; set; } = new List<PlanSnapshotView>();

    [JsonPropertyName("add_ons_removed")]
    public IEnumerable<PlanSnapshotView> AddOnsRemoved { get; set; } = new List<PlanSnapshotView>();

    [JsonPropertyName("api_key")]
    public ApiKeyResponseData? ApiKey { get; set; }

    [JsonPropertyName("api_key_request")]
    public ApiKeyRequestListResponseData? ApiKeyRequest { get; set; }

    [JsonPropertyName("base_plan")]
    public PlanSnapshotView? BasePlan { get; set; }

    /// <summary>
    /// Any special behavior that affected the assignment of the base plan during this change.
    /// </summary>
    [JsonPropertyName("base_plan_action")]
    public PlanChangeResponseDataBasePlanAction? BasePlanAction { get; set; }

    [JsonPropertyName("company")]
    public CompanyResponseData? Company { get; set; }

    [JsonPropertyName("company_id")]
    public required string CompanyId { get; set; }

    [JsonPropertyName("created_at")]
    public required DateTime CreatedAt { get; set; }

    [JsonPropertyName("environment_id")]
    public required string EnvironmentId { get; set; }

    [JsonPropertyName("id")]
    public required string Id { get; set; }

    [JsonPropertyName("previous_base_plan")]
    public PlanSnapshotView? PreviousBasePlan { get; set; }

    [JsonPropertyName("request_id")]
    public string? RequestId { get; set; }

    /// <summary>
    /// If a subscription was changed as a part of this plan change, indicates the type of change that was made.
    /// </summary>
    [JsonPropertyName("subscription_change_action")]
    public PlanChangeResponseDataSubscriptionChangeAction? SubscriptionChangeAction { get; set; }

    /// <summary>
    /// Any traits were updated as part of this plan change (via pay-in-advance entitlements).
    /// </summary>
    [JsonPropertyName("traits_updated")]
    public IEnumerable<SubscriptionTraitUpdate> TraitsUpdated { get; set; } =
        new List<SubscriptionTraitUpdate>();

    [JsonPropertyName("updated_at")]
    public required DateTime UpdatedAt { get; set; }

    [JsonPropertyName("user_id")]
    public string? UserId { get; set; }

    [JsonPropertyName("user_name")]
    public string? UserName { get; set; }

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
