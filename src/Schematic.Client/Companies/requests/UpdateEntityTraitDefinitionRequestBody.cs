using Schematic.Client;

namespace Schematic.Client;

public class UpdateEntityTraitDefinitionRequestBody
{
    public string? DisplayName { get; init; }

    public UpdateEntityTraitDefinitionRequestBodyTraitType TraitType { get; init; }
}
