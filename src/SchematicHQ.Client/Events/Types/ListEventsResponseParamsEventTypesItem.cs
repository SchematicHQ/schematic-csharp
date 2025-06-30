using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListEventsResponseParamsEventTypesItem>))]
[Serializable]
public readonly record struct ListEventsResponseParamsEventTypesItem : IStringEnum
{
    public static readonly ListEventsResponseParamsEventTypesItem Identify = new(Values.Identify);

    public static readonly ListEventsResponseParamsEventTypesItem Track = new(Values.Track);

    public static readonly ListEventsResponseParamsEventTypesItem FlagCheck = new(Values.FlagCheck);

    public ListEventsResponseParamsEventTypesItem(string value)
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
    public static ListEventsResponseParamsEventTypesItem FromCustom(string value)
    {
        return new ListEventsResponseParamsEventTypesItem(value);
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

    public static bool operator ==(ListEventsResponseParamsEventTypesItem value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListEventsResponseParamsEventTypesItem value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListEventsResponseParamsEventTypesItem value) =>
        value.Value;

    public static explicit operator ListEventsResponseParamsEventTypesItem(string value) =>
        new(value);

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
