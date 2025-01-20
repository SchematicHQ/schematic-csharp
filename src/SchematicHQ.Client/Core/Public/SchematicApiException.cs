using System;

#nullable enable

namespace SchematicHQ.Client;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class SchematicApiException(string message, Exception? innerException = null)
    : Exception(message, innerException) { }
