using System.Reflection;
using SchematicHQ.Client;

namespace SchematicHQ.Client.RulesEngine.Utils;

public static class SchemaVersionGenerator
{
  /// <summary>
  /// Gets the global version for all models from the RulesEngineSchemaVersion enum
  /// </summary>
  public static string GetGlobalSchemaVersion()
  {
    // Use the current schema version from the RulesEngineSchemaVersion enum
    return GetCurrentSchemaVersion();
  }

  /// <summary>
  /// Gets the current schema version from the RulesEngineSchemaVersion enum
  /// </summary>
  public static string GetCurrentSchemaVersion()
  {
    // Get the first non-placeholder enum value dynamically using reflection
    var enumType = typeof(RulesEngineSchemaVersion);
    var fields = enumType.GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
        .Where(f => f.FieldType == enumType && !f.Name.Contains("Placeholder"))
        .OrderBy(f => f.Name)
        .FirstOrDefault();
    
    if (fields != null)
    {
      var enumValue = (RulesEngineSchemaVersion)fields.GetValue(null)!;
      return enumValue.Value;
    }
    
    // Fallback - should not happen in normal cases
    throw new InvalidOperationException("No valid RulesEngineSchemaVersion enum values found");
  }

  /// <summary>
  /// Gets a specific schema version by enum value
  /// </summary>
  public static string GetSchemaVersion(RulesEngineSchemaVersion schemaVersion)
  {
    return schemaVersion.Value;
  }
}