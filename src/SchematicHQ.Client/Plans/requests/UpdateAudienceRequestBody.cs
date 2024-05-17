using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateAudienceRequestBody
{
    public List<CreateOrUpdateConditionGroupRequestBody> ConditionGroups { get; init; }

    public List<CreateOrUpdateConditionRequestBody> Conditions { get; init; }
}