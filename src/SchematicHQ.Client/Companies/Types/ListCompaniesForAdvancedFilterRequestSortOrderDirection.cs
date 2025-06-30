using System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(
    typeof(StringEnumSerializer<ListCompaniesForAdvancedFilterRequestSortOrderDirection>)
)]
[Serializable]
public readonly record struct ListCompaniesForAdvancedFilterRequestSortOrderDirection : IStringEnum
{
    public static readonly ListCompaniesForAdvancedFilterRequestSortOrderDirection Asc = new(
        Values.Asc
    );

    public static readonly ListCompaniesForAdvancedFilterRequestSortOrderDirection Desc = new(
        Values.Desc
    );

    public ListCompaniesForAdvancedFilterRequestSortOrderDirection(string value)
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
    public static ListCompaniesForAdvancedFilterRequestSortOrderDirection FromCustom(string value)
    {
        return new ListCompaniesForAdvancedFilterRequestSortOrderDirection(value);
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
        ListCompaniesForAdvancedFilterRequestSortOrderDirection value1,
        string value2
    ) => value1.Value.Equals(value2);

    public static bool operator !=(
        ListCompaniesForAdvancedFilterRequestSortOrderDirection value1,
        string value2
    ) => !value1.Value.Equals(value2);

    public static explicit operator string(
        ListCompaniesForAdvancedFilterRequestSortOrderDirection value
    ) => value.Value;

    public static explicit operator ListCompaniesForAdvancedFilterRequestSortOrderDirection(
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
