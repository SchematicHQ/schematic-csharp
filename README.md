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
    var response = await schematic.API.Companies.UpsertCompanyAsync(new UpsertCompanyRequestBody
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
    var response = await schematic.API.Companies.UpsertUserAsync(new UpsertUserRequestBody
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

## Contributing
While we value open-source contributions to this SDK, this library
is generated programmatically. Additions made directly to this library
would have to be moved over to our generation code, otherwise they would
be overwritten upon the next generated release. Feel free to open a PR as a
proof of concept, but know that we will not be able to schematic it as-is.
We suggest opening an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!
