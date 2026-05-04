// SDK E2E Test App for schematic-csharp.
//
// HTTP server implementing the shared E2E test app contract defined in the
// SDK spec (SchematicHQ/actions/sdk-e2e). The harness calls POST /configure
// after startup with an env-scoped API key, then runs assertions against the
// other endpoints.
//
// Endpoints:
//   GET  /health            Returns {"status":"waiting"} or {"status":"configured"}
//   POST /configure         Initialize SDK client. Body: apiKey, baseUrl,
//                           eventCaptureBaseUrl, noCache, redisUrl,
//                           useDataStream, replicatorUrl, offline, flagDefaults
//   POST /check-flag        Body: flagKey, company?, user?
//   POST /identify          Body: company?, user?, keys?
//   POST /track             Body: event, company?, user?, quantity?
//   POST /set-flag-default  Body: flagKey, value
//
// Usage:
//   dotnet run --project testapp

using SchematicHQ.Client;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.RulesEngine;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// Cache TTL — short for E2E so tests can sleep past it to verify expiration.
const int CacheTtlMs = 2000;

// State (single-process, single-threaded re-config is fine here)
Schematic? client = null;
Dictionary<string, object?> currentConfig = new();
var stateLock = new object();

string? GetString(Dictionary<string, object?> cfg, string key)
    => cfg.TryGetValue(key, out var v) && v is JsonElement el && el.ValueKind == JsonValueKind.String
        ? el.GetString()
        : null;

bool GetBool(Dictionary<string, object?> cfg, string key)
    => cfg.TryGetValue(key, out var v) && v is JsonElement el && el.ValueKind == JsonValueKind.True;

Dictionary<string, string>? GetStringMap(Dictionary<string, object?> body, string key)
{
    if (!body.TryGetValue(key, out var v) || v is not JsonElement el || el.ValueKind != JsonValueKind.Object)
        return null;
    var result = new Dictionary<string, string>();
    foreach (var prop in el.EnumerateObject())
    {
        result[prop.Name] = prop.Value.ToString();
    }
    return result;
}

Dictionary<string, bool>? GetBoolMap(Dictionary<string, object?> body, string key)
{
    if (!body.TryGetValue(key, out var v) || v is not JsonElement el || el.ValueKind != JsonValueKind.Object)
        return null;
    var result = new Dictionary<string, bool>();
    foreach (var prop in el.EnumerateObject())
    {
        if (prop.Value.ValueKind == JsonValueKind.True) result[prop.Name] = true;
        else if (prop.Value.ValueKind == JsonValueKind.False) result[prop.Name] = false;
    }
    return result;
}

app.MapGet("/health", () =>
{
    var status = client == null ? "waiting" : "configured";
    return Results.Json(new
    {
        status,
        config = new
        {
            offline = GetBool(currentConfig, "offline"),
            useDataStream = GetBool(currentConfig, "useDataStream"),
            cacheTtlMs = CacheTtlMs,
        }
    });
});

