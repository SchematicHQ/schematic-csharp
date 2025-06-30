using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListPlansResponseParamsPlanType>))]
[Serializable]
public readonly record struct ListPlansResponseParamsPlanType : IStringEnum
{
    public static readonly ListPlansResponseParamsPlanType Plan = new(Values.Plan);

    public static readonly ListPlansResponseParamsPlanType AddOn = new(Values.AddOn);

    public ListPlansResponseParamsPlanType(string value)
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
    public static ListPlansResponseParamsPlanType FromCustom(string value)
    {
        return new ListPlansResponseParamsPlanType(value);
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

    public static bool operator ==(ListPlansResponseParamsPlanType value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListPlansResponseParamsPlanType value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListPlansResponseParamsPlanType value) => value.Value;

    public static explicit operator ListPlansResponseParamsPlanType(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Plan = "plan";

        public const string AddOn = "add_on";
    }
}
