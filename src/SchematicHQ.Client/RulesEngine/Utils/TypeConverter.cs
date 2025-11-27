using System.Globalization;
using System.Text.Json.Serialization;

namespace SchematicHQ.Client.RulesEngine.Utils
{
  
  [JsonConverter(typeof(SchematicHQ.Client.Cache.ComparableTypeConverter))]
  public enum ComparableType
  {
    Unknown,
    String,
    Int,
    Bool,
    Date
  }

  [JsonConverter(typeof(JsonStringEnumConverter))]
  public enum ComparableOperator
  {
    [JsonPropertyName("eq")]
    Eq,

    [JsonPropertyName("ne")]
    Ne,

    [JsonPropertyName("lt")]
    Lt,

    [JsonPropertyName("lte")]
    Lte,

    [JsonPropertyName("gt")]
    Gt,

    [JsonPropertyName("gte")]
    Gte,

    [JsonPropertyName("is_empty")]
    IsEmpty,
    [JsonPropertyName("not_empty")]
    NotEmpty,
  }

  // Extension methods to convert from generated types to utility types
  public static class ComparableTypeExtensions
  {
    public static ComparableOperator ToComparableOperator(this RulesengineConditionOperator op)
    {
      return op.Value switch
      {
        "eq" => ComparableOperator.Eq,
        "ne" => ComparableOperator.Ne,
        "gt" => ComparableOperator.Gt,
        "lt" => ComparableOperator.Lt,
        "gte" => ComparableOperator.Gte,
        "lte" => ComparableOperator.Lte,
        "is_empty" => ComparableOperator.IsEmpty,
        "not_empty" => ComparableOperator.NotEmpty,
        _ => ComparableOperator.Eq
      };
    }

    public static ComparableType ToComparableType(this RulesengineTraitDefinitionComparableType comparableType)
    {
      return comparableType.Value switch
      {
        "bool" => ComparableType.Bool,
        "date" => ComparableType.Date,
        "int" => ComparableType.Int,
        "string" => ComparableType.String,
        _ => ComparableType.String
      };
    }
  }

    public static class TypeConverter
  {
    public static bool Compare(string a, string b, RulesengineTraitDefinitionComparableType comparableType, RulesengineConditionOperator op)
    {
      if (comparableType == RulesengineTraitDefinitionComparableType.String)
        return CompareString(a, b, op);
      if (comparableType == RulesengineTraitDefinitionComparableType.Int)
        return CompareInt64(StringToInt64(a), StringToInt64(b), op);
      if (comparableType == RulesengineTraitDefinitionComparableType.Bool)
        return CompareBool(StringToBool(a), StringToBool(b), op);
      if (comparableType == RulesengineTraitDefinitionComparableType.Date)
      {
        var dateA = StringToDate(a);
        var dateB = StringToDate(b);
        if (dateA == null || dateB == null)
          return false;
        return CompareDate(dateA.Value, dateB.Value, op);
      }
      return false;
    }

    public static bool CompareString(string a, string b, RulesengineConditionOperator op)
    {
      if (op == RulesengineConditionOperator.Eq)
        return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
      if (op == RulesengineConditionOperator.Ne)
        return !string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
      if (op == RulesengineConditionOperator.Lt)
        return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) < 0;
      if (op == RulesengineConditionOperator.Lte)
        return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) <= 0;
      if (op == RulesengineConditionOperator.Gt)
        return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) > 0;
      if (op == RulesengineConditionOperator.Gte)
        return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) >= 0;
      if (op == RulesengineConditionOperator.IsEmpty)
        return string.IsNullOrEmpty(a);
      if (op == RulesengineConditionOperator.NotEmpty)
        return !string.IsNullOrEmpty(a);
      return false;
    }

    public static bool CompareInt64(long a, long b, RulesengineConditionOperator op)
    {
      if (op == RulesengineConditionOperator.Eq)
        return a == b;
      if (op == RulesengineConditionOperator.Ne)
        return a != b;
      if (op == RulesengineConditionOperator.Lt)
        return a < b;
      if (op == RulesengineConditionOperator.Lte)
        return a <= b;
      if (op == RulesengineConditionOperator.Gt)
        return a > b;
      if (op == RulesengineConditionOperator.Gte)
        return a >= b;
      return false;
    }

    public static bool CompareFloat(double a, double b, RulesengineConditionOperator op)
    {
      if (op == RulesengineConditionOperator.Eq)
        return Math.Abs(a - b) < double.Epsilon;
      if (op == RulesengineConditionOperator.Ne)
        return Math.Abs(a - b) >= double.Epsilon;
      if (op == RulesengineConditionOperator.Lt)
        return a < b;
      if (op == RulesengineConditionOperator.Lte)
        return a <= b;
      if (op == RulesengineConditionOperator.Gt)
        return a > b;
      if (op == RulesengineConditionOperator.Gte)
        return a >= b;
      return false;
    }

    public static bool CompareBool(bool a, bool b, RulesengineConditionOperator op)
    {
      if (op == RulesengineConditionOperator.Eq)
        return a == b;
      if (op == RulesengineConditionOperator.Ne)
        return a != b;
      return false;
    }

    public static bool CompareDate(DateTime a, DateTime b, RulesengineConditionOperator op)
    {
      if (op == RulesengineConditionOperator.Eq)
        return a.Date == b.Date;
      if (op == RulesengineConditionOperator.Ne)
        return a.Date != b.Date;
      if (op == RulesengineConditionOperator.Lt)
        return a < b;
      if (op == RulesengineConditionOperator.Lte)
        return a <= b;
      if (op == RulesengineConditionOperator.Gt)
        return a > b;
      if (op == RulesengineConditionOperator.Gte)
        return a >= b;
      return false;
    }

    public static string BoolToString(bool v)
    {
      return v ? "true" : "false";
    }

    public static bool Int64ToBool(long v)
    {
      return v != 0;
    }

    public static string Int64ToString(long v)
    {
      return v.ToString();
    }

    public static bool StringToBool(string? v)
    {
      if (string.IsNullOrEmpty(v))
        return false;

      return v.ToLower() == "true" || v == "1";
    }

    public static long StringToInt64(string? v)
    {
      if (string.IsNullOrEmpty(v))
        return 0;

      if (long.TryParse(v, out long result))
        return result;

      return 0;
    }

    public static double StringToFloat(string? v)
    {
      if (string.IsNullOrEmpty(v))
        return 0;

      if (double.TryParse(v, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
        return result;

      return 0;
    }

    public static DateTime? StringToDate(string? v)
    {
      if (string.IsNullOrEmpty(v))
        return null;

      if (DateTime.TryParse(v, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
        return result;

      return null;
    }
  }
}