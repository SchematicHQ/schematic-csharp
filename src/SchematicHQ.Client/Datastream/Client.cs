using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OneOf.Types;
using SchematicHQ.Client.RulesEngine;
using SchematicHQ.Client.RulesEngine.Models;
using SchematicHQ.Client;
using SchematicHQ.Client.Cache;


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

    // Pending request tracking
    private readonly Dictionary<string, List<TaskCompletionSource<Company>>> _pendingCompanyRequests = new Dictionary<string, List<TaskCompletionSource<Company>>>();
    private readonly Dictionary<string, List<TaskCompletionSource<User>>> _pendingUserRequests = new Dictionary<string, List<TaskCompletionSource<User>>>();
    private TaskCompletionSource<bool>? _pendingFlagRequest;
    private readonly object _pendingRequestsLock = new object();

    // Constants
    private const string DefaultBaseUrl = "wss://datastream.schematichq.com";
    private const int MaxReconnectAttempts = 5;
    private static readonly TimeSpan MinReconnectDelay = TimeSpan.FromSeconds(1);
    private static readonly TimeSpan MaxReconnectDelay = TimeSpan.FromSeconds(60);
    private static readonly TimeSpan PongWait = TimeSpan.FromSeconds(30); // 30 seconds wait for Pong
    private static readonly TimeSpan PingPeriod = PongWait * .9; // 90% of PongWait
    private static readonly TimeSpan ResourceTimeout = TimeSpan.FromSeconds(2);

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
        DatastreamOptions? options = null
    )
    {
      _logger = logger ?? throw new ArgumentNullException(nameof(logger));
      _apiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
      _connectionStateCallback = connectionStateCallback ?? throw new ArgumentNullException(nameof(connectionStateCallback));

      // Use options if provided, otherwise use default values
      options ??= new DatastreamOptions();
      _cacheTtl = cacheTtl ?? options.CacheTTL ?? TimeSpan.FromHours(24);

      if (string.IsNullOrEmpty(baseUrl))
      {
        baseUrl = DefaultBaseUrl;
      }

      _baseUrl = GetBaseUrl(baseUrl);

      // Initialize cache providers

      // Flags always use LocalCache with unlimited TTL regardless of configuration
      _flagsCache = new LocalCache<Flag>(options.LocalCacheCapacity, TimeSpan.MaxValue); // Flags don't expire

      // Company and User caches use the configured provider type
      if (options.CacheProviderType == DatastreamCacheProviderType.Redis &&
          options.RedisConfig != null)
      {
        try
        {   
          _logger.Info("Initializing Redis cache for Datastream company and user data");
          // We need to use the Cache namespace version, but cast it to the Client namespace interface
          _companyCache = new RedisCache<Company>(options.RedisConfig);
          _userCache = new RedisCache<User>(options.RedisConfig);
        }
        catch (Exception ex)
        {
          _logger.Error("Failed to initialize Redis cache: {0}. Falling back to local cache.", ex.Message);
          _companyCache = new LocalCache<Company>(options.LocalCacheCapacity, _cacheTtl);
          _userCache = new LocalCache<User>(options.LocalCacheCapacity, _cacheTtl);
        }
      }
      else
      {
        // Use local cache (default)
        _companyCache = new LocalCache<Company>(options.LocalCacheCapacity, _cacheTtl);
        _userCache = new LocalCache<User>(options.LocalCacheCapacity, _cacheTtl);
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

      string scheme = uri.Scheme == "https" ? "wss" : "ws";

      var builder = new UriBuilder(uri)
      {
        Scheme = scheme,
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

    private void HandleCompanyMessage(DataStreamResponse response)
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

        if (response.MessageType == MessageType.Delete)
        {
          // Handle deletion by removing company from cache
          foreach (var key in company.Keys)
          {
            var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixCompany, key.Key, key.Value);
            _companyCache.Delete(cacheKey);
          }

          return;
        }

        foreach (var key in company.Keys)
        {
          var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixCompany, key.Key, key.Value);
          _companyCache.Set(cacheKey, company);
        }

        // Notify any pending requests
        List<TaskCompletionSource<Company>> channelsToNotify = new List<TaskCompletionSource<Company>>();

        lock (_pendingRequestsLock)
        {
          foreach (var key in company.Keys)
          {
            var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixCompany, key.Key, key.Value);
            if (_pendingCompanyRequests.TryGetValue(cacheKey, out var channels))
            {
              channelsToNotify.AddRange(channels);
              _pendingCompanyRequests.Remove(cacheKey);
            }
          }
        }

        // Notify outside the lock
        foreach (var channel in channelsToNotify)
        {
          channel.TrySetResult(company);
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
            var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixUser, key.Key, key.Value);
            _userCache.Delete(cacheKey);
            _logger.Debug("Deleted user from cache with key: {0}", cacheKey);
          }
          return;
        }

        foreach (var key in user.Keys)
        {
          var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixUser, key.Key, key.Value);
          _userCache.Set(cacheKey, user);
        }

        // Notify any pending requests
        List<TaskCompletionSource<User>> channelsToNotify = new List<TaskCompletionSource<User>>();

        lock (_pendingRequestsLock)
        {
          foreach (var key in user.Keys)
          {
            var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixUser, key.Key, key.Value);
            if (_pendingUserRequests.TryGetValue(cacheKey, out var channels))
            {
              channelsToNotify.AddRange(channels);
              _pendingUserRequests.Remove(cacheKey);
            }
          }
        }

        // Notify outside the lock
        foreach (var channel in channelsToNotify)
        {
          channel.TrySetResult(user);
        }
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
        if (error != null && !string.IsNullOrEmpty(error.Error))
          _logger.Error("Received error from server: {0}", error.Error);
      }
      catch (Exception ex)
      {
        _logger.Error("Failed to deserialize error message: {0}", ex.Message);
      }
    }

    public async Task<CheckFlagResult> CheckFlagAsync(CheckFlagRequestBody request, string flagKey, CancellationToken cancellationToken = default)
    {
      try
      {
        Company? company = null;
        if (request.Company != null)
        {
          company = await GetCompanyAsync(request.Company, cancellationToken);
        }

        User? user = null;
        if (request.User != null)
        {
          user = await GetUserAsync(request.User, cancellationToken);
        }

        var flag = GetFlag(flagKey);
        if (flag == null)
        {
          return new CheckFlagResult
          {
            Reason = "Flag not found",
            FlagKey = flagKey,
            Error = Errors.ErrorFlagNotFound,
            Value = false,
          };
        }

        var result = await FlagCheckService.CheckFlag(company, user, flag);



        return result;
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

    private async Task<Company> GetCompanyAsync(Dictionary<string, string> keys, CancellationToken cancellationToken)
    {
      var company = GetCompanyFromCache(keys);
      if (company != null)
      {
        return company;
      }

      var waitTask = new TaskCompletionSource<Company>();
      var cacheKeys = new List<string>();
      bool shouldSendRequest = true;

      lock (_pendingRequestsLock)
      {
        foreach (var key in keys)
        {
          var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixCompany, key.Key, key.Value);
          cacheKeys.Add(cacheKey);

          if (_pendingCompanyRequests.TryGetValue(cacheKey, out var existingChannels))
          {
            existingChannels.Add(waitTask);
            shouldSendRequest = false; // Someone else already requested this
          }
          else
          {
            _pendingCompanyRequests[cacheKey] = new List<TaskCompletionSource<Company>> { waitTask };
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

    private async Task<User> GetUserAsync(Dictionary<string, string> keys, CancellationToken cancellationToken)
    {
      var user = GetUserFromCache(keys);
      if (user != null)
      {
        return user;
      }

      var waitTask = new TaskCompletionSource<User>();
      var cacheKeys = new List<string>();
      bool shouldSendRequest = true;

      lock (_pendingRequestsLock)
      {
        foreach (var key in keys)
        {
          var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixUser, key.Key, key.Value);
          cacheKeys.Add(cacheKey);

          if (_pendingUserRequests.TryGetValue(cacheKey, out var existingChannels))
          {
            existingChannels.Add(waitTask);
            shouldSendRequest = false; // Someone else already requested this
          }
          else
          {
            _pendingUserRequests[cacheKey] = new List<TaskCompletionSource<User>> { waitTask };
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
        }else if (_webSocket.State != WebSocketState.Open)
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

    private Flag? GetFlag(string key)
    {
      var flag = _flagsCache.Get(FlagCacheKey(key));
      return flag;
    }

    private Company? GetCompanyFromCache(Dictionary<string, string> keys)
    {
      foreach (var key in keys)
      {
        var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixCompany, key.Key, key.Value);
        var company = _companyCache.Get(cacheKey);
        if (company != null)
        {
          return company;
        }
      }
      return null;
    }

    private User? GetUserFromCache(Dictionary<string, string> keys)
    {
      foreach (var key in keys)
      {
        var cacheKey = ResourceKeyToCacheKey(CacheKeyPrefixUser, key.Key, key.Value);
        var user = _userCache.Get(cacheKey);
        if (user != null)
        {
          return user;
        }
      }
      return null;

    }


    private void CleanupPendingCompanyRequests(List<string> cacheKeys, TaskCompletionSource<Company> waitTask)
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

    private void CleanupPendingUserRequests(List<string> cacheKeys, TaskCompletionSource<User> waitTask)
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

    private string FlagCacheKey(string key)
    {
      return $"{CacheKeyPrefix}:{CacheKeyPrefixFlags}:{key}";
    }

    private string ResourceKeyToCacheKey(string resourceType, string key, string value)
    {
      return $"{CacheKeyPrefix}:{resourceType}:{key}:{value}";
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
      }
    }
  }
}
