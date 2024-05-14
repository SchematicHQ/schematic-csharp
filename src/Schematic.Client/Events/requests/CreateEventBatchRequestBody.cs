using Schematic.Client;

namespace Schematic.Client;

public class CreateEventBatchRequestBody
{
    public List<CreateEventRequestBody> Events { get; init; }
}
