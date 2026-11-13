using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger _logger;
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
            ILogger logger,
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
                _logger.LogDebug(ex, "Failed to get cache version immediately");
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

            _logger.LogInformation("Starting replicator health check service for URL: {HealthUrl}", _healthUrl);
            
            // Perform an immediate health check to get the cache version right away
            _ = Task.Run(async () =>
            {
                try
                {
                    await PerformHealthCheck();
                    _logger.LogDebug("Initial health check completed");
                }
                catch (Exception ex)
                {
                    _logger.LogDebug(ex, "Initial health check failed");
                }
            });
            
            _healthCheckTask = Task.Run(HealthCheckLoop, _cancellationTokenSource.Token);
        }

        /// <inheritdoc/>
        public void Stop()
        {
            if (_disposed || _healthCheckTask == null)
                return;

            _logger.LogInformation("Stopping replicator health check service");
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
                _logger.LogError(ex, "Error stopping health check service");
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
                catch (OperationCanceledException) when (_cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error during health check");
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

        /// <summary>
        /// Polls the replicator once. Readiness and the cache version change only on an
        /// explicit answer: a JSON body with a boolean <c>ready</c> field, whatever the HTTP
        /// status (the replicator answers 503 with <c>ready: false</c> while its cache loads).
        /// A poll that gets no such answer (network error, timeout, or a body that is not
        /// JSON or has no boolean <c>ready</c>) keeps the readiness and cache version from the
        /// last answer. A missed poll says nothing about whether the cache is complete, and
        /// treating it as not ready would send every flag check to the API on a replicator blip.
        /// </summary>
        private async Task PerformHealthCheck()
        {
            bool ready;
            string? reportedCacheVersion;
            try
            {
                using var response = await _httpClient.GetAsync(_healthUrl, _cancellationTokenSource.Token);
                var responseBody = await response.Content.ReadAsStringAsync(_cancellationTokenSource.Token);
                _logger.LogDebug("Raw health response (HTTP {StatusCode}): {ResponseBody}", (int)response.StatusCode, responseBody);

                if (!TryParseHealthResponse(responseBody, out ready, out reportedCacheVersion))
                {
                    _logger.LogWarning("Replicator health response (HTTP {StatusCode}) has no boolean ready field; keeping last known readiness", (int)response.StatusCode);
                    return;
                }
            }
            catch (OperationCanceledException) when (_cancellationTokenSource.IsCancellationRequested)
            {
                // Expected during shutdown. A request timeout also surfaces as an
                // OperationCanceledException and is handled below as a failed poll.
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex, "Replicator health check failed; keeping last known readiness");
                return;
            }

            bool wasHealthy = _isHealthy;
            string? previousCacheVersion = _cacheVersion;

            _isHealthy = ready;

            // An empty or missing cache_version keeps the last known version, matching the Go client.
            if (!string.IsNullOrEmpty(reportedCacheVersion))
            {
                _cacheVersion = reportedCacheVersion;
            }

            if (_isHealthy && !wasHealthy)
            {
                _logger.LogInformation("External replicator is now ready");
            }
            else if (!_isHealthy && wasHealthy)
            {
                _logger.LogInformation("External replicator is no longer ready");
            }

            if (_cacheVersion != previousCacheVersion)
            {
                _logger.LogInformation("Replicator cache version updated: {PreviousCacheVersion} -> {CacheVersion}", previousCacheVersion ?? "(null)", _cacheVersion ?? "(null)");
                CacheVersionChanged?.Invoke(previousCacheVersion, _cacheVersion);
            }
        }

        private static bool TryParseHealthResponse(string body, out bool ready, out string? cacheVersion)
        {
            ready = false;
            cacheVersion = null;
            try
            {
                using var doc = System.Text.Json.JsonDocument.Parse(body);
                if (doc.RootElement.ValueKind != System.Text.Json.JsonValueKind.Object)
                    return false;

                bool foundReady = false;
                foreach (var property in doc.RootElement.EnumerateObject())
                {
                    if (string.Equals(property.Name, "ready", StringComparison.OrdinalIgnoreCase)
                        && (property.Value.ValueKind == System.Text.Json.JsonValueKind.True
                            || property.Value.ValueKind == System.Text.Json.JsonValueKind.False))
                    {
                        ready = property.Value.GetBoolean();
                        foundReady = true;
                    }
                    else if (string.Equals(property.Name, "cache_version", StringComparison.OrdinalIgnoreCase)
                        && property.Value.ValueKind == System.Text.Json.JsonValueKind.String)
                    {
                        cacheVersion = property.Value.GetString();
                    }
                }
                return foundReady;
            }
            catch (System.Text.Json.JsonException)
            {
                return false;
            }
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