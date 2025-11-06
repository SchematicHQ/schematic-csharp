using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<GetEnrichedCreditLedgerRequestPeriod>))]
[Serializable]
public readonly record struct GetEnrichedCreditLedgerRequestPeriod : IStringEnum
{
    public static readonly GetEnrichedCreditLedgerRequestPeriod Daily = new(Values.Daily);

    public static readonly GetEnrichedCreditLedgerRequestPeriod Weekly = new(Values.Weekly);

    public static readonly GetEnrichedCreditLedgerRequestPeriod Monthly = new(Values.Monthly);

    public static readonly GetEnrichedCreditLedgerRequestPeriod Raw = new(Values.Raw);

    public GetEnrichedCreditLedgerRequestPeriod(string value)
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
    public static GetEnrichedCreditLedgerRequestPeriod FromCustom(string value)
    {
        return new GetEnrichedCreditLedgerRequestPeriod(value);
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

    public static bool operator ==(GetEnrichedCreditLedgerRequestPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(GetEnrichedCreditLedgerRequestPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(GetEnrichedCreditLedgerRequestPeriod value) =>
        value.Value;

    public static explicit operator GetEnrichedCreditLedgerRequestPeriod(string value) =>
        new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Daily = "daily";

        public const string Weekly = "weekly";

        public const string Monthly = "monthly";

        public const string Raw = "raw";
    }
}
