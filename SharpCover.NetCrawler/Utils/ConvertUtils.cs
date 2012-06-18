using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpCover.NetCrawler.Utils
{
    public static class ConvertUtils
    {
        public static int ToInt(this string input, int fallback)
        {
            if (string.IsNullOrEmpty(input.Trim()))
                return fallback;

            try {
                return Convert.ToInt32(input);
            } catch { }

            return fallback;
        }

        public static double ToDouble(this string input, double fallback)
        {
            if (string.IsNullOrEmpty(input))
                return fallback;

            try {
                return Convert.ToDouble(input.Trim());
            } catch { }

            return fallback;
        }

        public static object Cast(this string val, Type toType, object defaultVal = null)
        {
            if (val == null)
                return defaultVal;

            if (toType == typeof(string)) {
                return val;
            } else if (toType == typeof(int) || toType == typeof(int?)) {
                try {
                    return int.Parse(val);
                } catch { return defaultVal; }
            } else if (toType == typeof(bool) || toType == typeof(bool?)) {
                try {
                    return bool.Parse(val);
                } catch { return defaultVal; }
            } else if (toType == typeof(double) || toType == typeof(double?)) {
                try {
                    return double.Parse(val);
                } catch { return defaultVal; }
            } else if (toType == typeof(float) || toType == typeof(float?)) {
                try {
                    return float.Parse(val);
                } catch { return defaultVal; }
            } else if (toType == typeof(decimal) || toType == typeof(decimal?)) {
                try {
                    return decimal.Parse(val);
                } catch { return defaultVal; }
            } else if (toType.IsEnum) {
                try {
                    return Enum.Parse(toType, val, true);
                } catch { return defaultVal; }
            } else if (toType == typeof(DateTime) || toType == typeof(DateTime?)) {
                try {
                    return DateTime.Parse(val);
                } catch { return defaultVal; }
            }

            throw new ArgumentOutOfRangeException(string.Format("Type {0} is not supported!", toType));
        }

        public static bool IsNullable<T>(T obj)
        {
            if (obj == null) return true; // obvious
            Type type = typeof(T);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }


        /// <summary>
        /// Indicates if the specific string is null or an empty string.
        /// </summary>
        /// <returns>True is the specific string is not null or en empty string; otherwise False.</returns>
        public static bool IsNullOrEmpty(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return true;

            return false;
        }

        /// <summary>
        /// Convert an object to string.
        /// </summary>
        /// <returns>A System.String value if it is not null; otherwise an empty string.</returns>
        public static string ToStringOrEmpty(this Object value)
        {
            if (value != null)
                return Convert.ToString(value);

            return string.Empty;
        }

    }
}
