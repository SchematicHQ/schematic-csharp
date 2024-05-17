using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreatePlanRequestBody
{
    public string Description { get; init; }

    public string Name { get; init; }

    public CreatePlanRequestBodyPlanType PlanType { get; init; }
}
