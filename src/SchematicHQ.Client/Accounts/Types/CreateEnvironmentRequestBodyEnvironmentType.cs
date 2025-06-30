using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateEnvironmentRequestBodyEnvironmentType>))]
[Serializable]
public readonly record struct CreateEnvironmentRequestBodyEnvironmentType : IStringEnum
{
    public static readonly CreateEnvironmentRequestBodyEnvironmentType Development = new(
        Values.Development
    );

    public static readonly CreateEnvironmentRequestBodyEnvironmentType Staging = new(
        Values.Staging
    );

    public static readonly CreateEnvironmentRequestBodyEnvironmentType Production = new(
        Values.Production
    );

    public CreateEnvironmentRequestBodyEnvironmentType(string value)
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
    public static CreateEnvironmentRequestBodyEnvironmentType FromCustom(string value)
    {
        return new CreateEnvironmentRequestBodyEnvironmentType(value);
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

    public static bool operator ==(
        CreateEnvironmentRequestBodyEnvironmentType value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        CreateEnvironmentRequestBodyEnvironmentType value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(CreateEnvironmentRequestBodyEnvironmentType value) =>
        value.Value;

    public static explicit operator CreateEnvironmentRequestBodyEnvironmentType(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Development = "development";

        public const string Staging = "staging";

        public const string Production = "production";
    }
}
