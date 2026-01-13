using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneOf.Types;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client.RulesEngine.Utils;
using SchematicHQ.Client;
using SchematicHQ.Client.Cache;
using System.Threading.Tasks;


namespace SchematicHQ.Client.Datastream
{
  public class DatastreamClient : IDisposable
  {
    private readonly ISchematicLogger _logger;
    private readonly string _apiKey;
    private readonly Uri _baseUrl;
    private readonly TimeSpan _cacheTtl;
    private readonly Action<bool> _connectionStateCallback;
    private IWebSocketClient _webSocket;
    private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
    private readonly SemaphoreSlim _reconnectSemaphore = new SemaphoreSlim(1, 1);
    private CancellationTokenSource _readCancellationSource = new CancellationTokenSource();

    // Cache providers
    private readonly ICacheProvider<Flag> _flagsCache;
    private readonly ICacheProvider<Company> _companyCache;
    private readonly ICacheProvider<User> _userCache;
    
    // Cache version provider (optional, for replicator mode)
    private readonly Func<string?>? _cacheVersionProvider;

    // Pending request tracking
    private readonly Dictionary<string, List<TaskCompletionSource<Company?>>> _pendingCompanyRequests = new Dictionary<string, List<TaskCompletionSource<Company?>>>();
    private readonly Dictionary<string, List<TaskCompletionSource<User?>>> _pendingUserRequests = new Dictionary<string, List<TaskCompletionSource<User?>>>();
    private TaskCompletionSource<bool>? _pendingFlagRequest;
    private readonly object _pendingRequestsLock = new object();

    // Company update locking - per-company locks to prevent concurrent modifications
    private readonly ConcurrentDictionary<string, SemaphoreSlim> _companyUpdateLocks = new ConcurrentDictionary<string, SemaphoreSlim>();
    private readonly object _companyLockCleanupLock = new object();
    private int _lockCleanupCounter = 0;
    private const int LOCK_CLEANUP_THRESHOLD = 100; // Clean up every 100 operations

    // Constants
    private const string DefaultBaseUrl = "wss://datastream.schematichq.com";
    private const int MaxReconnectAttempts = 5;
    private static readonly TimeSpan MinReconnectDelay = TimeSpan.FromSeconds(1);
    private static readonly TimeSpan MaxReconnectDelay = TimeSpan.FromSeconds(60);
    private static readonly TimeSpan PongWait = TimeSpan.FromSeconds(30); // 30 seconds wait for Pong
    private static readonly TimeSpan PingPeriod = PongWait * .9; // 90% of PongWait
    private static readonly TimeSpan ResourceTimeout = TimeSpan.FromSeconds(2);
    private static readonly TimeSpan MaxCacheTTL = TimeSpan.FromDays(30); // Maximum TTL for cache items

    // Cache key prefixes
    private const string CacheKeyPrefix = "schematic";
    private const string CacheKeyPrefixCompany = "company";
    private const string CacheKeyPrefixFlags = "flags";
    private const string CacheKeyPrefixUser = "user";

    private static readonly Random _jitterRandom = new Random();
    private static readonly object _randomLock = new object();

    public DatastreamClient(
        string baseUrl,
        ISchematicLogger logger,
        string apiKey,
        Action<bool> connectionStateCallback,
        TimeSpan? cacheTtl = null,
        IWebSocketClient? webSocket = null,
        DatastreamOptions? options = null,
        Func<string?>? cacheVersionProvider = null
    )
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
      _connectionStateCallback = connectionStateCallback ?? throw new ArgumentNullException(nameof(connectionStateCallback));
      _cacheVersionProvider = cacheVersionProvider;

      // Use options if provided, otherwise use default values
      options ??= new DatastreamOptions();
      _cacheTtl = cacheTtl ?? options.CacheTTL ?? TimeSpan.FromHours(24);

      if (string.IsNullOrEmpty(baseUrl))
      {
        baseUrl = DefaultBaseUrl;
      }

      _baseUrl = GetBaseUrl(baseUrl);

      // Initialize cache providers
      // Use the greater value between the configured TTL and the default maximum TTL
      var flagTTL = MaxCacheTTL;
      if (_cacheTtl > MaxCacheTTL)
      {
        flagTTL = _cacheTtl;
      }

      // Company and User caches use the configured provider type
      if (options.CacheProviderType == DatastreamCacheProviderType.Redis &&
          options.RedisConfig != null)
      {
        try
        {
          _logger.Info("Initializing Redis cache for Datastream company, user and flag data");
          // We need to use the Cache namespace version, but cast it to the Client namespace interface
          _companyCache = new RedisCache<Company>(options.RedisConfig);
          _userCache = new RedisCache<User>(options.RedisConfig);
          var flagConfig = options.RedisConfig;
          flagConfig.CacheTTL = flagTTL; // Set TTL for flags cache
          _flagsCache = new RedisCache<Flag>(flagConfig);
        }
        catch (Exception ex)
        {
          _logger.Error("Failed to initialize Redis cache: {0}. Falling back to local cache.", ex.Message);
          _companyCache = new LocalCache<Company>(options.LocalCacheCapacity, _cacheTtl);
          _userCache = new LocalCache<User>(options.LocalCacheCapacity, _cacheTtl);
          _flagsCache = new LocalCache<Flag>(options.LocalCacheCapacity, flagTTL);
        }
      }
      else
      {
        // Use local cache (default)
        _companyCache = new LocalCache<Company>(options.LocalCacheCapacity, _cacheTtl);
        _userCache = new LocalCache<User>(options.LocalCacheCapacity, _cacheTtl);
        _flagsCache = new LocalCache<Flag>(options.LocalCacheCapacity, flagTTL);
      }

