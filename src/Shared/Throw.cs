// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace ImageMagick
{
    internal static class Throw
    {
        public static void IfFalse(string paramName, bool condition, string message)
        {
            if (!condition)
                throw new ArgumentException(message, paramName);
        }

        public static void IfNull(string paramName, [NotNull] object? value)
        {
            if (value is null)
                throw new ArgumentNullException(paramName);
        }

        public static void IfNull(string paramName, [NotNull] object? value, string message)
        {
            if (value == null)
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

        public static void IfNullOrEmpty(string paramName, [NotNull] string value)
        {
            IfNull(paramName, value);

            if (value.Length == 0)
                throw new ArgumentException("Value cannot be empty.", paramName);
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
                throw new ArgumentException("Value should be greater than zero.", paramName);
        }

        public static void IfNegative(string paramName, Percentage value)
        {
            if ((double)value < 0.0)
                throw new ArgumentException("Value should be greater than zero.", paramName);
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

        public static void IfOutOfRange(string paramName, Percentage value)
        {
            double val = (double)value;

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
    }
}
