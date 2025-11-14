using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CheckFlagRequest
{
    /// <summary>
    /// key
    /// </summary>
    [JsonIgnore]
    public required string Key { get; set; }

    [JsonIgnore]
    public required CheckFlagRequestBody Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
