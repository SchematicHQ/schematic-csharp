using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteApiKeyRequest
{
    /// <summary>
    /// api_key_id
    /// </summary>
    [JsonIgnore]
    public required string ApiKeyId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
