namespace SchematicHQ.Client;

public record PreviewComponentDataRequest
{
    public string? CompanyId { get; set; }

    public string? ComponentId { get; set; }
}
