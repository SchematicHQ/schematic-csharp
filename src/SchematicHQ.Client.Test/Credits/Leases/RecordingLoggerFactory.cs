using Microsoft.Extensions.Logging;

namespace SchematicHQ.Client.Test.Credits.Leases;

/// <summary>
/// Collects the client's log lines so a test can assert on the startup warnings
/// that are the only outward sign of a misconfigured lease setup.
/// </summary>
public sealed class RecordingLoggerFactory : ILoggerFactory
{
    public List<(LogLevel Level, string Message)> Lines { get; } = new();

    public ILogger CreateLogger(string categoryName) => new RecordingLogger(Lines);

    public void AddProvider(ILoggerProvider provider) { }

    public void Dispose() { }

    public bool Logged(LogLevel level, string fragment) =>
        Lines.Any(line => line.Level == level && line.Message.Contains(fragment));

    private sealed class RecordingLogger : ILogger
    {
        private readonly List<(LogLevel, string)> _lines;

        public RecordingLogger(List<(LogLevel, string)> lines)
        {
            _lines = lines;
        }

        public IDisposable? BeginScope<TState>(TState state)
            where TState : notnull => null;

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(
            LogLevel logLevel,
            EventId eventId,
            TState state,
            Exception? exception,
            Func<TState, Exception?, string> formatter
        )
        {
            lock (_lines)
            {
                _lines.Add((logLevel, formatter(state, exception)));
            }
        }
    }
}
