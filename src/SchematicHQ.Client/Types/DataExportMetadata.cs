// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using global::System.Text.Json;
using global::System.Text.Json.Nodes;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(DataExportMetadata.JsonConverter))]
[Serializable]
public record DataExportMetadata
{
    internal DataExportMetadata(string type, object? value)
    {
        ExportType = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of DataExportMetadata with <see cref="DataExportMetadata.AuditLog"/>.
    /// </summary>
    public DataExportMetadata(DataExportMetadata.AuditLog value)
    {
        ExportType = "audit-log";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of DataExportMetadata with <see cref="DataExportMetadata.CompanyFeatureUsage"/>.
    /// </summary>
    public DataExportMetadata(DataExportMetadata.CompanyFeatureUsage value)
    {
        ExportType = "company-feature-usage";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of DataExportMetadata with <see cref="DataExportMetadata.CreditBalance"/>.
    /// </summary>
    public DataExportMetadata(DataExportMetadata.CreditBalance value)
    {
        ExportType = "credit-balance";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of DataExportMetadata with <see cref="DataExportMetadata.CreditLedger"/>.
    /// </summary>
    public DataExportMetadata(DataExportMetadata.CreditLedger value)
    {
        ExportType = "credit-ledger";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of DataExportMetadata with <see cref="DataExportMetadata.Event"/>.
    /// </summary>
    public DataExportMetadata(DataExportMetadata.Event value)
    {
        ExportType = "event";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of DataExportMetadata with <see cref="DataExportMetadata.User"/>.
    /// </summary>
    public DataExportMetadata(DataExportMetadata.User value)
    {
        ExportType = "user";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("export_type")]
    public string ExportType { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="ExportType"/> is "audit-log"
    /// </summary>
    public bool IsAuditLog => ExportType == "audit-log";

    /// <summary>
    /// Returns true if <see cref="ExportType"/> is "company-feature-usage"
    /// </summary>
    public bool IsCompanyFeatureUsage => ExportType == "company-feature-usage";

    /// <summary>
    /// Returns true if <see cref="ExportType"/> is "credit-balance"
    /// </summary>
    public bool IsCreditBalance => ExportType == "credit-balance";

    /// <summary>
    /// Returns true if <see cref="ExportType"/> is "credit-ledger"
    /// </summary>
    public bool IsCreditLedger => ExportType == "credit-ledger";

    /// <summary>
    /// Returns true if <see cref="ExportType"/> is "event"
    /// </summary>
    public bool IsEvent => ExportType == "event";

    /// <summary>
    /// Returns true if <see cref="ExportType"/> is "user"
    /// </summary>
    public bool IsUser => ExportType == "user";

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.AuditLogExportMetadata"/> if <see cref="ExportType"/> is 'audit-log', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="ExportType"/> is not 'audit-log'.</exception>
    public SchematicHQ.Client.AuditLogExportMetadata AsAuditLog() =>
        IsAuditLog
            ? (SchematicHQ.Client.AuditLogExportMetadata)Value!
            : throw new global::System.Exception(
                "DataExportMetadata.ExportType is not 'audit-log'"
            );

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.CompanyFeatureUsageExportMetadata"/> if <see cref="ExportType"/> is 'company-feature-usage', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="ExportType"/> is not 'company-feature-usage'.</exception>
    public SchematicHQ.Client.CompanyFeatureUsageExportMetadata AsCompanyFeatureUsage() =>
        IsCompanyFeatureUsage
            ? (SchematicHQ.Client.CompanyFeatureUsageExportMetadata)Value!
            : throw new global::System.Exception(
                "DataExportMetadata.ExportType is not 'company-feature-usage'"
            );

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.CreditBalanceExportMetadata"/> if <see cref="ExportType"/> is 'credit-balance', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="ExportType"/> is not 'credit-balance'.</exception>
    public SchematicHQ.Client.CreditBalanceExportMetadata AsCreditBalance() =>
        IsCreditBalance
            ? (SchematicHQ.Client.CreditBalanceExportMetadata)Value!
            : throw new global::System.Exception(
                "DataExportMetadata.ExportType is not 'credit-balance'"
            );

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.CreditLedgerExportMetadata"/> if <see cref="ExportType"/> is 'credit-ledger', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="ExportType"/> is not 'credit-ledger'.</exception>
    public SchematicHQ.Client.CreditLedgerExportMetadata AsCreditLedger() =>
        IsCreditLedger
            ? (SchematicHQ.Client.CreditLedgerExportMetadata)Value!
            : throw new global::System.Exception(
                "DataExportMetadata.ExportType is not 'credit-ledger'"
            );

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.EventExportMetadata"/> if <see cref="ExportType"/> is 'event', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="ExportType"/> is not 'event'.</exception>
    public SchematicHQ.Client.EventExportMetadata AsEvent() =>
        IsEvent
            ? (SchematicHQ.Client.EventExportMetadata)Value!
            : throw new global::System.Exception("DataExportMetadata.ExportType is not 'event'");

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.UserExportMetadata"/> if <see cref="ExportType"/> is 'user', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="ExportType"/> is not 'user'.</exception>
    public SchematicHQ.Client.UserExportMetadata AsUser() =>
        IsUser
            ? (SchematicHQ.Client.UserExportMetadata)Value!
            : throw new global::System.Exception("DataExportMetadata.ExportType is not 'user'");

    public T Match<T>(
        Func<SchematicHQ.Client.AuditLogExportMetadata, T> onAuditLog,
        Func<SchematicHQ.Client.CompanyFeatureUsageExportMetadata, T> onCompanyFeatureUsage,
        Func<SchematicHQ.Client.CreditBalanceExportMetadata, T> onCreditBalance,
        Func<SchematicHQ.Client.CreditLedgerExportMetadata, T> onCreditLedger,
        Func<SchematicHQ.Client.EventExportMetadata, T> onEvent,
        Func<SchematicHQ.Client.UserExportMetadata, T> onUser,
        Func<string, object?, T> onUnknown_
    )
    {
        return ExportType switch
        {
            "audit-log" => onAuditLog(AsAuditLog()),
            "company-feature-usage" => onCompanyFeatureUsage(AsCompanyFeatureUsage()),
            "credit-balance" => onCreditBalance(AsCreditBalance()),
            "credit-ledger" => onCreditLedger(AsCreditLedger()),
            "event" => onEvent(AsEvent()),
            "user" => onUser(AsUser()),
            _ => onUnknown_(ExportType, Value),
        };
    }

    public void Visit(
        Action<SchematicHQ.Client.AuditLogExportMetadata> onAuditLog,
        Action<SchematicHQ.Client.CompanyFeatureUsageExportMetadata> onCompanyFeatureUsage,
        Action<SchematicHQ.Client.CreditBalanceExportMetadata> onCreditBalance,
        Action<SchematicHQ.Client.CreditLedgerExportMetadata> onCreditLedger,
        Action<SchematicHQ.Client.EventExportMetadata> onEvent,
        Action<SchematicHQ.Client.UserExportMetadata> onUser,
        Action<string, object?> onUnknown_
    )
    {
        switch (ExportType)
        {
            case "audit-log":
                onAuditLog(AsAuditLog());
                break;
            case "company-feature-usage":
                onCompanyFeatureUsage(AsCompanyFeatureUsage());
                break;
            case "credit-balance":
                onCreditBalance(AsCreditBalance());
                break;
            case "credit-ledger":
                onCreditLedger(AsCreditLedger());
                break;
            case "event":
                onEvent(AsEvent());
                break;
            case "user":
                onUser(AsUser());
                break;
            default:
                onUnknown_(ExportType, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.AuditLogExportMetadata"/> and returns true if successful.
    /// </summary>
    public bool TryAsAuditLog(out SchematicHQ.Client.AuditLogExportMetadata? value)
    {
        if (ExportType == "audit-log")
        {
            value = (SchematicHQ.Client.AuditLogExportMetadata)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.CompanyFeatureUsageExportMetadata"/> and returns true if successful.
    /// </summary>
    public bool TryAsCompanyFeatureUsage(
        out SchematicHQ.Client.CompanyFeatureUsageExportMetadata? value
    )
    {
        if (ExportType == "company-feature-usage")
        {
            value = (SchematicHQ.Client.CompanyFeatureUsageExportMetadata)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.CreditBalanceExportMetadata"/> and returns true if successful.
    /// </summary>
    public bool TryAsCreditBalance(out SchematicHQ.Client.CreditBalanceExportMetadata? value)
    {
        if (ExportType == "credit-balance")
        {
            value = (SchematicHQ.Client.CreditBalanceExportMetadata)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.CreditLedgerExportMetadata"/> and returns true if successful.
    /// </summary>
    public bool TryAsCreditLedger(out SchematicHQ.Client.CreditLedgerExportMetadata? value)
    {
        if (ExportType == "credit-ledger")
        {
            value = (SchematicHQ.Client.CreditLedgerExportMetadata)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.EventExportMetadata"/> and returns true if successful.
    /// </summary>
    public bool TryAsEvent(out SchematicHQ.Client.EventExportMetadata? value)
    {
        if (ExportType == "event")
        {
            value = (SchematicHQ.Client.EventExportMetadata)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.UserExportMetadata"/> and returns true if successful.
    /// </summary>
    public bool TryAsUser(out SchematicHQ.Client.UserExportMetadata? value)
    {
        if (ExportType == "user")
        {
            value = (SchematicHQ.Client.UserExportMetadata)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator DataExportMetadata(DataExportMetadata.AuditLog value) =>
        new(value);

    public static implicit operator DataExportMetadata(
        DataExportMetadata.CompanyFeatureUsage value
    ) => new(value);

    public static implicit operator DataExportMetadata(DataExportMetadata.CreditBalance value) =>
        new(value);

    public static implicit operator DataExportMetadata(DataExportMetadata.CreditLedger value) =>
        new(value);

    public static implicit operator DataExportMetadata(DataExportMetadata.Event value) =>
        new(value);

    public static implicit operator DataExportMetadata(DataExportMetadata.User value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<DataExportMetadata>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(DataExportMetadata).IsAssignableFrom(typeToConvert);

        public override DataExportMetadata Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("export_type", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'export_type'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'export_type' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'export_type' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'export_type' is null");

            // Strip the discriminant property to prevent it from leaking into AdditionalProperties
            var jsonObject = System.Text.Json.Nodes.JsonObject.Create(json);
            jsonObject?.Remove("export_type");
            var jsonWithoutDiscriminator =
                jsonObject != null ? JsonSerializer.SerializeToElement(jsonObject, options) : json;

            var value = discriminator switch
            {
                "audit-log" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.AuditLogExportMetadata?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.AuditLogExportMetadata"
                        ),
                "company-feature-usage" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.CompanyFeatureUsageExportMetadata?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.CompanyFeatureUsageExportMetadata"
                        ),
                "credit-balance" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.CreditBalanceExportMetadata?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.CreditBalanceExportMetadata"
                        ),
                "credit-ledger" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.CreditLedgerExportMetadata?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.CreditLedgerExportMetadata"
                        ),
                "event" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.EventExportMetadata?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.EventExportMetadata"
                        ),
                "user" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.UserExportMetadata?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.UserExportMetadata"
                        ),
                _ => json.Deserialize<object?>(options),
            };
            return new DataExportMetadata(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            DataExportMetadata value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.ExportType switch
                {
                    "audit-log" => JsonSerializer.SerializeToNode(value.Value, options),
                    "company-feature-usage" => JsonSerializer.SerializeToNode(value.Value, options),
                    "credit-balance" => JsonSerializer.SerializeToNode(value.Value, options),
                    "credit-ledger" => JsonSerializer.SerializeToNode(value.Value, options),
                    "event" => JsonSerializer.SerializeToNode(value.Value, options),
                    "user" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["export_type"] = value.ExportType;
            json.WriteTo(writer, options);
        }

        public override DataExportMetadata ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new JsonException("The JSON property name could not be read as a string.");
            return new DataExportMetadata(stringValue, stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            DataExportMetadata value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.ExportType);
        }
    }

    /// <summary>
    /// Discriminated union type for audit-log
    /// </summary>
    [Serializable]
    public struct AuditLog
    {
        public AuditLog(SchematicHQ.Client.AuditLogExportMetadata value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.AuditLogExportMetadata Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator DataExportMetadata.AuditLog(
            SchematicHQ.Client.AuditLogExportMetadata value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for company-feature-usage
    /// </summary>
    [Serializable]
    public struct CompanyFeatureUsage
    {
        public CompanyFeatureUsage(SchematicHQ.Client.CompanyFeatureUsageExportMetadata value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.CompanyFeatureUsageExportMetadata Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator DataExportMetadata.CompanyFeatureUsage(
            SchematicHQ.Client.CompanyFeatureUsageExportMetadata value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for credit-balance
    /// </summary>
    [Serializable]
    public struct CreditBalance
    {
        public CreditBalance(SchematicHQ.Client.CreditBalanceExportMetadata value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.CreditBalanceExportMetadata Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator DataExportMetadata.CreditBalance(
            SchematicHQ.Client.CreditBalanceExportMetadata value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for credit-ledger
    /// </summary>
    [Serializable]
    public struct CreditLedger
    {
        public CreditLedger(SchematicHQ.Client.CreditLedgerExportMetadata value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.CreditLedgerExportMetadata Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator DataExportMetadata.CreditLedger(
            SchematicHQ.Client.CreditLedgerExportMetadata value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for event
    /// </summary>
    [Serializable]
    public struct Event
    {
        public Event(SchematicHQ.Client.EventExportMetadata value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.EventExportMetadata Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator DataExportMetadata.Event(
            SchematicHQ.Client.EventExportMetadata value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for user
    /// </summary>
    [Serializable]
    public struct User
    {
        public User(SchematicHQ.Client.UserExportMetadata value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.UserExportMetadata Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator DataExportMetadata.User(
            SchematicHQ.Client.UserExportMetadata value
        ) => new(value);
    }
}
