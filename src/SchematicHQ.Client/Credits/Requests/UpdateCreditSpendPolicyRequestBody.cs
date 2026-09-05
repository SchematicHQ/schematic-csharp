using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateCreditSpendPolicyRequestBody
{
    [JsonPropertyName("label")]
    public string? Label { get; set; }

    [JsonPropertyName("max_per_draw")]
    public double? MaxPerDraw { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
