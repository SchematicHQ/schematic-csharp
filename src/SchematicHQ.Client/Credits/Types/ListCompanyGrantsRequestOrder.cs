using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListCompanyGrantsRequestOrder>))]
[Serializable]
public readonly record struct ListCompanyGrantsRequestOrder : IStringEnum
{
    public static readonly ListCompanyGrantsRequestOrder CreatedAt = new(Values.CreatedAt);

    public static readonly ListCompanyGrantsRequestOrder ExpiresAt = new(Values.ExpiresAt);

    public static readonly ListCompanyGrantsRequestOrder Quantity = new(Values.Quantity);

    public static readonly ListCompanyGrantsRequestOrder QuantityRemaining = new(
        Values.QuantityRemaining
    );

    public ListCompanyGrantsRequestOrder(string value)
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
    public static ListCompanyGrantsRequestOrder FromCustom(string value)
    {
        return new ListCompanyGrantsRequestOrder(value);
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

    public static bool operator ==(ListCompanyGrantsRequestOrder value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListCompanyGrantsRequestOrder value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListCompanyGrantsRequestOrder value) => value.Value;

    public static explicit operator ListCompanyGrantsRequestOrder(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CreatedAt = "created_at";

        public const string ExpiresAt = "expires_at";

        public const string Quantity = "quantity";

        public const string QuantityRemaining = "quantity_remaining";
    }
}
