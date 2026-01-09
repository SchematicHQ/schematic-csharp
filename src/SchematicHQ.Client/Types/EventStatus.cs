using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<EventStatus>))]
[Serializable]
public readonly record struct EventStatus : IStringEnum
{
    public static readonly EventStatus Enriched = new(Values.Enriched);

    public static readonly EventStatus Failed = new(Values.Failed);

    public static readonly EventStatus Pending = new(Values.Pending);

    public static readonly EventStatus Processed = new(Values.Processed);

    public static readonly EventStatus Unknown = new(Values.Unknown);

    public EventStatus(string value)
    {
        Value = value;
    }

    /// <summary>
    /// The string value of the enum.
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Create a string enum with the given value.
    /// </summary>
    public static EventStatus FromCustom(string value)
    {
        return new EventStatus(value);
    }

    public bool Equals(string? other)
    {
        return Value.Equals(other);
    }

    /// <summary>
    /// Returns the string value of the enum.
    /// </summary>
    public override string ToString()
    {
        return Value;
    }

    public static bool operator ==(EventStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(EventStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(EventStatus value) => value.Value;

    public static explicit operator EventStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Enriched = "enriched";

        public const string Failed = "failed";

        public const string Pending = "pending";

        public const string Processed = "processed";

        public const string Unknown = "unknown";
    }
}