      _webSocket = webSocket ?? new StandardWebSocketClient();
    }

    public void Start()
    {
      Task.Run(ConnectAndReadAsync);
    }

    private Uri GetBaseUrl(string baseUrl)
    {
      var uri = new Uri(baseUrl);

      string newHost;

      // Handle special cases
      if (uri.Host == "localhost" || uri.HostNameType == UriHostNameType.IPv4 || uri.HostNameType == UriHostNameType.IPv6)
      {
        // For localhost or IP addresses, use as-is and don't add subdomain
        newHost = uri.Host;
      }
      else
      {
        // Extract the domain parts
        string[] hostParts = uri.Host.Split('.');
        string rootDomain;

        // Handle different domain formats
        if (hostParts.Length >= 2)
        {
          // Take the last two parts (example.com)
          rootDomain = string.Join(".", hostParts.Skip(Math.Max(0, hostParts.Length - 2)).Take(2));
        }
        else
        {
          rootDomain = uri.Host;
        }

        // Create new host with datastream subdomain
        newHost = $"datastream.{rootDomain}";
      }

      var builder = new UriBuilder(uri)
      {
        Host = newHost,
        Scheme = uri.Scheme == "https" ? "wss" : "ws",
        Path = "/datastream"
      };

      return builder.Uri;
    }

    private async Task ConnectAndReadAsync()
    {
      var attempts = 0;

      while (!_cancellationTokenSource.Token.IsCancellationRequested)
      {
        try
        {
          await _reconnectSemaphore.WaitAsync();
          if (_readCancellationSource.IsCancellationRequested)
          {
            _readCancellationSource.Dispose();
            _readCancellationSource = new CancellationTokenSource();
          }
          _webSocket.Options.SetRequestHeader("X-Schematic-Api-Key", _apiKey);
          _webSocket.Options.KeepAliveInterval = PingPeriod; // Set keep-alive interval

          try
          {

            await _webSocket.ConnectAsync(_baseUrl, _cancellationTokenSource.Token);
            _logger.Info("Connected to Schematic WebSocket");
            attempts = 0;

            // Signal connection state
            _connectionStateCallback(true);

            // Start reading messages
            var readTask = ReadMessagesAsync();

            // Request all flags immediately after connection
            try
            {
              await GetAllFlagsAsync(_cancellationTokenSource.Token);
            }
            catch (Exception flagEx)
            {
              // Log the error and retry connection
              throw new Exception($"Failed to retrieve initial flags: {flagEx.Message}");
            }

            // Wait for the read task to complete, which happens on disconnection
            await readTask;

            _readCancellationSource.Token.ThrowIfCancellationRequested();
          }
          catch (Exception connectEx)
          {
            // Handle connection errors specifically
            _logger.Error("Failed to connect to WebSocket: {0}", connectEx.Message);
            // Don't rethrow - allow the outer exception handler to handle retries
            throw;
          }
          finally
          {
            // Ensure connection is closed before reconnecting
            if (_webSocket.State == WebSocketState.Open)
            {
              try
              {
                await _webSocket.CloseAsync(
                    WebSocketCloseStatus.NormalClosure,
                    "Reconnecting",
                    CancellationToken.None
                );
              }
              catch (Exception ex)
              {
                _logger.Error("Error closing WebSocket: {0}", ex.Message);
                _webSocket.Abort();
              }
            }
          }
        }
        catch (Exception connectionEx)
        {
          _reconnectSemaphore.Release();
          _logger.Error("WebSocket connection error: {0}", connectionEx.Message);
          attempts++;
          _connectionStateCallback(false);

          if (_webSocket != null)
          {
            try { _webSocket.Dispose(); } catch { /* ignore */ }
          }
          _webSocket = new StandardWebSocketClient();

          if (attempts >= MaxReconnectAttempts)
          {
            _logger.Error("Unable to connect to server after {0} attempts", MaxReconnectAttempts);
            return;
          }

          await Task.Delay(CalculateBackoffDelay(attempts));
        }
      }
    }

    private TimeSpan CalculateBackoffDelay(int attempt)
    {
      int jitterMs;
      // Add jitter to prevent synchronized reconnection attempts
      // Thread-safe access to the shared Random instance
      lock (_randomLock)
      {
        jitterMs = _jitterRandom.Next((int)MinReconnectDelay.TotalMilliseconds);
      }

      var jitter = TimeSpan.FromMilliseconds(jitterMs);

      // Exponential backoff with a cap
      var delay = TimeSpan.FromMilliseconds(Math.Pow(2, attempt - 1) * MinReconnectDelay.TotalMilliseconds) + jitter;
      if (delay > MaxReconnectDelay)
      {
        delay = MaxReconnectDelay + jitter; // Ensure jitter is added even when capped
      }

      return delay;
    }

