// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick;

internal static class PercentageHelper
{
    public static Percentage FromQuantum(double value)
        => new Percentage((value / Quantum.Max) * 100);

    public static double ToQuantum(string paramName, Percentage percentage)
    {
        var value = percentage.ToDouble();

#if !Q16HDRI
        Throw.IfNegative(paramName, value);
#endif

        return Quantum.Max * (value / 100);
    }

    public static QuantumType ToQuantumType(string paramName, Percentage percentage)
    {
        var value = percentage.ToDouble();

#if !Q16HDRI
        Throw.IfNegative(paramName, value);
#endif

        return (QuantumType)(Quantum.Max * (value / 100));
    }
}
