using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListCreditBundlesResponseParamsStatus>))]
[Serializable]
public readonly record struct ListCreditBundlesResponseParamsStatus : IStringEnum
{
    public static readonly ListCreditBundlesResponseParamsStatus Active = new(Values.Active);

    public static readonly ListCreditBundlesResponseParamsStatus Inactive = new(Values.Inactive);

    public ListCreditBundlesResponseParamsStatus(string value)
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
    public static ListCreditBundlesResponseParamsStatus FromCustom(string value)
    {
        return new ListCreditBundlesResponseParamsStatus(value);
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

    public static bool operator ==(ListCreditBundlesResponseParamsStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListCreditBundlesResponseParamsStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListCreditBundlesResponseParamsStatus value) =>
        value.Value;

    public static explicit operator ListCreditBundlesResponseParamsStatus(string value) =>
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
