using Schematic.Client;

namespace Schematic.Client;

public class CreatePlanRequestBody
{
    public string Description { get; init; }

    public string Name { get; init; }

    public CreatePlanRequestBodyPlanType PlanType { get; init; }
}
