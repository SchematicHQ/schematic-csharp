# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is the official Schematic .NET client library for C#, supporting .NET Standard, .NET Core, and .NET Framework. It provides a convenient way to interact with the Schematic API for feature flags, user management, and analytics.

## Repository Structure

- `src/` - Contains the source code for the library
  - `SchematicHQ.Client/` - The main client library
  - `SchematicHQ.Client.Test/` - Unit tests for the client library
  - `SchematicHQ.Client.Example/` - Example application using the client library
- `scripts/` - Contains utility scripts like webhook-test-server

## Development Commands

### Building the Project

```bash
# Build the entire solution
dotnet build src

# Build in Release mode
dotnet build src -c Release
```

### Running Tests

```bash
# Run all tests
dotnet test src

# Run specific test class
dotnet test --filter "FullyQualifiedName~SchematicHQ.Client.Tests.EventBufferTests"

# Run specific test method
dotnet test --filter "FullyQualifiedName~SchematicHQ.Client.Tests.EventBufferTests.Push_ItemsAreAddedToBuffer"
```

### Packaging

```bash
# Package for NuGet
dotnet pack src -c Release
```

## Architecture Overview

### Key Components

1. **Schematic Client**
   - Main entry point for the API, instantiated with an API key
   - Provides methods for user/company identification, event tracking, and flag checking

2. **Event Buffer**
   - Batches events (identify, track) for efficient API usage
   - Provides retry logic with exponential backoff
   - Handles asynchronous processing of events

3. **Cache**
   - Local caching mechanism for feature flags
   - Configurable TTL and size

4. **API Clients**
   - Feature-specific clients (Companies, Features, Events, etc.)
   - Each handles its own area of the Schematic API

5. **Webhook Utils**
   - Utilities for verifying webhook signatures
   - Ensures security of webhook communication

### Key Concepts

- **Feature Flags** - The library provides methods to check feature flag values for companies and users
- **Events** - Track user actions and identify users/companies with their traits
- **Identify vs. Track** - Two primary event types for different purposes
- **Retry Logic** - Built-in retries for resilience
- **Type-Safe API** - Strongly typed request/response objects

## Core Patterns

1. **Async-first API**
   - Most methods return `Task` or `Task<T>` for asynchronous operation
   - Event buffer provides non-blocking event processing

2. **Client Options Pattern**
   - Configuration via `ClientOptions` parameter during client initialization
   - Supports caching, offline mode, retries, timeouts, etc.

3. **Generic Buffer Implementation**
   - Event buffer is implemented generically for reusability
   - Thread-safe implementation with concurrent collections

4. **Immutable Request/Response Objects**
   - Request and response objects are designed to be immutable
   - Clear separation between request parameters and response data

## Important Implementation Details

1. **Multi-framework Support**
   - Library targets multiple frameworks: net462, net8.0, net7.0, net6.0, netstandard2.0
   - Portable DateOnly implementation for older frameworks

2. **Error Handling**
   - Exception hierarchy with specific exception types
   - Automatic retries for certain error types (429, 5xx)

3. **Security Considerations**
   - Webhook verification using HMAC-SHA256
   - Secure handling of API keys