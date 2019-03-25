using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgNet
{
    public static class TypeHelper
    {
        public static bool IsNullableType(this Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static bool IsNumericType(this Type t)
        {
            if (t == null)
                return false;
            else if (t == typeof(byte)
                || t == typeof(sbyte)
                || t == typeof(short)
                || t == typeof(ushort)
                || t == typeof(int)
                || t == typeof(uint)
                || t == typeof(long)
                || t == typeof(ulong)
                || t == typeof(float)
                || t == typeof(double)
                || t == typeof(decimal))
            {
                return true;
            }
            else
                return false;

        }

        public static bool IsNullableNumericType(this Type t)
        {
            if (t == null)
                return false;
            else return IsNullableType(t) && IsNumericType(Nullable.GetUnderlyingType(t));
        }

        public static bool IsBaseValueType(this Type t)
        {
            if (t == null)
                return false;
            return IsNumericType(t)
                || t == typeof(bool)
                || t == typeof(DateTime)
                || t == typeof(char);
        }

        public static bool IsNullableBaseValueType(this Type t)
        {
            if (t == null)
                return false;
            else return IsNullableType(t) && IsBaseValueType(Nullable.GetUnderlyingType(t));
        }

        public static bool IsNullableEnumType(this Type t)
        {
            if (t == null)
                return false;
            else return IsNullableType(t) && Nullable.GetUnderlyingType(t).IsEnum;
        }

        public static bool IsBaseValueOrEnumType(this Type t)
        {
            return t.IsEnum || IsBaseValueType(t);
        }

        public static bool IsNullableBaseValueOrEnumType(this Type t)
        {
            return IsNullableEnumType(t) || IsNullableBaseValueType(t);
        }

        public static object ChangeType(object value, Type conversionType)
        {
            if (value == null)
                return null;
            if (IsNullableType(conversionType))
            {
                conversionType = Nullable.GetUnderlyingType(conversionType);
            }
            if (conversionType.IsEnum)
            {
                return Enum.Parse(conversionType, value.ToString());
            }
            else
            {
                return Convert.ChangeType(value, conversionType);
            }
        }

        public static bool TryChangeType(object value, Type conversionType, out object obj)
        {
            try
            {
                obj = ChangeType(value, conversionType);
            }
            catch
            {
                obj = null;
                return false;
            }
            return true;
        }
    }
}
