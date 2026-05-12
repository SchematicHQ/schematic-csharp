// ReSharper disable NullableWarningSuppressionIsUsed
// ReSharper disable InconsistentNaming

using global::System.Text.Json;
using global::System.Text.Json.Nodes;
using global::System.Text.Json.Serialization;
using SchematicHQ.Client.Core;

namespace SchematicHQ.Client;

[JsonConverter(typeof(IntegrationConfig.JsonConverter))]
[Serializable]
public record IntegrationConfig
{
    internal IntegrationConfig(string type, object? value)
    {
        Type = type;
        Value = value;
    }

    /// <summary>
    /// Create an instance of IntegrationConfig with <see cref="IntegrationConfig.Clerk"/>.
    /// </summary>
    public IntegrationConfig(IntegrationConfig.Clerk value)
    {
        Type = "clerk";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of IntegrationConfig with <see cref="IntegrationConfig.Orb"/>.
    /// </summary>
    public IntegrationConfig(IntegrationConfig.Orb value)
    {
        Type = "orb";
        Value = value.Value;
    }

    /// <summary>
    /// Create an instance of IntegrationConfig with <see cref="IntegrationConfig.Stripe"/>.
    /// </summary>
    public IntegrationConfig(IntegrationConfig.Stripe value)
    {
        Type = "stripe";
        Value = value.Value;
    }

    /// <summary>
    /// Discriminant value
    /// </summary>
    [JsonPropertyName("type")]
    public string Type { get; internal set; }

    /// <summary>
    /// Discriminated union value
    /// </summary>
    public object? Value { get; internal set; }

    /// <summary>
    /// Returns true if <see cref="Type"/> is "clerk"
    /// </summary>
    public bool IsClerk => Type == "clerk";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "orb"
    /// </summary>
    public bool IsOrb => Type == "orb";

    /// <summary>
    /// Returns true if <see cref="Type"/> is "stripe"
    /// </summary>
    public bool IsStripe => Type == "stripe";

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.ClerkIntegrationConfig"/> if <see cref="Type"/> is 'clerk', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'clerk'.</exception>
    public SchematicHQ.Client.ClerkIntegrationConfig AsClerk() =>
        IsClerk
            ? (SchematicHQ.Client.ClerkIntegrationConfig)Value!
            : throw new global::System.Exception("IntegrationConfig.Type is not 'clerk'");

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.OrbIntegrationConfig"/> if <see cref="Type"/> is 'orb', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'orb'.</exception>
    public SchematicHQ.Client.OrbIntegrationConfig AsOrb() =>
        IsOrb
            ? (SchematicHQ.Client.OrbIntegrationConfig)Value!
            : throw new global::System.Exception("IntegrationConfig.Type is not 'orb'");

    /// <summary>
    /// Returns the value as a <see cref="SchematicHQ.Client.StripeIntegrationConfig"/> if <see cref="Type"/> is 'stripe', otherwise throws an exception.
    /// </summary>
    /// <exception cref="Exception">Thrown when <see cref="Type"/> is not 'stripe'.</exception>
    public SchematicHQ.Client.StripeIntegrationConfig AsStripe() =>
        IsStripe
            ? (SchematicHQ.Client.StripeIntegrationConfig)Value!
            : throw new global::System.Exception("IntegrationConfig.Type is not 'stripe'");

    public T Match<T>(
        Func<SchematicHQ.Client.ClerkIntegrationConfig, T> onClerk,
        Func<SchematicHQ.Client.OrbIntegrationConfig, T> onOrb,
        Func<SchematicHQ.Client.StripeIntegrationConfig, T> onStripe,
        Func<string, object?, T> onUnknown_
    )
    {
        return Type switch
        {
            "clerk" => onClerk(AsClerk()),
            "orb" => onOrb(AsOrb()),
            "stripe" => onStripe(AsStripe()),
            _ => onUnknown_(Type, Value),
        };
    }

    public void Visit(
        Action<SchematicHQ.Client.ClerkIntegrationConfig> onClerk,
        Action<SchematicHQ.Client.OrbIntegrationConfig> onOrb,
        Action<SchematicHQ.Client.StripeIntegrationConfig> onStripe,
        Action<string, object?> onUnknown_
    )
    {
        switch (Type)
        {
            case "clerk":
                onClerk(AsClerk());
                break;
            case "orb":
                onOrb(AsOrb());
                break;
            case "stripe":
                onStripe(AsStripe());
                break;
            default:
                onUnknown_(Type, Value);
                break;
        }
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.ClerkIntegrationConfig"/> and returns true if successful.
    /// </summary>
    public bool TryAsClerk(out SchematicHQ.Client.ClerkIntegrationConfig? value)
    {
        if (Type == "clerk")
        {
            value = (SchematicHQ.Client.ClerkIntegrationConfig)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.OrbIntegrationConfig"/> and returns true if successful.
    /// </summary>
    public bool TryAsOrb(out SchematicHQ.Client.OrbIntegrationConfig? value)
    {
        if (Type == "orb")
        {
            value = (SchematicHQ.Client.OrbIntegrationConfig)Value!;
            return true;
        }
        value = null;
        return false;
    }

    /// <summary>
    /// Attempts to cast the value to a <see cref="SchematicHQ.Client.StripeIntegrationConfig"/> and returns true if successful.
    /// </summary>
    public bool TryAsStripe(out SchematicHQ.Client.StripeIntegrationConfig? value)
    {
        if (Type == "stripe")
        {
            value = (SchematicHQ.Client.StripeIntegrationConfig)Value!;
            return true;
        }
        value = null;
        return false;
    }

    public override string ToString() => JsonUtils.Serialize(this);

    public static implicit operator IntegrationConfig(IntegrationConfig.Clerk value) => new(value);

    public static implicit operator IntegrationConfig(IntegrationConfig.Orb value) => new(value);

    public static implicit operator IntegrationConfig(IntegrationConfig.Stripe value) => new(value);

    [Serializable]
    internal sealed class JsonConverter : JsonConverter<IntegrationConfig>
    {
        public override bool CanConvert(global::System.Type typeToConvert) =>
            typeof(IntegrationConfig).IsAssignableFrom(typeToConvert);

        public override IntegrationConfig Read(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var json = JsonElement.ParseValue(ref reader);
            if (!json.TryGetProperty("type", out var discriminatorElement))
            {
                throw new JsonException("Missing discriminator property 'type'");
            }
            if (discriminatorElement.ValueKind != JsonValueKind.String)
            {
                if (discriminatorElement.ValueKind == JsonValueKind.Null)
                {
                    throw new JsonException("Discriminator property 'type' is null");
                }

                throw new JsonException(
                    $"Discriminator property 'type' is not a string, instead is {discriminatorElement.ToString()}"
                );
            }

            var discriminator =
                discriminatorElement.GetString()
                ?? throw new JsonException("Discriminator property 'type' is null");

            // Strip the discriminant property to prevent it from leaking into AdditionalProperties
            var jsonObject = System.Text.Json.Nodes.JsonObject.Create(json);
            jsonObject?.Remove("type");
            var jsonWithoutDiscriminator =
                jsonObject != null ? JsonSerializer.SerializeToElement(jsonObject, options) : json;

            var value = discriminator switch
            {
                "clerk" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.ClerkIntegrationConfig?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.ClerkIntegrationConfig"
                        ),
                "orb" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.OrbIntegrationConfig?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.OrbIntegrationConfig"
                        ),
                "stripe" =>
                    jsonWithoutDiscriminator.Deserialize<SchematicHQ.Client.StripeIntegrationConfig?>(
                        options
                    )
                        ?? throw new JsonException(
                            "Failed to deserialize SchematicHQ.Client.StripeIntegrationConfig"
                        ),
                _ => json.Deserialize<object?>(options),
            };
            return new IntegrationConfig(discriminator, value);
        }

        public override void Write(
            Utf8JsonWriter writer,
            IntegrationConfig value,
            JsonSerializerOptions options
        )
        {
            JsonNode json =
                value.Type switch
                {
                    "clerk" => JsonSerializer.SerializeToNode(value.Value, options),
                    "orb" => JsonSerializer.SerializeToNode(value.Value, options),
                    "stripe" => JsonSerializer.SerializeToNode(value.Value, options),
                    _ => JsonSerializer.SerializeToNode(value.Value, options),
                } ?? new JsonObject();
            json["type"] = value.Type;
            json.WriteTo(writer, options);
        }

        public override IntegrationConfig ReadAsPropertyName(
            ref Utf8JsonReader reader,
            global::System.Type typeToConvert,
            JsonSerializerOptions options
        )
        {
            var stringValue =
                reader.GetString()
                ?? throw new JsonException("The JSON property name could not be read as a string.");
            return new IntegrationConfig(stringValue, stringValue);
        }

        public override void WriteAsPropertyName(
            Utf8JsonWriter writer,
            IntegrationConfig value,
            JsonSerializerOptions options
        )
        {
            writer.WritePropertyName(value.Type);
        }
    }

    /// <summary>
    /// Discriminated union type for clerk
    /// </summary>
    [Serializable]
    public struct Clerk
    {
        public Clerk(SchematicHQ.Client.ClerkIntegrationConfig value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.ClerkIntegrationConfig Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator IntegrationConfig.Clerk(
            SchematicHQ.Client.ClerkIntegrationConfig value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for orb
    /// </summary>
    [Serializable]
    public struct Orb
    {
        public Orb(SchematicHQ.Client.OrbIntegrationConfig value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.OrbIntegrationConfig Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator IntegrationConfig.Orb(
            SchematicHQ.Client.OrbIntegrationConfig value
        ) => new(value);
    }

    /// <summary>
    /// Discriminated union type for stripe
    /// </summary>
    [Serializable]
    public struct Stripe
    {
        public Stripe(SchematicHQ.Client.StripeIntegrationConfig value)
        {
            Value = value;
        }

        internal SchematicHQ.Client.StripeIntegrationConfig Value { get; set; }

        public override string ToString() => Value.ToString() ?? "null";

        public static implicit operator IntegrationConfig.Stripe(
            SchematicHQ.Client.StripeIntegrationConfig value
        ) => new(value);
    }
}
