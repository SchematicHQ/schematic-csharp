using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdatePlanRequestBody
{
    public string? Description { get; init; }

    public string Name { get; init; }

    public UpdatePlanRequestBodyPlanType? PlanType { get; init; }
}
