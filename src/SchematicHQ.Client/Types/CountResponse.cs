using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record CountResponse
{
    [JsonPropertyName("count")]
    public required int Count { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
