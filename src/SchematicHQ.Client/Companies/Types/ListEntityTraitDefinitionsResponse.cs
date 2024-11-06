using System.Text.Json.Serialization;

#nullable enable

namespace SchematicHQ.Client;

public record ListEntityTraitDefinitionsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<EntityTraitDefinitionResponseData> Data { get; set; } =
        new List<EntityTraitDefinitionResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListEntityTraitDefinitionsParams Params { get; set; }
}
