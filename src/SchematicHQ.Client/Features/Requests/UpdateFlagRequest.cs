using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record UpdateFlagRequest
{
    /// <summary>
    /// flag_id
    /// </summary>
    [JsonIgnore]
    public required string FlagId { get; set; }

    [JsonIgnore]
    public required CreateFlagRequestBody Body { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
