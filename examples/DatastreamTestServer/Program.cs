/*
 * Schematic C# Client - Replicator Mode Example
 * 
 * This example demonstrates how to use the Schematic C# client in replicator mode.
 * In replicator mode, the client only uses cached data from a replicator service
 * and falls back to the API if the replicator is not ready.
 * 
 * Environment Variables:
 * - SCHEMATIC_API_KEY: Your Schematic API key (required)
 * - SCHEMATIC_API_URL: Schematic API base URL (default: https://api.schematichq.com)
 * - REPLICATOR_HEALTH_URL: Readiness check URL for replicator service (default: http://localhost:8090/ready)
 * - REDIS_URL: Redis connection string (default: localhost:6380)
 * - REDIS_KEY_PREFIX: Redis key prefix (default: schematic:)
 * - CACHE_TTL_MS: Cache TTL in milliseconds (default: 5000)
 * 
 * Prerequisites:
 * - Redis server running on localhost:6379
 * - Schematic replicator service running with health endpoint
 * 
 * Usage:
 * 1. Set environment variables (only SCHEMATIC_API_KEY is required)
 * 2. Start Redis: docker run -d -p 6380:6379 redis:alpine
 * 3. Start replicator service (see schematic-datastream-replicator)
 * 4. Run this example: dotnet run
 * 5. Test endpoints:
 *    - GET / - Welcome message
 *    - GET /config - Show current configuration
 *    - GET /health - Check service health with config
 *    - GET /replicator-status - Check replicator mode status
 *    - POST /checkflag - Check feature flags
 * 
 * Example usage:
 * export SCHEMATIC_API_KEY="your-api-key"
 * export SCHEMATIC_API_URL="https://api.schematichq.com"
 * export REPLICATOR_HEALTH_URL="http://localhost:8090/ready"
 * dotnet run
 */

using Microsoft.Extensions.Options;
using SchematicHQ.Client;
using SchematicHQ.Client.Datastream;
using SchematicHQ.Client.Cache;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configuration from environment variables
string apiKey = Environment.GetEnvironmentVariable("SCHEMATIC_API_KEY") ?? 
    throw new InvalidOperationException("SCHEMATIC_API_KEY environment variable is not set");

string schematicApiUrl = Environment.GetEnvironmentVariable("SCHEMATIC_API_URL") ?? 
    "https://api.schematichq.com"; // Default Schematic API URL

string replicatorHealthUrl = Environment.GetEnvironmentVariable("REPLICATOR_HEALTH_URL") ?? 
    "http://localhost:8090/ready"; // Default health check URL

string redisUrl = Environment.GetEnvironmentVariable("REDIS_URL") ?? 
    "localhost:6380"; // Default Redis URL

string redisKeyPrefix = Environment.GetEnvironmentVariable("REDIS_KEY_PREFIX") ?? 
    ""; // Default Redis key prefix

int cacheTtlMs = int.TryParse(Environment.GetEnvironmentVariable("CACHE_TTL_MS"), out int ttl) ? ttl : 5000;

// Configure Redis cache
var redisConfig = new RedisCacheConfig
{
    Endpoints = new List<string> { redisUrl },
    KeyPrefix = ""
};

var datastreamOptions = new DatastreamOptions
{
    CacheTTL = TimeSpan.FromMilliseconds(cacheTtlMs),
};

datastreamOptions.WithRedisCache(redisConfig);

var options = new ClientOptions
{
    BaseUrl = schematicApiUrl, // Use configurable API URL
    DatastreamOptions = datastreamOptions
}
    .WithReplicatorMode(replicatorHealthUrl); // Enable replicator mode with health check

Schematic schematic = new Schematic(apiKey, options);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.MapGet("/", () => "Welcome to the Schematic Replicator Mode Test Server!");

// Configuration endpoint
app.MapGet("/config", () => Results.Ok(new { 
    replicatorMode = schematic.IsReplicatorMode(),
    configuration = new {
        schematicApiUrl,
        replicatorHealthUrl,
        redisUrl,
        redisKeyPrefix,
        cacheTtlMs,
        hasApiKey = !string.IsNullOrEmpty(apiKey)
    },
    endpoints = new {
        health = "/health",
        config = "/config", 
        replicatorStatus = "/replicator-status",
        checkFlag = "/checkflag (POST)"
    },
    timestamp = DateTime.UtcNow 
})).WithName("Configuration");

// Health check endpoint
app.MapGet("/health", () => Results.Ok(new { 
    status = "healthy", 
    replicatorMode = schematic.IsReplicatorMode(),
    configuration = new {
        schematicApiUrl,
        replicatorHealthUrl,
        redisUrl,
        redisKeyPrefix,
        cacheTtlMs
    },
    timestamp = DateTime.UtcNow 
})).WithName("Health");

// Replicator status endpoint
app.MapGet("/replicator-status", () => Results.Ok(new { 
    replicatorMode = schematic.IsReplicatorMode(),
    configuration = new {
        schematicApiUrl,
        replicatorHealthUrl
    },
    timestamp = DateTime.UtcNow 
})).WithName("ReplicatorStatus");

app.MapPost("/checkflag", async (CheckFlagRequestBody request) =>
{
    try 
    {
        var startTime = DateTime.UtcNow;
        var result = await schematic.CheckFlag(request.FlagKey, request.Company, request.User);
        var duration = DateTime.UtcNow - startTime;
        
        return Results.Ok(new { 
            flagKey = request.FlagKey,
            result,
            replicatorMode = schematic.IsReplicatorMode(),
            duration = duration.TotalMilliseconds,
            timestamp = DateTime.UtcNow
        });
    }
    catch (Exception ex)
    {
        return Results.Problem(
            detail: ex.Message,
            title: "Flag Check Failed",
            statusCode: 500
        );
    }
}).WithName("CheckFlag");

app.Run();

record CheckFlagRequestBody
{
    [JsonPropertyName("company")]
    public Dictionary<string, string>? Company { get; init; } = null;
    [JsonPropertyName("user")]
    public Dictionary<string, string>? User { get; init; } = null;
    [JsonPropertyName("flag-key")]
    public string FlagKey { get; init; } = string.Empty;
};