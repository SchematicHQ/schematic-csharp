using Schematic.Client;

namespace Schematic.Client;

public class CreateFeatureRequestBody
{
    public string Description { get; init; }

    public string? EventSubtype { get; init; }

    public CreateFeatureRequestBodyFeatureType FeatureType { get; init; }

    public CreateOrUpdateFlagRequestBody? Flag { get; init; }

    public string? LifecyclePhase { get; init; }

    public string Name { get; init; }

    public string? TraitId { get; init; }
}
