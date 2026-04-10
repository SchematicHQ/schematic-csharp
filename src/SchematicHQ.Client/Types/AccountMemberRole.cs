using global::System.Text.Json;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(AccountMemberRole.AccountMemberRoleSerializer))]
[Serializable]
public readonly record struct AccountMemberRole : IStringEnum
{
    public static readonly AccountMemberRole Admin = new(Values.Admin);

    public static readonly AccountMemberRole Member = new(Values.Member);

    public AccountMemberRole(string value)
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
    public static AccountMemberRole FromCustom(string value)
    {
        return new AccountMemberRole(value);
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

    public static bool operator ==(AccountMemberRole value1, string value2) =>
        value1.Value.Equals(value2);

    public static bool operator !=(AccountMemberRole value1, string value2) =>
        !value1.Value.Equals(value2);

    public static explicit operator string(AccountMemberRole value) => value.Value;

    public static explicit operator AccountMemberRole(string value) => new(value);

    internal class AccountMemberRoleSerializer : JsonConverter<AccountMemberRole>
    {
        public override AccountMemberRole Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON value could not be read as a string."
                );
            return new AccountMemberRole(stringValue);
        }

        public override void Write(
            Utf8JsonWriter writer,
            AccountMemberRole value,
            JsonSerializerOptions options
        )
        {
            writer.WriteStringValue(value.Value);
        }

        public override AccountMemberRole ReadAsPropertyName(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new global::System.Exception(
                    "The JSON property name could not be read as a string."
                );
            return new AccountMemberRole(stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            AccountMemberRole value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Value);
        }
    }

    /// <summary>
    /// Constant strings for enum values
    /// </summary>
    [Serializable]
    public static class Values
    {
        public const string Admin = "admin";

        public const string Member = "member";
    }
}
