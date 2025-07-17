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
        private readonly TaskCompletionSource<bool> _connectionMonitor;
        private readonly ISchematicLogger _logger;

        /// <summary>
        /// Creates a new datastream client adapter
        /// </summary>
        public DatastreamClientAdapter(string baseUrl, ISchematicLogger logger, string apiKey, DatastreamOptions options)
        {
            _logger = logger;
            _connectionMonitor = new TaskCompletionSource<bool>();
            _client = new DatastreamClient(
                baseUrl,
                _logger,
                apiKey,
                _connectionMonitor,
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
            // If timeout is specified, we'll wait at most that long for a connection
            if (timeout.HasValue)
            {
                try
                {
                    // Create a cancellation token that expires after the timeout
                    using var cts = new CancellationTokenSource(timeout.Value);
                    // Wait for the connection monitor to complete or timeout
                    return await _connectionMonitor.Task.WaitAsync(cts.Token).ConfigureAwait(false);
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

            // No timeout specified, check if the task has completed
            return _connectionMonitor.Task.IsCompleted && _connectionMonitor.Task.Result;
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
