using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record CreateEventBatchRequestBody
{
    [JsonPropertyName("events")]
    public IEnumerable<CreateEventRequestBody> Events { get; set; } =
        new List<CreateEventRequestBody>();

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
