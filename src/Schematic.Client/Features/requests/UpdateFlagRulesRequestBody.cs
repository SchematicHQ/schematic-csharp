using Schematic.Client;

namespace Schematic.Client;

public class UpdateFlagRulesRequestBody
{
    public List<CreateOrUpdateRuleRequestBody> Rules { get; init; }
}
