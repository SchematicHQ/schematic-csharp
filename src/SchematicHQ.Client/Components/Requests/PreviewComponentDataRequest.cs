namespace SchematicHQ.Client;

public record PreviewComponentDataRequest
{
    public string? CompanyId { get; init; }

    public string? ComponentId { get; init; }
}
