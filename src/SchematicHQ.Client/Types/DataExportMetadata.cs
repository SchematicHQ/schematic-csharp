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

    public T Match<T>(
        Func<SchematicHQ.Client.AuditLogExportMetadata, T> onAuditLog,
        Func<SchematicHQ.Client.CompanyFeatureUsageExportMetadata, T> onCompanyFeatureUsage,
        Func<string, object?, T> onUnknown_
    )
    {
        return ExportType switch
        {
            "audit-log" => onAuditLog(AsAuditLog()),
            "company-feature-usage" => onCompanyFeatureUsage(AsCompanyFeatureUsage()),
            _ => onUnknown_(ExportType, Value),
        };
    }

    public void Visit(
        Action<SchematicHQ.Client.AuditLogExportMetadata> onAuditLog,
        Action<SchematicHQ.Client.CompanyFeatureUsageExportMetadata> onCompanyFeatureUsage,
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

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator DataExportMetadata(DataExportMetadata.AuditLog value) =>
        new(value);

    public static implicit operator DataExportMetadata(
        DataExportMetadata.CompanyFeatureUsage value
    ) => new(value);

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
}
