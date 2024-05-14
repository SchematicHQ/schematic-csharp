using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class UpdateEntityTraitDefinitionResponse
{
    [JsonPropertyName("data")]
    public EntityTraitDefinitionResponseData Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public Dictionary<string, object> Params { get; init; }
}
