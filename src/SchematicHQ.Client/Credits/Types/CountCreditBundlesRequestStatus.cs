using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountCreditBundlesRequestStatus>))]
[Serializable]
public readonly record struct CountCreditBundlesRequestStatus : IStringEnum
{
    public static readonly CountCreditBundlesRequestStatus Active = new(Values.Active);

    public static readonly CountCreditBundlesRequestStatus Inactive = new(Values.Inactive);

    public CountCreditBundlesRequestStatus(string value)
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
    public static CountCreditBundlesRequestStatus FromCustom(string value)
    {
        return new CountCreditBundlesRequestStatus(value);
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

    public static bool operator ==(CountCreditBundlesRequestStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CountCreditBundlesRequestStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CountCreditBundlesRequestStatus value) => value.Value;

    public static explicit operator CountCreditBundlesRequestStatus(string value) => new(value);

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
