namespace SchematicHQ.Client;

public class CreateApiKeyRequestBody
{
    public string? Description { get; init; }

    public string? EnvironmentId { get; init; }

    public string Name { get; init; }
}
