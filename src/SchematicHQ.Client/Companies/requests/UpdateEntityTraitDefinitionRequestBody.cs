using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateEntityTraitDefinitionRequestBody
{
    public string? DisplayName { get; init; }

    public UpdateEntityTraitDefinitionRequestBodyTraitType TraitType { get; init; }
}
