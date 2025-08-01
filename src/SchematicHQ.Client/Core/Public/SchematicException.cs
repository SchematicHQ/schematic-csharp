namespace SchematicHQ.Client;

/// <summary>
/// Base exception class for all exceptions thrown by the SDK.
/// </summary>
public class SchematicException(string message, Exception? innerException = null)
    : Exception(message, innerException);
