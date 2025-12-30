using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ProrationBehavior>))]
[Serializable]
public readonly record struct ProrationBehavior : IStringEnum
{
    public static readonly ProrationBehavior CreateProrations = new(Values.CreateProrations);

    public static readonly ProrationBehavior InvoiceImmediately = new(Values.InvoiceImmediately);

    public ProrationBehavior(string value)
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
    public static ProrationBehavior FromCustom(string value)
    {
        return new ProrationBehavior(value);
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

    public static bool operator ==(ProrationBehavior value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ProrationBehavior value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ProrationBehavior value) => value.Value;

    public static explicit operator ProrationBehavior(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string CreateProrations = "create_prorations";

        public const string InvoiceImmediately = "invoice_immediately";
    }
}
