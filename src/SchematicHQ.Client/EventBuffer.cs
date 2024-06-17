using System;
using System.Collections.Generic;
using System.Threading;

#nullable enable

namespace SchematicHQ.Client;

public class EventBuffer : IDisposable
{
    private readonly EventsClient _eventsApi;
    private readonly ISchematicLogger _logger;
    private readonly int _interval;
    private readonly int _maxSize;
    private readonly List<CreateEventRequestBody> _events;
    private int _currentSize;
    private readonly object _flushLock = new object();
    private readonly object _pushLock = new object();
    private readonly CancellationTokenSource _cancellationTokenSource;
    private bool _stopped;

    public EventBuffer(EventsClient eventsApi, ISchematicLogger logger, int? period = null)
    {
        _eventsApi = eventsApi;
        _logger = logger;
        _interval = period ?? DEFAULT_EVENT_BUFFER_PERIOD;
        _maxSize = DEFAULT_BUFFER_MAX_SIZE;
        _events = new List<CreateEventRequestBody>();
        _cancellationTokenSource = new CancellationTokenSource();

        var flushThread = new Thread(PeriodicFlush);
        flushThread.IsBackground = true;
        flushThread.Start();
    }

    private async Task Flush()
    {
        List<CreateEventRequestBody> eventsToFlush;
        lock (_flushLock)
        {
            if (_events.Count == 0)
                return;

            eventsToFlush = _events.FindAll(e => e != null);
            _events.Clear();
            _currentSize = 0;
        }

        try
        {
            var request = new CreateEventBatchRequestBody
            {
                Events = eventsToFlush
            };
            await _eventsApi.CreateEventBatchAsync(request);
        }
        catch (Exception ex)
        {
            _logger.Error("Error flushing events: {0}", ex.Message);
        }
    }

    private void PeriodicFlush()
    {
        while (!_cancellationTokenSource.IsCancellationRequested)
        {
            Task.Run(async () => await Flush());
            _cancellationTokenSource.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(_interval));
        }
    }

    public void Push(CreateEventRequestBody @event)
    {
        if (_stopped)
        {
            _logger.Error("Event buffer is stopped, not accepting new events");
            return;
        }

        lock (_pushLock)
        {
            if (_currentSize + 1 > _maxSize)
                Task.Run(async () => await Flush());

            _events.Add(@event);
            _currentSize += 1;
        }
    }

    public void Stop()
    {
        try
        {
            _stopped = true;
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Token.WaitHandle.WaitOne(TimeSpan.FromSeconds(5));
        }
        catch (Exception ex)
        {
            _logger.Error("Error stopping event buffer: {0}", ex.Message);
        }
    }

    public void Dispose()
    {
        Stop();
    }

    private const int DEFAULT_BUFFER_MAX_SIZE = 100; // Flush after 100 events
    private const int DEFAULT_EVENT_BUFFER_PERIOD = 5; // Flush after 5 seconds
}
