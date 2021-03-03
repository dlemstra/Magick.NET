// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Globalization;

namespace ImageMagick
{
    internal static class EnumHelper
    {
        public static string ConvertFlags<TEnum>(TEnum value)
          where TEnum : struct, IConvertible
        {
            List<string> flags = new List<string>();

            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (HasFlag(value, enumValue))
                    flags.Add(Enum.GetName(typeof(TEnum), enumValue));
            }

            return string.Join(",", flags.ToArray());
        }

        public static string GetName<TEnum>(TEnum value)
          where TEnum : struct, IConvertible
        {
            return Enum.GetName(typeof(TEnum), value);
        }

        public static bool HasFlag<TEnum>(TEnum value, TEnum flag)
          where TEnum : struct, IConvertible
        {
            uint flagValue = flag.ToUInt32(CultureInfo.InvariantCulture);
            return (value.ToUInt32(CultureInfo.InvariantCulture) & flagValue) == flagValue;
        }

        public static TEnum Parse<TEnum>(int value, TEnum defaultValue)
          where TEnum : struct, IConvertible
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (value == enumValue.ToInt32(CultureInfo.InvariantCulture))
                    return enumValue;
            }

            return defaultValue;
        }

        public static TEnum Parse<TEnum>(string? value, TEnum defaultValue)
          where TEnum : struct, IConvertible
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            foreach (string name in Enum.GetNames(typeof(TEnum)))
            {
                if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return (TEnum)Enum.Parse(typeof(TEnum), name);
            }

            return defaultValue;
        }

        public static TEnum? Parse<TEnum>(string value)
          where TEnum : struct, IConvertible
        {
            foreach (string name in Enum.GetNames(typeof(TEnum)))
            {
                if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return (TEnum?)Enum.Parse(typeof(TEnum), name);
            }

            return null;
        }

        public static TEnum Parse<TEnum>(ushort value, TEnum defaultValue)
          where TEnum : struct, IConvertible
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (value == enumValue.ToUInt16(CultureInfo.InvariantCulture))
                    return enumValue;
            }

            return defaultValue;
        }

        public static object? Parse(Type enumType, string value)
        {
            foreach (string name in Enum.GetNames(enumType))
            {
                if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return Enum.Parse(enumType, name);
            }

            return null;
        }
    }
}