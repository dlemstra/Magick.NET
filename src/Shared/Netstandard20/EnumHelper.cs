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
            => Parse((object)value, defaultValue);

        public static TEnum Parse<TEnum>(string? value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            if (Enum.TryParse(value, true, out TEnum result))
                return result;

            return defaultValue;
        }

        public static TEnum Parse<TEnum>(ushort value, TEnum defaultValue)
            where TEnum : struct, Enum
            => Parse((object)(int)value, defaultValue);

        private static TEnum Parse<TEnum>(object value, TEnum defaultValue)
            where TEnum : struct, Enum
        {
            var enumValue = (TEnum)value;
            if (Enum.IsDefined(typeof(TEnum), enumValue))
                return enumValue;

            return defaultValue;
        }
    }
}
