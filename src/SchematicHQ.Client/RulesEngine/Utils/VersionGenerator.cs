using System.Reflection;
using System.Text;

namespace SchematicHQ.Client.RulesEngine.Utils;
public static class SchemaVersionGenerator
{
  private static readonly Dictionary<Type, string> _schemaVersions = new Dictionary<Type, string>();

  public static string GetSchemaVersion<T>()
  {
    var type = typeof(T);
    if (!_schemaVersions.TryGetValue(type, out var version))
    {
      version = CalculateSchemaHash(type);
      _schemaVersions[type] = version;
    }
    return version;
  }

  private static string CalculateSchemaHash(Type type)
  {
    // Get all public properties and fields
    var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        .Select(p => $"{p.Name}:{p.PropertyType.FullName}");

    var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
        .Select(f => $"{f.Name}:{f.FieldType.FullName}");

    // Create a string representing the schema
    var schemaString = string.Join("|", properties.Concat(fields).OrderBy(s => s));

    // Calculate hash of the schema string
    using var sha = System.Security.Cryptography.SHA256.Create();
    var bytes = Encoding.UTF8.GetBytes(schemaString);
    var hash = sha.ComputeHash(bytes);

    // Return first 8 characters of the hash as hex string
    return Convert.ToHexString(hash.AsSpan(0, 4));
  }
}