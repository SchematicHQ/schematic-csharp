using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

public record ListEntityKeyDefinitionsResponse
{
    /// <summary>
    /// The returned resources
    /// </summary>
    [JsonPropertyName("data")]
    public IEnumerable<EntityKeyDefinitionResponseData> Data { get; set; } =
        new List<EntityKeyDefinitionResponseData>();

    /// <summary>
    /// Input parameters
    /// </summary>
    [JsonPropertyName("params")]
    public required ListEntityKeyDefinitionsParams Params { get; set; }

    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
