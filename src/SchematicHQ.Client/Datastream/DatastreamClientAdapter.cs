using Microsoft.Extensions.Logging;
using SchematicHQ.Client.RulesEngine;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SchematicHQ.Client;
using SchematicHQ.Client.Core;
using System.Net.WebSockets;

namespace SchematicHQ.Client.Datastream
{
    /// <summary>
    /// Client adapter for interfacing with the Schematic class
    /// </summary>
    public class DatastreamClientAdapter
    {
        private readonly DatastreamClient _client;
        private readonly ISchematicLogger _logger;

        private readonly ConnectionStateTracker _connectionTracker = new ConnectionStateTracker();


        /// <summary>
        /// Creates a new datastream client adapter
        /// </summary>
        public DatastreamClientAdapter(string baseUrl, ISchematicLogger logger, string apiKey, DatastreamOptions options)
        {
            _logger = logger;
            _client = new DatastreamClient(
                baseUrl,
                _logger,
                apiKey,
                _connectionTracker.UpdateConnectionState, // callback to update connection state
                options.CacheTTL,
                null, // default websocket client
                options
                );
        }

        /// <summary>
        /// Start the datastream client
        /// </summary>
        public void Start()
        {
            _client.Start();
        }

        /// <summary>
        /// Close the datastream client
        /// </summary>
        public void Close()
        {
            _client.Dispose();
        }

        /// <summary>
        /// Get a task that completes when the datastream connection is established
        /// </summary>
        /// <param name="timeout">Optional timeout for the connection check</param>
        /// <returns>A task that completes with true if connected, or false if timeout or not connected</returns>
        public async Task<bool> IsConnectedAsync(TimeSpan? timeout = null)
        {
            if (timeout.HasValue)
            {
                try
                {
                    bool isConnected = await _connectionTracker.WaitForConnectionAsync(timeout.Value);
                    _logger.Debug($"Datastream connection state checked: {isConnected}");
                    return isConnected;
                }
                catch (OperationCanceledException)
                {
                    // Timeout occurred
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.Error($"Error checking datastream connection: {ex.Message}");
                    return false;
                }
            }

            // No timeout - just return current state
            return _connectionTracker.IsConnected;
        }

        /// <summary>
        /// Check a feature flag via datastream
        /// </summary>
        public Task<CheckFlagResult> CheckFlag(Dictionary<string, string>? company, Dictionary<string, string>? user, string flagKey)
        {
            var request = new CheckFlagRequestBody
            {
                Company = company,
                User = user
            };

            return _client.CheckFlagAsync(request, flagKey);
        }
        
        private class ConnectionStateTracker
        {
            private bool _isConnected = false;
            private readonly SemaphoreSlim _stateLock = new SemaphoreSlim(1, 1);
            private TaskCompletionSource<bool>? _waitTask = null;

            /// <summary>
            /// Gets the current connection state
            /// </summary>
            public bool IsConnected => _isConnected;

            /// <summary>
            /// Update the connection state
            /// </summary>
            public void UpdateConnectionState(bool connected)
            {
                _stateLock.Wait();
                try
                {
                    if (_isConnected != connected)
                    {
                        _isConnected = connected;
                        
                        // If we have a waiting task and we're now connected, complete it
                        if (connected && _waitTask != null && !_waitTask.Task.IsCompleted)
                        {
                            _waitTask.TrySetResult(true);
                        }
                    }
                }
                finally
                {
                    _stateLock.Release();
                }
            }

            /// <summary>
            /// Wait for the connection to be established or timeout
            /// </summary>
            public async Task<bool> WaitForConnectionAsync(TimeSpan timeout)
            {
                // Fast path - already connected
                if (_isConnected)
                {
                    return true;
                }

                // Create a task to wait for connection
                TaskCompletionSource<bool> waitTask = new TaskCompletionSource<bool>();
                
                await _stateLock.WaitAsync();
                try
                {
                    // Check again under lock - might have connected
                    if (_isConnected)
                    {
                        return true;
                    }
                    
                    // Set the wait task
                    _waitTask = waitTask;
                }
                finally
                {
                    _stateLock.Release();
                }

                // Wait with timeout
                using var cts = new CancellationTokenSource(timeout);
                var completionTask = await Task.WhenAny(
                    waitTask.Task,
                    Task.Delay(Timeout.Infinite, cts.Token)
                );

                // If we get here with cancellation, it was a timeout
                if (cts.IsCancellationRequested)
                {
                    await _stateLock.WaitAsync();
                    try
                    {
                        if (_waitTask == waitTask)
                        {
                            _waitTask = null;
                        }
                    }
                    finally
                    {
                        _stateLock.Release();
                    }
                    return false;
                }

                return true;
            }
        }

        // Adapter to convert ISchematicLogger to ILogger for the DatastreamClient
        private class LoggerAdapter : ILogger<DatastreamClient>
        {
            private readonly ISchematicLogger _logger;

            public LoggerAdapter(ConsoleLogger logger)
            {
                _logger = logger;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return new NoopDisposable();
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
            {
                var message = formatter(state, exception);

                switch (logLevel)
                {
                    case LogLevel.Critical:
                    case LogLevel.Error:
                        _logger.Error(message);
                        break;
                    case LogLevel.Warning:
                        _logger.Warn(message);
                        break;
                    case LogLevel.Information:
                        _logger.Info(message);
                        break;
                    case LogLevel.Debug:
                    case LogLevel.Trace:
                        _logger.Debug(message);
                        break;
                    default:
                        _logger.Info(message);
                        break;
                }
            }

            // A no-operation disposable class
            private class NoopDisposable : IDisposable
            {
                public void Dispose() { }
            }
        }
    }
}
