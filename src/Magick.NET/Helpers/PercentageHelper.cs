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

namespace ImageMagick
{
    internal static class PercentageHelper
    {
        public static Percentage FromQuantum(double value)
            => new Percentage((value / Quantum.Max) * 100);

        public static double ToQuantum(Percentage percentage)
            => Quantum.Max * (percentage.ToDouble() / 100);

        public static QuantumType ToQuantumType(Percentage self)
            => (QuantumType)(Quantum.Max * (self.ToDouble() / 100));
    }
}
