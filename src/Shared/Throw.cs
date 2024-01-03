// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;

namespace ImageMagick;

internal static partial class Throw
{
    public static void IfFalse(string paramName, bool condition, string message)
    {
        if (!condition)
            throw new ArgumentException(message, paramName);
    }

    public static void IfFalse<T0>(string paramName, bool condition, string message, T0 arg0)
    {
        if (!condition)
            throw new ArgumentException(FormatMessage(message, arg0), paramName);
    }

    public static void IfNull(string paramName, [NotNull] object? value)
    {
        if (value is null)
            throw new ArgumentNullException(paramName);
    }

    public static void IfNull(string paramName, [NotNull] object? value, string message)
    {
        if (value is null)
            throw new ArgumentNullException(paramName, message);
    }

    public static void IfNullOrEmpty(string paramName, Stream value)
    {
        IfNull(paramName, value);

        if (value.CanSeek)
        {
            if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.", paramName);

            if (value.Position == value.Length)
                throw new ArgumentException("Stream position is at the end of the stream. Make sure to reset the stream position to 0.", paramName);
        }
    }

    public static void IfNullOrEmpty(string paramName, [NotNull] string? value)
        => IfNullOrEmpty(paramName, value, "Value cannot be null or empty.");

    public static void IfNullOrEmpty(string paramName, [NotNull] string? value, string message)
    {
        IfNull(paramName, value, message);

        if (value.Length == 0)
            throw new ArgumentException(message, paramName);
    }

    public static void IfNullOrEmpty(string paramName, [NotNull] Array value)
    {
        IfNull(paramName, value);

        if (value.Length == 0)
            throw new ArgumentException("Value cannot be empty.", paramName);
    }

    public static void IfNegative(string paramName, int value)
    {
        if (value < 0)
            throw new ArgumentException("Value should not be negative.", paramName);
    }

    public static void IfNegative(string paramName, Percentage value)
    {
        if ((double)value < 0.0)
            throw new ArgumentException("Value should not be negative.", paramName);
    }

    public static void IfOutOfRange(string paramName, int index, int length)
    {
        if (index < 0 || index >= length)
            throw new ArgumentOutOfRangeException(paramName);
    }

    public static void IfOutOfRange(string paramName, int min, int max, int value, string message)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName, message);
    }

    public static void IfOutOfRange<T>(string paramName, int min, int max, int value, string message, T arg0)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName, FormatMessage(message, arg0));
    }

    public static void IfOutOfRange(string paramName, Percentage value)
    {
        var val = (double)value;

        if (val < 0.0)
            throw new ArgumentOutOfRangeException(paramName, "Value should be greater than zero.");

        if (val > 100.0)
            throw new ArgumentOutOfRangeException(paramName, "Value should be smaller than 100.");
    }

    public static void IfTrue(string paramName, bool condition, string message)
    {
        if (condition)
            throw new ArgumentException(message, paramName);
    }

    public static void IfTrue<T0, T1>(string paramName, bool condition, string message, T0 arg0, T1 arg1)
    {
        if (condition)
            throw new ArgumentException(FormatMessage(message, arg0, arg1), paramName);
    }

    private static string FormatMessage(string message, params object?[] args)
        => string.Format(CultureInfo.InvariantCulture, message, args);
}
