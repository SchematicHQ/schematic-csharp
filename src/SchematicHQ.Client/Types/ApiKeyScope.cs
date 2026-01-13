using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ApiKeyScope>))]
[Serializable]
public readonly record struct ApiKeyScope : IStringEnum
{
    public static readonly ApiKeyScope Admin = new(Values.Admin);

    public static readonly ApiKeyScope Capture = new(Values.Capture);

    public static readonly ApiKeyScope Read = new(Values.Read);

    public static readonly ApiKeyScope Write = new(Values.Write);

    public ApiKeyScope(string value)
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
    public static ApiKeyScope FromCustom(string value)
    {
        return new ApiKeyScope(value);
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

    public static bool operator ==(ApiKeyScope value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ApiKeyScope value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ApiKeyScope value) => value.Value;

    public static explicit operator ApiKeyScope(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Admin = "admin";

        public const string Capture = "capture";

        public const string Read = "read";

        public const string Write = "write";
    }
}
