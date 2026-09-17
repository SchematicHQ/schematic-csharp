namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Keeps every event the client enqueues, so a test can read the billing event
/// a settle produced without standing up the capture service.
/// </summary>
public sealed class RecordingEventBuffer : IEventBuffer<CreateEventRequestBody>
{
    public List<CreateEventRequestBody> Events { get; } = new();

    public List<EventBodyTrack> Tracks =>
        Events
            .Where(e => e.EventType == EventType.Track && e.Body?.IsT0 == true)
            .Select(e => e.Body!.Value.AsT0)
            .ToList();

    public int FlushCount { get; private set; }

    public void Push(CreateEventRequestBody item) => Events.Add(item);

    public void Start() { }

    public Task Stop() => Task.CompletedTask;

    public Task Flush()
    {
        FlushCount++;
        return Task.CompletedTask;
    }

    public int GetEventCount() => Events.Count;
}
