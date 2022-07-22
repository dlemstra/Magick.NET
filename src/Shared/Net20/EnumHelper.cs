// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick
{
    internal static class EnumHelper
    {
        public static string ConvertFlags<TEnum>(TEnum value)
            where TEnum : Enum
            => value.ToString();

        public static bool HasFlag<TEnum>(TEnum value, TEnum flag)
          where TEnum : Enum
        {
            var flagValue = Convert.ToInt32(flag);
            return (Convert.ToInt32(value) & flagValue) == flagValue;
        }

        public static TEnum Parse<TEnum>(int value, TEnum defaultValue)
          where TEnum : Enum
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (value == Convert.ToInt32(enumValue))
                    return enumValue;
            }

            return defaultValue;
        }

        public static TEnum Parse<TEnum>(string? value, TEnum defaultValue)
          where TEnum : Enum
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
          where TEnum : Enum
        {
            foreach (TEnum enumValue in Enum.GetValues(typeof(TEnum)))
            {
                if (value == Convert.ToInt32(enumValue))
                    return enumValue;
            }

            return defaultValue;
        }

        public static MagickFormat ParseMagickFormatFromExtension(FileInfo file)
        {
            MagickFormat format = default;
            if (file.Extension is not null && file.Extension.Length > 1)
                format = Parse(file.Extension.Substring(1), MagickFormat.Unknown);

            return format;
        }
    }
}
