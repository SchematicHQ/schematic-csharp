using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateFlagRulesRequestBody
{
    public List<CreateOrUpdateRuleRequestBody> Rules { get; init; }
}