    private async Task ReadMessagesAsync()
    {
      var buffer = new byte[4096];
      var receiveBuffer = new List<byte>();
      _logger.Info("Starting to read messages from WebSocket");
      try
      {
        while (_webSocket.State == WebSocketState.Open && !_cancellationTokenSource.Token.IsCancellationRequested)
        {
          WebSocketReceiveResult result;
          try
          {
            do
            {
              result = await _webSocket.ReceiveAsync(
                  new ArraySegment<byte>(buffer),
                  _cancellationTokenSource.Token);

              if (result.MessageType == WebSocketMessageType.Close)
              {
                return;
              }

              receiveBuffer.AddRange(new ArraySegment<byte>(buffer, 0, result.Count));
            }
            while (!result.EndOfMessage);

            if (result.MessageType == WebSocketMessageType.Text)
            {
              var buffArray = receiveBuffer.ToArray();
              var message = Encoding.UTF8.GetString(buffArray);
              receiveBuffer.Clear();

              if (string.IsNullOrEmpty(message))
              {
                _logger.Debug("Received empty message from WebSocket");
                return; // Trigger reconnection
              }

              try
              {
                var response = JsonSerializer.Deserialize<DataStreamResponse>(message);
                HandleMessageResponse(response);
              }
              catch (Exception ex)
              {
                _logger.Error("Failed to process WebSocket message: {0}", ex.Message);
              }
            }
          }
          catch (OperationCanceledException)
          {
            _logger.Info("WebSocket read operation was cancelled");
            return;
          }
          catch (Exception ex)
          {
            _logger.Error("Error reading from WebSocket: {0}", ex.Message);
            return; // Exit and trigger reconnection
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Error("Fatal error in ReadMessagesAsync: {0}", ex.Message);
      }
      finally
      {
        // Signal that reconnection should happen
        if (!_cancellationTokenSource.IsCancellationRequested)
        {
          _logger.Info("Signaling for WebSocket reconnection");
          _readCancellationSource.Cancel();
        }
      }
    }

    private void HandleMessageResponse(DataStreamResponse? message)
    {
      if (message == null)
      {
        return;
      }

      if (message.MessageType == MessageType.Error)
      {
        HandleErrorMessage(message);
        return;
      }

      switch (message.EntityType)
      {
        case EntityType.Company:
          HandleCompanyMessage(message);
          break;
        case EntityType.Flags:
          HandleFlagsMessage(message);
          break;
        case EntityType.Flag:
          HandleFlagMessage(message);
          break;
        case EntityType.User:
          HandleUserMessage(message);
          break;
        default:
          _logger.Error("Received unknown entity type: {0}", message.EntityType);
          break;
      }
    }

    private void HandleFlagsMessage(DataStreamResponse response)
    {
      try
      {
        if (response.Data == null)
        {
          _logger.Warn("Received empty flags data");
          return;
        }

        var options = new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = true,
          // This REPLACES the existing converter with one that has the right settings
          Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false) }
        };

        var jsonString = response.Data.ToString() ?? string.Empty;
        var flags = JsonSerializer.Deserialize<List<Flag>>(jsonString, options);
        var cacheKeys = new List<string>();

        if (flags == null || flags.Count == 0)
        {
          _logger.Warn("Received empty or null flags list");
          return;
        }

        foreach (var flag in flags)
        {
          if (string.IsNullOrEmpty(flag.Key))
          {
            _logger.Debug("Flag key is null, skipping flag: {0}", flag.Id);
            continue;
          }
          var cacheKey = FlagCacheKey(flag.Key);
          _flagsCache.Set(cacheKey, flag);
          cacheKeys.Add(cacheKey);
        }

        _flagsCache.DeleteMissing(cacheKeys);

        lock (_pendingRequestsLock)
        {
          if (_pendingFlagRequest != null)
          {
            _pendingFlagRequest.TrySetResult(true);
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Error("Failed to handle flags message: {0}", ex.Message);
      }
    }

    private void HandleFlagMessage(DataStreamResponse response)
    {
      try
      {
        if (response.Data == null)
        {
          _logger.Warn("Received empty flag data");
          return;
        }

        var options = new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = true,
          // This REPLACES the existing converter with one that has the right settings
          Converters = { new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false) }
        };

        var jsonString = response.Data.ToString() ?? string.Empty;

        if (response.MessageType == MessageType.Delete)
        {
          // Handle single flag deletion
          var deleteData = JsonSerializer.Deserialize<dynamic>(jsonString, options);
          string? flagKey = null;

          // Try to extract the flag key from the delete message
          if (deleteData != null)
          {
            var deleteDict = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonString, options);
            if (deleteDict != null)
            {
              if (deleteDict.TryGetValue("key", out var keyValue))
              {
                flagKey = keyValue?.ToString();
              }
              else if (deleteDict.TryGetValue("id", out var idValue))
              {
                flagKey = idValue?.ToString();
              }
            }
          }

          if (!string.IsNullOrEmpty(flagKey))
          {
            var deleteCacheKey = FlagCacheKey(flagKey);
            _flagsCache.Delete(deleteCacheKey);
            _logger.Debug("Deleted single flag from cache: {0}", flagKey);
          }
          else
          {
            _logger.Warn("Could not extract flag key from delete message");
          }

          return;
        }

