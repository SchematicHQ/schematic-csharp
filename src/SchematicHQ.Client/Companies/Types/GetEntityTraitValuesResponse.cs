using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record GetEntityTraitValuesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<EntityTraitValue> Data { get; set; } = new List<EntityTraitValue>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required GetEntityTraitValuesParams Params { get; set; }
}
