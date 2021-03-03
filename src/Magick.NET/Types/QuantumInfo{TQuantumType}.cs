// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    internal class QuantumInfo<TQuantumType> : IQuantumInfo<TQuantumType>
        where TQuantumType : struct
    {
        public QuantumInfo(int depth, TQuantumType max)
        {
            Depth = depth;
            Max = max;
        }

        public int Depth { get; }

        public TQuantumType Max { get; }

        public IQuantumInfo<double> ToDouble()
            => new QuantumInfo<double>(Depth, (double)(object)Max);
    }
}