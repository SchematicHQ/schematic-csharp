using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record CountFlagChecksParams
{
    [JsonPropertyName("flag_id")]
    public string? FlagId { get; init; }

    [JsonPropertyName("flag_ids")]
    public IEnumerable<string>? FlagIds { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    /// <summary>
    /// Page limit (default 100)
    /// </summary>
    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    /// <summary>
    /// Page offset (default 0)
    /// </summary>
    [JsonPropertyName("offset")]
    public int? Offset { get; init; }
}
