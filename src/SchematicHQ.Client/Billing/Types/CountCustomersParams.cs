using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountCustomersParams
{
    [JsonPropertyName("failed_to_import")]
    public bool? FailedToImport { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}