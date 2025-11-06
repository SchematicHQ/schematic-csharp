using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<GetEnrichedCreditLedgerResponseParamsPeriod>))]
[Serializable]
public readonly record struct GetEnrichedCreditLedgerResponseParamsPeriod : IStringEnum
{
    public static readonly GetEnrichedCreditLedgerResponseParamsPeriod Daily = new(Values.Daily);

    public static readonly GetEnrichedCreditLedgerResponseParamsPeriod Weekly = new(Values.Weekly);

    public static readonly GetEnrichedCreditLedgerResponseParamsPeriod Monthly = new(
        Values.Monthly
    );

    public static readonly GetEnrichedCreditLedgerResponseParamsPeriod Raw = new(Values.Raw);

    public GetEnrichedCreditLedgerResponseParamsPeriod(string value)
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
    public static GetEnrichedCreditLedgerResponseParamsPeriod FromCustom(string value)
    {
        return new GetEnrichedCreditLedgerResponseParamsPeriod(value);
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
        GetEnrichedCreditLedgerResponseParamsPeriod value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        GetEnrichedCreditLedgerResponseParamsPeriod value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(GetEnrichedCreditLedgerResponseParamsPeriod value) =>
        value.Value;

    public static explicit operator GetEnrichedCreditLedgerResponseParamsPeriod(string value) =>
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
