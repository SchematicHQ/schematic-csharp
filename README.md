# Schematic .NET Library

[![fern shield](https://img.shields.io/badge/%F0%9F%8C%BF-SDK%20generated%20by%20Fern-brightgreen)](https://github.com/fern-api/fern)

The official Schematic C# library, supporting .NET Standard, .NET Core, and .NET Framework.

## Installation

Using the .NET Core command-line interface (CLI) tools:

```sh
dotnet add package SchematicHQ.Client
```

Using the NuGet Command Line Interface (CLI):

```sh
nuget install SchematicHQ.Client
```

## Instantiation
Instantiate the SDK using the `Schematic` client class. Note that all
of the SDK methods are awaitable!

```csharp
using SchematicHQ;

Schematic schematic = new Schematic("YOUR_API_KEY")
```

## HTTP Client
You can override the HttpClient by passing in `ClientOptions`.

```csharp
schematic = new Schematic("YOUR_API_KEY", new ClientOptions{
    HttpClient = ... // Override the Http Client
    BaseURL = ... // Override the Base URL
})
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

## Usage

Below are code snippets of how you can use the C# SDK.

```csharp
using SchematicHQ;

Schematic schematic = new Schematic("YOUR_API_KEY")
Employee employee = schematic.Accounts.ListApiKeysAsync(
    new ListApiKeysRequest{
        RequireEnvironment = true
    }
);
```

## Retries
429 Rate Limit, and >=500 Internal errors will all be
retried twice with exponential backoff. You can override this behavior
globally or per-request.

```csharp
var schematic = new Schematic("...", new ClientOptions{
    MaxRetries = 1 // Only retry once
});
```

## Timeouts
The SDK defaults to a 60s timeout. You can override this behaviour
globally or per-request.

```csharp
var schematic = new Schematic("...", new ClientOptions{
    TimeoutInSeconds = 20 // Lower timeout
});
```

## Contributing
While we value open-source contributions to this SDK, this library
is generated programmatically. Additions made directly to this library
would have to be moved over to our generation code, otherwise they would
be overwritten upon the next generated release. Feel free to open a PR as a
proof of concept, but know that we will not be able to schematic it as-is.
We suggest opening an issue first to discuss with us!

On the other hand, contributions to the README are always very welcome!
