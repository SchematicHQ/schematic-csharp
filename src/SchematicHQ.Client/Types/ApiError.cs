using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ApiError
{
    /// <summary>
    /// Error message
    /// </summary>
    [JsonPropertyName("error")]
    public required string Error { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
