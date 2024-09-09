// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;
using Xunit.Sdk;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET;

internal static class ColorAssert
{
    public static void Equal(IMagickColor<QuantumType> expected, IMagickColor<QuantumType>? actual)
        => Equal(expected, actual, null);

    public static void Equal(IMagickColor<QuantumType> expected, IMagickColor<QuantumType>? actual, string? messageSuffix)
    {
        Assert.NotNull(actual);

#if Q16HDRI
        /* Allow difference of 1 due to rounding issues */
        QuantumType delta = 1;
#else
        QuantumType delta = 0;
#endif

        Equal(expected.R, actual.R, expected, actual, delta, "R", messageSuffix);
        Equal(expected.G, actual.G, expected, actual, delta, "G", messageSuffix);
        Equal(expected.B, actual.B, expected, actual, delta, "B", messageSuffix);
        Equal(expected.A, actual.A, expected, actual, delta, "A", messageSuffix);
    }

    public static void Equal(IMagickColor<QuantumType> expected, IMagickImage<QuantumType> image, int x, int y)
    {
        using var pixels = image.GetPixelsUnsafe();
        Equal(expected, pixels.GetPixel(x, y), $"at position {x}x{y}");
    }

    public static void NotEqual(IMagickColor<QuantumType> notExpected, IMagickColor<QuantumType>? actual)
        => NotEqual(notExpected, actual, null);

    public static void NotEqual(IMagickColor<QuantumType> notExpected, IMagickColor<QuantumType>? actual, string? messageSuffix)
    {
        Assert.NotNull(actual);

        if (notExpected.R == actual.R && notExpected.G == actual.G && notExpected.B == actual.B && notExpected.A == actual.A)
            throw new XunitException("Colors are the same (" + actual.ToString() + ")" + messageSuffix);
    }

    public static void NotEqual(IMagickColor<QuantumType> notExpected, IMagickImage<QuantumType> image, int x, int y)
    {
        using var collection = image.GetPixelsUnsafe();
        NotEqual(notExpected, collection.GetPixel(x, y), $"at position {x}x{y}");
    }

    public static void Transparent(float alpha)
        => Assert.Equal(0, alpha);

    public static void NotTransparent(float alpha)
        => Assert.Equal(Quantum.Max, alpha);

    private static void Equal(IMagickColor<QuantumType> expected, IPixel<QuantumType> actual, string messageSuffix)
        => Equal(expected, actual.ToColor(), messageSuffix);

    private static void Equal(QuantumType expected, QuantumType actual, IMagickColor<QuantumType> expectedColor, IMagickColor<QuantumType> actualColor, double delta, string channel, string? messageSuffix)
    {
#if Q16HDRI
        if (double.IsNaN(actual))
            actual = 0;
#endif

        if (actual < expected - delta || actual > expected + delta)
            throw new XunitException(channel + " is not equal (" + expectedColor.ToString() + " != " + actualColor.ToString() + ") (" + expected + " != " + actual + ") " + messageSuffix);
    }

    private static void NotEqual(IMagickColor<QuantumType> expected, IPixel<QuantumType> actual, string messageSuffix)
        => NotEqual(expected, actual.ToColor(), messageSuffix);
}
