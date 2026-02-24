using System.Collections.Concurrent;

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
    private static readonly TimeSpan DefaultFlushPeriod = TimeSpan.FromMilliseconds(3000);
    private const int MaxWaitForBuffer = 3; //seconds to wait for event buffer to flush on Stop
    private const int MaxRetries = 3; // Maximum number of retry attempts
    private const double InitialRetryDelaySeconds = 1.0; // Initial retry delay in seconds

    private readonly int _maxSize;
    private readonly TimeSpan _flushPeriod;
    private readonly Func<List<T>, Task> _action;
    private readonly ISchematicLogger _logger;
    private readonly ConcurrentQueue<T> _queue;
    private readonly SemaphoreSlim _semaphore;
    private CancellationTokenSource _cts;
    private bool _isRunning;
    private Task _periodicFlushTask = Task.CompletedTask;
    private Task _processBufferTask = Task.CompletedTask;
    private readonly object _taskLock = new object();
    private readonly object _runningLock = new object();

    public EventBuffer(Func<List<T>, Task> action, ISchematicLogger logger, int maxSize = DefaultMaxSize, TimeSpan? flushPeriod = null)
    {
        _maxSize = maxSize;
        _flushPeriod = flushPeriod ?? DefaultFlushPeriod;
        _action = action ?? throw new ArgumentNullException(nameof(action));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _queue = new ConcurrentQueue<T>();
        _semaphore = new SemaphoreSlim(0);
        _cts = new CancellationTokenSource();
        _isRunning = false;

        _logger.Debug("EventBuffer initialized with maxSize: {0}, flushPeriod: {1}", _maxSize, _flushPeriod);

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
            _logger.Debug("Emergency flush triggered by program termination");
            // Don't check _isRunning here since we're in an emergency shutdown
            var items = new List<T>();
            while (_queue.TryDequeue(out var item))
            {
                items.Add(item);
            }

            if (items.Count > 0)
            {
                _logger.Info("Emergency flushing {0} items", items.Count);
                await _action(items);
            }
            _logger.Info("Emergency flush completed");
        }
        catch (Exception ex)
        {
            _logger.Error("Error during emergency flush: {0}", ex.Message);
        }
    }

    public void Push(T item)
    {
        lock (_runningLock)
        {
            if (!_isRunning) throw new InvalidOperationException("Buffer is not running.");
        }

        _queue.Enqueue(item);
        _logger.Debug("Item added to buffer. Current size: {0}", _queue.Count);
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

        _logger.Info("EventBuffer started.");
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
        _logger.Info("Buffer flushed manually.");
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
                _logger.Warn("Process buffer task was canceled.");
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
                _logger.Warn("Periodic flush task was canceled.");
            }
            catch (Exception ex)
            {
                _logger.Error("An error occurred during periodic flush: {0}", ex.Message);
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
            _logger.Info("Flushing buffer with {0} items.", items.Count);

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
                        _logger.Info("Retrying event batch submission (attempt {0} of {1})", retryCount, MaxRetries);
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

                        _logger.Warn(
                            string.Format("Event batch submission failed: {0}. Retrying in {1:0.##} seconds...",
                            ex.Message, waitTime.TotalSeconds)
                        );

                        // Wait before retry
                        await Task.Delay(waitTime);
                    }
                }
            }

            // After all retries, if still not successful, log the error
            if (!success)
            {
                _logger.Error("Event batch submission failed after {0} retries: {1}", MaxRetries, lastException?.Message ?? "Unknown error");
            }
            else if (retryCount > 0)
            {
                _logger.Info("Event batch submission succeeded after {0} retries", retryCount);
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
            _logger.Info("EventBuffer shut down cleanly.");
        }
        catch (Exception ex)
        {
            _logger.Error("Error during shutdown: {0}", ex.Message);
            throw;
        }
    }

    public int GetEventCount()
    {
        return _queue.Count;
    }
}
