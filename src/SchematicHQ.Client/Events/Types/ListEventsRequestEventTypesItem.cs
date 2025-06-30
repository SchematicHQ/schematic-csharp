using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEventsRequestEventTypesItem>))]
[Serializable]
public readonly record struct ListEventsRequestEventTypesItem : IStringEnum
{
    public static readonly ListEventsRequestEventTypesItem Identify = new(Values.Identify);

    public static readonly ListEventsRequestEventTypesItem Track = new(Values.Track);

    public static readonly ListEventsRequestEventTypesItem FlagCheck = new(Values.FlagCheck);

    public ListEventsRequestEventTypesItem(string value)
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
    public static ListEventsRequestEventTypesItem FromCustom(string value)
    {
        return new ListEventsRequestEventTypesItem(value);
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

    public static bool operator ==(ListEventsRequestEventTypesItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListEventsRequestEventTypesItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListEventsRequestEventTypesItem value) => value.Value;

    public static explicit operator ListEventsRequestEventTypesItem(string value) => new(value);

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
