# Schematic .NET Library

The official Schematic C# library, supporting .NET Standard, .NET Core, and .NET Framework.

## Installation and Setup

1. Install the library using the .NET Core command-line interface (CLI) tools:

```sh
dotnet add package SchematicHQ.Client
```

or using the NuGet Command Line Interface (CLI):

```sh
nuget install SchematicHQ.Client
```

2. [Issue an API key](https://docs.schematichq.com/quickstart#create-an-api-key) for the appropriate environment using the [Schematic app](https://app.schematichq.com/settings/api-keys).

3. Using this secret key, initialize a client in your application:

```csharp
using SchematicHQ;

Schematic schematic = new Schematic("YOUR_API_KEY")
```

## Usage

A number of these examples use `keys` to identify companies and users. Learn more about keys [here](https://docs.schematichq.com/developer_resources/key_management).

### Sending identify events

Create or update users and companies using identify events.

```csharp
using SchematicHQ.Client;
using System.Collections.Generic;
using OneOf;

Schematic schematic = new Schematic("YOUR_API_KEY");

schematic.Identify(
    keys: new Dictionary<string, string>
    {
        { "email", "wcoyote@acme.net" },
        { "user_id", "your-user-id" }
    },
    company: new EventBodyIdentifyCompany
    {
        Keys = new Dictionary<string, string> { { "id", "your-company-id" } },
        Name = "Acme Widgets, Inc.",
        Traits = new Dictionary<string, OneOf<string, double, bool, OneOf<string, double, bool>>>
        {
            { "city", "Atlanta" }
        }
    },
    name: "Wile E. Coyote",
    traits: new Dictionary<string, OneOf<string, double, bool, OneOf<string, double, bool>>>
    {
        { "login_count", 24 },
        { "is_staff", false }
    }
);

// to guarantee that all events are sent before the application exits, call this method before your program shuts down
await schematic.Shutdown();
```

This call is non-blocking and there is no response to check.

### Sending track events

Track activity in your application using track events; these events can later be used to produce metrics for targeting.

```csharp
Schematic schematic = new Schematic("YOUR_API_KEY");
schematic.Track(
    eventName: "some-action",
    user: new Dictionary<string, string> { { "user_id", "your-user-id" } },
    company: new Dictionary<string, string> { { "id", "your-company-id" } }
);

// to guarantee that all events are sent before the application exits, call this method before your program shuts down
await schematic.Shutdown();
```

This call is non-blocking and there is no response to check.

If you want to record large numbers of the same event at once, or perhaps measure usage in terms of a unit like tokens or memory, you can optionally specify a quantity for your event:

```csharp
schematic.Track(
    eventName: "some-action",
    user: new Dictionary<string, string> { { "user_id", "your-user-id" } },
    company: new Dictionary<string, string> { { "id", "your-company-id" } },
    quantity: 10
);
```

### Creating and updating companies

Although it is faster to create companies and users via identify events, if you need to handle a response, you can use the companies API to upsert companies. Because you use your own identifiers to identify companies, rather than a Schematic company ID, creating and updating companies are both done via the same upsert operation:


```csharp
using SchematicHQ.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

Schematic schematic = new Schematic("YOUR_API_KEY");

// Creating and updating companies
async Task UpsertCompanyExample()
{
    var response = await schematic.Companies.UpsertCompanyAsync(new UpsertCompanyRequestBody
    {
        Keys = new Dictionary<string, string> { { "id", "your-company-id" } },
        Name = "Acme Widgets, Inc.",
        Traits = new Dictionary<string, object>
        {
            { "city", "Atlanta" },
            { "high_score", 25 },
            { "is_active", true }
        }
    });

    // Handle the response as needed
    Console.WriteLine($"Company upserted: {response.Data.Name}");
}
```

You can define any number of company keys; these are used to address the company in the future, for example by updating the company's traits or checking a flag for the company.

You can also define any number of company traits; these can then be used as targeting parameters.

### Creating and updating users

Similarly, you can upsert users using the Schematic API, as an alternative to using identify events. Because you use your own identifiers to identify users, rather than a Schematic user ID, creating and updating users are both done via the same upsert operation:

```csharp
using SchematicHQ.Client;
using System.Collections.Generic;
using System.Threading.Tasks;

Schematic schematic = new Schematic("YOUR_API_KEY");

// Creating and updating users
async Task UpsertUserExample()
{
    var response = await schematic.Companies.UpsertUserAsync(new UpsertUserRequestBody
    {
        Keys = new Dictionary<string, string>
        {
            { "email", "wcoyote@acme.net" },
            { "user_id", "your-user-id" }
        },
        Name = "Wile E. Coyote",
        Traits = new Dictionary<string, object>
        {
            { "city", "Atlanta" },
            { "high_score", 25 },
            { "is_active", true }
        },
        Company = new Dictionary<string, string> { { "id", "your-company-id" } }
    });

    // Handle the response as needed
    Console.WriteLine($"User upserted: {response.Data.Name}");
}
```

You can define any number of user keys; these are used to address the user in the future, for example by updating the user's traits or checking a flag for the user.

You can also define any number of user traits; these can then be used as targeting parameters.

### Checking flags

When checking a flag, you'll provide keys for a company and/or keys for a user. You can also provide no keys at all, in which case you'll get the default value for the flag.

```csharp
Schematic schematic = new Schematic("YOUR_API_KEY");

bool flagValue = await schematic.CheckFlag(
    "some-flag-key",
    company: new Dictionary<string, string> { { "id", "your-company-id" } },
    user: new Dictionary<string, string> { { "user_id", "your-user-id" } }
);
```

### OpenFeature Integration

The Schematic .NET SDK includes built-in support for [OpenFeature](https://openfeature.dev/), allowing you to use Schematic's feature flags through the OpenFeature standard API.

#### Using Schematic with OpenFeature

```csharp
using OpenFeature;
using SchematicHQ.Client.OpenFeature;

// Create and set the Schematic provider
var provider = new SchematicProvider("YOUR_API_KEY");
await Api.Instance.SetProviderAsync(provider);

// Get the OpenFeature client
var client = Api.Instance.GetClient();

// Evaluate a boolean feature flag
var isEnabled = await client.GetBooleanValue("your-flag-key", false);
```

#### OpenFeature Context

The Schematic provider supports company and user context through OpenFeature's evaluation context:

```csharp
var context = EvaluationContext.Builder()
    .Set("company", new Structure(new Dictionary<string, Value>
    {
        ["id"] = new Value("company-123"),
        ["name"] = new Value("Acme Corp"),
        ["plan"] = new Value("enterprise")
    }))
    .Set("user", new Structure(new Dictionary<string, Value>
    {
        ["id"] = new Value("user-456"),
        ["email"] = new Value("user@example.com"),
        ["role"] = new Value("admin")
    }))
    .Build();

// Evaluate with context
var isEnabled = await client.GetBooleanValue("your-flag-key", false, context);
```

#### Event Tracking with OpenFeature

The provider includes a method to track events:

```csharp
var provider = (SchematicProvider)Api.Instance.GetProvider();

await provider.TrackEventAsync(
    "button_clicked",
    context,
    new Dictionary<string, object>
    {
        ["button_name"] = "submit",
        ["page"] = "checkout"
    }
);
```

## Webhook Verification

Schematic can send webhooks to notify your application of events. To ensure the security of these webhooks, Schematic signs each request using HMAC-SHA256. The .NET SDK provides utility functions to verify these signatures.

### Verifying Webhook Signatures

When your application receives a webhook request from Schematic, you should verify its signature to ensure it's authentic:

```csharp
using SchematicHQ.Client.Webhooks.WebhookUtils;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class WebhooksController : ControllerBase
{
    [HttpPost("schematic")]
    public async Task<IActionResult> HandleSchematicWebhook()
    {
        try
        {
            // Read the request body
            string body;
            using (var reader = new StreamReader(Request.Body))
            {
                body = await reader.ReadToEndAsync();
            }

            // Extract headers into a dictionary
            var headers = new Dictionary<string, string>();
            foreach (var header in Request.Headers)
            {
                headers[header.Key] = header.Value;
            }

            // Each webhook has a distinct secret; you can access this via the Schematic app
            string webhookSecret = "your-webhook-secret";

            // Verify the webhook signature
            WebhookVerifier.VerifyWebhookSignature(body, headers, webhookSecret);

            // Process the webhook payload
            // ...

            return Ok();
        }
        catch (WebhookSignatureException ex)
        {
            // Handle signature verification failure
            return Unauthorized(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            // Handle other errors
            return StatusCode(500, new { error = "Internal server error" });
        }
    }
}
```

### Verifying Signatures Manually

If you need to verify a webhook signature outside of the context of an HTTP request, you can use the `VerifySignature` method:

```csharp
using SchematicHQ.Client.Webhooks.WebhookUtils;

public bool VerifyWebhookManually(string body, string signature, string timestamp, string secret)
{
    try
    {
        WebhookVerifier.VerifySignature(body, signature, timestamp, secret);
        Console.WriteLine("Signature verification successful!");
        return true;
    }
    catch (WebhookSignatureException ex)
    {
        Console.WriteLine($"Signature verification failed: {ex.Message}");
        return false;
    }
}
```

## Configuration Options

There are a number of configuration options that can be specified passing in `ClientOptions` as a second parameter when instantiating the Schematic client.

### Flag Check Options

By default, the client will do some local caching for flag checks. If you would like to change this behavior, you can do so using an initialization option to specify the max size of the cache (in terms of number of cached items) and the max age of the cache:

```csharp
using SchematicHQ.Client;
using System.Collections.Generic;

int cacheMaxItems = 1000; // Max number of entries in the cache
TimeSpan cacheTtl = TimeSpan.FromSeconds(1); // Set TTL to 1 second

var options = new ClientOptions
{
    CacheProviders = new List<ICacheProvider<bool?>>
    {
        new LocalCache<bool?>(cacheMaxItems, cacheTtl)
    }
};

Schematic schematic = new Schematic("YOUR_API_KEY", options);
```
***Note about LocalCache:*** LocalCache implementation returns default value the type it is initiated with when it is a cache miss. Hence, when using with Schematic it is initiated with type (bool?) so that cache returns null when it is a miss

### Redis Cache Configuration

For distributed applications or when you need persistent caching across application restarts, you can use Redis as your cache provider:

```csharp
using SchematicHQ.Client;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;

var options = new ClientOptions
{
    CacheConfiguration = new CacheConfiguration
    {
        ProviderType = CacheProviderType.Redis,
        RedisConfig = new RedisCacheConfig
        {
            // Required: Redis server endpoints
            Endpoints = new List<string> { "redis.example.com:6379" },

            // Optional: Authentication
            Password = "your-redis-password",
            Username = "default",  // For Redis 6.0+ ACL

            // Optional: Connection settings
            Ssl = true,
            ConnectTimeout = 5000,  // milliseconds
            ConnectRetry = 3,
            AbortOnConnectFail = false,  // Continue if Redis is unavailable

            // Optional: Cache settings
            KeyPrefix = "schematic:",
            Database = 0,
            CacheTTL = TimeSpan.FromMinutes(5)
        }
    }
};

Schematic schematic = new Schematic("YOUR_API_KEY", options);
```

You can also use the fluent API for simpler Redis configuration:

```csharp
var options = new ClientOptions()
    .WithRedisCache(new RedisCacheConfig
    {
        Endpoints = new List<string> { "redis.example.com:6379" },
        Password = "secret",
        Username = "default"  // For Redis 6.0+ ACL
    });
```

#### Redis Cluster Configuration

For Redis Cluster deployments, use the `RedisCacheClusterConfig` class:

```csharp
var options = new ClientOptions
{
    CacheConfiguration = new CacheConfiguration
    {
        ProviderType = CacheProviderType.Redis,
        RedisConfig = new RedisCacheClusterConfig
        {
            // Multiple cluster nodes
            Endpoints = new List<string>
            {
                "cluster1.example.com:6379",
                "cluster2.example.com:6379",
                "cluster3.example.com:6379"
            },

            // Authentication
            Password = "cluster-password",
            Username = "default",  // For Redis 6.0+ ACL

            // Cluster optimization
            RouteByLatency = true,  // Enable latency-based routing

            // Other settings
            ConnectTimeout = 10000,
            AbortOnConnectFail = false
        }
    }
};
```

You can also disable local caching entirely; bear in mind that, in this case, every flag check will result in a network request:

```csharp
using SchematicHQ.Client;
using System.Collections.Generic;

var options = new ClientOptions
{
    CacheProviders = new List<ICacheProvider<bool?>>()
};

Schematic schematic = new Schematic("YOUR_API_KEY", options);
```

You may want to specify default flag values for your application, which will be used if there is a service interruption or if the client is running in offline mode (see below):

```csharp
using SchematicHQ.Client;
using System.Collections.Generic;

var options = new ClientOptions
{
    FlagDefaults = new Dictionary<string, bool>
    {
        { "some-flag-key", true }
    }
};

Schematic schematic = new Schematic("YOUR_API_KEY", options);
```

### Offline Mode

In development or testing environments, you may want to avoid making network requests to the Schematic API. You can run Schematic in offline mode by specifying the `Offline` option; in this case, it does not matter what API key you specify:

```csharp
using SchematicHQ.Client;

var options = new ClientOptions
{
    Offline = true
};

Schematic schematic = new Schematic("", options); // API key doesn't matter in offline mode
```

Offline mode works well with flag defaults:

```csharp
using SchematicHQ.Client;
using System.Collections.Generic;

var options = new ClientOptions
{
    FlagDefaults = new Dictionary<string, bool>
    {
        { "some-flag-key", true }
    },
    Offline = true
};

Schematic schematic = new Schematic("", options);

bool flagValue = await schematic.CheckFlag("some-flag-key"); // Returns true
```

### Event Buffer
Schematic API uses an Event Buffer to batch *Identify* and *Track* requests and avoid multiple API calls.
You can set the event buffer flush period in options:
```csharp
using SchematicHQ.Client;

var options = new ClientOptions
{
	DefaultEventBufferPeriod = TimeSpan.FromSeconds(5)
};
```
You may also want to use your custom event buffer. To do so, your custom event buffer has to implement *IEventBuffer* interface, and pass an instance to the Schematic API through options:
```csharp
using SchematicHQ.Client;

var options = new ClientOptions
{
	EventBuffer = new MyCustomEventBuffer();//instance of your custom event buffer
}
```

### HTTP Client
You can override the HttpClient:

```csharp
schematic = new Schematic("YOUR_API_KEY", new ClientOptions{
    HttpClient = ... // Override the Http Client
    BaseURL = ... // Override the Base URL
})
```

### Retries
429 Rate Limit, and >=500 Internal errors will all be
retried twice with exponential backoff. You can override this behavior
globally or per-request.

```csharp
var schematic = new Schematic("...", new ClientOptions{
    MaxRetries = 1 // Only retry once
});
```

### Timeouts
The SDK defaults to a 60s timeout. You can override this behaviour
globally or per-request.

```csharp
var schematic = new Schematic("...", new ClientOptions{
    TimeoutInSeconds = 20 // Lower timeout
});
```

## Exception Handling
When the API returns a non-zero status code, (4xx or 5xx response),
a subclass of SchematicException will be thrown:

```csharp
using SchematicHQ;

try {
  schematic.Accounts.ListApiKeysAsync(...);
} catch (SchematicException e) {
  System.Console.WriteLine(e.Message)
  System.Console.WriteLine(e.StatusCode)
}
```

## Datastream

Datastream is Schematic's real-time connection service that optimizes flag check performance and reliability. When enabled, the Schematic client maintains a WebSocket connection to our servers, which pushes down feature flag definitions, company data, and user data as needed.

### Benefits of Datastream

- **Improved Performance**: Flag checks become near-instantaneous after the initial data load
- **Reduced API Load**: Minimizes HTTP requests to the Schematic API
- **Real-time Updates**: Flag changes are immediately pushed to your application
- **Fault Tolerance**: Falls back to standard API requests when needed

### Enabling Datastream

**Important**: Datastream is disabled by default. You must explicitly enable it in your client options:

```csharp
using SchematicHQ.Client;

// Create options with Datastream enabled
var options = new ClientOptions
{
    UseDatastream = true  // Enable Datastream
};

// Initialize client with options
var schematic = new Schematic("YOUR_API_KEY", options);
```

### Customizing Datastream

You can customize Datastream's behavior through additional client options:

```csharp
using SchematicHQ.Client;
using SchematicHQ.Client.Cache;
using SchematicHQ.Client.Datastream;

// Create options with Datastream configuration
var options = new ClientOptions
{
    // Enable Datastream (required)
    UseDatastream = true,

    // Configure Datastream-specific options
    DatastreamOptions = new DatastreamOptions()
        // Use Redis cache for better performance in distributed environments
        .WithRedisCache(new RedisCacheConfig
        {
            Endpoints = new List<string>
            {
                "redis-primary.example.com:6379",
                "redis-replica.example.com:6379"
            },
            Password = "your-redis-password",  // Optional
            Username = "default",  // Optional (Redis 6.0+ ACL)
            KeyPrefix = "my-app:",
            CacheTTL = TimeSpan.FromMinutes(10),
            Database = 0,
            Ssl = true  // Enable SSL if needed
        })
        // Or use local cache for single-instance applications
        //.WithLocalCache(capacity: 10000, ttl: TimeSpan.FromMinutes(5))
};

// Initialize client with options
var schematic = new Schematic("YOUR_API_KEY", options);
```

### Using Datastream with Shared Cache Configuration

You can configure your main client cache and Datastream to use the same cache settings:

```csharp
using SchematicHQ.Client;
using SchematicHQ.Client.Cache;

// Configure with multiple Redis endpoints for high availability
var options = new ClientOptions
{
    // Enable Datastream
    UseDatastream = true,

    // Create cache configuration with Redis settings (used by both main client and Datastream)
    CacheConfiguration = new CacheConfiguration
    {
        ProviderType = CacheProviderType.Redis,
        RedisConfig = new RedisCacheConfig
        {
            Endpoints = new List<string>
            {
                "redis-1.example.com:6379",
                "redis-2.example.com:6379",
                "redis-3.example.com:6379"
            },
            Password = "your-redis-password",  // Optional
            Username = "default",              // Optional (Redis 6.0+)
            KeyPrefix = "prod:",
            CacheTTL = TimeSpan.FromMinutes(10),
            Ssl = true,                       // Enable SSL if needed
            ConnectTimeout = 5000,            // Connection timeout in ms
            AbortOnConnectFail = false        // Continue if Redis is unavailable
        }
    }
};

// Schematic will use the same cache configuration for both
// the main client and Datastream client
var schematic = new Schematic("YOUR_API_KEY", options);
```

### Checking Flags with Datastream

The flag checking experience remains the same whether Datastream is enabled or not:

```csharp
// Check a feature flag with Datastream enabled
bool flagValue = await schematic.CheckFlag(
    "premium-feature",
    company: new Dictionary<string, string> { { "id", "company-123" } },
    user: new Dictionary<string, string> { { "email", "user@example.com" } }
);

// Use the flag result
if (flagValue) {
    // Enable premium feature
} else {
    // Use standard feature
}
```

The difference is that with Datastream enabled, after the initial data load, subsequent flag checks for the same company and user will be nearly instantaneous and won't require additional network requests.

## Contributing
While we value open-source contributions to this SDK, this library
is generated programmatically. Additions made directly to this library
would have to be moved over to our generation code, otherwise they would
be overwritten upon the next generated release. Feel free to open a PR as a
proof of concept, but know that we will not be able to schematic it as-is.
We suggest opening an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!
