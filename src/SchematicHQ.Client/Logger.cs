#nullable enable

namespace SchematicHQ.Client;

/// <summary>
/// Severity levels for <see cref="ISchematicLogger"/>. Lower numeric values
/// are more verbose; e.g. a logger configured at <see cref="Warn"/> drops
/// <see cref="Debug"/> and <see cref="Info"/> calls.
/// </summary>
public enum LogLevel
{
    Debug = 0,
    Info = 1,
    Warn = 2,
    Error = 3,
}

public interface ISchematicLogger
{
    void Error(string message, params object[] args);
    void Warn(string message, params object[] args);
    void Info(string message, params object[] args);
    void Debug(string message, params object[] args);
}

/// <summary>
/// Default console-based logger. Filters messages by the configured
/// <see cref="LogLevel"/>; defaults to <see cref="LogLevel.Warn"/> so debug
/// and info diagnostics are suppressed unless the consumer explicitly opts
/// in via <c>ClientOptions.LogLevel</c>.
/// </summary>
public class ConsoleLogger : ISchematicLogger
{
    private readonly LogLevel _level;

    public ConsoleLogger() : this(LogLevel.Warn) { }

    public ConsoleLogger(LogLevel level)
    {
        _level = level;
    }

    public void Error(string message, params object[] args)
    {
        if (_level > LogLevel.Error) return;
        Console.WriteLine($"[ERROR] {string.Format(message, args)}");
    }

    public void Warn(string message, params object[] args)
    {
        if (_level > LogLevel.Warn) return;
        Console.WriteLine($"[WARN] {string.Format(message, args)}");
    }

    public void Info(string message, params object[] args)
    {
        if (_level > LogLevel.Info) return;
        Console.WriteLine($"[INFO] {string.Format(message, args)}");
    }

    public void Debug(string message, params object[] args)
    {
        if (_level > LogLevel.Debug) return;
        Console.WriteLine($"[DEBUG] {string.Format(message, args)}");
    }
}
