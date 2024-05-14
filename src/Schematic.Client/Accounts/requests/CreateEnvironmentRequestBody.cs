using Schematic.Client;

namespace Schematic.Client;

public class CreateEnvironmentRequestBody
{
    public CreateEnvironmentRequestBodyEnvironmentType EnvironmentType { get; init; }

    public string Name { get; init; }
}
