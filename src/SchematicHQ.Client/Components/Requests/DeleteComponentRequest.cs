using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteComponentRequest
{
    /// <summary>
    /// component_id
    /// </summary>
    [JsonIgnore]
    public required string ComponentId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
