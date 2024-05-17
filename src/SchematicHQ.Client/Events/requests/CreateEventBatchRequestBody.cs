using SchematicHQ.Client;

namespace SchematicHQ.Client;

public class CreateEventBatchRequestBody
{
    public List<CreateEventRequestBody> Events { get; init; }
}
