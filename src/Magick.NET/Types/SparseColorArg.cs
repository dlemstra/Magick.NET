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
    /// <summary>
    /// Represents an argument for the SparseColor method.
    /// </summary>
    public sealed class SparseColorArg : ISparseColorArg<QuantumType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SparseColorArg"/> class.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="color">The color.</param>
        public SparseColorArg(double x, double y, IMagickColor<QuantumType> color)
        {
            Throw.IfNull(nameof(color), color);

            X = x;
            Y = y;
            Color = color;
        }

        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public IMagickColor<QuantumType> Color { get; set; }
    }
}