        // Handle single flag creation/update
        var flag = JsonSerializer.Deserialize<Flag>(jsonString, options);

        if (flag == null)
        {
          _logger.Warn("Received null flag data");
          return;
        }

        if (string.IsNullOrEmpty(flag.Key))
        {
          _logger.Debug("Flag key is null, skipping flag: {0}", flag.Id);
          return;
        }

        var cacheKey = FlagCacheKey(flag.Key);
        _flagsCache.Set(cacheKey, flag);
        _logger.Debug("Cached single flag: {0}", flag.Key);

        // Note: Unlike bulk flags processing, we do NOT call DeleteMissing for single flag updates
      }
      catch (Exception ex)
      {
        _logger.Error("Failed to handle single flag message: {0}", ex.Message);
      }
    }

    /// <summary>
    /// Helper method to notify pending requests with a success result
    /// </summary>
    private void NotifyPendingRequests<T>(T? entity, IDictionary<string, string> keys, string cacheKeyPrefix,
      Dictionary<string, List<TaskCompletionSource<T?>>> pendingRequests) where T : class
    {
      List<TaskCompletionSource<T?>> channelsToNotify = new List<TaskCompletionSource<T?>>();

      lock (_pendingRequestsLock)
      {
        foreach (var key in keys)
        {
          var cacheKey = ResourceKeyToCacheKey<T>(cacheKeyPrefix, key.Key, key.Value);
          if (pendingRequests.TryGetValue(cacheKey, out var channels))
          {
            channelsToNotify.AddRange(channels);
            pendingRequests.Remove(cacheKey);
          }
        }
      }
      
      // Notify outside the lock
      foreach (var channel in channelsToNotify)
      {
        channel.TrySetResult(entity);
      }
    }
    
    private async void HandleCompanyMessage(DataStreamResponse response)
    {
      try
      {
        if (response.Data == null)
        {
          _logger.Warn("Received empty company data");
          return;
        }

        var options = new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = true,
          Converters = {
            new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false),
            new EntityTypeConverter()
          }
        };

        var jsonString = response.Data.ToString() ?? string.Empty;
        var company = JsonSerializer.Deserialize<Company>(jsonString, options);

        if (company == null)
        {
          _logger.Warn("Received null company data");
          return;
        }

        // Get the company-specific lock to coordinate with UpdateCompanyMetrics
        var companyLock = GetCompanyLock(company.Keys);
        
        try
        {
          // Wait for the lock with a reasonable timeout
          if (!await companyLock.WaitAsync(TimeSpan.FromSeconds(5)))
          {
            _logger.Warn("Timeout waiting for company lock during WebSocket update");
            return;
          }

          try
          {
            if (response.MessageType == MessageType.Delete)
            {
              // Handle deletion by removing company from cache
              foreach (var key in company.Keys)
              {
                var cacheKey = ResourceKeyToCacheKey<Company>(CacheKeyPrefixCompany, key.Key, key.Value);
                _companyCache.Delete(cacheKey);
              }

              return;
            }

            // Update cache (inside the lock to prevent race conditions)
            foreach (var key in company.Keys)
            {
              var cacheKey = ResourceKeyToCacheKey<Company>(CacheKeyPrefixCompany, key.Key, key.Value);
              _companyCache.Set(cacheKey, company);
            }

            // Notify pending requests
            NotifyPendingRequests(company, company.Keys, CacheKeyPrefixCompany, _pendingCompanyRequests);
          }
          finally
          {
            // Always release the lock
            companyLock.Release();
          }
        }
        catch (Exception lockEx)
        {
          _logger.Error($"Error during locked company update: {lockEx.Message}");
        }
      }
      catch (Exception ex)
      {
        _logger.Error("Failed to handle company message: {0}", ex.Message);
      }
    }

    private void HandleUserMessage(DataStreamResponse response)
    {
      try
      {
        if (response.Data == null)
        {
          _logger.Warn("Received empty user data");
          return;
        }

        var options = new JsonSerializerOptions
        {
          PropertyNameCaseInsensitive = true,
          Converters = {
            new JsonStringEnumConverter(JsonNamingPolicy.SnakeCaseLower, false),
            new EntityTypeConverter()
          }
        };
        var jsonString = response.Data.ToString() ?? string.Empty;
        var user = JsonSerializer.Deserialize<User>(jsonString, options);

        if (user == null)
        {
          _logger.Warn("Received null user data");
          return;
        }

        if (response.MessageType == MessageType.Delete)
        {
          // Handle deletion by removing user from cache
          foreach (var key in user.Keys)
          {
            var cacheKey = ResourceKeyToCacheKey<User>(CacheKeyPrefixUser, key.Key, key.Value);
            _userCache.Delete(cacheKey);
            _logger.Debug("Deleted user from cache with key: {0}", cacheKey);
          }
          return;
        }

        // Update cache
        foreach (var key in user.Keys)
        {
          var cacheKey = ResourceKeyToCacheKey<User>(CacheKeyPrefixUser, key.Key, key.Value);
          _userCache.Set(cacheKey, user);
        }

        // Notify pending requests
        NotifyPendingRequests(user, user.Keys, CacheKeyPrefixUser, _pendingUserRequests);
      }
      catch (Exception ex)
      {
        _logger.Error("Failed to handle user message: {0}", ex.Message);
      }
    }

    private void HandleErrorMessage(DataStreamResponse response)
    {
      try
      {
        if (response.Data == null)
        {
          _logger.Warn("Received empty error data");
          return;
        }

        // Deserialize the error message
        var jsonString = response.Data.ToString() ?? string.Empty;
        var error = JsonSerializer.Deserialize<DataStreamError>(jsonString);
        if (error != null)
        {
          if (!string.IsNullOrEmpty(error.Error))
          {
            _logger.Error("Received error from server: {0}", error.Error);
          }
          
          // Check if we have keys and entity type in the error response
          if (error.Keys != null && error.Keys.Count > 0 && error.EntityType.HasValue)
          {
            switch (error.EntityType.Value)
            {
              case EntityType.Company:
                NotifyPendingRequests<Company>(null, error.Keys, CacheKeyPrefixCompany, _pendingCompanyRequests);
                break;
              case EntityType.User:
                NotifyPendingRequests<User>(null, error.Keys, CacheKeyPrefixUser, _pendingUserRequests);
                break;
              default:
                _logger.Warn("Received error for unsupported entity type: {0}", error.EntityType.Value);
                break;
            }
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Error("Failed to deserialize error message: {0}", ex.Message);
      }
    }

    internal async Task<CheckFlagResult> CheckFlag(Company? company, User? user, Flag flag, CancellationToken cancellationToken = default)
    {
      try
      {
        var result = await FlagCheckService.CheckFlag(company, user, flag);
        return result;
      }
      catch (Exception ex)
      {
        _logger.Error("Error checking flag {0}: {1}", flag.Key, ex.Message);
        return new CheckFlagResult
        {
          Reason = "Error",
          FlagKey = flag.Key,
          Error = ex,
          Value = false,
        };
      }
    }

    internal async Task<Company?> GetCompanyAsync(Dictionary<string, string> keys, CancellationToken cancellationToken)
    {

      var waitTask = new TaskCompletionSource<Company?>();
      var cacheKeys = new List<string>();
      bool shouldSendRequest = true;

      lock (_pendingRequestsLock)
      {
        foreach (var key in keys)
        {
          var cacheKey = ResourceKeyToCacheKey<Company>(CacheKeyPrefixCompany, key.Key, key.Value);
          cacheKeys.Add(cacheKey);

          if (_pendingCompanyRequests.TryGetValue(cacheKey, out var existingChannels))
          {
            existingChannels.Add(waitTask);
            shouldSendRequest = false; // Someone else already requested this
          }
          else
          {
            _pendingCompanyRequests[cacheKey] = new List<TaskCompletionSource<Company?>> { waitTask };
          }
        }
      }

      if (shouldSendRequest)
      {
        var request = new DataStreamRequest
        {
          EntityType = EntityType.Company,
          Keys = keys
        };

        await SendWebSocketMessageAsync(request, cancellationToken);
      }

      using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
      timeoutCts.CancelAfter(ResourceTimeout);

      try
      {
        var completedTask = await Task.WhenAny(
            waitTask.Task,
            Task.Delay(Timeout.Infinite, timeoutCts.Token)
        );

        if (completedTask == waitTask.Task)
        {
          return await waitTask.Task;
        }

        throw new TimeoutException("Timeout while waiting for company data");
      }
      catch (Exception)
      {
        // Clean up all channels involved in this request
        CleanupPendingCompanyRequests(cacheKeys, waitTask);
        throw;
      }
    }

    internal async Task<User?> GetUserAsync(Dictionary<string, string> keys, CancellationToken cancellationToken)
    {


      var waitTask = new TaskCompletionSource<User?>();
      var cacheKeys = new List<string>();
      bool shouldSendRequest = true;

      lock (_pendingRequestsLock)
      {
        foreach (var key in keys)
        {
          var cacheKey = ResourceKeyToCacheKey<User>(CacheKeyPrefixUser, key.Key, key.Value);
          cacheKeys.Add(cacheKey);

          if (_pendingUserRequests.TryGetValue(cacheKey, out var existingChannels))
          {
            existingChannels.Add(waitTask);
            shouldSendRequest = false; // Someone else already requested this
          }
          else
          {
            _pendingUserRequests[cacheKey] = new List<TaskCompletionSource<User?>> { waitTask };
          }
        }
      }

      if (shouldSendRequest)
      {
        var request = new DataStreamRequest
        {
          EntityType = EntityType.User,
          Keys = keys
        };

        await SendWebSocketMessageAsync(request, cancellationToken);
      }

      using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
      timeoutCts.CancelAfter(ResourceTimeout);

      try
      {
        var completedTask = await Task.WhenAny(
            waitTask.Task,
            Task.Delay(Timeout.Infinite, timeoutCts.Token)
        );

        if (completedTask == waitTask.Task)
        {
          return await waitTask.Task;
        }

        throw new TimeoutException("Timeout while waiting for user data");
      }
      catch (Exception)
      {
        // Clean up all channels involved in this request
        CleanupPendingUserRequests(cacheKeys, waitTask);
        throw;
      }
    }

    private async Task GetAllFlagsAsync(CancellationToken cancellationToken)
    {
      // Check if there is already a pending request for flags
      TaskCompletionSource<bool>? waitTask = null;

      lock (_pendingRequestsLock)
      {
        if (_pendingFlagRequest != null)
        {
          // If there is a pending request, use that
          return;
        }
        else if (_webSocket.State != WebSocketState.Open)
        {
          _logger.Warn("WebSocket is not open, cannot request flags data");
          return;
        }

        waitTask = new TaskCompletionSource<bool>();
        _pendingFlagRequest = waitTask;
      }

      try
      {
        var request = new DataStreamRequest
        {
          EntityType = EntityType.Flags
        };

        // Send the request for flags data
        await SendWebSocketMessageAsync(request, cancellationToken);

        using var timeoutCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        timeoutCts.CancelAfter(ResourceTimeout);

        var completedTask = await Task.WhenAny(
            waitTask.Task,
            Task.Delay(ResourceTimeout, timeoutCts.Token)
        );

        if (completedTask != waitTask.Task)
        {
          throw new TimeoutException("Timeout while waiting for flags data");
        }
      }
      finally
      {
        lock (_pendingRequestsLock)
        {
          _pendingFlagRequest = null;
        }
      }
    }

    internal Flag? GetFlag(string key)
    {
      var flag = _flagsCache.Get(FlagCacheKey(key));
      return flag;
    }

    internal Company? GetCompanyFromCache(Dictionary<string, string> keys)
    {
      foreach (var key in keys)
      {
        var cacheKey = ResourceKeyToCacheKey<Company>(CacheKeyPrefixCompany, key.Key, key.Value);
        var company = _companyCache.Get(cacheKey);
        if (company != null)
        {
          return company;
        }
      }
      return null;
    }

    internal User? GetUserFromCache(Dictionary<string, string> keys)
    {
      foreach (var key in keys)
      {
        var cacheKey = ResourceKeyToCacheKey<User>(CacheKeyPrefixUser, key.Key, key.Value);
        var user = _userCache.Get(cacheKey);
        if (user != null)
        {
          return user;
        }
      }
      return null;
    }
    
    /// <summary>
    /// Updates company metrics based on a tracked event. This method retrieves the company
    /// from cache, creates a deep copy, increments the metric value if there's a metric matching 
    /// for the matching event type, and then recaches the updated company data.
    /// Uses per-company locking to prevent concurrent modifications.
    /// </summary>
    /// <param name="eventBody">The event tracking request containing company keys and event details</param>
    /// <returns>Boolean indicating if the metrics were successfully updated</returns>
    public async Task<bool> UpdateCompanyMetricsAsync(EventBodyTrack eventBody)
    {
        if (eventBody == null)
        {
            _logger.Error("Event body cannot be null");
            return false;
        }

        if (eventBody.Company == null || eventBody.Company.Count == 0)
        {
            _logger.Error("No keys provided for company lookup");
            return false;
        }

        // Get the company-specific lock to prevent concurrent modifications
        var companyLock = GetCompanyLock(eventBody.Company);
        
        try
        {
            // Wait for the lock with a reasonable timeout to prevent deadlocks
            if (!await companyLock.WaitAsync(TimeSpan.FromSeconds(5)))
            {
                _logger.Warn("Timeout waiting for company lock during metrics update");
                return false;
            }

            try
            {
                // Get company from cache (inside the lock to ensure consistency)
                var company = GetCompanyFromCache(eventBody.Company);
                if (company == null)
                {
                    return false;
                }

                // Create a deep copy of the company to avoid modifying the cached object directly
                var companyCopy = DeepCopyCompany(company);
                if (companyCopy == null)
                {
                    _logger.Error("Failed to create deep copy of company");
                    return false;
                }

                // Update the metric value if it matches the event
                bool metricUpdated = false;
                foreach (var metric in companyCopy.Metrics)
                {
                    if (metric != null && metric.EventSubtype == eventBody.Event)
                    {
                        int quantity = eventBody.Quantity ?? 0;
                        metric.Value += quantity;
                        metricUpdated = true;
                    }
                }

                if (!metricUpdated)
                {
                    _logger.Debug($"No matching metric found for event {eventBody.Event}");
                    return false;
                }

                // Cache the updated company for all keys (still inside the lock)
                bool cacheSuccess = true;
                foreach (var key in companyCopy.Keys)
                {
                    try
                    {
                        var cacheKey = ResourceKeyToCacheKey<Company>(CacheKeyPrefixCompany, key.Key, key.Value);
                        _companyCache.Set(cacheKey, companyCopy);
                    }
                    catch (Exception ex)
                    {
                        _logger.Warn($"Failed to cache company metric for key '{key.Key}': {ex.Message}");
                        cacheSuccess = false;
                    }
                }

                return cacheSuccess;
            }
            finally
            {
                // Always release the lock
                companyLock.Release();
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"Error during company metrics update: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Synchronous version of UpdateCompanyMetricsAsync for backward compatibility.
    /// Uses per-company locking to prevent concurrent modifications.
    /// </summary>
    /// <param name="eventBody">The event tracking request containing company keys and event details</param>
    /// <returns>Boolean indicating if the metrics were successfully updated</returns>
    public bool UpdateCompanyMetrics(EventBodyTrack eventBody)
    {
        try
        {
            return UpdateCompanyMetricsAsync(eventBody).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            _logger.Error($"Error in synchronous company metrics update: {ex.Message}");
            return false;
        }
    }    /// <summary>
    /// Creates a complete deep copy of a Company object and all its nested fields.
    /// This ensures that modifying the returned company won't affect the original cached object.
    /// All nested objects including Subscription, Metrics, and Traits are deep copied.
    /// </summary>
    /// <param name="company">The company to copy</param>
    /// <returns>A new independent copy of the company</returns>
    private Company? DeepCopyCompany(Company? company)
    {
        if (company == null)
        {
            return null;
        }

        // Create a new company instance
        var companyCopy = new Company
        {
            Id = company.Id,
            AccountId = company.AccountId,
            EnvironmentId = company.EnvironmentId,
            BasePlanId = company.BasePlanId,
            BillingProductIds = new List<string>(company.BillingProductIds),
            CrmProductIds = new List<string>(company.CrmProductIds),
            PlanIds = new List<string>(company.PlanIds),
            Subscription = company.Subscription != null ? new Subscription
            {
                Id = company.Subscription.Id,
                PeriodStart = company.Subscription.PeriodStart,
                PeriodEnd = company.Subscription.PeriodEnd
            } : null,
            Keys = new Dictionary<string, string>(),
            Metrics = new List<CompanyMetric>(),
            Traits = new List<Trait>()
        };

        // Copy the keys dictionary
        foreach (var key in company.Keys)
        {
            companyCopy.Keys[key.Key] = key.Value;
        }

        // Deep copy metrics
        foreach (var metric in company.Metrics)
        {
            if (metric == null)
            {
                // Skip null metrics
                continue;
            }

            var metricCopy = new CompanyMetric
            {
                AccountId = metric.AccountId,
                EnvironmentId = metric.EnvironmentId,
                CompanyId = metric.CompanyId,
                EventSubtype = metric.EventSubtype,
                Period = metric.Period,
                MonthReset = metric.MonthReset,
                Value = metric.Value,
                CreatedAt = metric.CreatedAt,
                ValidUntil = metric.ValidUntil
            };

            companyCopy.Metrics.Add(metricCopy);
        }

        // Copy traits
        foreach (var trait in company.Traits)
        {
            if (trait == null)
            {
                // Skip null traits
                continue;
            }
            
            // Create a new trait instance
            var traitCopy = new Trait
            {
                Value = trait.Value,
                TraitDefinition = trait.TraitDefinition
            };
            
            companyCopy.Traits.Add(traitCopy);
        }

        return companyCopy;
    }


    private void CleanupPendingCompanyRequests(List<string> cacheKeys, TaskCompletionSource<Company?> waitTask)
    {
      lock (_pendingRequestsLock)
      {
        foreach (var cacheKey in cacheKeys)
        {
          if (_pendingCompanyRequests.TryGetValue(cacheKey, out var channels))
          {
            channels.Remove(waitTask);
            if (channels.Count == 0)
            {
              _pendingCompanyRequests.Remove(cacheKey);
            }
          }
        }
      }
    }

    private void CleanupPendingUserRequests(List<string> cacheKeys, TaskCompletionSource<User?> waitTask)
    {
      lock (_pendingRequestsLock)
      {
        foreach (var cacheKey in cacheKeys)
        {
          if (_pendingUserRequests.TryGetValue(cacheKey, out var channels))
          {
            channels.Remove(waitTask);
            if (channels.Count == 0)
            {
              _pendingUserRequests.Remove(cacheKey);
            }
          }
        }
      }
    }

    private async Task SendWebSocketMessageAsync(DataStreamRequest request, CancellationToken cancellationToken)
    {
      if (_webSocket.State != WebSocketState.Open)
      {
        throw new InvalidOperationException("WebSocket connection is not initialized");
      }

      var baseRequest = new DataStreamBaseRequest
      {
        Data = request
      };

      // Serialize the request to JSON
      var json = JsonSerializer.Serialize(baseRequest);
      var buffer = Encoding.UTF8.GetBytes(json);

      // Send the message
      await _webSocket.SendAsync(
          new ArraySegment<byte>(buffer),
          WebSocketMessageType.Text,
          true,
          cancellationToken);

    }

    /// <summary>
    /// Gets the cache version to use for cache keys.
    /// In replicator mode, uses the replicator-provided version if available.
    /// Otherwise, uses the SDK's generated model hash.
    /// </summary>
    private string GetCacheVersion()
    {
      var replicatorVersion = _cacheVersionProvider?.Invoke();
      _logger.Debug("Cache version provider returned: {0}", replicatorVersion ?? "(null)");
      
      if (!string.IsNullOrEmpty(replicatorVersion))
      {
        _logger.Info("Using replicator cache version: {0}", replicatorVersion);
        return replicatorVersion;
      }
      
      var sdkVersion = SchemaVersionGenerator.GetGlobalSchemaVersion();
      _logger.Info("Using SDK cache version: {0}", sdkVersion);
      return sdkVersion;
    }

    private string FlagCacheKey(string key)
    {
      var schemaVersion = GetCacheVersion();
      return $"{CacheKeyPrefix}:{CacheKeyPrefixFlags}:{schemaVersion}:{key.ToLowerInvariant()}";
    }

    private string ResourceKeyToCacheKey<T>(string resourceType, string key, string value)
    {
      var schemaVersion = GetCacheVersion();
      return $"{CacheKeyPrefix}:{resourceType}:{schemaVersion}:{key.ToLowerInvariant()}:{value.ToLowerInvariant()}";
    }

    /// <summary>
    /// Gets or creates a per-company lock for synchronizing updates to company data.
    /// This ensures that concurrent modifications to the same company are serialized.
    /// </summary>
    /// <param name="companyKeys">The company's keys to generate a unique lock identifier</param>
    /// <returns>A SemaphoreSlim for the specific company</returns>
    private SemaphoreSlim GetCompanyLock(IDictionary<string, string> companyKeys)
    {
      // Create a deterministic key based on all company keys
      var sortedKeys = companyKeys.OrderBy(kvp => kvp.Key).ToList();
      var lockKey = string.Join("|", sortedKeys.Select(kvp => $"{kvp.Key}:{kvp.Value}"));
      
      // Periodic cleanup to prevent memory leaks
      if (Interlocked.Increment(ref _lockCleanupCounter) % LOCK_CLEANUP_THRESHOLD == 0)
      {
        Task.Run(CleanupUnusedCompanyLocks); // Run cleanup in background
      }
      
      return _companyUpdateLocks.GetOrAdd(lockKey, _ => new SemaphoreSlim(1, 1));
    }

    /// <summary>
    /// Attempts to clean up unused company locks to prevent memory leaks.
    /// This should be called periodically or when the number of locks grows too large.
    /// </summary>
    private void CleanupUnusedCompanyLocks()
    {
      lock (_companyLockCleanupLock)
      {
        var locksToRemove = new List<string>();
        
        foreach (var kvp in _companyUpdateLocks)
        {
          var semaphore = kvp.Value;
          // If the semaphore has no waiters and is currently available, it's likely unused
          if (semaphore.CurrentCount == 1)
          {
            // Try to acquire the lock briefly to ensure it's really unused
            if (semaphore.Wait(0)) // Non-blocking wait
            {
              try
              {
                locksToRemove.Add(kvp.Key);
              }
              finally
              {
                semaphore.Release();
              }
            }
          }
        }
        
        // Remove unused locks
        foreach (var key in locksToRemove)
        {
          if (_companyUpdateLocks.TryRemove(key, out var removedSemaphore))
          {
            removedSemaphore.Dispose();
          }
        }
        
        if (locksToRemove.Count > 0)
        {
          _logger.Debug($"Cleaned up {locksToRemove.Count} unused company locks");
        }
      }
    }

    public void Dispose()
    {
      _cancellationTokenSource.Cancel();

      try
      {
        if (_webSocket.State == WebSocketState.Open)
        {
          // Create a CancellationTokenSource with timeout
          using var closeCts = new CancellationTokenSource(TimeSpan.FromSeconds(5));

          // Try to close gracefully
          Task closeTask = _webSocket.CloseAsync(
              WebSocketCloseStatus.NormalClosure,
              "Closing",
              closeCts.Token);

          try
          {
            // Wait for completion or timeout asynchronously, but since Dispose() is synchronous,
            // we need to eventually block here, but with a clean timeout
            if (!closeTask.Wait(TimeSpan.FromSeconds(5)))
            {
              _logger.Warn("WebSocket close handshake timed out");
            }
          }
          catch (OperationCanceledException)
          {
            _logger.Warn("WebSocket close handshake was cancelled");
          }
          catch (AggregateException ex) when (ex.InnerExceptions.Any(e => e is OperationCanceledException))
          {
            _logger.Warn("WebSocket close handshake was cancelled");
          }

          // If it didn't close gracefully, abort
          if (_webSocket.State != WebSocketState.Closed)
          {
            _logger.Warn("WebSocket didn't close gracefully, aborting");
            _webSocket.Abort();
          }
        }
      }
      catch (Exception ex)
      {
        _logger.Error("Error closing WebSocket connection: {0}", ex.Message);

        // Ensure we abort in case of errors
        try { _webSocket.Abort(); } catch { /* Ignore any errors during abort */ }
      }
      finally
      {
        _webSocket.Dispose();
        _reconnectSemaphore.Dispose();
        _cancellationTokenSource.Dispose();
        _readCancellationSource.Dispose();
        
        // Clean up company locks
        foreach (var lockEntry in _companyUpdateLocks)
        {
          lockEntry.Value.Dispose();
        }
        _companyUpdateLocks.Clear();
      }
    }
  }
}
