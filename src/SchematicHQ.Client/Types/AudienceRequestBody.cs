using System.Text.Json.Serialization;
using SchematicHQ.Client;

#nullable enable

namespace SchematicHQ.Client;

public class AudienceRequestBody
{
    [JsonPropertyName("condition_groups")]
    public IEnumerable<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; }

    [JsonPropertyName("conditions")]
    public IEnumerable<CreateOrUpdateConditionRequestBody> Conditions { get; init; }

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

    [JsonPropertyName("q")]
    public string? Q { get; init; }
}
