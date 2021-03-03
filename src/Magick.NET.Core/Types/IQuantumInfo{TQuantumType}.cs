// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to acquire information about the quantum.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IQuantumInfo<TQuantumType>
    {
        /// <summary>
        /// Gets the quantum depth.
        /// </summary>
        int Depth { get; }

        /// <summary>
        /// Gets the maximum value of the quantum.
        /// </summary>
        TQuantumType Max { get; }

        /// <summary>
        /// Returns an instance that has a double as the quantum type.
        /// </summary>
        /// <returns>An instance that has a double as the quantum type.</returns>
        IQuantumInfo<double> ToDouble();
    }
}
