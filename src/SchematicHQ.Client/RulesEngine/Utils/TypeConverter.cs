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

  // Extension methods to convert from generated types to utility types
  public static class ComparableTypeExtensions
  {
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
      switch (op.Value)
      {
        case ComparableOperator.Values.Eq:
          return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        case ComparableOperator.Values.Ne:
          return !string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        case ComparableOperator.Values.Lt:
          return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) < 0;
        case ComparableOperator.Values.Lte:
          return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) <= 0;
        case ComparableOperator.Values.Gt:
          return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) > 0;
        case ComparableOperator.Values.Gte:
          return string.Compare(a, b, StringComparison.OrdinalIgnoreCase) >= 0;
        case ComparableOperator.Values.IsEmpty:
          return string.IsNullOrEmpty(a);
        case ComparableOperator.Values.NotEmpty:
          return !string.IsNullOrEmpty(a);
        default:
          return false;
      }
    }

    public static bool CompareInt64(long a, long b, ComparableOperator op)
    {
      switch (op.Value)
      {
        case ComparableOperator.Values.Eq:
          return a == b;
        case ComparableOperator.Values.Ne:
          return a != b;
        case ComparableOperator.Values.Lt:
          return a < b;
        case ComparableOperator.Values.Lte:
          return a <= b;
        case ComparableOperator.Values.Gt:
          return a > b;
        case ComparableOperator.Values.Gte:
          return a >= b;
        default:
          return false;
      }
    }

    public static bool CompareFloat(double a, double b, ComparableOperator op)
    {
      switch (op.Value)
      {
        case ComparableOperator.Values.Eq:
          return Math.Abs(a - b) < double.Epsilon;
        case ComparableOperator.Values.Ne:
          return Math.Abs(a - b) >= double.Epsilon;
        case ComparableOperator.Values.Lt:
          return a < b;
        case ComparableOperator.Values.Lte:
          return a <= b;
        case ComparableOperator.Values.Gt:
          return a > b;
        case ComparableOperator.Values.Gte:
          return a >= b;
        default:
          return false;
      }
    }

    public static bool CompareBool(bool a, bool b, ComparableOperator op)
    {
      switch (op.Value)
      {
        case ComparableOperator.Values.Eq:
          return a == b;
        case ComparableOperator.Values.Ne:
          return a != b;
        default:
          return false;
      }
    }

    public static bool CompareDate(DateTime a, DateTime b, ComparableOperator op)
    {
      switch (op.Value)
      {
        case ComparableOperator.Values.Eq:
          return a.Date == b.Date;
        case ComparableOperator.Values.Ne:
          return a.Date != b.Date;
        case ComparableOperator.Values.Lt:
          return a < b;
        case ComparableOperator.Values.Lte:
          return a <= b;
        case ComparableOperator.Values.Gt:
          return a > b;
        case ComparableOperator.Values.Gte:
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
