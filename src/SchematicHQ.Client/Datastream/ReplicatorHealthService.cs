using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client.Datastream
{
    /// <summary>
    /// Service for monitoring the health of a replicator instance
    /// </summary>
    public interface IReplicatorHealthService : IDisposable
    {
        /// <summary>
        /// Gets whether the replicator is currently healthy
        /// </summary>
        bool IsHealthy { get; }

        /// <summary>
        /// Starts the health check monitoring
        /// </summary>
        void Start();

        /// <summary>
        /// Stops the health check monitoring
        /// </summary>
        void Stop();
    }

    /// <summary>
    /// Implementation of the replicator health service
    /// </summary>
    internal class ReplicatorHealthService : IReplicatorHealthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _healthUrl;
        private readonly ISchematicLogger _logger;
        private readonly TimeSpan _checkInterval;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private Task? _healthCheckTask;
        private bool _isHealthy = false;
        private bool _disposed = false;

        /// <summary>
        /// Creates a new instance of the replicator health service
        /// </summary>
        /// <param name="httpClient">HTTP client for making health check requests</param>
        /// <param name="healthUrl">URL to check for replicator health</param>
        /// <param name="logger">Logger for diagnostics</param>
        /// <param name="checkInterval">Interval between health checks (default 30 seconds)</param>
        public ReplicatorHealthService(
            HttpClient httpClient,
            string healthUrl,
            ISchematicLogger logger,
            TimeSpan? checkInterval = null)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _healthUrl = healthUrl ?? throw new ArgumentNullException(nameof(healthUrl));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _checkInterval = checkInterval ?? TimeSpan.FromSeconds(30);
            _cancellationTokenSource = new CancellationTokenSource();
            
            // Set timeout to match Go client (5 seconds)
            _httpClient.Timeout = TimeSpan.FromSeconds(5);
        }

        /// <inheritdoc/>
        public bool IsHealthy => _isHealthy;

        /// <inheritdoc/>
        public void Start()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ReplicatorHealthService));

            if (_healthCheckTask != null)
                return; // Already started

            _logger.Info("Starting replicator health check service for URL: {0}", _healthUrl);
            _healthCheckTask = Task.Run(HealthCheckLoop, _cancellationTokenSource.Token);
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (_disposed || _healthCheckTask == null)
                return;

            _logger.Info("Stopping replicator health check service");
            _cancellationTokenSource.Cancel();

            try
            {
                _healthCheckTask.Wait(TimeSpan.FromSeconds(5));
            }
            catch (AggregateException ex) when (ex.InnerException is OperationCanceledException)
            {
                // Expected when cancelling
            }
            catch (Exception ex)
            {
                _logger.Error("Error stopping health check service: {0}", ex.Message);
            }

            _healthCheckTask = null;
            _isHealthy = false;
        }

        private async Task HealthCheckLoop()
        {
            while (!_cancellationTokenSource.Token.IsCancellationRequested)
            {
                try
                {
                    await PerformHealthCheck();
                }
                catch (Exception ex)
                {
                    _logger.Error("Error during health check: {0}", ex.Message);
                    _isHealthy = false;
                }

                try
                {
                    await Task.Delay(_checkInterval, _cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    break; // Exit loop on cancellation
                }
            }
        }

        private async Task PerformHealthCheck()
        {
            try
            {
                using var response = await _httpClient.GetAsync(_healthUrl, _cancellationTokenSource.Token);
                
                if (!response.IsSuccessStatusCode)
                {
                    bool wasHealthyBefore = _isHealthy;
                    _isHealthy = false;
                    
                    if (wasHealthyBefore)
                    {
                        _logger.Warn("Replicator health check failed with status: {0}", response.StatusCode);
                    }
                    return;
                }

                // Read and parse the JSON response
                var responseBody = await response.Content.ReadAsStringAsync(_cancellationTokenSource.Token);
                
                var healthResponse = System.Text.Json.JsonSerializer.Deserialize<HealthResponse>(responseBody, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                bool wasHealthy = _isHealthy;
                _isHealthy = healthResponse?.Ready == true;

                // Log state changes - matching Go client exactly
                if (_isHealthy && !wasHealthy)
                {
                    _logger.Info("External replicator is now ready");
                }
                else if (!_isHealthy && wasHealthy)
                {
                    _logger.Info("External replicator is no longer ready");
                }
            }
            catch (OperationCanceledException)
            {
                // Expected during shutdown
                throw;
            }
            catch (System.Text.Json.JsonException ex)
            {
                bool wasHealthyBefore = _isHealthy;
                _isHealthy = false;
                
                if (wasHealthyBefore)
                {
                    _logger.Warn("Failed to parse replicator health response: {0}", ex.Message);
                }
            }
            catch (Exception ex)
            {
                bool wasHealthyBefore = _isHealthy;
                _isHealthy = false;
                
                if (wasHealthyBefore)
                {
                    _logger.Debug("Replicator health check failed: {0}", ex.Message);
                }
            }
        }

        private class HealthResponse
        {
            public bool Ready { get; set; }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            if (_disposed)
                return;

            Stop();
            _cancellationTokenSource.Dispose();
            _disposed = true;
        }
    }
}