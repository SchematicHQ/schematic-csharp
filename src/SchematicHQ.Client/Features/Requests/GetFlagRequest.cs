using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetFlagRequest
{
    /// <summary>
    /// flag_id
    /// </summary>
    [JsonIgnore]
    public required string FlagId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
