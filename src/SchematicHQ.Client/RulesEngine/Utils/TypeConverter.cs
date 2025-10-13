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
    public static class TypeConverter
  {
    public static bool Compare(string a, string b, ComparableType comparableType, ComparableOperator op)
    {
      switch (comparableType)
      {
        case ComparableType.String:
          return CompareString(a, b, op);
        case ComparableType.Int:
          return CompareInt64(StringToInt64(a), StringToInt64(b), op);
        case ComparableType.Bool:
          return CompareBool(StringToBool(a), StringToBool(b), op);
        case ComparableType.Date:
          var dateA = StringToDate(a);
          var dateB = StringToDate(b);
          if (dateA == null || dateB == null)
            return false;
          return CompareDate(dateA.Value, dateB.Value, op);
        default:
          return false;
      }
    }

    public static bool CompareString(string a, string b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Eq:
          return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        case ComparableOperator.Ne:
          return !string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        case ComparableOperator.Lt:
          return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) < 0;
        case ComparableOperator.Lte:
          return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) <= 0;
        case ComparableOperator.Gt:
          return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) > 0;
        case ComparableOperator.Gte:
          return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) >= 0;
        case ComparableOperator.IsEmpty:
          return string.IsNullOrEmpty(a);
        case ComparableOperator.NotEmpty:
          return !string.IsNullOrEmpty(a);
        default:
          return false;
      }
    }

    public static bool CompareInt64(long a, long b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Eq:
          return a == b;
        case ComparableOperator.Ne:
          return a != b;
        case ComparableOperator.Lt:
          return a < b;
        case ComparableOperator.Lte:
          return a <= b;
        case ComparableOperator.Gt:
          return a > b;
        case ComparableOperator.Gte:
          return a >= b;
        default:
          return false;
      }
    }

    public static bool CompareFloat(double a, double b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Eq:
          return Math.Abs(a - b) < double.Epsilon;
        case ComparableOperator.Ne:
          return Math.Abs(a - b) >= double.Epsilon;
        case ComparableOperator.Lt:
          return a < b;
        case ComparableOperator.Lte:
          return a <= b;
        case ComparableOperator.Gt:
          return a > b;
        case ComparableOperator.Gte:
          return a >= b;
        default:
          return false;
      }
    }

    public static bool CompareBool(bool a, bool b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Eq:
          return a == b;
        case ComparableOperator.Ne:
          return a != b;
        default:
          return false;
      }
    }

    public static bool CompareDate(DateTime a, DateTime b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Eq:
          return a.Date == b.Date;
        case ComparableOperator.Ne:
          return a.Date != b.Date;
        case ComparableOperator.Lt:
          return a < b;
        case ComparableOperator.Lte:
          return a <= b;
        case ComparableOperator.Gt:
          return a > b;
        case ComparableOperator.Gte:
          return a >= b;
        default:
          return false;
      }
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