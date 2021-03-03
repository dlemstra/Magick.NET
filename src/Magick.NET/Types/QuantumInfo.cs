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
    internal sealed class QuantumInfo : IQuantumInfo<QuantumType>
    {
        private QuantumInfo(int depth, QuantumType max)
        {
            Depth = depth;
            Max = max;
        }

        public static QuantumInfo Instance { get; } = new QuantumInfo(Quantum.Depth, Quantum.Max);

        public int Depth { get; }

        public QuantumType Max { get; }

        public IQuantumInfo<double> ToDouble()
            => new QuantumInfo<double>(Depth, Max);
    }
}