using System.Text.Json;
using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record OrderedPlansInGroup
{
    [JsonPropertyName("entitlements")]
    public IEnumerable<EntitlementsInPlan>? Entitlements { get; set; }

    [JsonPropertyName("plan_id")]
    public required string PlanId { get; set; }

    /// <summary>
    /// Additional properties received from the response, if any.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement> AdditionalProperties { get; internal set; } =
        new Dictionary<string, JsonElement>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
