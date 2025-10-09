using Microsoft.Extensions.Logging;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Models;
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
    private readonly bool _replicatorMode;
    private readonly IReplicatorHealthService? _replicatorHealthService;

    private readonly ConnectionStateTracker _connectionTracker = new ConnectionStateTracker();


    /// <summary>
    /// Creates a new datastream client adapter
    /// </summary>
    public DatastreamClientAdapter(string baseUrl, ISchematicLogger logger, string apiKey, DatastreamOptions options, bool replicatorMode = false, string? replicatorHealthUrl = null)
    {
      _logger = logger;
      _replicatorMode = replicatorMode;
      
      // Initialize replicator health service if in replicator mode
      if (_replicatorMode && !string.IsNullOrWhiteSpace(replicatorHealthUrl))
      {
        // Create a simple HTTP client for health checks
        var httpClient = new System.Net.Http.HttpClient();
        _replicatorHealthService = new ReplicatorHealthService(httpClient, replicatorHealthUrl, logger);
        
        // Subscribe to cache version changes for logging and potential cache invalidation
        _replicatorHealthService.CacheVersionChanged += OnCacheVersionChanged;
        
        _replicatorHealthService.Start();
      }
      
      _client = new DatastreamClient(
          baseUrl,
          _logger,
          apiKey,
          _connectionTracker.UpdateConnectionState, // callback to update connection state
          options.CacheTTL,
          null, // default websocket client
          options,
          _replicatorMode ? GetReplicatorCacheVersionWithFallback : null // cache version provider
          );
    }

    /// <summary>
    /// Updates company metrics based on an event tracking request.
    /// This method forwards the request to the datastream client to update the metric value
    /// for the matching event type.
    /// </summary>
    /// <param name="eventBody">The event tracking request containing company keys and event details</param>
    /// <returns>Boolean indicating if the metrics were successfully updated</returns>
    public bool UpdateCompanyMetrics(EventBodyTrack eventBody)
    {
      return _client.UpdateCompanyMetrics(eventBody);
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
      if (_replicatorHealthService != null)
      {
        _replicatorHealthService.CacheVersionChanged -= OnCacheVersionChanged;
        _replicatorHealthService.Dispose();
      }
      _client.Dispose();
    }

    /// <summary>
    /// Check if replicator is ready (only valid in replicator mode)
    /// </summary>
    public bool IsReplicatorReady()
    {
      return _replicatorMode && _replicatorHealthService?.IsHealthy == true;
    }

    /// <summary>
    /// Check if running in replicator mode
    /// </summary>
    public bool IsReplicatorMode()
    {
      return _replicatorMode;
    }

    /// <summary>
    /// Gets the cache version from the replicator, attempting an immediate check if not available
    /// </summary>
    private string? GetReplicatorCacheVersionWithFallback()
    {
      if (_replicatorHealthService == null)
        return null;

      // If we already have a cache version, return it immediately
      if (!string.IsNullOrEmpty(_replicatorHealthService.CacheVersion))
        return _replicatorHealthService.CacheVersion;

      // If we don't have a cache version yet, try to get it synchronously
      // This handles the case where flag checks happen before the first health check
      try
      {
        var task = _replicatorHealthService.GetCacheVersionAsync();
        if (task.Wait(TimeSpan.FromSeconds(2))) // Wait up to 2 seconds
        {
          return task.Result;
        }
        else
        {
          _logger.Debug("Timed out waiting for replicator cache version");
          return null;
        }
      }
      catch (Exception ex)
      {
        _logger.Debug("Failed to get replicator cache version: {0}", ex.Message);
        return null;
      }
    }

    /// <summary>
    /// Handle cache version changes from the replicator
    /// </summary>
    private void OnCacheVersionChanged(string? oldVersion, string? newVersion)
    {
      _logger.Info("Cache version changed from {0} to {1} - new cache keys will use updated version", 
          oldVersion ?? "(null)", newVersion ?? "(null)");
      
      // Note: Cache invalidation is automatic because cache keys include the version.
      // Old cache entries will naturally become inaccessible as new operations use the new version.
    }

    /// <summary>
    /// Get the cache version from replicator (only valid in replicator mode)
    /// </summary>
    public string? GetReplicatorCacheVersion()
    {
      return _replicatorMode ? _replicatorHealthService?.CacheVersion : null;
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
    public async Task<CheckFlagResult> CheckFlag(CheckFlagRequestBody request, string flagKey)
    {
      CancellationToken cancellationToken = CancellationToken.None;
      
      // Get flag first - return error if not found
      var cachedFlag = _client.GetFlag(flagKey);
      if (cachedFlag == null)
      {
        return new CheckFlagResult
        {
          Reason = "FlagNotFound",
          FlagKey = flagKey,
          Value = false,
          Error = Errors.ErrorFlagNotFound,
        };
      }

      var needsCompany = request.Company != null && request.Company.Count > 0;
      var needsUser = request.User != null && request.User.Count > 0;

      Company? cachedCompany = null;
      User? cachedUser = null;

      // Try to get cached data first
      if (needsCompany && request.Company != null)
      {
        cachedCompany = _client.GetCompanyFromCache(request.Company);
      }
      if (needsUser && request.User != null)
      {
        cachedUser = _client.GetUserFromCache(request.User);
      }

      // If we have all cached data we need, use it
      if ((!needsCompany || cachedCompany != null) && (!needsUser || cachedUser != null))
      {
        return await _client.CheckFlag(cachedCompany, cachedUser, cachedFlag);
      }

      // Handle replicator mode behavior - similar to Go client
      if (_replicatorMode)
      {
        // In replicator mode, check if replicator is healthy before proceeding
        if (_replicatorHealthService?.IsHealthy != true)
        {
          _logger.Warn("Replicator mode: external replicator is not healthy, returning flag evaluation with available data for flag '{0}'", flagKey);
        }
        
        // In replicator mode, if we don't have all cached data, evaluate with nil values instead of fetching
        // The external replicator should have populated the cache with all necessary data
        return await _client.CheckFlag(cachedCompany, cachedUser, cachedFlag);
      }

      // Otherwise, check if we're connected to datastream
      if (!_connectionTracker.IsConnected)
      {
        throw new InvalidOperationException("Not connected to datastream");
      }

      // Fetch missing data from datastream
      try
      {
        Company? company = cachedCompany;
        User? user = cachedUser;

        if (needsCompany && company == null && request.Company != null)
        {
          company = await _client.GetCompanyAsync(request.Company, cancellationToken);
        }

        if (needsUser && user == null && request.User != null)
        {
          user = await _client.GetUserAsync(request.User, cancellationToken);
        }

        return await _client.CheckFlag(company, user, cachedFlag);
      }
      catch (Exception ex)
      {
        _logger.Error("Error checking flag {0}: {1}", flagKey, ex.Message);
        return new CheckFlagResult
        {
          Reason = "Error",
          FlagKey = flagKey,
          Error = ex,
          Value = false,
        };
      }
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
