using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record ZeroOutGrantRequestBody
{
    /// <summary>
    /// grant_id
    /// </summary>
    [JsonIgnore]
    public required string GrantId { get; set; }

    [JsonPropertyName("reason")]
    public ZeroOutGrantRequestBodyReason? Reason { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