app.MapPost("/configure", async (HttpRequest req) =>
{
    Dictionary<string, object?>? config;
    try
    {
        config = await JsonSerializer.DeserializeAsync<Dictionary<string, object?>>(req.Body);
    }
    catch (Exception ex)
    {
        return Results.Json(new { success = false, error = ex.Message }, statusCode: 400);
    }
    if (config == null)
    {
        return Results.Json(new { success = false, error = "empty body" }, statusCode: 400);
    }

    string apiKey = GetString(config, "apiKey") ?? "";
    string? baseUrl = GetString(config, "baseUrl");
    string? eventCaptureBaseUrl = GetString(config, "eventCaptureBaseUrl");
    bool offline = GetBool(config, "offline");
    bool noCache = GetBool(config, "noCache");
    bool useDataStream = GetBool(config, "useDataStream");
    string? redisUrl = GetString(config, "redisUrl");
    string? replicatorUrl = GetString(config, "replicatorUrl");
    var flagDefaults = GetBoolMap(config, "flagDefaults");

    Schematic? oldClient;
    lock (stateLock)
    {
        oldClient = client;
        currentConfig = config;
    }
    if (oldClient != null)
    {
        try { (oldClient as IDisposable)?.Dispose(); } catch { /* ignore */ }
    }

    var options = string.IsNullOrEmpty(baseUrl)
        ? new ClientOptions()
        : new ClientOptions { BaseUrl = baseUrl };
    if (!string.IsNullOrEmpty(eventCaptureBaseUrl)) options.EventCaptureBaseUrl = eventCaptureBaseUrl;
    if (offline) options.Offline = true;
    if (flagDefaults != null) options.FlagDefaults = flagDefaults;

    // Cache configuration
    var cacheTtl = TimeSpan.FromMilliseconds(CacheTtlMs);
    if (noCache)
    {
        // The SDK auto-adds a default LocalCache when CacheProviders is empty.
        // Use a 0-capacity LocalCache so Get/Set are no-ops — effectively no cache.
        options.CacheProviders = new List<ICacheProvider<CheckFlagWithEntitlementResponse?>>
        {
            new LocalCache<CheckFlagWithEntitlementResponse?>(maxItems: 0, ttl: cacheTtl, enableBackgroundCleanup: false)
        };
    }
    // else: SDK default LocalCache (replaced by Redis/datastream wiring below).

    // DataStream / Replicator
    if (useDataStream)
    {
        options.UseDatastream = true;
        var dsOpts = new DatastreamOptions
        {
            CacheTTL = cacheTtl,
        };

        if (!string.IsNullOrEmpty(redisUrl))
        {
            // Strip optional redis:// prefix.
            var endpoint = redisUrl.StartsWith("redis://")
                ? redisUrl.Substring("redis://".Length)
                : redisUrl;
            dsOpts.WithRedisCache(new RedisCacheConfig
            {
                Endpoints = new List<string> { endpoint },
                CacheTTL = cacheTtl,
            });
        }

        options.DatastreamOptions = dsOpts;

        if (!string.IsNullOrEmpty(replicatorUrl))
        {
            options.WithReplicatorMode(replicatorUrl + "/ready");
        }
    }
    else if (!string.IsNullOrEmpty(redisUrl))
    {
        // Redis cache without DataStream.
        var endpoint = redisUrl.StartsWith("redis://")
            ? redisUrl.Substring("redis://".Length)
            : redisUrl;
        options.WithRedisCache(new RedisCacheConfig
        {
            Endpoints = new List<string> { endpoint },
            CacheTTL = cacheTtl,
        });
    }

    var newClient = new Schematic(apiKey, options);

    // When DataStream is enabled, the SDK starts a WebSocket connection asynchronously
    // and won't fall back to the API on cache miss until that connection is established.
    // Wait for the underlying datastream client to report a connection (or timeout) so
    // tests don't race against the initial sync. Uses reflection because the client is
    // not exposed publicly on `Schematic`.
    if (useDataStream && !offline)
    {
        // Wait for the underlying datastream client to report a connection so
        // tests don't race against the initial sync. Uses reflection because the
        // client is not exposed publicly on Schematic.
        try
        {
            var dsField = typeof(Schematic).GetField("_datastreamClient",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            var ds = dsField?.GetValue(newClient);
            if (ds != null)
            {
                var method = ds.GetType().GetMethod("IsConnectedAsync");
                if (method != null)
                {
                    var task = (Task<bool>?)method.Invoke(ds, new object?[] { TimeSpan.FromSeconds(10) });
                    if (task != null) await task;
                }
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"warn: waiting for datastream connection failed: {ex.Message}");
        }

        // The initial flag/cache sync arrives in a separate WebSocket message after
        // connection. Give it a moment to land before serving requests.
        await Task.Delay(2000);
    }

    lock (stateLock)
    {
        client = newClient;
    }

    return Results.Json(new { success = true, cacheTtlMs = CacheTtlMs });
});

app.MapPost("/check-flag", async (HttpRequest req) =>
{
    if (client == null) return Results.Json(new { error = "not configured" }, statusCode: 503);

    Dictionary<string, object?>? body;
    try
    {
        body = await JsonSerializer.DeserializeAsync<Dictionary<string, object?>>(req.Body);
    }
    catch (Exception ex)
    {
        return Results.Json(new { error = ex.Message }, statusCode: 400);
    }
    if (body == null) return Results.Json(new { error = "empty body" }, statusCode: 400);

    string flagKey = GetString(body, "flagKey") ?? "";
    var company = GetStringMap(body, "company");
    var user = GetStringMap(body, "user");

    try
    {
        bool value = await client.CheckFlag(flagKey, company, user);
        return Results.Json(new { value });
    }
    catch (Exception ex)
    {
        return Results.Json(new { value = false, error = ex.Message });
    }
});

app.MapPost("/identify", async (HttpRequest req) =>
{
    if (client == null) return Results.Json(new { error = "not configured" }, statusCode: 503);

    Dictionary<string, object?>? body;
    try
    {
        body = await JsonSerializer.DeserializeAsync<Dictionary<string, object?>>(req.Body);
    }
    catch (Exception ex)
    {
        return Results.Json(new { error = ex.Message }, statusCode: 400);
    }
    if (body == null) return Results.Json(new { error = "empty body" }, statusCode: 400);

    // E2E:  { company: {k:v}, user: {k:v}, keys: {k:v} }
    // SDK:  Identify(keys, EventBodyIdentifyCompany(keys=...))
    var keys = GetStringMap(body, "keys") ?? GetStringMap(body, "user") ?? new Dictionary<string, string>();
    var companyKeys = GetStringMap(body, "company");

    try
    {
        EventBodyIdentifyCompany? company = null;
        if (companyKeys != null && companyKeys.Count > 0)
        {
            company = new EventBodyIdentifyCompany { Keys = companyKeys };
        }
        client.Identify(keys, company);
        return Results.Json(new { success = true });
    }
    catch (Exception ex)
    {
        return Results.Json(new { success = false, error = ex.Message });
    }
});

app.MapPost("/track", async (HttpRequest req) =>
{
    if (client == null) return Results.Json(new { error = "not configured" }, statusCode: 503);

    Dictionary<string, object?>? body;
    try
    {
        body = await JsonSerializer.DeserializeAsync<Dictionary<string, object?>>(req.Body);
    }
    catch (Exception ex)
    {
        return Results.Json(new { error = ex.Message }, statusCode: 400);
    }
    if (body == null) return Results.Json(new { error = "empty body" }, statusCode: 400);

    string eventName = GetString(body, "event") ?? "";
    var company = GetStringMap(body, "company");
    var user = GetStringMap(body, "user");
    int? quantity = null;
    if (body.TryGetValue("quantity", out var q) && q is JsonElement qel && qel.ValueKind == JsonValueKind.Number)
    {
        quantity = qel.GetInt32();
    }

    try
    {
        client.Track(eventName, company, user, traits: null, quantity: quantity);
        return Results.Json(new { success = true });
    }
    catch (Exception ex)
    {
        return Results.Json(new { success = false, error = ex.Message });
    }
});

app.MapPost("/set-flag-default", async (HttpRequest req) =>
{
    if (client == null) return Results.Json(new { error = "not configured" }, statusCode: 503);

    Dictionary<string, object?>? body;
    try
    {
        body = await JsonSerializer.DeserializeAsync<Dictionary<string, object?>>(req.Body);
    }
    catch (Exception ex)
    {
        return Results.Json(new { error = ex.Message }, statusCode: 400);
    }
    if (body == null) return Results.Json(new { error = "empty body" }, statusCode: 400);

    string flagKey = GetString(body, "flagKey") ?? "";
    bool value = GetBool(body, "value");

    try
    {
        client.SetFlagDefault(flagKey, value);
        return Results.Json(new { success = true });
    }
    catch (Exception ex)
    {
        return Results.Json(new { success = false, error = ex.Message });
    }
});

// Configure listen URL
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Clear();
app.Urls.Add($"http://0.0.0.0:{port}");

Console.WriteLine($"SDK E2E test app listening on http://localhost:{port}");
Console.WriteLine("Waiting for POST /configure to initialize SchematicClient...");

app.Run();
