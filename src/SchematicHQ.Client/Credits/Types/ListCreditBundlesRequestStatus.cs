using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListCreditBundlesRequestStatus>))]
[Serializable]
public readonly record struct ListCreditBundlesRequestStatus : IStringEnum
{
    public static readonly ListCreditBundlesRequestStatus Active = new(Values.Active);

    public static readonly ListCreditBundlesRequestStatus Inactive = new(Values.Inactive);

    public ListCreditBundlesRequestStatus(string value)
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
    public static ListCreditBundlesRequestStatus FromCustom(string value)
    {
        return new ListCreditBundlesRequestStatus(value);
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

    public static bool operator ==(ListCreditBundlesRequestStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListCreditBundlesRequestStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListCreditBundlesRequestStatus value) => value.Value;

    public static explicit operator ListCreditBundlesRequestStatus(string value) => new(value);

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
