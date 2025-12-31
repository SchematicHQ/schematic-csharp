using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CheckFlagsBulkRequestBody
{
    [JsonPropertyName("contexts")]
    public IEnumerable<CheckFlagRequestBody> Contexts { get; set; } =
        new List<CheckFlagRequestBody>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
