using Schematic.Client;

namespace Schematic.Client;

public class UpdateFeatureRequestBody
{
    public string? Description { get; init; }

    public string? EventSubtype { get; init; }

    public UpdateFeatureRequestBodyFeatureType? FeatureType { get; init; }

    public CreateOrUpdateFlagRequestBody? Flag { get; init; }

    public string? LifecyclePhase { get; init; }

    public string? Name { get; init; }

    public string? TraitId { get; init; }
}
