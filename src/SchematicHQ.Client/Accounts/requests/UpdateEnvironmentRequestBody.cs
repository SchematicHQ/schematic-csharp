using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class UpdateEnvironmentRequestBody
{
    public UpdateEnvironmentRequestBodyEnvironmentType? EnvironmentType { get; init; }

    public string? Name { get; init; }
}
