using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateBillingLinkedPlanRequestBody
{
    [JsonPropertyName("billing_provider")]
    public required BillingProviderType BillingProvider { get; set; }

    [JsonPropertyName("description")]
    public required string Description { get; set; }

    [JsonPropertyName("external_resource_id")]
    public required string ExternalResourceId { get; set; }

    [JsonPropertyName("icon")]
    public PlanIcon? Icon { get; set; }

    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("plan_type")]
    public required PlanType PlanType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
