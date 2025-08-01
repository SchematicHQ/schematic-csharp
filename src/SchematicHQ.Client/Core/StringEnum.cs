using System.Text.Json.Serialization;

namespace SchematicHQ.Client.Core;

public interface IStringEnum : IEquatable<string>
{
    public string Value { get; }
}
