using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetDataExportArtifactRequest
{
    /// <summary>
    /// data_export_id
    /// </summary>
    [JsonIgnore]
    public required string DataExportId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
