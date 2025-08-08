namespace SchematicHQ.Client.RulesEngine.Utils;
public static class SchemaVersionGenerator
{
  /// <summary>
  /// Gets the global version for all models
  /// </summary>
  public static string GetGlobalSchemaVersion()
  {
    // Use the pre-generated hash for all models
    return GeneratedModelHash.Value;
  }
}