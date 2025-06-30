using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateComponentRequestBodyState>))]
[Serializable]
public readonly record struct UpdateComponentRequestBodyState : IStringEnum
{
    public static readonly UpdateComponentRequestBodyState Draft = new(Values.Draft);

    public static readonly UpdateComponentRequestBodyState Live = new(Values.Live);

    public UpdateComponentRequestBodyState(string value)
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
    public static UpdateComponentRequestBodyState FromCustom(string value)
    {
        return new UpdateComponentRequestBodyState(value);
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

    public static bool operator ==(UpdateComponentRequestBodyState value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(UpdateComponentRequestBodyState value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(UpdateComponentRequestBodyState value) => value.Value;

    public static explicit operator UpdateComponentRequestBodyState(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Draft = "draft";

        public const string Live = "live";
    }
}
