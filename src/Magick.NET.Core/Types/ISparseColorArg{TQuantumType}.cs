// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

namespace ImageMagick
{
    /// <summary>
    /// Represents an argument for the SparseColor method.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface ISparseColorArg<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        double X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        double Y { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        IMagickColor<TQuantumType> Color { get; set; }
    }
}