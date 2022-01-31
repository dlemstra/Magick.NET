// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;

namespace ImageMagick
{
    internal static class EnumHelper
    {
        public static string ConvertFlags<TEnum>(TEnum value)
            where TEnum : struct, Enum
        {
            var flags = new List<string>();

            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (HasFlag(value, enumValue))
                {
                    var name = GetName(enumValue);
                    if (!flags.Contains(name))
                        flags.Add(name);
                }
            }

            return string.Join(",", flags.ToArray());
        }

        public static string GetName<TEnum>(TEnum value)
            where TEnum : struct, Enum
            => Enum.GetName(typeof(TEnum), value);

        public static bool HasFlag<TEnum>(TEnum value, TEnum flag)
            where TEnum : struct, Enum
            => value.HasFlag(flag);

        public static TEnum Parse<TEnum>(int value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (value == Convert.ToInt32(enumValue))
                    return enumValue;
            }

            return defaultValue;
        }

        public static TEnum Parse<TEnum>(string? value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            foreach (var name in Enum.GetNames(typeof(TEnum)))
            {
                if (name.Equals(value, StringComparison.OrdinalIgnoreCase))
                    return (TEnum)Enum.Parse(typeof(TEnum), name);
            }

            return defaultValue;
        }

        public static TEnum Parse<TEnum>(ushort value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (value == Convert.ToInt32(enumValue))
                    return enumValue;
            }

            return defaultValue;
        }
    }
}
