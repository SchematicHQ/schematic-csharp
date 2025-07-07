using System.Collections.Generic;
using System.Linq;
using OpenFeature.Model;

namespace SchematicHQ.Client.OpenFeature
{
    /// <summary>
    /// Extension methods for OpenFeature Value types.
    /// </summary>
    internal static class ValueExtensions
    {
        /// <summary>
        /// Converts an OpenFeature Value to a dictionary of strings.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A dictionary of string key-value pairs, or null if the value is not a structure.</returns>
        public static Dictionary<string, string>? ToDictionaryString(this Value value)
        {
            if (!value.IsStructure)
            {
                return null;
            }

            return value.AsStructure
                .Where(kvp => kvp.Value.IsString || kvp.Value.IsNumber || kvp.Value.IsBoolean)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.AsString ?? kvp.Value.ToString()
                );
        }

        /// <summary>
        /// Converts an OpenFeature Value to a dictionary of objects.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>A dictionary of object values, or null if the value is not a structure.</returns>
        public static Dictionary<string, object>? ToDictionaryObject(this Value value)
        {
            if (!value.IsStructure)
            {
                return null;
            }

            return value.AsStructure
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => ConvertValueToObject(kvp.Value)
                );
        }

        private static object ConvertValueToObject(Value value)
        {
            if (value.IsNull)
                return null!;
            if (value.IsBoolean)
                return value.AsBoolean!.Value;
            if (value.IsNumber)
                return value.AsDouble!.Value;
            if (value.IsString)
                return value.AsString!;
            if (value.IsList)
                return value.AsList!.Select(ConvertValueToObject).ToList();
            if (value.IsStructure)
                return value.ToDictionaryObject()!;
            if (value.IsDateTime)
                return value.AsDateTime!.Value;
            
            return value.AsObject!;
        }
    }
}