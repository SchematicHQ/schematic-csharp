using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class GetEntityTraitValuesResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<EntityTraitValue> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public GetEntityTraitValuesParams Params { get; init; }
}
