using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record CreateDataExportRequestBody
{
    [JsonPropertyName("export_type")]
    public string ExportType { get; set; } = "company-feature-usage";

    [JsonPropertyName("metadata")]
    public required string Metadata { get; set; }

    [JsonPropertyName("output_file_type")]
    public string OutputFileType { get; set; } = "csv";

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
