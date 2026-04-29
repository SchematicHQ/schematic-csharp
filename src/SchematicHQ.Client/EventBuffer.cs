using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;

#nullable enable

namespace SchematicHQ.Client;

public interface IEventBuffer<T>
{
    void Push(T item);
    void Start();
    Task Stop();
    Task Flush();
    int GetEventCount();
}

public class EventBuffer<T> : IEventBuffer<T>
{
    private const int DefaultMaxSize = 100;
    private static readonly TimeSpan DefaultFlushPeriod = TimeSpan.FromMilliseconds(5000);
    private const int MaxWaitForBuffer = 3; //seconds to wait for event buffer to flush on Stop
    private const int MaxRetries = 3; // Maximum number of retry attempts
    private const double InitialRetryDelaySeconds = 1.0; // Initial retry delay in seconds

    private readonly int _maxSize;
    private readonly TimeSpan _flushPeriod;
    private readonly Func<List<T>, Task> _action;
    private readonly ILogger _logger;
    private readonly ConcurrentQueue<T> _queue;
    private readonly SemaphoreSlim _semaphore;
    private CancellationTokenSource _cts;
    private bool _isRunning;
    private Task _periodicFlushTask = Task.CompletedTask;
    private Task _processBufferTask = Task.CompletedTask;
    private readonly object _taskLock = new object();
    private readonly object _runningLock = new object();

    public EventBuffer(Func<List<T>, Task> action, ILogger logger, int maxSize = DefaultMaxSize, TimeSpan? flushPeriod = null)
    {
        _maxSize = maxSize;
        _flushPeriod = flushPeriod ?? DefaultFlushPeriod;
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _queue = new ConcurrentQueue<T>();
        _semaphore = new SemaphoreSlim(0);
        _cts = new CancellationTokenSource();
        _isRunning = false;

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
            // Don't check _isRunning here since we're in an emergency shutdown
            var items = new List<T>();
            while (_queue.TryDequeue(out var item))
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
        lock (_runningLock)
        {
            if (!_isRunning) throw new InvalidOperationException("Buffer is not running.");
        }

        _queue.Enqueue(item);
        _logger.LogDebug("Item added to buffer. Current size: {QueueSize}", _queue.Count);
        if (_queue.Count >= _maxSize)
        {
            _semaphore.Release();
        }
    }

    public void Start()
    {
        lock (_runningLock)
        {
            if (_isRunning) return;

            _isRunning = true;
            _cts = new CancellationTokenSource();
        }

        lock (_taskLock)
        {
            if (_periodicFlushTask == Task.CompletedTask || _periodicFlushTask.IsCompleted)
            {
                _periodicFlushTask = Task.Run(() => PeriodicFlushAsync(_cts.Token));
            }
            if (_processBufferTask == Task.CompletedTask || _processBufferTask.IsCompleted)
            {
                _processBufferTask = Task.Run(() => ProcessBufferAsync(_cts.Token));
            }
        }

        _logger.LogInformation("EventBuffer started.");
    }

    public async Task Flush()
    {
        lock (_runningLock)
        {
            if (!_isRunning) throw new InvalidOperationException("Buffer is not running.");
        }

        while (!_queue.IsEmpty)
        {
            await FlushBufferAsync();
        }
        _logger.LogInformation("Buffer flushed manually.");
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

                if (_queue.Count > 0)
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
        var items = new List<T>();
        while (items.Count < _maxSize && _queue.TryDequeue(out var item))
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
            Random random = new Random();

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
                        double jitter = random.NextDouble() * 0.1 * delay; // 10% jitter
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
        bool needsFlush = false;
        
        lock (_runningLock)
        {
            if (!_isRunning) return;
            needsFlush = true;
        }

        try
        {
            if (needsFlush)
            {
                try
                {
                    await Flush();
                }
                catch (InvalidOperationException)
                {
                    // Another thread might have changed the state between our check and the flush
                    // Just continue with shutdown
                }
            }

            lock (_runningLock)
            {
                if (!_isRunning) return;
                _isRunning = false;
                _cts.Cancel();
            }

            if (_periodicFlushTask != Task.CompletedTask)
            {
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(MaxWaitForBuffer));
                await Task.WhenAny(_periodicFlushTask, timeoutTask);
            }

            if (_processBufferTask != Task.CompletedTask)
            {
                var timeoutTask = Task.Delay(TimeSpan.FromSeconds(MaxWaitForBuffer));
                await Task.WhenAny(_processBufferTask, timeoutTask);
            }

            _semaphore.Dispose();
            _cts.Dispose();
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
        return _queue.Count;
    }
}
