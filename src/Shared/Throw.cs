// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;

namespace ImageMagick;

internal static partial class Throw
{
    public static void IfFalse(string paramName, bool condition, string message)
    {
        if (!condition)
            throw new ArgumentException(message, paramName);
    }

    public static void IfFalse<T0>(string paramName, bool condition, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string message, T0 arg0)
    {
        if (!condition)
            throw new ArgumentException(FormatMessage(message, arg0), paramName);
    }

    public static void IfNull([NotNull] object? value, [CallerArgumentExpression(nameof(value))] string? paramName = null, string? message = "Must not be null")
    {
        if (value is null)
            throw new ArgumentNullException(paramName, message);
    }

    public static void IfNullOrEmpty(Stream value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        IfNull(value, paramName);

        if (value.CanSeek)
        {
            if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.", paramName);

            if (value.Position == value.Length)
                throw new ArgumentException("Stream position is at the end of the stream. Make sure to reset the stream position to 0.", paramName);
        }
    }

    public static void IfNullOrEmpty([NotNull] string? value, [CallerArgumentExpression(nameof(value))] string? paramName = null, string? message = "Value cannot be null or empty.")
    {
        IfNull(value, paramName, message);

        if (value.Length == 0)
            throw new ArgumentException(message, paramName);
    }

    public static void IfNullOrEmpty([NotNull] Array value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        IfNull(value, paramName);

        if (value.Length == 0)
            throw new ArgumentException("Value cannot be empty.", paramName);
    }

    public static void IfNegative(double value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < 0.0)
            throw new ArgumentException("Value should not be negative.", paramName);
    }

    public static void IfNegative(Percentage value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if ((double)value < 0.0)
            throw new ArgumentException("Value should not be negative.", paramName);
    }

    public static void IfOutOfRange(int index, uint length, [CallerArgumentExpression(nameof(index))] string? paramName = null)
    {
        if (index < 0 || index >= length)
            throw new ArgumentOutOfRangeException(paramName);
    }

    public static void IfOutOfRange(int min, int max, int value, [CallerArgumentExpression(nameof(value))] string? paramName = null, string? message = null)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName, message);
    }

    public static void IfOutOfRange<T>(int min, int max, int value, string message, T arg0, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        if (value < min || value > max)
            throw new ArgumentOutOfRangeException(paramName, FormatMessage(message, arg0));
    }

    public static void IfOutOfRange(Percentage value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
    {
        var val = (double)value;

        if (val < 0.0)
            throw new ArgumentOutOfRangeException(paramName, "Value should be greater than zero.");

        if (val > 100.0)
            throw new ArgumentOutOfRangeException(paramName, "Value should be smaller than 100.");
    }

    public static void IfTrue(bool condition, [CallerArgumentExpression(nameof(condition))] string? paramName = null, string? message = null)
    {
        if (condition)
            throw new ArgumentException(message, paramName);
    }

    public static void IfTrue<T0>(bool condition, string paramName, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string message, T0 arg0)
    {
        if (condition)
            throw new ArgumentException(FormatMessage(message, arg0), paramName);
    }

    public static void IfTrue<T0, T1>(bool condition, string paramName, [StringSyntax(StringSyntaxAttribute.CompositeFormat)] string message, T0 arg0, T1 arg1)
    {
        if (condition)
            throw new ArgumentException(FormatMessage(message, arg0, arg1), paramName);
    }

    private static string FormatMessage(string message, params object?[] args)
        => string.Format(CultureInfo.InvariantCulture, message, args);
}
