using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreateEnvironmentRequestBody
{
    public CreateEnvironmentRequestBodyEnvironmentType EnvironmentType { get; init; }

    public string Name { get; init; }
}
