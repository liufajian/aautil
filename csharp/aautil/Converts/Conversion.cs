using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace AAUtil.Converts
{
    /// <summary>
    /// 
    /// </summary>
    public static class Conversion
    {
        /// <summary> 
        /// Gets the specified property value and converts it, if necessary. 
        /// </summary> 
        /// <typeparam name="T">The type to convert to.</typeparam> 
        /// <param name="o">The object to convert.</param> 
        /// <param name="defaultValue">The default value.</param> 
        /// <returns></returns>
        public static T Convert<T>(this object o, T defaultValue = default)
        {
            if (o == null) return defaultValue;
            o = o.Convert(typeof(T), defaultValue);
            return o is T o1 ? o1 : defaultValue;
        }

        /// <summary> 
        /// Gets the specified property value and converts it, if necessary. 
        /// </summary> 
        /// <param name="o">The object to convert.</param> 
        /// <param name="type">The type to convert to.</param> 
        /// <param name="defaultValue">The default value.</param> 
        /// <returns></returns> 
        public static object Convert(this object o, Type type, object defaultValue)
        {
            if (o == null || o is DBNull) return defaultValue;
            if (type.IsInstanceOfType(o)) return o;

            try
            {
                if (o is string s)
                {
                    if (string.IsNullOrWhiteSpace(s)) return defaultValue;
                    var result = Parse(type, s);
                    if (result != null) return result;
                }

                var tc = TypeDescriptor.GetConverter(o.GetType());
                return tc.CanConvertTo(type) ? tc.ConvertTo(o, type) : Parse(type, o) ?? defaultValue;
            }
            catch
            {
                return Parse(type, o.ToString()) ?? defaultValue;
            }
        }

        /// <summary> 
        /// Parses the specified object into the specified type. 
        /// </summary> 
        /// <param name="type">The type to parse into.</param> 
        /// <param name="o">The object to parse.</param> 
        /// <returns>An object of the specified type, if successful; otherwise, null.</returns> 
        public static object Parse(Type type, object o)
        {
            var text = o?.ToString();

            return string.IsNullOrWhiteSpace(text) ? default(object) : type.Name switch
            {
                "UInt16" when ushort.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2) => v2,
                "UInt32" when uint.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2) => v2,
                "UInt64" when ulong.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2) => v2,
                "Int16" when short.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2) => v2,
                "Int32" when int.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2) => v2,
                "Int64" when long.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2) => v2,
                "Byte" when byte.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2) => v2,
                "Decimal" when decimal.TryParse(text, NumberStyles.Any, CultureInfo.InvariantCulture, out var v2) => v2,
                "DateTime" when DateTime.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowWhiteSpaces, out var v2) => v2,
                "DateTimeOffset" when DateTimeOffset.TryParse(text, CultureInfo.InvariantCulture, DateTimeStyles.AllowInnerWhite | DateTimeStyles.AllowLeadingWhite | DateTimeStyles.AllowTrailingWhite | DateTimeStyles.AllowWhiteSpaces, out var v2) => v2,
                "TimeSpan" when TimeSpan.TryParse(text, CultureInfo.InvariantCulture, out var v2) => v2,
                "Boolean" => BooleanValues.Contains(text),
                _ => null
            };
        }

        private static readonly HashSet<string> BooleanValues;

        static Conversion()
        {
            BooleanValues = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "true", "1", "on", "yes" };
        }
    }
}