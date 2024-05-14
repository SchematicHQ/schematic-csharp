using Schematic.Client;

namespace Schematic.Client;

public class UpdateEnvironmentRequestBody
{
    public UpdateEnvironmentRequestBodyEnvironmentType? EnvironmentType { get; init; }

    public string? Name { get; init; }
}
