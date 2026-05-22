namespace SchematicHQ.Client.Test.Datastream.Mocks
{
    /// <summary>
    /// Mock implementation of ISchematicLogger for testing.
    /// Records every call regardless of level (no filtering) so tests can
    /// assert that a specific message was attempted at a specific severity.
    /// </summary>
    public class MockSchematicLogger : ISchematicLogger
    {
        public List<LogEntry> LogEntries { get; } = new();

        public void Debug(string message, params object[] args)
        {
            LogEntries.Add(new LogEntry(LogLevel.Debug, message, args));
        }

        public void Error(string message, params object[] args)
        {
            LogEntries.Add(new LogEntry(LogLevel.Error, message, args));
        }

        public void Info(string message, params object[] args)
        {
            LogEntries.Add(new LogEntry(LogLevel.Info, message, args));
        }

        public void Warn(string message, params object[] args)
        {
            LogEntries.Add(new LogEntry(LogLevel.Warn, message, args));
        }

        public bool HasLogEntry(LogLevel level, string messageContains)
        {
            return LogEntries.Any(e => e.Level == level && e.Message.Contains(messageContains));
        }
    }

    public class LogEntry
    {
        public LogEntry(LogLevel level, string message, object[] args)
        {
            Level = level;
            Message = message;
            Args = args;
        }

        public LogLevel Level { get; }
        public string Message { get; }
        public object[] Args { get; }

        public string FormattedMessage => string.Format(Message, Args);
    }
}
