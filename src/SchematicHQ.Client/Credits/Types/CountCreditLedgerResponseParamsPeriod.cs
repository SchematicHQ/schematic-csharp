using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<CountCreditLedgerResponseParamsPeriod>))]
[Serializable]
public readonly record struct CountCreditLedgerResponseParamsPeriod : IStringEnum
{
    public static readonly CountCreditLedgerResponseParamsPeriod Daily = new(Values.Daily);

    public static readonly CountCreditLedgerResponseParamsPeriod Weekly = new(Values.Weekly);

    public static readonly CountCreditLedgerResponseParamsPeriod Monthly = new(Values.Monthly);

    public static readonly CountCreditLedgerResponseParamsPeriod Raw = new(Values.Raw);

    public CountCreditLedgerResponseParamsPeriod(string value)
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
    public static CountCreditLedgerResponseParamsPeriod FromCustom(string value)
    {
        return new CountCreditLedgerResponseParamsPeriod(value);
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

    public static bool operator ==(CountCreditLedgerResponseParamsPeriod value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(CountCreditLedgerResponseParamsPeriod value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(CountCreditLedgerResponseParamsPeriod value) =>
        value.Value;

    public static explicit operator CountCreditLedgerResponseParamsPeriod(string value) =>
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
