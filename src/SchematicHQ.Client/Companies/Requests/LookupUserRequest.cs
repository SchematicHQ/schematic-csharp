using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record LookupUserRequest
{
    /// <summary>
    /// Key/value pairs
    /// </summary>
    [JsonIgnore]
    public object Keys { get; set; } = new Dictionary<string, object?>();

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
