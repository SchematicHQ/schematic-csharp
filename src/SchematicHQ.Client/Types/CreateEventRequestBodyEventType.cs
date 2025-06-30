using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateEventRequestBodyEventType>))]
[Serializable]
public readonly record struct CreateEventRequestBodyEventType : IStringEnum
{
    public static readonly CreateEventRequestBodyEventType Identify = new(Values.Identify);

    public static readonly CreateEventRequestBodyEventType Track = new(Values.Track);

    public static readonly CreateEventRequestBodyEventType FlagCheck = new(Values.FlagCheck);

    public CreateEventRequestBodyEventType(string value)
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
    public static CreateEventRequestBodyEventType FromCustom(string value)
    {
        return new CreateEventRequestBodyEventType(value);
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

    public static bool operator ==(CreateEventRequestBodyEventType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateEventRequestBodyEventType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateEventRequestBodyEventType value) => value.Value;

    public static explicit operator CreateEventRequestBodyEventType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Identify = "identify";

        public const string Track = "track";

        public const string FlagCheck = "flag_check";
    }
}
