using System.Threading.Channels;
using Microsoft.Extensions.Logging;

#nullable enable

namespace SchematicHQ.Client;

public interface IEventBuffer<in T> where T: notnull
{
    void Push(T item);
    void Start();
    Task Stop();
    Task Flush();
    int GetEventCount();
}

public class EventBuffer<T> : IEventBuffer<T> where T : notnull
{
    private const int DefaultMaxSize = 100;
    private static readonly TimeSpan DefaultFlushPeriod = TimeSpan.FromMilliseconds(5000);
    private const int MaxWaitForBuffer = 3; //seconds to wait for event buffer to flush on Stop
    private const int MaxRetries = 3; // Maximum number of retry attempts
    private const double InitialRetryDelaySeconds = 1.0; // Initial retry delay in seconds

    private const int StateStopped = 0;
    private const int StateRunning = 1;

    private readonly int _maxSize;
    private readonly TimeSpan _flushPeriod;
    private readonly Func<List<T>, Task> _action;
    private readonly ILogger _logger;
    private readonly Channel<T> _channel;
    private readonly SemaphoreSlim _semaphore;
    private CancellationTokenSource _cts;
    private int _state;
    private Task _periodicFlushTask = Task.CompletedTask;
    private Task _processBufferTask = Task.CompletedTask;

    public EventBuffer(Func<List<T>, Task> action, ILogger logger, int maxSize = DefaultMaxSize, TimeSpan? flushPeriod = null)
    {
        _maxSize = maxSize;
        _flushPeriod = flushPeriod ?? DefaultFlushPeriod;
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _channel = Channel.CreateUnbounded<T>(new UnboundedChannelOptions
        {
            SingleReader = false,
            SingleWriter = false
        });
        _cts = new CancellationTokenSource();
        _state = StateStopped;
        _semaphore = new SemaphoreSlim(0);

        _logger.LogDebug("EventBuffer initialized with maxSize: {MaxSize}, flushPeriod: {FlushPeriod}", _maxSize, _flushPeriod);

        AppDomain.CurrentDomain.ProcessExit += async (s, e) => {
            await EmergencyFlush();
        };

        Console.CancelKeyPress += async (s, e) => {
            e.Cancel = true; // Prevent immediate termination
            await EmergencyFlush();
        };
    }

    private async Task EmergencyFlush()
    {
        try
        {
            _logger.LogDebug("Emergency flush triggered by program termination");
            var items = new List<T>(_channel.Reader.Count);
            while (_channel.Reader.TryRead(out var item))
            {
                items.Add(item);
            }

            if (items.Count > 0)
            {
                _logger.LogInformation("Emergency flushing {ItemCount} items", items.Count);
                await _action(items);
            }
            _logger.LogInformation("Emergency flush completed");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during emergency flush");
        }
    }

    public void Push(T item)
    {
        if (Volatile.Read(ref _state) != StateRunning)
            throw new InvalidOperationException("Buffer is not running.");

        if (!_channel.Writer.TryWrite(item))
            throw new InvalidOperationException("Failed to write item to buffer channel.");

        _logger.LogDebug("Item added to buffer. Current size: {QueueSize}", _channel.Reader.Count);
        
        if (_channel.Reader.Count >= _maxSize)
        {
            _semaphore.Release();
        }
    }

    public void Start()
    {
        if (Interlocked.CompareExchange(ref _state, StateRunning, StateStopped) != StateStopped)
            return;

        _cts = new CancellationTokenSource();
        _periodicFlushTask = Task.Run(() => PeriodicFlushAsync(_cts.Token));
        _processBufferTask = Task.Run(() => ProcessBufferAsync(_cts.Token));

        _logger.LogInformation("EventBuffer started.");
    }

    public async Task Flush()
    {
        if (Volatile.Read(ref _state) != StateRunning)
            throw new InvalidOperationException("Buffer is not running.");

        await DrainAsync();
        _logger.LogInformation("Buffer flushed manually.");
    }

    private async Task DrainAsync()
    {
        while (_channel.Reader.Count > 0)
        {
            await FlushBufferAsync();
        }
    }

    private async Task ProcessBufferAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await _semaphore.WaitAsync(token);
                
                await FlushBufferAsync();
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Process buffer task was canceled.");
            }
        }
    }

    private async Task PeriodicFlushAsync(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(_flushPeriod, token);

                if (_channel.Reader.Count > 0)
                {
                    await FlushBufferAsync();
                }
            }
            catch (OperationCanceledException)
            {
                _logger.LogWarning("Periodic flush task was canceled.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during periodic flush");
            }
        }
    }

    private async Task FlushBufferAsync()
    {
        var items = new List<T>(_channel.Reader.Count);
        while (items.Count < _maxSize && _channel.Reader.TryRead(out var item))
        {
            items.Add(item);
        }

        if (items.Count > 0)
        {
            _logger.LogInformation("Flushing buffer with {ItemCount} items.", items.Count);

            // Initialize retry counter and success flag
            int retryCount = 0;
            bool success = false;
            Exception? lastException = null;

            // Try with retries and exponential backoff
            while (retryCount <= MaxRetries && !success)
            {
                try
                {
                    if (retryCount > 0)
                    {
                        // Log retry attempt
                        _logger.LogInformation("Retrying event batch submission (attempt {Attempt} of {MaxRetries})", retryCount, MaxRetries);
                    }

                    // Attempt to send events
                    await _action(items);
                    success = true;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                    retryCount++;

                    if (retryCount <= MaxRetries)
                    {
                        // Calculate backoff with jitter
                        double baseDelay = InitialRetryDelaySeconds;
                        double delay = baseDelay * Math.Pow(2, retryCount - 1);
                        double jitter = Random.Shared.NextDouble() * 0.1 * delay; // 10% jitter
                        TimeSpan waitTime = TimeSpan.FromSeconds(delay + jitter);

                        _logger.LogWarning(ex,
                            "Event batch submission failed. Retrying in {WaitSeconds:0.##} seconds...",
                            waitTime.TotalSeconds);

                        // Wait before retry
                        await Task.Delay(waitTime);
                    }
                }
            }

            // After all retries, if still not successful, log the error
            if (!success)
            {
                _logger.LogError(lastException, "Event batch submission failed after {MaxRetries} retries", MaxRetries);
            }
            else if (retryCount > 0)
            {
                _logger.LogInformation("Event batch submission succeeded after {RetryCount} retries", retryCount);
            }
        }
    }

    public async Task Stop()
    {
        if (Interlocked.CompareExchange(ref _state, StateStopped, StateRunning) != StateRunning)
            return;

        try
        {
            await _cts.CancelAsync();                                                                                                                                                                                                  
            _channel.Writer.TryComplete();      
            
            try
            {
                await DrainAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarning("Error draining buffer on stop: {Message}", ex.Message);
            }

            var timeout = TimeSpan.FromSeconds(MaxWaitForBuffer);
            await Task.WhenAny(Task.WhenAll(_periodicFlushTask, _processBufferTask), Task.Delay(timeout));

            _cts.Dispose();
            _semaphore.Dispose();
            _logger.LogInformation("EventBuffer shut down cleanly.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during shutdown");
            throw;
        }
    }

    public int GetEventCount()
    {
        return _channel.Reader.Count;
    }
}
