// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;

namespace ImageMagick;

internal static class EnumHelper
{
    public static string ConvertFlags<TEnum>(TEnum value)
        where TEnum : struct, Enum
        => value.ToString();

    public static string GetName(Enum value)
        => Enum.GetName(value.GetType(), value);

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

    public static MagickFormat ParseMagickFormatFromExtension(FileInfo file)
    {
        MagickFormat format = default;
        if (file.Extension is not null && file.Extension.Length > 1)
            format = Parse(file.Extension.Substring(1), MagickFormat.Unknown);

        return format;
    }

    private static TEnum Parse<TEnum>(object value, TEnum defaultValue)
        where TEnum : struct, Enum
    {
        if (Enum.IsDefined(typeof(TEnum), value))
            return (TEnum)value;

        return defaultValue;
    }
}
