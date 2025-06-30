using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CreateCreditBundleRequestBodyStatus>))]
[Serializable]
public readonly record struct CreateCreditBundleRequestBodyStatus : IStringEnum
{
    public static readonly CreateCreditBundleRequestBodyStatus Active = new(Values.Active);

    public static readonly CreateCreditBundleRequestBodyStatus Inactive = new(Values.Inactive);

    public CreateCreditBundleRequestBodyStatus(string value)
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
    public static CreateCreditBundleRequestBodyStatus FromCustom(string value)
    {
        return new CreateCreditBundleRequestBodyStatus(value);
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

    public static bool operator ==(CreateCreditBundleRequestBodyStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CreateCreditBundleRequestBodyStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CreateCreditBundleRequestBodyStatus value) =>
        value.Value;

    public static explicit operator CreateCreditBundleRequestBodyStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Active = "active";

        public const string Inactive = "inactive";
    }
}
