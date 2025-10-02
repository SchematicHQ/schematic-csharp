# Replicator Mode Implementation for C# Schematic Client

This document describes the implementation of replicator mode in the C# Schematic client, which mirrors the functionality available in the Go client.

## Overview

Replicator mode allows the Schematic client to operate using only cached data from a separate replicator service, reducing API calls and improving performance for high-traffic applications.

## Key Features

### 1. Configuration Options

Two new properties have been added to `ClientOptions`:

- `ReplicatorMode`: Boolean flag to enable replicator mode
- `ReplicatorHealthUrl`: Health check URL for the replicator service (required when ReplicatorMode is true)

### 2. Health Check Service

The `ReplicatorHealthService` class provides:

- Background health monitoring of the replicator service
- Configurable health check interval (default: 30 seconds)
- Proper logging of health status changes
- Graceful shutdown and resource cleanup

### 3. Modified CheckFlag Behavior

When replicator mode is enabled:

1. **Normal Operation**: Uses datastream client to evaluate flags with cached company/user data
2. **Health Monitoring**: Continuously monitors external replicator health in the background
3. **Error Handling**: If datastream fails and replicator is unhealthy, falls back to API calls
4. **Cache-Only Evaluation**: Never fetches missing data over WebSocket, evaluates with available cache

### 4. Automatic Datastream Disabling

When `ReplicatorMode` is enabled, the datastream connection is automatically disabled to prevent duplicate connections and potential conflicts.

## Usage Example

```csharp
using SchematicHQ.Client;

// Configure client with replicator mode
var options = new ClientOptions()
    .WithReplicatorMode("http://localhost:8080/health")
    .WithLocalCache(capacity: 10000, ttl: TimeSpan.FromHours(1));

// Initialize client
var schematic = new Schematic("your-api-key", options);

// Check flags - uses datastream client with cached data only
var flagValue = await schematic.CheckFlag("feature-flag", company, user);

// Clean up
await schematic.Shutdown();
```

## Configuration Methods

### Extension Method

```csharp
public static ClientOptions WithReplicatorMode(
    this ClientOptions options,
    string healthUrl)
```

This convenience method:
- Enables replicator mode
- Sets the health URL
- Ensures datastream client is created for cache access but without WebSocket connections

### Manual Configuration

```csharp
var options = new ClientOptions
{
    ReplicatorMode = true,
    ReplicatorHealthUrl = "http://replicator:8080/health"
};
```

## Health Check Behavior

The health service:
- Performs HTTP GET requests to the configured health URL with 5-second timeout
- Expects JSON response with `{"ready": true/false}` format
- Logs health status changes (ready ↔ not ready transitions)
- Runs checks every 30 seconds by default (configurable)
- Handles network errors and JSON parsing failures gracefully

### Health Status Methods

```csharp
// Check if running in replicator mode
bool isReplicatorMode = schematic.IsReplicatorMode();

// Check if external replicator is healthy (only valid in replicator mode)
bool isHealthy = schematic.IsReplicatorHealthy();
```

## Error Handling and Fallback

- **Validation**: Throws `ArgumentException` if ReplicatorMode is true but ReplicatorHealthUrl is null/empty
- **Datastream Errors**: Returns flag default values when datastream evaluation fails in replicator mode
- **Cache Misses**: Evaluates flags with nil company/user data instead of fetching over WebSocket
- **Health Monitoring**: Continuously monitors external replicator health but doesn't block flag evaluation
- **Network Errors**: Logs errors and updates replicator health status

## Logging

The implementation provides comprehensive logging:

- Replicator mode initialization
- Health check status changes
- Cache hits/misses in replicator mode
- Fallback behavior when unhealthy
- Service shutdown events

## Comparison with Go Client

The C# implementation maintains feature parity with the Go client:

| Feature | Go Client | C# Client |
|---------|-----------|-----------|
| ReplicatorMode config | ✅ | ✅ |
| Health URL config | ✅ | ✅ |
| Background health checks | ✅ | ✅ |
| Cached-only when healthy | ✅ | ✅ |
| API fallback when unhealthy | ✅ | ✅ |
| Datastream disabling | ✅ | ✅ |
| Comprehensive logging | ✅ | ✅ |

## Implementation Files

1. **ClientOptionsCustom.cs**: Configuration options and extension methods
2. **Datastream/ReplicatorHealthService.cs**: Health monitoring service  
3. **Datastream/DatastreamClientAdapter.cs**: Modified to support replicator mode
4. **Schematic.cs**: Modified constructor and CheckFlag method for replicator mode
5. **examples/ReplicatorModeExample.cs**: Usage example

## Future Enhancements

Potential improvements could include:

- Configurable health check intervals
- Circuit breaker patterns for health failures
- Metrics collection for health check performance
- Support for authentication in health check requests