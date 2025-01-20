using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record LookupUserParams
{
    [JsonPropertyName("keys")]
    public object? Keys { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
