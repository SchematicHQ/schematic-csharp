using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

#nullable enable

namespace SchematicHQ.Client;

public record DeleteResponse
{
    /// <summary>
    /// Whether the delete was successful
    /// </summary>
    [JsonPropertyName("deleted")]
    public bool? Deleted { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
