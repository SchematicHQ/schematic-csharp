using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[Serializable]
public record GetEntityTraitDefinitionRequest
{
    /// <summary>
    /// entity_trait_definition_id
    /// </summary>
    [JsonIgnore]
    public required string EntityTraitDefinitionId { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return JsonUtils.Serialize(this);
    }
}
