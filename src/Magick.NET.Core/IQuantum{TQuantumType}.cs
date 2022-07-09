// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Interface that represents the quantum information of ImageMagick.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IQuantum<TQuantumType> : IQuantum
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets the maximum value of the quantum.
        /// </summary>
        TQuantumType Max { get; }
    }
}
