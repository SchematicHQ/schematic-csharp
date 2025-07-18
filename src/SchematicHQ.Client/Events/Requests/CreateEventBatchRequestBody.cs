using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateEventBatchRequestBody
{
    [JsonPropertyName("events")]
    public IEnumerable<CreateEventRequestBody> Events { get; set; } =
        new List<CreateEventRequestBody>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
