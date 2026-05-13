using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Testing;

namespace SchematicHQ.Client.Test.Datastream.Mocks
{
    public static class FakeLoggerExtensions
    {
        public static bool HasLogEntry(this FakeLogger logger, LogLevel level, string messageContains)
        {
            return logger.Collector.GetSnapshot().Any(r => r.Level == level && r.Message.Contains(messageContains));
        }

        public static ILoggerFactory ToLoggerFactory(this FakeLogger logger)
        {
            return LoggerFactory.Create(b => b
                .SetMinimumLevel(LogLevel.Trace)
                .AddProvider(new FakeLoggerProvider(logger.Collector)));
        }
    }
}
