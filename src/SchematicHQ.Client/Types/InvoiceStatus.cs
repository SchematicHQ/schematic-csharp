using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<InvoiceStatus>))]
[Serializable]
public readonly record struct InvoiceStatus : IStringEnum
{
    public static readonly InvoiceStatus Draft = new(Values.Draft);

    public static readonly InvoiceStatus Open = new(Values.Open);

    public static readonly InvoiceStatus Paid = new(Values.Paid);

    public static readonly InvoiceStatus Uncollectible = new(Values.Uncollectible);

    public static readonly InvoiceStatus Void = new(Values.Void);

    public InvoiceStatus(string value)
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
    public static InvoiceStatus FromCustom(string value)
    {
        return new InvoiceStatus(value);
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

    public static bool operator ==(InvoiceStatus value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(InvoiceStatus value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(InvoiceStatus value) => value.Value;

    public static explicit operator InvoiceStatus(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Draft = "draft";

        public const string Open = "open";

        public const string Paid = "paid";

        public const string Uncollectible = "uncollectible";

        public const string Void = "void";
    }
}
