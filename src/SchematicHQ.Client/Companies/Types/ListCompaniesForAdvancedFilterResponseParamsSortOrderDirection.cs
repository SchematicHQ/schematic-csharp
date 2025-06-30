using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection>)
)]
[Serializable]
public readonly record struct ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection
    : IStringEnum
{
    public static readonly ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection Asc = new(
        Values.Asc
    );

    public static readonly ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection Desc =
        new(Values.Desc);

    public ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection(string value)
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
    public static ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection FromCustom(
        string value
    )
    {
        return new ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection(value);
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
        ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection value
    ) => value.Value;

    public static explicit operator ListCompaniesForAdvancedFilterResponseParamsSortOrderDirection(
        string value
    ) => new(value);

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
