using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateDataExportRequestBody
{
    [JsonPropertyName("export_type")]
    public required DataExportType ExportType { get; set; }

    [JsonPropertyName("metadata")]
    public DataExportMetadata? Metadata { get; set; }

    [JsonPropertyName("output_file_type")]
    public required DataExportOutputFileType OutputFileType { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
