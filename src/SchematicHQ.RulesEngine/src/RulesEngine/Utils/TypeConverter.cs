using System.Globalization;

namespace RulesEngine.Utils
{
  
  public enum ComparableType
  {
    String,
    Int,
    Boolean,
    Date
  }

  public enum ComparableOperator
  {
    Equals,
    NotEquals,
    LessThan,
    LessThanOrEqual,
    GreaterThan,
    GreaterThanOrEqual,
    Contains,
    NotContains,
    StartsWith,
    NotStartsWith,
    EndsWith,
    NotEndsWith
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
        case ComparableType.Boolean:
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
        case ComparableOperator.Equals:
          return string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        case ComparableOperator.NotEquals:
          return !string.Equals(a, b, StringComparison.OrdinalIgnoreCase);
        case ComparableOperator.Contains:
          return a?.IndexOf(b, StringComparison.OrdinalIgnoreCase) >= 0;
        case ComparableOperator.NotContains:
          return !CompareString(a, b, ComparableOperator.Contains);
        case ComparableOperator.StartsWith:
          return a?.StartsWith(b, StringComparison.OrdinalIgnoreCase) == true;
        case ComparableOperator.NotStartsWith:
          return !CompareString(a, b, ComparableOperator.StartsWith);
        case ComparableOperator.EndsWith:
          return a?.EndsWith(b, StringComparison.OrdinalIgnoreCase) == true;
        case ComparableOperator.NotEndsWith:
          return !CompareString(a, b, ComparableOperator.EndsWith);
        default:
          return false;
      }
    }

    public static bool CompareInt64(long a, long b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Equals:
          return a == b;
        case ComparableOperator.NotEquals:
          return a != b;
        case ComparableOperator.LessThan:
          return a < b;
        case ComparableOperator.LessThanOrEqual:
          return a <= b;
        case ComparableOperator.GreaterThan:
          return a > b;
        case ComparableOperator.GreaterThanOrEqual:
          return a >= b;
        default:
          return false;
      }
    }

    public static bool CompareFloat(double a, double b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Equals:
          return Math.Abs(a - b) < double.Epsilon;
        case ComparableOperator.NotEquals:
          return Math.Abs(a - b) >= double.Epsilon;
        case ComparableOperator.LessThan:
          return a < b;
        case ComparableOperator.LessThanOrEqual:
          return a <= b;
        case ComparableOperator.GreaterThan:
          return a > b;
        case ComparableOperator.GreaterThanOrEqual:
          return a >= b;
        default:
          return false;
      }
    }

    public static bool CompareBool(bool a, bool b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Equals:
          return a == b;
        case ComparableOperator.NotEquals:
          return a != b;
        default:
          return false;
      }
    }

    public static bool CompareDate(DateTime a, DateTime b, ComparableOperator op)
    {
      switch (op)
      {
        case ComparableOperator.Equals:
          return a.Date == b.Date;
        case ComparableOperator.NotEquals:
          return a.Date != b.Date;
        case ComparableOperator.LessThan:
          return a < b;
        case ComparableOperator.LessThanOrEqual:
          return a <= b;
        case ComparableOperator.GreaterThan:
          return a > b;
        case ComparableOperator.GreaterThanOrEqual:
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