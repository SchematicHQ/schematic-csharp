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
        /// Gets the cache version provided by the replicator
        /// </summary>
        string? CacheVersion { get; }

        /// <summary>
        /// Gets the cache version, performing an immediate health check if not available yet
        /// </summary>
        /// <returns>The cache version from the replicator, or null if not available</returns>
        Task<string?> GetCacheVersionAsync();

        /// <summary>
        /// Event raised when the cache version changes
        /// </summary>
        event Action<string?, string?>? CacheVersionChanged;

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
        private string? _cacheVersion = null;
        private bool _disposed = false;

        /// <inheritdoc/>
        public event Action<string?, string?>? CacheVersionChanged;

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
        public string? CacheVersion => _cacheVersion;

        /// <summary>
        /// Gets the cache version, performing an immediate health check if not available yet
        /// </summary>
        /// <returns>The cache version from the replicator, or null if not available</returns>
        public async Task<string?> GetCacheVersionAsync()
        {
            if (!string.IsNullOrEmpty(_cacheVersion))
                return _cacheVersion;

            // If we don't have a cache version yet, try to get it immediately
            try
            {
                await PerformHealthCheck();
                return _cacheVersion;
            }
            catch (Exception ex)
            {
                _logger.Debug("Failed to get cache version immediately: {0}", ex.Message);
                return null;
            }
        }

        /// <inheritdoc/>
        public void Start()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(ReplicatorHealthService));

            if (_healthCheckTask != null)
                return; // Already started

            _logger.Info("Starting replicator health check service for URL: {0}", _healthUrl);
            
            // Perform an immediate health check to get the cache version right away
            _ = Task.Run(async () =>
            {
                try
                {
                    await PerformHealthCheck();
                    _logger.Debug("Initial health check completed");
                }
                catch (Exception ex)
                {
                    _logger.Debug("Initial health check failed: {0}", ex.Message);
                }
            });
            
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
                
                // Read and parse the JSON response first
                var responseBody = await response.Content.ReadAsStringAsync(_cancellationTokenSource.Token);
                _logger.Debug("Raw health response: {0}", responseBody);
                
                var healthResponse = System.Text.Json.JsonSerializer.Deserialize<HealthResponse>(responseBody, new System.Text.Json.JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                _logger.Debug("Deserialized health response - Ready: {0}, CacheVersion: '{1}'", 
                    healthResponse?.Ready ?? false, healthResponse?.CacheVersion ?? "NULL");

                bool wasHealthy = _isHealthy;
                string? previousCacheVersion = _cacheVersion;
                
                // Replicator is ready only when:
                // 1. HTTP status is 200 OK (ready endpoint returns 503 when not ready)
                // 2. Ready field in response is true
                // This matches the Go replicator behavior exactly
                _isHealthy = response.IsSuccessStatusCode && healthResponse?.Ready == true;
                _cacheVersion = healthResponse?.CacheVersion;
                
                _logger.Debug("Updated cache version from '{0}' to '{1}'", 
                    previousCacheVersion ?? "NULL", _cacheVersion ?? "NULL");

                // Log state changes - matching Go client exactly
                if (_isHealthy && !wasHealthy)
                {
                    _logger.Info("External replicator is now ready");
                }
                else if (!_isHealthy && wasHealthy)
                {
                    _logger.Info("External replicator is no longer ready");
                }

                // Log readiness status for debugging
                if (!response.IsSuccessStatusCode)
                {
                    _logger.Debug("Replicator readiness check: HTTP {0}, Ready={1}", response.StatusCode, healthResponse?.Ready ?? false);
                }

                // Log cache version changes and notify subscribers
                if (_cacheVersion != previousCacheVersion)
                {
                    _logger.Info("Replicator cache version updated: {0} -> {1}", previousCacheVersion ?? "(null)", _cacheVersion ?? "(null)");
                    
                    // Notify subscribers of the cache version change
                    CacheVersionChanged?.Invoke(previousCacheVersion, _cacheVersion);
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
            [System.Text.Json.Serialization.JsonPropertyName("ready")]
            public bool Ready { get; set; }
            
            [System.Text.Json.Serialization.JsonPropertyName("cache_version")]
            public string? CacheVersion { get; set; }
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