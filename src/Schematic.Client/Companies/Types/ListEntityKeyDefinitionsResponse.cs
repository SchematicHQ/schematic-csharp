using System.Text.Json.Serialization;
using Schematic.Client;

namespace Schematic.Client;

public class ListEntityKeyDefinitionsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public List<EntityKeyDefinitionResponseData> Data { get; init; }

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public ListEntityKeyDefinitionsParams Params { get; init; }
}
