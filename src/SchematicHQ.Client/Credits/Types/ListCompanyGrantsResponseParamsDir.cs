using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(StringEnumSerializer<ListCompanyGrantsResponseParamsDir>))]
[Serializable]
public readonly record struct ListCompanyGrantsResponseParamsDir : IStringEnum
{
    public static readonly ListCompanyGrantsResponseParamsDir Asc = new(Values.Asc);

    public static readonly ListCompanyGrantsResponseParamsDir Desc = new(Values.Desc);

    public ListCompanyGrantsResponseParamsDir(string value)
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
    public static ListCompanyGrantsResponseParamsDir FromCustom(string value)
    {
        return new ListCompanyGrantsResponseParamsDir(value);
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

    public static bool operator ==(ListCompanyGrantsResponseParamsDir value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(ListCompanyGrantsResponseParamsDir value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(ListCompanyGrantsResponseParamsDir value) => value.Value;

    public static explicit operator ListCompanyGrantsResponseParamsDir(string value) => new(value);

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Asc = "asc";

        public const string Desc = "desc";
    }
}
