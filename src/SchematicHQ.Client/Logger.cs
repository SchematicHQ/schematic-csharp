#nullable enable

namespace SchematicHQ.Client;

public interface ISchematicLogger
{
    void Error(string message, params object[] args);
    void Warn(string message, params object[] args);
    void Info(string message, params object[] args);
    void Debug(string message, params object[] args);
}

public class ConsoleLogger : ISchematicLogger
{
    public void Error(string message, params object[] args)
    {
        Console.WriteLine($"[ERROR] {string.Format(message, args)}");
    }

    public void Warn(string message, params object[] args)
    {
        Console.WriteLine($"[WARN] {string.Format(message, args)}");
    }

    public void Info(string message, params object[] args)
    {
        Console.WriteLine($"[INFO] {string.Format(message, args)}");
    }

    public void Debug(string message, params object[] args)
    {
        Console.WriteLine($"[DEBUG] {string.Format(message, args)}");
    }
}
