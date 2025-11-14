using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record DeleteFeatureRequest
{
    /// <summary>
    /// feature_id
    /// </summary>
    [JsonIgnore]
    public required string FeatureId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
