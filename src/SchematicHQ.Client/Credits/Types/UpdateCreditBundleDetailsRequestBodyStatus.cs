using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<UpdateCreditBundleDetailsRequestBodyStatus>))]
[Serializable]
public readonly record struct UpdateCreditBundleDetailsRequestBodyStatus : IStringEnum
{
    public static readonly UpdateCreditBundleDetailsRequestBodyStatus Active = new(Values.Active);

    public static readonly UpdateCreditBundleDetailsRequestBodyStatus Inactive = new(
        Values.Inactive
    );

    public UpdateCreditBundleDetailsRequestBodyStatus(string value)
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
    public static UpdateCreditBundleDetailsRequestBodyStatus FromCustom(string value)
    {
        return new UpdateCreditBundleDetailsRequestBodyStatus(value);
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
        UpdateCreditBundleDetailsRequestBodyStatus value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        UpdateCreditBundleDetailsRequestBodyStatus value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(UpdateCreditBundleDetailsRequestBodyStatus value) =>
        value.Value;

    public static explicit operator UpdateCreditBundleDetailsRequestBodyStatus(string value) =>
        new(value);

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
