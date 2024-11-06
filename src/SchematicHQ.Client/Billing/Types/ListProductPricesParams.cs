using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListProductPricesParams
{
    [JsonPropertyName("ids")]
    public IEnumerable<string>? Ids { get; set; }

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

    /// <summary>
    /// Filter products that are not linked to any plan
    /// </summary>
    [JsonPropertyName("without_linked_to_plan")]
    public bool? WithoutLinkedToPlan { get; set; }
}
