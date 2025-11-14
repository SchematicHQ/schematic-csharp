using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetApiRequestRequest
{
    /// <summary>
    /// api_request_id
    /// </summary>
    [JsonIgnore]
    public required string ApiRequestId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
