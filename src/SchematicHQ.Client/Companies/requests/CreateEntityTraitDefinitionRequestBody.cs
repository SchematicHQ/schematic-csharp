using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreateEntityTraitDefinitionRequestBody
{
    public string? DisplayName { get; init; }

    public CreateEntityTraitDefinitionRequestBodyEntityType EntityType { get; init; }

    public List<string> Hierarchy { get; init; }

    public CreateEntityTraitDefinitionRequestBodyTraitType TraitType { get; init; }
}
