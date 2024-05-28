using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public class CountFlagChecksParams
{
    [JsonPropertyName("flag_id")]
    public string? FlagId { get; init; }

    [JsonPropertyName("flag_ids")]
    public List<string>? FlagIds { get; init; }

    [JsonPropertyName("id")]
    public string? Id { get; init; }

    [JsonPropertyName("limit")]
    public int? Limit { get; init; }

    [JsonPropertyName("offset")]
    public int? Offset { get; init; }
}
