using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListCustomersParams
{
    [JsonPropertyName("failed_to_import")]
    public bool? FailedToImport { get; set; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; set; }

    [JsonPropertyName("q")]
    public string? Q { get; set; }
}
