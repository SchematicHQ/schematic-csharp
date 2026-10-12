using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetPlanVersionDiffRequest
{
    /// <summary>
    /// The version to compare against. Defaults to the plan's published version.
    /// </summary>
    [JsonIgnore]
    public string? Against { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
