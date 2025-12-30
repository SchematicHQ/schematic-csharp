using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<EventType>))]
[Serializable]
public readonly record struct EventType : IStringEnum
{
    public static readonly EventType FlagCheck = new(Values.FlagCheck);

    public static readonly EventType Identify = new(Values.Identify);

    public static readonly EventType Track = new(Values.Track);

    public EventType(string value)
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
    public static EventType FromCustom(string value)
    {
        return new EventType(value);
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

    public static bool operator ==(EventType value1, string value2) => value1.Value.Equals(value2);

    public static bool operator !=(EventType value1, string value2) => !value1.Value.Equals(value2);

    public static explicit operator string(EventType value) => value.Value;

    public static explicit operator EventType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string FlagCheck = "flag_check";

        public const string Identify = "identify";

        public const string Track = "track";
    }
}
