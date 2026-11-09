using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateBillingEntitySettingsRequestBody
{
    /// <summary>
    /// Whether the companies this company pays for may change their own plan and add-ons in components. The change is billed to this company. When false, they may only cancel.
    /// </summary>
    [JsonPropertyName("children_may_change_plan")]
    public bool? ChildrenMayChangePlan { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
